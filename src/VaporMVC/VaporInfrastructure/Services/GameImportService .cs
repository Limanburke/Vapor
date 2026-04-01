//using ClosedXML.Excel;
//using Microsoft.EntityFrameworkCore;
//using VaporDomain.Model;

//namespace VaporInfrastructure.Services
//{
//    public class GameImportService : IImportService<Game>
//    {
//        private readonly VaporContext _context;

//        public GameImportService(VaporContext context)
//        {
//            _context = context;
//        }

//        public async Task ImportFromStreamAsync(Stream stream, CancellationToken cancellationToken)
//        {
//            if (!stream.CanRead)
//            {
//                throw new ArgumentException("Дані не можуть бути прочитані", nameof(stream));
//            }

//            var workbook = new XLWorkbook(stream);
//            var worksheet = workbook.Worksheet(1);


//            foreach (var row in worksheet.RowsUsed().Skip(1))
//            {

//                var gameTitle = row.Cell(1).Value.ToString();
//                var game = await _context.Games.FirstOrDefaultAsync(g => g.Title == gameTitle, cancellationToken);
//                    if (game == null)
//                    {
//                        game = new Game();
//                        game.Title = gameTitle;
//                        game.IsAvailable = row.Cell(2).Value;
//                        game.Description = row.Cell(3).Value.ToString();
//                        game.Price = row.Cell(4).Value;
//                        game.ReleasedDate = row.Cell(5).Value.ToString("dd.MM.yyyy HH:mm");
                        
//                    var publisherName = row.Cell(6).Value.ToString();
//                    var publisher = await _context.Publishers.FirstOrDefaultAsync(p => p.Name == publisherName);

//                    if(publisher == null)
//                    {
//                        publisher = new Publisher();
//                        publisher.Name = publisherName;
//                        _context.Publishers.Add(publisher);
//                    }
//                    else
//                    {
//                        game.Publisher = publisher;
//                    }

//                        _context.Games.Add(game);
//                    }
//                    //перегляд усіх рядків                    
//                    foreach (var row in worksheet.RowsUsed().Skip(1))
//                    //пропустити перший рядок, бо це заголовок
//                    {
//                        await AddBookAsync(row, cancellationToken, category);
//                    }
//                }
            
//            await _context.SaveChangesAsync(cancellationToken);
//        }

//        private async Task AddBookAsync(IXLRow row, CancellationToken cancellationToken, Category category)
//        {
//            Book book = new Book();
//            book.Name = GetBookName(row);
//            book.Info = GetBookInfo(row);
//            book.Category = category;
//            _context.Books.Add(book);
//            await GetAuthorsAsync(row, book, cancellationToken);
//        }

//        private static string GetBookName(IXLRow row)
//        {
//            return row.Cell(1).Value.ToString();
//        }

//        private static string GetBookInfo(IXLRow row)
//        {
//            return row.Cell(6).Value.ToString();
//        }

//        private async Task GetAuthorsAsync(IXLRow row, Book book, CancellationToken cancellationToken)
//        {
//            //у разі наявності автора знайти його, у разі відсутності - додати
//            for (int i = 2; i <= 5; i++)
//            {
//                //чи є запис про автора
//                if (row.Cell(i).Value.ToString().Length > 0)
//                {
//                    var autName = row.Cell(i).Value.ToString();
//                    //перевірка чи є такий автор в базі
//                    var author = await _context.Authors.FirstOrDefaultAsync(aut => aut.Name == autName, cancellationToken);
//                    if (author is null)
//                    {
//                        author = new Author();
//                        author.Name = autName;
//                        author.Info = "from EXCEL";
//                        _context.Authors.Add(author);
//                    }

//                    AuthorBook ab = new AuthorBook();
//                    ab.Book = book;
//                    ab.Author = author;
//                    _context.AuthorBooks.Add(ab);
//                }
//            }
//        }
//    }
//}
