using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeatherCRUD_Operations.Models
{
    public class Weather
    {
        public Weather(int temperatureC)
        {
            Date = DateTime.Now;
            TemperatureC = temperatureC;
        }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SummaryId { get; set; }
        public DateTime Date { get; set; }
        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        public string? Summary { get; set; }
        public  string city { get; set; }
    }
}
