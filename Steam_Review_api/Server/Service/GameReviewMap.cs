using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using Server.Model.Entity;

namespace Server.Service;

public sealed class GameReviewMap : ClassMap<GameReview>
{
    public GameReviewMap()
    {
        Map(m => m.ReviewId).Name("review_id");
        Map(m => m.Game).Name("game");
        Map(m => m.Year).Name("year");
        Map(m => m.Review).Name("review");
        Map(m => m.Language).Name("language");
        Map(m => m.Sentiment).Name("sentiment").TypeConverter<SentimentTypeConverter>();
    }
}