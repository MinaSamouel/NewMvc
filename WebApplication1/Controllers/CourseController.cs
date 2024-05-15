using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Reposatory;

namespace WebApplication1.Controllers;

public class CourseController : Controller
{
    private readonly IValidator<Course> _validator;
    private readonly ICourseRepo _courseRepo;

    public CourseController(IValidator<Course> validator, ICourseRepo courseRepo)
    {
        _validator = validator;
        _courseRepo = courseRepo;
    }

    public IActionResult Index()
    {
        var courses = _courseRepo.GetAll();
        return View(courses);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Course course)
    {
        var result = await _validator.ValidateAsync(course);

        if (!result.IsValid)
        {
            ModelState.AddModelError("", result.ToString());
            return View();
        }

        _courseRepo.Add(course);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult Edit(int? id)
    {
        if (id == null) return RedirectToAction("HttpStatusCodeHandler", "Error", new { statusCode = 400});
        var model = _courseRepo.Get(id);
        if (model == null) return RedirectToAction("HttpStatusCodeHandler", "Error", new { statusCode = 404 }); ;
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Course course, int id)
    {
        var result = await _validator.ValidateAsync(course);
        if (!result.IsValid)
        {
            ModelState.AddModelError("", result.ToString());
            return View();
        }
        _courseRepo.Update(course);
        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult Delete(int id)
    {
        var model = _courseRepo.Get(id);
        _courseRepo.Remove(model!);
        return RedirectToAction("Index");
    }
}