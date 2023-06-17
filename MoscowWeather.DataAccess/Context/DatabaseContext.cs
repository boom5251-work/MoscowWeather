using Microsoft.EntityFrameworkCore;
using MoscowWeather.DataAccess.Models;

namespace MoscowWeather.DataAccess.Context
{
    /// <summary>
    /// Контекст базы данных.
    /// </summary>
    public sealed class DatabaseContext : DbContext, IDatabaseContext
    {
        /// <summary>
        /// Параметризированный конструктор.
        /// </summary>
        /// <param name="options">Настройки контекста базы данных.</param>
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
            Database.EnsureCreated();
        }


        /// <inheritdoc />
        public DbSet<WeatherCondition> WeatherConditions { get; set; } = null!;
    }
}