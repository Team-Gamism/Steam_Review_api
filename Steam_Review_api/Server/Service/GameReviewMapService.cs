using CsvHelper.Configuration;
using Server.Model;

namespace Server.Service;

public sealed class GameReviewMapService : ClassMap<GameReview>
{
    public GameReviewMapService()
    {
        Map(m => m.ReviewId).Name("review_id");
        Map(m => m.Game).Name("game");
        Map(m => m.Year).Name("year");
        Map(m => m.Review).Name("review");
        Map(m => m.Language).Name("language");
        Map(m => m.Sentiment).Name("sentiment");
    }
}