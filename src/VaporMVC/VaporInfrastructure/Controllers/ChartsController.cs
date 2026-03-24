using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace VaporInfrastructure.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChartsController : ControllerBase
    {

        private record CountGameByPublisherResponseItem(string Publisher, int Count);
        private record CountGameByGenreResponseItem(string Genre, int Count);
        private record CountOrdersByDayResponseItem(string Day, int OrderCount);

        private readonly VaporContext _context;

        public ChartsController(VaporContext context)
        {
            this._context = context;
        }

        [HttpGet("countGameByPublisher")]
        public async Task<JsonResult> GetGameByPublisherAsync(CancellationToken
        cancellationToken)
        {
            var responseItems = await _context
                .Games
                .GroupBy(game => game.Publisher.Name)
                    .Select(group => new CountGameByPublisherResponseItem(group.Key, group.Count()))
                .ToListAsync(cancellationToken);

            return new JsonResult(responseItems);
        }

        [HttpGet("countGameByGenre")]
        public async Task<JsonResult> GetGameByGenreAsync(CancellationToken
        cancellationToken)
        {
            var responseItems = await _context
                .Games
                .SelectMany(game => game.Genres)
                .GroupBy(gen => gen.Name)
                    .Select(group => new CountGameByGenreResponseItem(group.Key, group.Count()))
                .ToListAsync(cancellationToken);

            return new JsonResult(responseItems);
        }

        [HttpGet("countOrdersByDay")]
        public async Task<JsonResult> GetOrdersByDayAsync(CancellationToken
        cancellationToken)
        {
            var responseItems = await _context
                .Orders
                .Where(o => o.StatusId == 2)
                .GroupBy(order => order.CreatedDate.Date)
                .OrderBy(group => group.Key)
                    .Select(group => new CountOrdersByDayResponseItem(group.Key.ToString("dd.MM.yyyy"), group.Count()))
                .ToListAsync(cancellationToken);

            return new JsonResult(responseItems);
        }
    }

}

