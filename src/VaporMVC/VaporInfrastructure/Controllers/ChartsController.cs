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
                               .GroupBy(x => x.Publisher.Name)
                               .OrderByDescending(x => x.Count())
                               .Select(x => new CountGameByPublisherResponseItem(x.Key, x.Count()))
                               .Take(3)
                               .ToListAsync(cancellationToken);

            return new JsonResult(responseItems);
        }

        [HttpGet("countGameByGenre")]
        public async Task<JsonResult> GetGameByGenreAsync(CancellationToken
        cancellationToken)
        {
            var responseItems = await _context
                                .Games.SelectMany(game => game.Genres)
                                .GroupBy(gen => gen.Name)
                                .OrderByDescending(gen => gen.Count())
                                .Select(group => new CountGameByGenreResponseItem(group.Key, group.Count()))
                                .Take(3)
                                .ToListAsync(cancellationToken);

            var item = await _context.Games.SelectMany(game => game.Genres).CountAsync(cancellationToken);
            var top3Sum = responseItems.Sum(x => x.Count);

            var otherSum = item - top3Sum;
            if (otherSum > 0)
            {
                responseItems.Add(new CountGameByGenreResponseItem("Інші", otherSum));
            }

            return new JsonResult(responseItems);
        }

        [HttpGet("countOrdersByMonth")]
        public async Task<JsonResult> GetOrdersByMonthAsync(CancellationToken
        cancellationToken, int? year)
        {
            if (year == null) 
            { 
                year = DateTime.Now.Year;
            }

            var dateDict = new Dictionary<int, int>
            {
                {1, 0 }, {2, 0 }, {3, 0 }, {4, 0 }, {5, 0 }, {6, 0 },
                {7, 0 }, {8, 0 }, {9, 0 }, {10, 0 }, {11, 0 }, {12, 0 }
            };

            var responseItems = await _context
                                .Orders
                                .Where(o => o.StatusId == 2 && o.CreatedDate.Year == year)
                                .GroupBy(order => order.CreatedDate.Month)
                                .OrderBy(group => group.Key)
                                .Select(group => new { month = group.Key, count = group.Count() })
                                .ToListAsync(cancellationToken);

            foreach (var response in responseItems)
            {
                dateDict[response.month] = response.count;
            }

            var orderedValues = dateDict.OrderBy(x => x.Key).Select(x => x.Value);
            return new JsonResult(orderedValues);
        }
    }
}