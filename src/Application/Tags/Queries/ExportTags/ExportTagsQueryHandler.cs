using Application.Commons.Models.Results;
using Domain.Enums;
using Domain.Managers;
using MediatR;
using Newtonsoft.Json;
using System.IO.Compression;
using System.Text;

namespace Application.Tags.Queries.ExportTags;

public class ExportTagsQueryHandler(IRepositoryManager repositoryManager) : IRequestHandler<ExportTagsQuery, Result<ExportTagsQueryResult>>
{
    private readonly IRepositoryManager _repositoryManager = repositoryManager;

    public async Task<Result<ExportTagsQueryResult>> Handle(ExportTagsQuery request, CancellationToken cancellationToken)
    {
        var tags = _repositoryManager.TagRepository.GetCollection()
          .FindAll()
          .Select(tag => new TagExportModel
          {
              Id = tag.Id,
              Name = tag.Name,
              ColorHexCode = tag.BackgroundColorCode,
              IsArchived = tag.IsArchived
          })
          .ToList();

        string fileContent;
        string innerFileName;
        string contentType;

        if (request.Format == FileFormat.CSV)
        {
            var csv = new StringBuilder();
            csv.AppendLine("Id,Name,ColorHexCode,ColorGroup,IsArchived");
            foreach (var tag in tags)
            {
                csv.AppendLine($"{tag.Id},{tag.Name},{tag.ColorHexCode},{tag.ColorGroup},{tag.IsArchived}");
            }

            fileContent = csv.ToString();
            innerFileName = "tags.csv";
            contentType = "text/csv";
        }
        else
        {
            fileContent = JsonConvert.SerializeObject(tags, Formatting.Indented);
            innerFileName = "tags.json";
            contentType = "application/json";
        }

        using var memoryStream = new MemoryStream();
        using (var archive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
        {
            var entry = archive.CreateEntry(innerFileName);
            using var entryStream = entry.Open();
            using var writer = new StreamWriter(entryStream);
            writer.Write(fileContent);
        }


        return Result<ExportTagsQueryResult>.Success(new ExportTagsQueryResult
        {
            FileName = "tags-export.zip",
            ContentType = "application/zip",
            FileContent = memoryStream.ToArray()
        });
    }

}

