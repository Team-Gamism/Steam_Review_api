using Microsoft.AspNetCore.Mvc;
using Server.Service.Interface;

namespace Server.Controller
{
    [Route("api/[controller]")]
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
    }
}
