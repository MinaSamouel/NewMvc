using WebApplication1.Models;

namespace WebApplication1.Reposatory;

public interface ICourseRepo
{
    List<Course> GetAll();
    bool Exists(int? id);
    Course? Get(int? id);
    void Add(Course course);
    void Update(Course course);
    void Remove(Course course);
}


public class CourseRepo : ICourseRepo
{
    private readonly StudentDBContext _dbContext;

    public CourseRepo(StudentDBContext dbContext)
    {
        _dbContext = dbContext;
    }

    public List<Course> GetAll()
    {
        return _dbContext.Courses.ToList();
    }

    public bool Exists(int? id)
    {
        return _dbContext.Courses.Any(c => c.Id == id);
    }

    public Course? Get(int? id)
    {
        return _dbContext.Courses.FirstOrDefault(c => c.Id == id);
    }

    public void Add(Course course)
    {
        _dbContext.Courses.Add(course);
        _dbContext.SaveChanges();
    }

    public void Update(Course course)
    {
        _dbContext.Courses.Update(course);
        _dbContext.SaveChanges();
    }

    public void Remove(Course course)
    {
        _dbContext.Courses.Remove(course);
        _dbContext.SaveChanges();
    }
}