using Domain.Auditing;

namespace Domain.Repositories
{
    public interface IApplicationLogRepository 
    {
        IEnumerable<ApplicationLog> GetAll(Func<ApplicationLog, bool> func, int pageNumber, int pageSize);
        public int Count();
    }
}
