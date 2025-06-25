using Domain.Repositories;

namespace Application.Commons.Managers
{
    public interface IAuditManager
    {
        public IApplicationLogRepository ApplicationLogRepository { get; } 
    }
}
