using Domain.Auditing;
using Domain.Extentions;
using LiteDB;
using Serilog.Core;
using Serilog.Events;
using System.Collections.Concurrent;

namespace Infrastructure.Services
{
    public class LoggingService : ILogEventSink, IDisposable
    {
        private readonly BlockingCollection<LogEvent> _logQueue = new(1000);
        private readonly ILiteCollection<ApplicationLog> _collection;
        private readonly Task _worker;
        private readonly CancellationTokenSource _cts = new();

        public LoggingService(string databasePath)
        {
            var db = new LiteDatabase(databasePath);
            _collection = db.GetCollection<ApplicationLog>(nameof(ApplicationLog));

            // Indexes for better search
            _collection.EnsureIndex(x => x.Timestamp);
            _collection.EnsureIndex(x => x.Level);

            // Background writer
            _worker = Task.Factory.StartNew(
                ProcessQueue,
                _cts.Token,
                TaskCreationOptions.LongRunning,
                TaskScheduler.Default);
        }


        public IEnumerable<ApplicationLog> GetAllLogs()
        {

            return _collection.FindAll();
        }




        public void Emit(LogEvent logEvent)
        {
            _logQueue.TryAdd(logEvent);
        }

        private void ProcessQueue()
        {
            try 
            {
                foreach (var logEvent in _logQueue.GetConsumingEnumerable(_cts.Token))
                {
                    _collection.Insert(BuildLog(logEvent));

                }
            }
            catch (OperationCanceledException) { /* Graceful shutdown */ }
        }


        private static Func<LogEvent, ApplicationLog> MapIntoLogs() => (LogEvent logEvent) =>
        {
            return new ApplicationLog
            {
                Id = Guid.CreateVersion7(),
                Timestamp = logEvent.Timestamp,
                Message = logEvent.RenderMessage(),
                Exception = logEvent.Exception.IsNotNull() ? ExceptionInfo.FromException(logEvent.Exception) : null,
                Level = logEvent.Level,
                Properties = logEvent.Properties.ToDictionary(
                         p => p.Key,
                         p => p.Value.ToString()
                     )
            };
        };

        private static ApplicationLog BuildLog(LogEvent logEvent)
        {
            return new ApplicationLog
            {
                Id = Guid.CreateVersion7(),
                Timestamp = logEvent.Timestamp,
                Message = logEvent.RenderMessage(),
                Exception = logEvent.Exception.IsNotNull() ? ExceptionInfo.FromException(logEvent.Exception) : null,
                Level = logEvent.Level,
                Properties = logEvent.Properties.ToDictionary(
                        p => p.Key,
                        p => p.Value.ToString()
                    )
            };
        }


        public void Dispose()
        {
            _cts.Cancel();
            _logQueue.CompleteAdding();
            try { _worker.Wait(); } catch { }
        }
    }
}
