using Microsoft.AspNetCore.Mvc;
using Server.Service.Interface;

namespace Server.Controller
{
    [Route("api/review")]
    [ApiController]
    public class GameReviewController : ControllerBase
    {
        private readonly IGameReviceService _gameReviewService;

        public GameReviewController(IGameReviceService gameReviewService)
        {
            _gameReviewService = gameReviewService;
        }
        
        [HttpPost("import")]
        public async Task<IActionResult> ImportCsv([FromQuery] string path)
        {
            if (string.IsNullOrEmpty(path))
                return BadRequest("CSV 파일 경로를 입력하세요.");

            await _gameReviewService.ImportCsvToDb(path);
            return Ok("CSV 데이터가 DB에 저장되었습니다.");
        }

        [HttpGet]
        public async Task<IActionResult> GetReviewById([FromQuery] int id)
        {
            var review = await _gameReviewService.GetByIdAsync(id);
            if (review == null)
                return NotFound();
            
            return Ok(review);
        }

        [HttpGet("average")]
        public async Task<IActionResult> GetAverageSentimentAsync([FromQuery] string game)
        {
            if (string.IsNullOrEmpty(game))
                return BadRequest("게임 이름을 입력하세요.");

            var avgSentiment = await _gameReviewService.GetAverageSentimentAsync(game);

            if (avgSentiment == null)
                return NotFound("해당 게임에 대한 리뷰가 없습니다.");

            return Ok(new { game, averageSentiment = avgSentiment });
        }
    }
}
