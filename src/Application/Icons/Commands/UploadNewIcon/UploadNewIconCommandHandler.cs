using Application.Commons.Models.Results;
using Domain.Extentions;
using MediatR;

namespace Application.Icons.Commands.UploadNewIcon;

public class UploadNewIconCommandHandler : IRequestHandler<UploadNewIconCommand, Result<IEnumerable<UploadNewIconCommandResult>>>
{
    public async Task<Result<IEnumerable<UploadNewIconCommandResult>>> Handle(UploadNewIconCommand request, CancellationToken cancellationToken)
    {
        //if (request.File == null || request.File.Count == 0)
        //    return BadRequest("No files provided.");

        var allowedExtensions = new[] { ".svg", ".png" };
        var result = new List<UploadNewIconCommandResult>();

        for (int i = 0; i < request.File.Count; i++)
        {
            var file = request.File[i];
            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();

            if (!allowedExtensions.Contains(extension))
                continue; // Skip invalid

            var name = Path.GetFileNameWithoutExtension(file.FileName);
            var slug = StringExtension.Slugify(name);
            var fileName = $"{slug}-{Guid.NewGuid()}{extension}";
            var path = Path.Combine("wwwroot", "icons", "uploaded", fileName);

            Directory.CreateDirectory(Path.GetDirectoryName(path)!);

            using var stream = new FileStream(path, FileMode.Create);
            await file.CopyToAsync(stream);

            result.Add(new UploadNewIconCommandResult
            {
                IconName = fileName,
                IconUrl = $"/icons/uploaded/{fileName}"
            });
        }

        return Result<IEnumerable<UploadNewIconCommandResult>>.Success(result);
    }
}
