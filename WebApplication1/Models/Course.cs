using FluentValidation;

namespace WebApplication1.Models;

public class Course
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    //public string Description { get; set; }

    public ICollection<StudentLearning> StudentLearnings { get; set; } = new HashSet<StudentLearning>();
}

public class CourseValidator : AbstractValidator<Course>
{
    public CourseValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Course Name is Required")
            .Length(3, 50).WithMessage("Course Name must be between 3 and 50 characters");
    }
}