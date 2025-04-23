using KPO_HW2.Application.Services;
using KPO_HW2.Domain.Service;
using Microsoft.AspNetCore.Mvc;

namespace KPO_HW2.Presentation.Controllers
{
    [ApiController]
    [Route("api/zoo-statistics")]
    public class ZooStatisticsController : ControllerBase
    {
        private readonly IZooStatisticsService _statisticsService;

        public ZooStatisticsController(IZooStatisticsService statisticsService)
        {
            _statisticsService = statisticsService;
        }

        [HttpGet("animals-count")]
        public async Task<IActionResult> GetAnimalsCount()
        { 
            var count = await _statisticsService.GetAnimalsCount();
            return Ok(new { AnimalsCount = count });
        }

        [HttpGet("free-enclosures-count")]
        public async Task<IActionResult> GetFreeEnclosuresCount()
        {
            var count = await _statisticsService.GetFreeEnclosuresCount();
            return Ok(new { FreeEnclosuresCount = count });
        }

        [HttpGet("completed-feedings-count")]
        public async Task<IActionResult> GetCompletedFeedingsCount()
        {

            var count = await _statisticsService.GetCompletedFeedingsCount();
            return Ok(new { CompletedFeedingsCount = count });

        }

        [HttpGet("summary")]
        public async Task<IActionResult> GetSummaryStatistics()
        {

            var animalsCount = await _statisticsService.GetAnimalsCount();
            var freeEnclosures = await _statisticsService.GetFreeEnclosuresCount();
            var completedFeedings = await _statisticsService.GetCompletedFeedingsCount();

            return Ok(new
            {
                AnimalsCount = animalsCount,
                FreeEnclosuresCount = freeEnclosures,
                CompletedFeedingsCount = completedFeedings,
            });         
        }
    }
}