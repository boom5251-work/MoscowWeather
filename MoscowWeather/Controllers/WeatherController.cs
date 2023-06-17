using Microsoft.AspNetCore.Mvc;
using MoscowWeather.DataAccess.Context;
using MoscowWeather.DataAccess.Files.Xlsx;
using System.IO.Compression;

namespace MoscowWeather.Controllers
{
    /// <summary>
    /// Контроллер погоды.
    /// </summary>
    public class WeatherController : Controller
    {
        /// <summary>
        /// Контекст базы данных.
        /// </summary>
        private readonly IDatabaseContext _context;



        /// <summary>
        /// Параметризированный конструктор.
        /// </summary>
        /// <param name="context">Контекст базы данных.</param>
        public WeatherController(IDatabaseContext context)
        {
            _context = context;
        }



        #region Представления.
        /// <summary>
        /// Возвращает представление главной страницы.
        /// </summary>
        /// <returns>Представление.</returns>
        public IActionResult Index() => View();


        /// <summary>
        /// Возвращает представление страницы выгрузки данных.
        /// </summary>
        /// <returns>Представление.</returns>
        public IActionResult Upload() => View();


        /// <summary>
        /// Возвращает представление страницы отображения данных.s
        /// </summary>
        /// <returns>Представление.</returns>
        public IActionResult DataView() => View();


        /// <summary>
        /// Возвращает представление страницы отображения данных.
        /// </summary>
        /// <returns>Представление.</returns>
        [HttpGet("Weather/DataView/{year:int}")]
        public IActionResult DataView(int year)
        {
            ViewData["Year"] = year;
            return View();
        }
        #endregion



        #region Работа с данными.
        /// <summary>
        /// Обеспечивает загрузку данных из файла.
        /// </summary>
        /// <returns>Результат действия.</returns>
        [HttpPost]
        public async Task<IActionResult> UploadFile()
        {
            var formFile = Request.Form.Files.SingleOrDefault();

            if (formFile is not null && Path.GetExtension(formFile.FileName) == ".zip")
            {
                int changesCount = 0;

                // Распаковка архива.
                using (var zipFileStream = formFile.OpenReadStream())
                using (var archive = new ZipArchive(zipFileStream))
                {
                    var files = archive.Entries.Where(entry => Path.GetExtension(entry.FullName) == ".xlsx");
                    var xlsxFileStreams = files.Select(file => file.Open()).ToList();

                    foreach (var stream in xlsxFileStreams)
                    {
                        // Извлечение данных из файлов XLSX.
                        var parseAction = () => XlsxParser.GetWeatherConditions(stream);
                        var conditions = await Task.Run(parseAction);

                        // Сохранение данных в БД.
                        _context.WeatherConditions.AddRange(conditions);
                        changesCount += await _context.SaveChangesAsync();
                    }
                }

                return Ok(new { ChangesCount = changesCount });
            }
            else
            {
                return BadRequest(new { ErrorMessage = "Файл имеет неверный формат или отсутствует." });
            }
        }
        #endregion
    }
}