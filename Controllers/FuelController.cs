using Microsoft.AspNetCore.Mvc;

namespace CadastroDeCarros.Controllers
{
    public class FuelController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
