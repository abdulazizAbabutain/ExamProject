using Application.Questions.Commands.UpdateQuestion.Models;
using Domain.Enums;
using MediatR;

namespace Application.Questions.Commands.UpdateQuestion
{
    public class UpdateQuestionCommand : IRequest
    {
        public Guid Id { get; set; }
        public string QuestionText { get; set; }
        public List<string>? Variants { get; set; }
        public int Mark { get; set; }
        public bool RequireManulReview { get; set; }
        public List<string>? Tags { get; set; }
        public QuestionDifficulty Difficulty { get;  set; }
        public List<QuestionSourceUpdateCommand>? Sources { get; set; }

    }
}
