using Microsoft.EntityFrameworkCore;
using QuanLyDiem.Data.Models;

namespace QuanLyDiem.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }
        public DbSet<Semester> Semesters { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<SubjectPoint> SubjectPoints { get; set; }
    }
}