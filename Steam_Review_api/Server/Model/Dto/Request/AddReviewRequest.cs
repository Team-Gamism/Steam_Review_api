using Server.Model.Entity;

namespace Server.Model.Dto.Request;

public class AddReviewRequest
{
    public int ReviewId { get; set; }
    public string Game { get; set; } = null!;
    public int Year { get; set; }
    public string Review { get; set; } = null!;
    public string Sentiment { get; set; }
    public string Language { get; set; } = null!;
}