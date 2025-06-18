using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.Model.Dto.Request;
using Server.Model.Dto.Response;
using Server.Model.Entity;
using Server.Service.Interface;

namespace Server.Controller
{
    [ApiController]
    [Route("api/review")]
    public class GameReviewController : ControllerBase
    {
        private readonly IGameReviewService _reviewService;

        public GameReviewController(IGameReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpPost("import")]
        public async Task<IActionResult> ImportCsv([FromQuery] string path)
        {
            if (string.IsNullOrEmpty(path))
                return BadRequest("CSV 파일 경로를 입력하세요.");

            await _reviewService.ImportCsvToDb(path);
            return Ok("CSV 데이터가 DB에 저장되었습니다.");
        }

        [HttpGet]
        public async Task<IActionResult> GetTotalId()
        {
            var count = await _reviewService.GetTotalCountOfIdAsync();
            return Ok(new { totalIdCount = count });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetReviewById(int id)
        {
            var review = await _reviewService.GetByIdAsync(id);
            if (review == null)
                return NotFound();

            return Ok(review);
        }

        [HttpGet("average")]
        public async Task<IActionResult> GetAverageSentimentAsync([FromQuery] string game)
        {
            if (string.IsNullOrWhiteSpace(game))
                return BadRequest("게임 이름을 입력하세요.");

            var avgSentiment = await _reviewService.GetAverageSentimentAsync(game);
            if (avgSentiment == null)
                return NotFound("해당 게임에 대한 리뷰가 없습니다.");

            return Ok(new { game, averageSentiment = Math.Round(avgSentiment.Value, 2) });
        }

        [HttpGet("games")]
        public async Task<IActionResult> GetAllGamesAsync()
        {
            var games = await _reviewService.GetAllGamesAsync();
            return Ok(games);
        }

        [HttpGet("game")]
        public async Task<IActionResult> GetGameStatisticsAsync([FromQuery] string game)
        {
            if (string.IsNullOrWhiteSpace(game))
                return BadRequest("게임 이름을 입력하세요.");

            var stats = await _reviewService.GetStatisticsByGameAsync(game);
            if (stats.TotalReviews == 0)
                return NotFound($"'{game}'에 대한 리뷰가 없습니다.");

            return Ok(stats);
        }
    }
}
