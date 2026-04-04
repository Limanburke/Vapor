using ClosedXML.Excel;
using Microsoft.EntityFrameworkCore;
using VaporDomain.Model;

namespace VaporInfrastructure.Services
{
    public class GameExportService : IExportService<Game>
    {
        private readonly VaporContext _context;

        public GameExportService(VaporContext context)
        {
            _context = context;
        }

        public async Task WriteToAsync(Stream stream, CancellationToken cancellationToken)
        {

            var allGames = await _context.Games
                               .Include(g => g.Publisher)
                               .Include(g => g.Genres)
                               .ToListAsync(cancellationToken);

            if (!stream.CanWrite || allGames == null)
            {
                throw new ArgumentException("Дані не можуть бути записані");
            }

            var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Ігри");

            string[] header = ["Назва", "Ціна", "Доступна для покупки", "Опис", "Дата виходу", "Видавець", "Жанри"];

            for (int i = 0; i < header.Count(); i++)
            {
                worksheet.Cell(1, i + 1).Value = header[i];
                worksheet.Cell(1, i + 1).Style.Font.Bold = true;
            }

            int rowIndex = 2;

            foreach (var game in allGames)
            {
                worksheet.Cell(rowIndex, 1).Value = game.Title;
                worksheet.Cell(rowIndex, 2).Value = game.Price;
                worksheet.Cell(rowIndex, 3).Value = game.IsAvailable;
                worksheet.Cell(rowIndex, 4).Value = game.Description;
                worksheet.Cell(rowIndex, 5).Value = game.ReleasedDate.ToString("dd.MM.yyyy HH:mm");
                worksheet.Cell(rowIndex, 6).Value = game.Publisher?.Name;

                worksheet.Cell(rowIndex, 7).Value = string.Join(", ", game.Genres.Select(g => g.Name));

                rowIndex++;
            }
            workbook.SaveAs(stream);
        }
    }
}
