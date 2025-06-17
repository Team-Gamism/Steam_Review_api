using Server.Model;

namespace Server.Service.Interface;

public interface IGameReviceService
{
    Task ImportCsvToDb(string path);
    Task<GameReview?> GetByIdAsync(int id);
    Task<double?> GetAverageSentimentAsync(string game);
}