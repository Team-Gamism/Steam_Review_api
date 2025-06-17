namespace Server.Model;

public class SentimentSummary
{
    public string Game { get; set; }
    public int? Year { get; set; }
    public int TotalReviews { get; set; }
    public int Positive { get; set; }
    public int Neutral { get; set; }
    public int Negative { get; set; }
}