using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Reposatory;
using WebApplication1.ViewModel;

namespace WebApplication1.Controllers;

public class StudentController : Controller
{
    private readonly IStudentRepo _studentRepo;
    private readonly IValidator<Student> _validator;
    private readonly ICourseRepo _courseRepo;
    private readonly IStudentLearningRepo _studentLearningRepo;

    public StudentController(IStudentRepo studentRepo, IValidator<Student> validator, ICourseRepo courseRepo, IStudentLearningRepo studentLearningRepo)
    {
        _studentRepo = studentRepo;
        _validator = validator;
        _courseRepo = courseRepo;
        _studentLearningRepo = studentLearningRepo;
    }

    public IActionResult Index()
    {
        var students = _studentRepo.GetAll();
        var firstStudent = students[0];
        //if (firstStudent.StudentLearnings.Count() != 0)
        //{
        //    var courseNames = _studentLearningRepo.GetCoursesName(_studentLearningRepo.StudentCourses(firstStudent.Id));
        //}
        return View(students);
    }

    [HttpGet]
    public IActionResult Create()
    {
        ViewBag.Courses = _courseRepo.GetAll();
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Student student)
    {
        var result = await _validator.ValidateAsync(student);

        if (!result.IsValid)
        {
            ModelState.AddModelError("", result.ToString());
            return View();
        }

        _studentRepo.Add(student);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult Edit(int? id)
    {
        if (id == null) return BadRequest();
        var model = _studentRepo.Get(id);
        if (model == null) return NotFound();
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Student student, int id)
    {
        var result = await _validator.ValidateAsync(student);
        if (!result.IsValid)
        {
            ModelState.AddModelError("", result.ToString());
            return View();
        }
        _studentRepo.Update(student);
        return RedirectToAction("Index");
    }

    public IActionResult ManageCourses(int? id)
    {
        if (id == null) return BadRequest();
        var model = _studentRepo.Get(id);
        if (model == null) return NotFound();

        var studentWithCourses = new StudentWithCourses() { Student = model};

        studentWithCourses.CoursesStudied = _studentLearningRepo.StudentCourses(model.Id);
        var allCourses = _courseRepo.GetAll();
        studentWithCourses.CoursesNotStudied = allCourses.Except(studentWithCourses.CoursesStudied).ToList();

        return View(studentWithCourses);
    }

    public IActionResult RemoveCourseFromStudent(int? courseId, int? studentId)
    {
        if (studentId == null || courseId == null) return BadRequest();
        var course = _courseRepo.Get(courseId);
        var student = _studentRepo.Get(studentId);
        if (course == null || student == null) return NotFound();

        var studentLearn = _studentLearningRepo.GetElement((int)studentId, (int)courseId);

        _studentLearningRepo.Remove(studentLearn!);
        return RedirectToAction("ManageCourses", new { id = studentId });

    }

    public IActionResult AddCourseToStudent(int? courseId, int? studentId)
    {
        if (studentId == null || courseId == null) return BadRequest();
        var course = _courseRepo.Get(courseId);
        var student = _studentRepo.Get(studentId);
        if (course == null || student == null) return NotFound();

        var studentLearn = new StudentLearning()
            { Course = course, CourseId = (int)courseId, Student = student, StudentId = (int)studentId };

        _studentLearningRepo.Add(studentLearn);
        return RedirectToAction("ManageCourses", new { id = studentId });
    }

    [HttpPost]
    public IActionResult Delete(int id)
    {
        //if (studentId == null) return BadRequest();
        var student = _studentRepo.Get(id);
        if(student == null) return NotFound();

        _studentRepo.Remove(student);
        return RedirectToAction("Index");

    }
}

