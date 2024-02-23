using Microsoft.AspNetCore.Mvc;
using SimpleWeatherMVCFrontEnd.Models;
using System.Diagnostics;

namespace SimpleWeatherMVCFrontEnd.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IHttpClientFactory _httpClientFactory;
        private ConfigModel _config;

        public HomeController(ILogger<HomeController> logger, IHttpClientFactory factory, ConfigModel config)
        {
            _logger = logger;
            _httpClientFactory = factory;
            _config = config;
        }

        public async Task<IActionResult> Index()
        {
            var httpClient = _httpClientFactory.CreateClient();

            httpClient.BaseAddress = new Uri(_config.BASE_API_URL);

            List<WeatherForecast> weatherData = new List<WeatherForecast>();

            var reponse = await httpClient.GetAsync("/weatherforecast");
            if (reponse.IsSuccessStatusCode)
            {
                if (reponse.Content != null)
                {
                    weatherData = await reponse.Content.ReadAsAsync<List<WeatherForecast>>();

                }

            }

            return View(weatherData);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
