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
    }
}
