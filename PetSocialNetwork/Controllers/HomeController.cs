using Microsoft.AspNetCore.Mvc;

namespace PetSocialNetwork.Controllers
{
    [Route("")]
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index() => View();

        [HttpGet(nameof(Login))]
        public IActionResult Login() => View();
        
        [HttpGet(nameof(Privacy))]
        public IActionResult Privacy() => View();
    }
}
