using Domain.Entities.Examiner;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class ExaminerDbcontext : DbContext
    {
        public ExaminerDbcontext(DbContextOptions<ExaminerDbcontext> options)
            : base(options) 
        {
            
        }
        public DbSet<Question> Questions => Set<Question>();


    }
}
