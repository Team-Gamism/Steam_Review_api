using Server.Model.Entity;
using Server.Repository.Interface;
using Server.Service.Interface;

namespace Server.Service;

public class GameReviewService : IGameReviewService
{
    private readonly IGameReviewRepository _gameReviewRepository;
    private readonly ICsvService _csvService;
    
    private static readonly string[] AllowedSentiments = { "positive", "neutral", "negative" };

    public GameReviewService(IGameReviewRepository gameReviewRepository, ICsvService csvService)
    {
        _gameReviewRepository = gameReviewRepository;
        _csvService = csvService;
    }
    
    public async Task ImportCsvToDb(string path)
    {
        var reviews = _csvService.ReadAll(path);
        await _gameReviewRepository.InsertAsync(reviews);
    }

    public async Task<GameReview?> GetByIdAsync(int id)
    {
        return await _gameReviewRepository.GetByIdAsync(id);
    }

    public async Task<double?> GetAverageSentimentAsync(string game)
    {
        return await _gameReviewRepository.GetAverageSentimentAsync(game);
    }

    public async Task<IEnumerable<string>> GetAllGamesAsync()
    {
        return await _gameReviewRepository.GetDistinctGamesAsync();
    }

    public async Task AddNewGameReviewAsync(GameReview review)
    {
        if (review == null)
            throw new ArgumentNullException(nameof(review));
        
        if (string.IsNullOrWhiteSpace(review.Sentiment))
            throw new ArgumentException("Sentiment를 입력하세요.");
        
        var sentimentLower = review.Sentiment.Trim().ToLowerInvariant();
        if (!AllowedSentiments.Contains(sentimentLower))
            throw new ArgumentException($"sentiment 값은 {string.Join(", ", AllowedSentiments)} 중 하나여야 합니다.");
        
        review.Sentiment = sentimentLower;
        
        if (await _gameReviewRepository.ExistReviewIdAsync(review.ReviewId))
            throw new Exception($"이미 존재하는 리뷰 ID 입니다.: {review.ReviewId}");
        
        await _gameReviewRepository.AddNewReviewAsync(review);
    }

    public async Task<GameReviewStatistics> GetStatisticsByGameAsync(string game)
    {
        var reviews = await _gameReviewRepository.GetReviewsByGameAsync(game);
        var total = reviews.Count();

        int positive = reviews.Count(r => r.Sentiment.Equals("positive", StringComparison.OrdinalIgnoreCase));
        int neutral = reviews.Count(r => r.Sentiment.Equals("neutral", StringComparison.OrdinalIgnoreCase));
        int negative = reviews.Count(r => r.Sentiment.Equals("negative", StringComparison.OrdinalIgnoreCase));
        
        double avgScore = Math.Round(reviews.Average(r =>
            r.Sentiment.Equals("positive", StringComparison.OrdinalIgnoreCase) ? 5.0 :
            r.Sentiment.Equals("neutral", StringComparison.OrdinalIgnoreCase) ? 3.0 : 0.0), 2);
        
        if (total == 0)
        {
            return new GameReviewStatistics
            {
                Game = game,
                TotalReviews = total,
                PositiveCount = positive,
                NeutralCount = neutral,
                NegativeCount = negative,
                PositiveRatio = Math.Round(positive / (double)total, 2),
                NeutralRatio = Math.Round(neutral / (double)total, 2),
                NegativeRatio = Math.Round(negative / (double)total, 2),
                AverageSentimentScore = avgScore
            };
        }
        
        return new GameReviewStatistics
        {
            Game = game,
            TotalReviews = total,
            PositiveCount = positive,
            NeutralCount = neutral,
            NegativeCount = negative,
            PositiveRatio = positive / (double)total,
            NeutralRatio = neutral / (double)total,
            NegativeRatio = negative / (double)total,
            AverageSentimentScore = avgScore
        };
    }
}