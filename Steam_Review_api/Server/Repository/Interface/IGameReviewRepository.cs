using Server.Model;
using Server.Model.Entity;

namespace Server.Repository.Interface;

public interface IGameReviewRepository
{
    Task InsertAsync(IEnumerable<GameReview> gameReview);
    Task<GameReview?> GetByIdAsync(int reviewId);
    Task<double?> GetAverageSentimentAsync(string game);
    Task<IEnumerable<string>> GetDistinctGamesAsync();
    Task AddNewReviewAsync(GameReview review);
}