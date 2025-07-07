using Application.Tags.Commands.AddTag;
using Swashbuckle.AspNetCore.Filters;

namespace API.ApiDoc.Tags.Requests
{
    public class AddTagCommandRequestExample : IExamplesProvider<AddTagCommand>
    {
        public AddTagCommand GetExamples()
        {
            return new AddTagCommand
            {
                Name = "CS-109",
                BackgroundColorCode = "#326721"
            };
          
        }
    }
}
