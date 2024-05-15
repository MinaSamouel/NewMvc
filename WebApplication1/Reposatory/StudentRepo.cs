using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;
using WebApplication1.Models;

namespace WebApplication1.Reposatory;

public interface IStudentRepo
{
    List<Student> GetAll();
    bool Exists(int id);
    Student? Get(int? id);
    void Add(Student student);
    void Update(Student student);
    void Remove(Student student);
}

public class StudentRepo : IStudentRepo
{
    private readonly StudentDBContext _dbContext;

    public StudentRepo(StudentDBContext dbContext)
    {
        _dbContext = dbContext;
    }

    public List<Student> GetAll()
    {
        return _dbContext.Students.AsSplitQuery().Include(c => c.StudentLearnings).ThenInclude(sl => sl.Course).AsSplitQuery().ToList();
    }

    public bool Exists(int id)
    {
        return _dbContext.Students.Any(e => e.Id == id);
    }

    public Student? Get(int? id)
    {
        return _dbContext.Students.Include(c => c.StudentLearnings).AsSplitQuery().FirstOrDefault(e => e.Id == id);
    }

    public void Add(Student student)
    {
        _dbContext.Students.Add(student);
        _dbContext.SaveChanges();
    }

    public void Update(Student student)
    {
        _dbContext.Entry(student).State = EntityState.Modified;
        _dbContext.SaveChanges();
    }

    public void Remove(Student student)
    {
        _dbContext.Students.Remove(student);
        _dbContext.SaveChanges();
    }

    
}

