using MoscowWeather.DataAccess.Models;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;
using System.Globalization;

namespace MoscowWeather.DataAccess.Files.Xlsx
{
    /// <summary>
    /// Парсер документов XLSX.
    /// </summary>
    public static class XlsxParser
    {
        /// <summary>
        /// Разбирает документ XLSX.<br />
        /// Формирует данные погоды.
        /// </summary>
        /// <param name="stream">Поток файла XLSX с данными погоды.</param>
        /// <returns>Список погодных данных.</returns>
        public static List<WeatherCondition> GetWeatherConditions(Stream stream)
        {
            var workbook = new XSSFWorkbook(stream);
            stream.Dispose();

            var conditions = new List<WeatherCondition>();

            // Перебор таблиц в книге.
            for (int sheetIndex = 0; sheetIndex < workbook.NumberOfSheets; sheetIndex++)
            {
                var sheet = workbook.GetSheetAt(sheetIndex);

                // Перебор строк в таблице.
                for (int rowIndex = sheet.FirstRowNum + 4; rowIndex < sheet.LastRowNum; rowIndex++)
                {
                    var condition = CreateCondition(sheet.GetRow(rowIndex));

                    if (condition is not null)
                        conditions.Add(condition);
                }
            }

            return conditions;
        }


        /// <summary>
        /// Создает экземпляр модели погоды.
        /// </summary>
        /// <param name="row">Строка таблицы.</param>
        /// <returns>Модель погоды.</returns>
        private static WeatherCondition? CreateCondition(IRow? row)
        {
            try
            {
                string? date = row?.GetCell(0)?.StringCellValue;
                string? time = row?.GetCell(1)?.StringCellValue;
                var dateTime = DateTime.ParseExact($"{date} {time}", "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture);

                string? windDirection = !string.IsNullOrWhiteSpace(row?.GetCell(6)?.ToString()) ?
                    row.GetCell(6).StringCellValue : null;

                string? weatherPhenomena = !string.IsNullOrWhiteSpace(row?.GetCell(11)?.ToString()) ?
                    row.GetCell(11).StringCellValue : null;


                return new WeatherCondition
                {
                    DateTime = dateTime,

                    Temperature = GetNumericCellValue(row?.GetCell(2)),
                    RelativeHumidity = GetNumericCellValue(row?.GetCell(3)),
                    DewPoint = GetNumericCellValue(row?.GetCell(4)),
                    AirPressure = (int?)GetNumericCellValue(row?.GetCell(5)),

                    WindDirection = windDirection,
                    WindSpeed = (int?)GetNumericCellValue(row?.GetCell(7)),
                    CloudCover = (int?)GetNumericCellValue(row?.GetCell(8)),
                    CloudHeight = (int?)GetNumericCellValue(row?.GetCell(9)),
                    HorizontalVisibility = (int?)GetNumericCellValue(row?.GetCell(10)),
                    WeatherPhenomena = weatherPhenomena
                };
            }
            catch (Exception)
            {
                return null;
            }
        }


        /// <summary>
        /// Получает цифровое значение из ячейки.
        /// </summary>
        /// <param name="cell">Ячейка.</param>
        /// <returns>Цифровое значение.</returns>
        private static double? GetNumericCellValue(ICell? cell)
        {
            if (!string.IsNullOrWhiteSpace(cell?.ToString()))
            {
                cell.SetCellType(CellType.Numeric);
                return cell.NumericCellValue;
            }
            else
            {
                return null;
            }
        }
    }
}