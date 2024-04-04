using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WeatherCRUD_Operations.Models;

namespace WeatherCRUD_Operations.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly WeatherContext _context;

        public WeatherForecastController(WeatherContext context)
        {
            _context = context;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<Weather> Get()
        {
            return _context.WeatherForecastingSummary.ToList();
        }

        [HttpPost(Name = "AddNewWeatherForecastingSummary")]
        public IActionResult AddNewWeatherForecastingSummary(Weather newWeather)
        {
            try
            {
                //var newWeather = new Weather(Random.Shared.Next(-20, 55))
                //{
                //    Date = DateTime.Now,
                //    Summary = newSummary,
                //};

                _context.WeatherForecastingSummary.Add(newWeather);
                _context.SaveChanges();

                return CreatedAtRoute("GetWeatherForecast", new { id = newWeather.Date }, newWeather);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete(Name = "DeleteWeatherForecastingSummary")]
        public IActionResult DeleteWeatherForecastingSummary(string summary)
        {
            try
            {
                var weather = _context.WeatherForecastingSummary.FirstOrDefault(w => w.Summary == summary);
                if (weather == null)
                {
                    return NotFound();
                }

                _context.WeatherForecastingSummary.Remove(weather);
                _context.SaveChanges();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("GetAllWeatherSummaries", Name = "GetAllWeatherSummaries")]
        public IActionResult GetAllWeatherSummaries()
        {
            try
            {
                var summaries = _context.WeatherForecastingSummary.Select(w => w.Summary).ToList();
                return Ok(summaries);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet("GetAllHumidDaysTemperatureGreaterthanThirtyDegree",Name = "GetAllHumidDaysTemperatureGreaterthanThirtyDegree")]
        public ActionResult<List<Weather>> GetAllHumidDaysTemperatureGreaterthanThirtyDegree()
        {
            try
            {
                var startDate = DateTime.Today.AddDays(-30);
                var endDate = DateTime.Today;
                var weatherData = _context.WeatherForecastingSummary.Where(x => x.Date >= startDate && x.Date <= endDate).ToList();
                if (weatherData.Any())
                {
                    var humidDays = weatherData.Where(w => w.TemperatureC > 30 && w.Summary == "Humid").ToList();
                    return Ok(humidDays);
                }
                else
                {
                    return NotFound("No weather data for last 30 days");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500,$"An error occured:{ex.Message}");
            }
        }
        [HttpGet("GetAverageTemperatureForLastThirtyDays", Name = "GetAverageTemperatureForLastThirtyDays")]
        public IActionResult GetAverageTemperatureForLastThirtyDays()
        {
            try
            {
                var startDate = DateTime.Today.AddDays(-30);
                var endDate = DateTime.Today;
                //var averageTemperature = _context.WeatherForecastingSummary.Where(x => x.Date >= startDate && x.Date <= endDate).Average(x => x.TemperatureC);
                var LastThirtyDays = _context.WeatherForecastingSummary.Where(x => x.Date >= startDate && x.Date <= endDate).ToList();
                var temperatureSum = LastThirtyDays.Sum(y => y.TemperatureC);
                var temperatureDaysCount = LastThirtyDays.Count;
                //var averageTemperature = temperatureSum / temperatureDaysCount;
                var averageTemperature = temperatureDaysCount > 0 ? temperatureSum / temperatureDaysCount : 0;
                return Ok(averageTemperature);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occured:{ex.Message}");
            }
        }
        [HttpGet("GetAverageTemperatureForSnowDaysFor7Days", Name = "GetAverageTemperatureForSnowDaysFor7Days")]
        public IActionResult GetAverageTemperatureForSnowDaysFor7Days()
        {
            try
            {
                var startDate = DateTime.Today.AddDays(-7);
                var endDate = DateTime.Today;
                //var averageTemperature = _context.WeatherForecastingSummary.Where(x => x.Date >= startDate && x.Date <= endDate && x.Summary == "Snow").Average(x => x.TemperatureC);
                var LastThirtyDays = _context.WeatherForecastingSummary.Where(x => x.Date >= startDate && x.Date <= endDate && x.Summary == "Snow").ToList();
                var temperatureSum = LastThirtyDays.Sum(y => y.TemperatureC);
                var temperatureDaysCount = LastThirtyDays.Count;
                //var averageTemperature = temperatureSum / temperatureDaysCount;
                var averageTemperature = temperatureDaysCount > 0 ? temperatureSum / temperatureDaysCount : 0;
                return Ok(averageTemperature);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occured:{ex.Message}");
            }
        }
    }
}