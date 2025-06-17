using Server.Repository.Interface;
using Server.Service.Interface;

namespace Server.Service;

public class GameRevieweService : IGameReviceService
{
    private readonly IGameReviewRepository _gameReviewRepository;
    private readonly ICsvService _csvService;

    public GameRevieweService(IGameReviewRepository gameReviewRepository, ICsvService csvService)
    {
        _gameReviewRepository = gameReviewRepository;
        _csvService = csvService;
    }
    
    public async Task ImportCsvToDb(string path)
    {
        var reviews = _csvService.ReadAll(path);
        await _gameReviewRepository.InsertAsync(reviews);
    }
}