using Domain.Entities.Examiner;
using Domain.Entities.History;
using Domain.Repositories.RepositoryBase;

namespace Domain.Repositories
{
    public interface IQuestionRepository : IBaseRepository<Question>
    {
        IEnumerable<QuestionHistory> GetQuestionHistories(Guid queastionId, int pageNumber, int pageSize);
    }
}
