using Microsoft.AspNetCore.Mvc;
using MVCDatabaseApp.Models;
using MVCDatabaseApp.Services;
using System.Diagnostics;

namespace MVCDatabaseApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductService _productService;

        public HomeController(ILogger<HomeController> logger, 
            IProductService service)
        {
            _logger = logger;
            _productService = service;
            _logger.LogWarning("WARNING:Inside home controller");
            
        }

        public IActionResult Index()
        {
            _logger.LogWarning("WARNING:Inside home controller Index");
            var products = _productService.GetProducts();

            return View(products);
        }

        public IActionResult Privacy()
        {
            _logger.LogWarning("WARNING:Inside home controller Privacy");
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            _logger.LogWarning("WARNING:Inside home controller error");
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
