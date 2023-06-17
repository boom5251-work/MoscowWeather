using Microsoft.EntityFrameworkCore;
using MoscowWeather.DataAccess.Models;

namespace MoscowWeather.DataAccess.Context
{
    /// <summary>
    /// Интерфейс контекста базы данных.
    /// </summary>
    public interface IDatabaseContext
    {
        /// <summary>
        /// Набор погодных состояний.
        /// </summary>
        public DbSet<WeatherCondition> WeatherConditions { get; set; }


        public int SaveChanges();
        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}