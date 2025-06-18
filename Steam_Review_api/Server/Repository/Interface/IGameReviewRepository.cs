using Server.Model;
using Server.Model.Entity;

namespace Server.Repository.Interface;

public interface IGameReviewRepository
{
    Task InsertAsync(IEnumerable<GameReview> gameReview);
    Task<GameReview?> GetByIdAsync(int reviewId);
    Task<IEnumerable<GameReview?>> GetReviewsByGameAsync(string steamId);
    Task<double?> GetAverageSentimentAsync(string game);
    Task<IEnumerable<string>> GetDistinctGamesAsync();
    Task<bool> ExistReviewIdAsync(int reviewId);
}