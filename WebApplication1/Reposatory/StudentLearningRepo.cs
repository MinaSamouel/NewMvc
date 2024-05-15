using WebApplication1.Models;

namespace WebApplication1.Reposatory;

public interface IStudentLearningRepo
{
    bool Exists(int studentId, int courseId);
    void Add(StudentLearning studentLearning);
    void Remove(StudentLearning studentLearning);
    List<Course> StudentCourses(int studentId);
    StudentLearning? GetElement(int studentId, int courseId);
    string GetCoursesName(List<Course> studentLearnings);
}

public class StudentLearningRepo : IStudentLearningRepo
{
    private readonly StudentDBContext _dbContext;

    public StudentLearningRepo(StudentDBContext dbContext)
    {
        _dbContext = dbContext;
    }

    //Check if the student studied this course
    public bool Exists(int studentId, int courseId)
    {
        return _dbContext.StudentLearnings.Any(sl => sl.CourseId == courseId && sl.StudentId == studentId);
    }

    //Add a Course studied to the student
    public void Add(StudentLearning studentLearning)
    {
        _dbContext.StudentLearnings.Add(studentLearning);
        _dbContext.SaveChanges();
    }

    //Remove the course from the student
    public void Remove(StudentLearning studentLearning)
    {
        _dbContext.StudentLearnings.Remove(studentLearning);
        _dbContext.SaveChanges();
    }

    //Get The courses the student study
    public List<Course> StudentCourses(int studentId)
    {
        var courses = _dbContext.StudentLearnings.Where(sl => sl.StudentId == studentId).Select(sl => sl.Course)
            .ToList();
        return courses;
    }

    //get the course that the student study
    public StudentLearning? GetElement(int studentId, int courseId)
    {
        return _dbContext.StudentLearnings.FirstOrDefault(sl => sl.CourseId == courseId && sl.StudentId == studentId);
    }

    public string GetCoursesName(List<Course> studentCourses)
    {
        var courseName = studentCourses.Select(c => c.Name).ToList();
        return string.Join('-', courseName);
    }
}