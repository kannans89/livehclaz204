using CosmosMVCApp.Models;
using CosmosMVCApp.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CosmosMVCApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ConfigModel _configOptions;

        public HomeController(ILogger<HomeController> logger, ConfigModel config)
        {
            _logger = logger;
            _configOptions = config;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> AddTodo(Todo model)
        {

            TodoService db = new TodoService(_configOptions.CosomosdbConnectionString, _configOptions.CosmosdbDatabaseName, _configOptions.CosmosdbContainerName);
            bool result = await db.AddTodo(model);

            if (result)
                return RedirectToAction("ViewTodos");
            
            return NotFound();

        }
        public IActionResult AddTodo()
        {
            Todo model = new Todo();
            return View(model);

        }


        public async Task<IActionResult> ViewTodos()
        {

            TodoService db = new TodoService(_configOptions.CosomosdbConnectionString, _configOptions.CosmosdbDatabaseName, _configOptions.CosmosdbContainerName);
            List<Todo> todos = await db.GetTodosAsync();

            return View(todos);

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
