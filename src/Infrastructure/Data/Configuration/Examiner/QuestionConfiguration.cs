using Domain.Constants;
using Domain.Entities.Examiner;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuration.Examiner
{
    public class QuestionConfiguration : IEntityTypeConfiguration<Question>
    {
        public void Configure(EntityTypeBuilder<Question> builder)
        {

            #region keys
            builder.HasKey(x => x.Id);
            #endregion

            #region properties config
            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Property(p => p.QuestionText).HasMaxLength(StringLength.QUESTION_TEXT_MAX_LENGTH);
            #endregion

            #region relationship config>
            builder.HasOne(e => e.MultipleChoiseQuestion).WithOne();
            builder.HasOne(e => e.TrueFalseQuestion).WithOne();
            builder.HasOne(e => e.ShortAnswerQuestion).WithOne();
            builder.HasMany(e => e.Histories).WithOne().HasForeignKey(e => e.QuestionId);
            #endregion
        }
    }
}
