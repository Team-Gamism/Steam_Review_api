using Microsoft.AspNetCore.Mvc;
using Server.Model.Dto.Request;
using Server.Model.Dto.Response;
using Server.Model.Entity;
using Server.Service.Interface;

namespace Server.Controller
{
    [Route("api/review")]
    [ApiController]
    public class GameReviewController : ControllerBase
    {
        private readonly IGameReviewService _gameReviewService;

        public GameReviewController(IGameReviewService gameReviewService)
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

        [HttpPost]
        public async Task<ActionResult<AddReviewResponse>> AddReview([FromBody] AddReviewRequest req)
        {
            if (req == null)
                return BadRequest("리뷰 데이터를 확인하세요.");

            var review = new GameReview
            {
                ReviewId = req.ReviewId,
                Game = req.Game,
                Year = req.Year,
                Review = req.Review,
                Sentiment = req.Sentiment.Trim().ToLowerInvariant(),
                Language = req.Language
            };

            try
            {
                await _gameReviewService.AddNewGameReviewAsync(review);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }

            var response = new AddReviewResponse
            {
                ReviewId = review.ReviewId,
                Message = "리뷰가 추가되었습니다."
            };

            return CreatedAtAction(nameof(GetReviewById), new { id = review.ReviewId }, response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetReviewById(int id)
        {
            var review = await _gameReviewService.GetByIdAsync(id);
            if (review == null)
                return NotFound();

            return Ok(review);
        }

        [HttpGet("average")]
        public async Task<IActionResult> GetAverageSentimentAsync([FromQuery] string game)
        {
            if (string.IsNullOrWhiteSpace(game))
                return BadRequest("게임 이름을 입력하세요.");

            var avgSentiment = await _gameReviewService.GetAverageSentimentAsync(game);
            if (avgSentiment == null)
                return NotFound("해당 게임에 대한 리뷰가 없습니다.");

            return Ok(new { game, averageSentiment = Math.Round(avgSentiment.Value, 2) });
        }

        [HttpGet("games")]
        public async Task<IActionResult> GetAllGamesAsync()
        {
            var games = await _gameReviewService.GetAllGamesAsync();
            return Ok(games);
        }
    }
}
