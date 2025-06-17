using Server.Model;

namespace Server.Repository.Interface;

public interface IGameReviewRepository
{
    Task InsertAsync(IEnumerable<GameReview> gameReview);
    Task<IEnumerable<GameReview>> GetReviewsByGameAndSentimentAsync(string game, string sentiment);
    Task<SentimentSummary> GetSentimentSummaryAsync(string game, int? year);
    Task<GameReview?> GetByIdAsync(int reviewId);
    Task<double?> GetAverageSentimentAsync(string game);
}