using Microsoft.EntityFrameworkCore;
using WeatherCRUD_Operations.Models;

public class WeatherContext : DbContext
{
    public DbSet<Weather> WeatherForecastingSummary { get; set; }
    public WeatherContext(DbContextOptions<WeatherContext> options)
        : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Weather>()
            .HasKey(w => w.SummaryId);
    }
}