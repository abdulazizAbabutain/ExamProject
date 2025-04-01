using Domain.Entities.Examiner;
using Domain.Repositories;
using Infrastructure.Repositories.BaseRepository;

namespace Infrastructure.Repositories
{
    public class QuestionRepository : BaseRepository<Question>, IQuestionRepository
    {
        public QuestionRepository(string databasePath) 
            : base(databasePath, nameof(Question))
        {
        }
    }
}
