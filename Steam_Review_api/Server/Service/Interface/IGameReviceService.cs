using Server.Model;
using Server.Model.Entity;

namespace Server.Service.Interface;

public interface IGameReviceService
{
    Task ImportCsvToDb(string path);
    Task<GameReview?> GetByIdAsync(int id);
    Task<double?> GetAverageSentimentAsync(string game);
    Task<IEnumerable<string>> GetAllGamesAsync();
    Task AddNewGameReviewAsync(GameReview review);
}