using Domain.Entities.Examiner;
using Domain.Entities.History;
using Domain.Repositories;
using Infrastructure.Repositories.BaseRepository;

namespace Infrastructure.Repositories
{
    internal sealed class QuestionRepository : BaseRepository<Question>, IQuestionRepository
    {
        public QuestionRepository(string databasePath) 
            : base(databasePath, nameof(Question))
        {
        }

        public IEnumerable<QuestionHistory> GetQuestionHistories(Guid queastionId, int pageNumber, int pageSize)
        {
            var collection = GetCollection();
            return collection.FindById(queastionId)
                .Histories
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize).ToList();
        }
    }
}
