using Microsoft.AspNetCore.Mvc;

namespace HrProject.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult NotAllowed()
        {
            return PartialView("_NotFoundPartialView");
        }
    }
}
