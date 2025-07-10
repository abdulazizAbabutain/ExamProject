using Application.Commons.Models.Results;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Icons.Commands.UploadNewIcon
{
    public class UploadNewIconCommand : IRequest<Result<IEnumerable<UploadNewIconCommandResult>>>
    {
        public List<IFormFile> File { get; set; }
    }
}
