//using Microsoft.AspNetCore.Mvc;

//namespace WebApplication1.Controllers
//{
//    public class ErrorController : Controller
//    {
//        public IActionResult Index()
//        {
//            return View();
//        }
//    }
//}

using Microsoft.AspNetCore.Mvc;
namespace WebApplication1.Controllers;

public class ErrorController : Controller
{
    [Route("/Error/{statusCode}")]
    public IActionResult HttpStatusCodeHandler(int statusCode)
    {
        switch (statusCode)
        {
            case 400:
                return View("BadRequest");
            case 404:
                return View("NotFound");
            default:
                return View("Error");
        }
    }

    public IActionResult Index()
    {
        return View("Error");
    }
}
