using CsvHelper.TypeConversion;
using CsvHelper;
using CsvHelper.Configuration;
using Server.Model.Entity;

namespace Server.Service;

public class SentimentTypeConverter : EnumConverter
{
    public SentimentTypeConverter() : base(typeof(SentimentType))
    {
    }

    public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
    {
        if (Enum.TryParse<SentimentType>(text, true, out var result))
            return result;

        throw new ArgumentException($"'{text}' is not valid for SentimentType");
    }
}