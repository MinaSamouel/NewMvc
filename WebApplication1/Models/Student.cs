using FluentValidation;

namespace WebApplication1.Models;

public class Student
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public DateTime DateOfBirth { get; set; }
    public string Address { get; set; } = null!;

    public ICollection<StudentLearning> StudentLearnings { get; set; } = new HashSet<StudentLearning>();
}

public class StudentValidator : AbstractValidator<Student>
{
    public StudentValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Student Name is Required")
            .Length(3, 50).WithMessage("Student Name must be between 3 and 50 characters");

        RuleFor(x => x.DateOfBirth)
            .NotEmpty().WithMessage("Date of Birth is Required")
            .Must(BeValidAge).WithMessage("Student must be between 5 and 30 years old");

        RuleFor(x => x.Address)
            .NotEmpty().WithMessage("Address is Required")
            .Length(5, 100).WithMessage("Address must be between 5 and 100 characters");
    }

    private bool BeValidAge(DateTime dateOfBirth)
    {
        var today = DateTime.Today;
        var age = today.Year - dateOfBirth.Year;

        return age is >= 5 and <= 30;
    }
    
    
}