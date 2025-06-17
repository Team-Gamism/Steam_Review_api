namespace Server.Model;

public class GameReview
{
    public int ReviewId { get; set; }
    public string Game { get; set; }
    public int Year { get; set; }
    public string Review { get; set; }
    public string Sentiment { get; set; }
    public string Language { get; set; }
}