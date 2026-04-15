using ClosedXML.Excel;
using Microsoft.EntityFrameworkCore;
using VaporDomain.Model;

namespace VaporInfrastructure.Services
{
    public class GameDataPortServiceFactory : IDataPortServiceFactory<Game>
    {
        private readonly VaporContext _context;

        public GameDataPortServiceFactory(VaporContext context)
        {
            _context = context;
        }

        public IImportService<Game> GetImportService(string contentType)
        {
            if (contentType is "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
            {
                return new GameImportService(_context);
            }
            throw new NotImplementedException($"No import service implemented for movies with content type {contentType}");
        }
        public IExportService<Game> GetExportService(string contentType)
        {
            if (contentType is "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
            {
                return new GameExportService(_context);
            }
            throw new NotImplementedException($"No export service implemented {contentType}");
        }
    }
}