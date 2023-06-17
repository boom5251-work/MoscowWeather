using Microsoft.EntityFrameworkCore;

namespace MoscowWeather.DataAccess.Models
{
    /// <summary>
    /// Модель погоды.
    /// </summary>
    [Index(nameof(DateTime), IsUnique = true)]
    public class WeatherCondition
    {
        /// <summary>
        /// Уникальный идентификатор.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Дата и время.
        /// </summary>
        public required DateTime DateTime { get; set; }

        /// <summary>
        /// Температура воздуха.
        /// </summary>
        public double? Temperature { get; set; }

        /// <summary>
        /// Относительная влажность.
        /// </summary>
        public double? RelativeHumidity { get; set; }

        /// <summary>
        /// Точка росы.
        /// </summary>
        public double? DewPoint { get; set; }

        /// <summary>
        /// Атмосферное давление.
        /// </summary>
        public int? AirPressure { get; set; }

        /// <summary>
        /// Направление ветра.
        /// </summary>
        public string? WindDirection { get; set; }

        /// <summary>
        /// Скорость ветра.
        /// </summary>
        public int? WindSpeed { get; set; }

        /// <summary>
        /// Облачность.
        /// </summary>
        public int? CloudCover { get; set; }

        /// <summary>
        /// Нижняя граница облачности.
        /// </summary>
        public int? CloudHeight { get; set; }

        /// <summary>
        /// Горизонтальная видимость.
        /// </summary>
        public int? HorizontalVisibility { get; set; }

        /// <summary>
        /// Погодные явления.
        /// </summary>
        public string? WeatherPhenomena { get; set; }
    }
}