namespace Server.Model.Entity;

public class GameReviewStatistics
{
    public string Game { get; set; }
    public int TotalReviews { get; set; }
    public int PositiveCount { get; set; }
    public int NeutralCount { get; set; }
    public int NegativeCount { get; set; }
    public double PositiveRatio { get; set; }
    public double NeutralRatio { get; set; }
    public double NegativeRatio { get; set; }
    public double AverageSentimentScore { get; set; }
}