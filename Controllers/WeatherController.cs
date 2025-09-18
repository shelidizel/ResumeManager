using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ResumeManager.Models;

namespace ResumeManager.Controllers
{
    public class WeatherController : Controller
    {

        private readonly HttpClient _httpClient;
        private readonly ILogger<WeatherController> _logger; // Declare the logger

        public WeatherController(HttpClient httpClient, ILogger<WeatherController> logger) // Inject the logger
        {
            _httpClient = httpClient;
            _logger = logger;
            _httpClient.BaseAddress = new Uri("https://api.stormglass.io/v2/");
        }
        public IActionResult Details(double lat, double lng, string @params)
        {

            if (string.IsNullOrEmpty(@params))
            {
                return BadRequest("The 'params' query parameter is required.");
            }

             WeatherDetailsViewModel viewModel = new WeatherDetailsViewModel();

            _httpClient.DefaultRequestHeaders.Add("Authorization", "cf41e6e8-9493-11f0-b0b8-0242ac130006-cf41e7d8-9493-11f0-b0b8-0242ac130006");

            HttpResponseMessage response =
                _httpClient.GetAsync(_httpClient.BaseAddress + "weather/point?lat=" + lat + "&lng=" + lng + "&params=" + @params).Result;



            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                _logger.LogDebug(data);
                WeatherApiResponse weatherData = JsonConvert.DeserializeObject<WeatherApiResponse>(data);

                if (weatherData != null && weatherData.hours != null && weatherData.hours.Any())
                {
                    // Create and populate the view model
                    viewModel = new WeatherDetailsViewModel
                    {
                        Latitude = lat,
                        Longitude = lng,
                        Time = weatherData.hours[0].time, // Get the time from the first hour
                        AirTemperature = weatherData.hours[0].airTemperature.noaa // Get the NOAA temperature
                    };

             
                }

            }

             return View(viewModel);
        }
    }
}
