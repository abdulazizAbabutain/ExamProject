using Domain.Entities.Examiner;
using Domain.Entities.History;
using Domain.Repositories.RepositoryBase;
using System.Linq.Expressions;

namespace Domain.Repositories
{
    public interface IQuestionRepository : IBaseRepository<Question>
    {
    }
}
