using Domain.Entities.Examiner;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuration.Examiner
{
    public class MultipleChoiseQuestionConfiguration : IEntityTypeConfiguration<MultipleChoiseQuestion>
    {
        public void Configure(EntityTypeBuilder<MultipleChoiseQuestion> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Type)
                .IsRequired();

            builder.HasMany(e => e.Options).WithOne().HasForeignKey(e => e.QuestionId);
        }
    }
}
