using Domain.Entities.Examiner;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class ExaminerDbcontext : DbContext
    {
        public DbSet<Question> Questions => Set<Question>();


    }
}
