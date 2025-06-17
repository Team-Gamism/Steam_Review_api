namespace Server.Model.Entity;

public class GameReview
{
    public int ReviewId { get; set; }
    public string Game { get; set; }
    public int Year { get; set; }
    public string Review { get; set; }
    public SentimentType Sentiment { get; set; }
    public string Language { get; set; }
}

public enum SentimentType
{
    Positive,
    Neutral,
    Negative
}