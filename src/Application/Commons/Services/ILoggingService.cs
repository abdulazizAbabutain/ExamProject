using Domain.Logs;

namespace Application.Commons.Services
{
    public interface ILoggingService
    {
        IEnumerable<ApplicationLog> GetAllLogs();
    }
}
