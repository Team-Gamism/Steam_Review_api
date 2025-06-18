using Server.Model;
using Server.Model.Entity;

namespace Server.Service.Interface;

public interface IGameReviewService
{
    Task ImportCsvToDb(string path);
    Task<GameReview?> GetByIdAsync(int id);
    Task<int> GetTotalCountOfIdAsync();
    Task<IdRange?> GetIdRangeAsync();
    Task<double?> GetAverageSentimentAsync(string game);
    Task<IEnumerable<string>> GetAllGamesAsync();
    Task<GameReviewStatistics> GetStatisticsByGameAsync(string game);
}