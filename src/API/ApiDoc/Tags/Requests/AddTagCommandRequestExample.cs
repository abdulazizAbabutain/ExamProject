using Application.Lookups.Commands.Tags.AddTag;
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
                ColorCode = "#326721"
            };
          
        }
    }
}
