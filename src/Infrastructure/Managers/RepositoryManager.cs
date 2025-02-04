using Domain.Managers;
using Domain.Repositories;
using Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Managers
{
    public class RepositoryManager : IRepositoryManager
    {

        private readonly Lazy<IQuestionRepository> _QuestionRepository;
        public RepositoryManager(IConfiguration configuration)
        {
            _QuestionRepository = new Lazy<IQuestionRepository>(() => new QuestionRepository(configuration.GetConnectionString("Examiner")));
        }

        public IQuestionRepository QuestionRepository => _QuestionRepository.Value;
    }
}
