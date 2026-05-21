using Microsoft.AspNetCore.Mvc;

namespace SkillFlow.Controllers
{
    public class SkillsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
