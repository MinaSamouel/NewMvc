using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Models;

public class StudentDBContext : DbContext
{
    public DbSet<Student> Students { get; set; } = null!;
    public DbSet<Course> Courses { get; set; } = null!;
    public DbSet<StudentLearning> StudentLearnings { get; set; } = null!;

    public StudentDBContext()
    {
        
    }

    public StudentDBContext(DbContextOptions<StudentDBContext> options) : base(options)
    {

    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            @"Server=.\SQLEXPRESS;Database=StudentDBContext;Trusted_Connection=True;TrustServerCertificate=True;");
        //base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        #region Create Student Table

        modelBuilder.Entity<Student>()
            .Property(s => s.Id)
            .HasColumnName("Student_Id");

        modelBuilder.Entity<Student>()
            .Property(s => s.Name)
            .HasColumnName("Student_Name")
            .HasColumnType("nvarchar(60)");

        modelBuilder.Entity<Student>()
            .Property(s => s.DateOfBirth)
            .HasColumnName("DateOfBirth")
            .HasColumnType("date");

        modelBuilder.Entity<Student>()
            .Property(s => s.Address) 
            .HasColumnName("Address")
            .HasColumnType("nvarchar(150)");

        #endregion

        #region Create Course Table

        modelBuilder.Entity<Course>()
            .Property(c => c.Id)
            .HasColumnName("Course_Id");

        modelBuilder.Entity<Course>()
            .Property(c => c.Name)
            .HasColumnName("Course_Name")
            .HasColumnType("nvarchar(60)");

        #endregion

        #region Create StudentLearning Table

        modelBuilder.Entity<StudentLearning>()
            .HasKey(sl => new {sl.StudentId, sl.CourseId});

        modelBuilder.Entity<StudentLearning>()
            .HasOne(sl => sl.Student)
            .WithMany(s => s.StudentLearnings)
            .HasForeignKey(sl => sl.StudentId);

        modelBuilder.Entity<StudentLearning>()
            .HasOne(sl => sl.Course)
            .WithMany(c => c.StudentLearnings)
            .HasForeignKey(sl => sl.CourseId);

        #endregion

        //base.OnModelCreating(modelBuilder);
    }
}