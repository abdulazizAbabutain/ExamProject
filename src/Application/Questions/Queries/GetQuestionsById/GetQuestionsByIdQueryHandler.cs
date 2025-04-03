using Application.Commons.Extentions;
using Application.Commons.Managers;
using Application.Commons.SharedModelResult;
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
            List<TagResult>? tags = null;
            List<QuestionSourceResult>? sources = null;
            string? category = null;


            var question = _repositoryManager.QuestionRepository.GetById(request.Id);

            if (question.IsNull())
                return null;

            if (question.Tags.IsNotNull())
                tags = _repositoryManager.TagRepository.GetCollection().Find(t => question.Tags.Contains(t.Id)).Select(e => new TagResult
                {
                    ColorCode = e.ColorHexCode,
                    Name = e.Name,
                }).ToList();
            if (question.Sources.IsNotNull())
                sources = _repositoryManager.SourceRepository.GetCollection().Find(s => question.Sources.Contains(s.Id)).Select(s => new QuestionSourceResult
                {
                    Id = s.Id,
                    Description = s.Description,
                    Title = s.Title,
                    Type = new SourceTypeLookup(s.Type),
                    URL = s.URL,
                }).ToList();


            var language = _repositoryManager.LanguageRepository.GetCollection().Find(s => s.Id == question.Language).Select(lan => new LanguageResult
            {
                Code = lan.Code,
                Name = lan.DisplayName
            }).FirstOrDefault();

            category = _repositoryManager.CategoryRepository.GetCollection().Find(c => c.Id == question.Category).Select(e => e.Name).FirstOrDefault();

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
                Sources = sources,
                RequireManualReview = question.RequireManualReview,
                Tags = tags,
                Category = category,
                Language = language,
                MultipleChoiceOptions = question.MultipleChoiseQuestion is not null ? question.MultipleChoiseQuestion.Options.Select(e => new MultipleChoiseQuestionResult
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