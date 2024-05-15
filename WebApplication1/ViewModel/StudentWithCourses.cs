using WebApplication1.Models;

namespace WebApplication1.ViewModel;

public class StudentWithCourses
{
    public Student Student { get; set; } = null!;
    public List<Course> CoursesStudied { get; set; } = new List<Course>();
    public List<Course> CoursesNotStudied { get; set; } = new List<Course>();
}

