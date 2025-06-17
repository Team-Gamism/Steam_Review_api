using Server.Model;

namespace Server.Repository.Interface;

public interface IGameReviewRepository
{
    Task InsertAsync(GameReview gameReview);
    Task<IEnumerable<GameReview>> GetReviewsByGameAndSentimentAsync(string game, string sentiment);
    Task<SentimentSummary> GetSentimentSummaryAsync(string game, int? year);
}