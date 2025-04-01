using Application.Commons.Extentions;
using Application.Commons.Managers;
using Application.Questions.Queries.GetQuestionsById.ResultModel;
using Domain.Extentions;
using Domain.Lookups;
using Domain.Managers;
using MediatR;

namespace Application.Questions.Queries.GetQuestionsById
{
    internal class GetQuestionsByIdQueryHandler(IRepositoryManager repositoryManager) : IRequestHandler<GetQuestionsByIdQuery, GetQuestionsByIdQueryResult>
    {
        private readonly IRepositoryManager _repositoryManager = repositoryManager;

        public async Task<GetQuestionsByIdQueryResult> Handle(GetQuestionsByIdQuery request, CancellationToken cancellationToken)
        {
            List<string>? tags = null;

            var question = _repositoryManager.QuestionRepository.GetById(request.Id);
            
            if (question.IsNull())
                return null;

            if(question.Tags.IsNotNull())
                tags = _repositoryManager.TagRepository.GetCollection().Find(t => question.Tags.Contains(t.Id)).Select(e => e.Name).ToList();

            return new GetQuestionsByIdQueryResult
            {
                Id = question.Id,
                Mark = question.Mark,
                QuestionText = question.QuestionText,
                Variants = question.Variants,
                VersionNumber = question.VersionNumber,
                QuestionType = new QuestionTypeLookup
                {
                    Id = question.QuestionType
                },
                Difficulty = new QuestionDifficultyLookup
                {
                    Id = question.DifficultyIndex.GetDifficultyCategory()
                },
                Sources = null,
                RequireManulReview = question.RequireManulReview,
                Tags = tags,
                MultipleChoiseOptions = question.MultipleChoiseQuestion is not null ? question.MultipleChoiseQuestion.Options.Select(e => new MultipleChoiseQuestionResult
                {
                    Id = e.Id,
                    IsCorrect = e.IsCorrect,
                    OptionText = e.OptionText,
                    FeedBack = e.FeedBack,
                    Weight = e.Weight
                }) : null,
                TrueAndFalse = question.TrueFalseQuestion is not null ? new TrueFalseQuestionQueryResult
                {
                    Id = question.TrueFalseQuestion.Id,
                    AnswerFeedBack = question.TrueFalseQuestion.AnswerFeedBack,
                    IsCorrect = question.TrueFalseQuestion.IsCorrect,
                    WrongFeedBack = question.TrueFalseQuestion.WrongAnswerFeedBack,
                } : null
            };
        }
    }
}