using Microsoft.AspNetCore.Mvc;

namespace Stock_Invoice.Controllers
{
    public class SignupController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
