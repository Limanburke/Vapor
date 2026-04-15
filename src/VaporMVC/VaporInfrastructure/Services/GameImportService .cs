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
                int rowNumber = 1;
                var worksheet = workbook.Worksheet(1);
                var importedTitles = new List<string>();

                foreach (var row in worksheet.RowsUsed().Skip(1))
                {
                    rowNumber++;

                    var gameTitle = row.Cell(1).Value.ToString().Trim();
                    if (string.IsNullOrWhiteSpace(gameTitle))
                    {
                        throw new InvalidDataException($"Помилка у рядку {rowNumber}: Назва гри є обов'язковою");
                    }

                    if (gameTitle.Length > 200)
                    {
                        throw new InvalidDataException($"Помилка у рядку {rowNumber}: Назва гри не може перевищувати 200 символів");
                    }

                    if(importedTitles.Contains(gameTitle.ToLower()))
                    {
                        throw new InvalidDataException($"Помилка у рядку {rowNumber}: Гра з назвою '{gameTitle}' дублюється всередині файлу");
                    }

                    importedTitles.Add(gameTitle.ToLower());

                    var game = await _context.Games.FirstOrDefaultAsync(g => g.Title.ToLower() == gameTitle, cancellationToken);
                    if (game == null)
                    {
                        game = new Game();
                        game.Title = gameTitle;

                        var priceCell = row.Cell(2);
                        if (priceCell.DataType == XLDataType.Number)
                        {
                            game.Price = (decimal)priceCell.GetDouble();
                            if (game.Price < 0 || game.Price > 99999999.99m)
                            {
                                throw new InvalidDataException($"Помилка у рядку {rowNumber}: Ціна має бути від 0 до 99 999 999.99");
                            }
                        }
                        else
                        {
                            throw new InvalidDataException($"Помилка у рядку {rowNumber}: Комірка з ціною має бути числового формату");
                        }

                        var isAvailableCell = row.Cell(3);
                        if (isAvailableCell.DataType == XLDataType.Boolean)
                        {
                            game.IsAvailable = isAvailableCell.GetBoolean();
                        }
                        else
                        {
                            game.IsAvailable = false;
                        }

                        game.Description = row.Cell(4).Value.ToString();

                        var dateCell = row.Cell(5);
                        if (dateCell.DataType == XLDataType.DateTime)
                        {
                            game.ReleasedDate = dateCell.GetDateTime();
                        }
                        else
                        {
                            game.ReleasedDate = DateTime.UtcNow;
                        }

                        var publisherName = row.Cell(6).Value.ToString().Trim();
                        if (string.IsNullOrWhiteSpace(publisherName))
                        {
                            throw new InvalidDataException($"Помилка у рядку {rowNumber}: Назва видавця є обов'язковою");
                        }
                        if (publisherName.Length > 100)
                        {
                            throw new InvalidDataException($"Помилка у рядку {rowNumber}: Назва видавця не може перевищувати 100 символів");
                        }

                        var publisher = await _context.Publishers.FirstOrDefaultAsync(p => p.Name == publisherName);
                        if (publisher == null)
                        {
                            publisher = new Publisher();
                            publisher.Name = publisherName;
                            _context.Publishers.Add(publisher);
                        }

                        game.Publisher = publisher;

                        var genresText = row.Cell(7).Value.ToString();
                        if (!string.IsNullOrWhiteSpace(genresText))
                        {
                            var genres = genresText.Split(',');

                            foreach (var gen in genres)
                            {
                                var cleanGenreName = gen.Trim();
                                if (string.IsNullOrWhiteSpace(cleanGenreName)) continue;

                                if (cleanGenreName.Length > 100)
                                {
                                    throw new InvalidDataException($"Помилка у рядку {rowNumber}: Назва жанру '{cleanGenreName}' не може перевищувати 100 символів");
                                }

                                var genre = await _context.Genres.FirstOrDefaultAsync(g => g.Name == cleanGenreName);
                                if (genre == null)
                                {
                                    genre = new Genre();
                                    genre.Name = cleanGenreName;
                                    _context.Genres.Add(genre);
                                }

                                game.Genres.Add(genre);
                            }
                        }
                        _context.Games.Add(game);
                    }
                }
                await _context.SaveChangesAsync(cancellationToken);
            }
        }
    }
}