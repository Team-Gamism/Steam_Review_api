using Dapper;
using MySqlConnector;
using Server.Model;
using Server.Model.Entity;
using Server.Repository.Interface;

namespace Server.Repository;

public class GameReviewRepository : IGameReviewRepository
{
    private readonly string _connectionString;

    public GameReviewRepository(IConfiguration config)
    {
        _connectionString = config.GetConnectionString("DbConnection");
    }
    
    private MySqlConnection CreateConnection() => new MySqlConnection(_connectionString);

    public async Task InsertAsync(IEnumerable<GameReview> gameReviews)
    {
        const string sql = @"
        INSERT INTO game_review_data (review_id, game, year, review, sentiment, language)
        VALUES (@ReviewId, @Game, @Year, @Review, @Sentiment, @Language)
        ON DUPLICATE KEY UPDATE
        review = VALUES(review), sentiment = VALUES(sentiment), language = VALUES(language);";

        await using var connection = CreateConnection();
        await connection.OpenAsync();

        foreach (var review in gameReviews)
        {
            try
            {
                await connection.ExecuteAsync(sql, review);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] review_id={review.ReviewId}, game={review.Game}: {ex.Message}");
            }
        }
    }

    public async Task<GameReview?> GetByIdAsync(int reviewId)
    {
        const string sql = @"
            SELECT review_id AS ReviewId, game AS Game, year AS Year, review AS Review, sentiment AS Sentiment, language AS Language
            FROM game_review_data
            WHERE review_id = @ReviewId;";

        await using var connection = CreateConnection();
        await connection.OpenAsync();

        return await connection.QueryFirstOrDefaultAsync<GameReview>(sql, new { ReviewId = reviewId });
    }

    public async Task<IEnumerable<GameReview?>> GetReviewsByGameAsync(string steamId)
    {
        const string sql = @"
        SELECT review_id AS ReviewId, game AS Game, year AS Year, review AS Review,
               sentiment AS Sentiment, language AS Language
        FROM game_review_data
        WHERE game = @SteamId;";

        await using var connection = CreateConnection();
        await connection.OpenAsync();

        var reviews = await connection.QueryAsync<GameReview>(sql, new { SteamId = steamId });
        return reviews;
    }

    public async Task<double?> GetAverageSentimentAsync(string game)
    {
        const string sql = @"
            SELECT AVG(
                CASE 
                    WHEN Sentiment = 'positive' THEN 5
                    WHEN Sentiment = 'neutral' THEN 3
                    WHEN Sentiment = 'negative' THEN 1
                    ELSE NULL
                END
            ) as AverageSentiment
            FROM game_review_data
            WHERE game = @Game;";
        
        await using var connection = CreateConnection();
        await connection.OpenAsync();

        return await connection.ExecuteScalarAsync<double?>(sql, new { Game = game });
    }

    public async Task<IEnumerable<string>> GetDistinctGamesAsync()
    {
        const string sql = @"SELECT DISTINCT game FROM game_review_data ORDER BY game;";
        
        await using var connection = CreateConnection();
        await connection.OpenAsync();
        
        return await connection.QueryAsync<string>(sql);
    }
}