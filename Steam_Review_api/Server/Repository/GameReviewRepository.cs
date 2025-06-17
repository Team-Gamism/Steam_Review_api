using Dapper;
using MySqlConnector;
using Server.Model;
using Server.Repository.Interface;

namespace Server.Repository;

public class GameReviewRepository :  IGameReviewRepository
{
    private readonly string _connectionString;

    public GameReviewRepository(IConfiguration config)
    {
        _connectionString = config.GetConnectionString("DbConnection");
    }
    
    private MySqlConnection CreateConnection() => new MySqlConnection(_connectionString);
    
    public async Task InsertAsync(GameReview gameReview)
    {
        const string sql = @"
            INSERT INTO game_reviews (review_id, game, year, review, sentiment, language)
            VALUES (@ReviewId, @Game, @Year, @Review, @Sentiment, @Language);
            ";
        
        await using var connection = CreateConnection();
        await connection.OpenAsync();

        await connection.ExecuteAsync(sql, gameReview);
    }

    public async Task<IEnumerable<GameReview>> GetReviewsByGameAndSentimentAsync(string game, string sentiment)
    {
        const string sql = @"
        SELECT ReviewId, Game, Year, Review, Sentiment, Language
        FROM GameReviews
        WHERE Game = @Game AND Sentiment = @Sentiment;
        ";
        
        await using var connection = CreateConnection();
        await connection.OpenAsync();
        
        return await connection.QueryAsync<GameReview>(sql, new { Game = game,  Sentiment = sentiment });
    }

    public async Task<SentimentSummary> GetSentimentSummaryAsync(string game, int? year)
    {
        var sql = @"
        SELECT 
            COUNT(*) AS TotalReviews,
            SUM(CASE WHEN Sentiment = 'positive' THEN 1 ELSE 0 END) AS Positive,
            SUM(CASE WHEN Sentiment = 'neutral' THEN 1 ELSE 0 END) AS Neutral,
            SUM(CASE WHEN Sentiment = 'negative' THEN 1 ELSE 0 END) AS Negative
        FROM GameReviews
        WHERE Game = @Game
        ";
        
        if (year.HasValue)
            sql += " AND Year = @Year";
        
        await using var connection = CreateConnection();
        
        var result = await connection.QuerySingleAsync(sql, new { Game = game, Year = year });

        return new SentimentSummary
        {
            Game = game,
            Year = year,
            TotalReviews = (int)result.TotalReviews,
            Positive = (int)result.Positive,
            Neutral = (int)result.Neutral,
            Negative = (int)result.Negative
        };
    }
}