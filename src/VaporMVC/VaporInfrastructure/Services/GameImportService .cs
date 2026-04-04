using ClosedXML.Excel;
using Microsoft.EntityFrameworkCore;
using VaporDomain.Model;

namespace VaporInfrastructure.Services
{
    public class GameImportService : IImportService<Game>
    {
        private readonly VaporContext _context;

        public GameImportService(VaporContext context)
        {
            _context = context;
        }

        public async Task ImportFromStreamAsync(Stream stream, CancellationToken cancellationToken)
        {
            if (!stream.CanRead)
            {
                throw new ArgumentException("Дані не можуть бути прочитані", nameof(stream));
            }

            using (var workbook = new XLWorkbook(stream))
            {
                var worksheet = workbook.Worksheet(1);
                foreach (var row in worksheet.RowsUsed().Skip(1))
                {

                    var gameTitle = row.Cell(1).Value.ToString();
                    var game = await _context.Games.FirstOrDefaultAsync(g => g.Title == gameTitle, cancellationToken);

                    if (game == null)
                    {
                        game = new Game();
                        game.Title = gameTitle;
                        game.Price = (decimal)row.Cell(2).Value.GetNumber();
                        game.IsAvailable = row.Cell(3).Value.GetBoolean();
                        game.Description = row.Cell(4).Value.ToString();
                        game.ReleasedDate = row.Cell(5).Value.GetDateTime();

                        var publisherName = row.Cell(6).Value.ToString();
                        var publisher = await _context.Publishers.FirstOrDefaultAsync(p => p.Name == publisherName);

                        if (publisher == null)
                        {
                            publisher = new Publisher();
                            publisher.Name = publisherName;
                            _context.Publishers.Add(publisher);
                        }

                        game.Publisher = publisher;

                        var genres = row.Cell(7).Value.ToString().Split(',');

                        foreach (var gen in genres)
                        {
                            var genre = await _context.Genres.FirstOrDefaultAsync(g => g.Name == gen.Trim());
                            if (genre == null)
                            {
                                genre = new Genre();
                                genre.Name = gen.Trim();
                                _context.Genres.Add(genre);
                            }

                            game.Genres.Add(genre);
                        }
                        _context.Games.Add(game);
                    }
                }

                await _context.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
