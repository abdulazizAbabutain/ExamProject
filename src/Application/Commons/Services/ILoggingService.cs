using Domain.Auditing;

namespace Application.Commons.Services
{
    public interface ILoggingService
    {
        IEnumerable<ApplicationLog> GetAllLogs();
    }
}
