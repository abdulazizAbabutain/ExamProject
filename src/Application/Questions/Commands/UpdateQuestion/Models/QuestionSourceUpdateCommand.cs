using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Questions.Commands.UpdateQuestion.Models
{
    public class QuestionSourceUpdateCommand
    {
        public Guid Id { get; set; }
        public SourceType Type { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public string URL { get; set; }
        public string? ISBN { get; set; }
    }
}
