using Microsoft.EntityFrameworkCore;

namespace College.Models
{
    public class ExamDbContext : DbContext
    {
        public ExamDbContext(DbContextOptions<ExamDbContext> options) : base(options)
        {
        }

        public DbSet<CandidateExam> CandidateExams { get; set; }
        public DbSet<Marks> Marks { get; set; }
        public DbSet<Subject> Subjects { get; set; }
    }
}
