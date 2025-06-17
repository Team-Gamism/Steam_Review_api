using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

namespace Server.Service;

public class LowerCaseEnumConverter<T> : DefaultTypeConverter where T : struct, Enum
{
    public override string ConvertToString(object? value, IWriterRow row, MemberMapData memberMapData)
    {
        return value?.ToString()?.ToLowerInvariant() ?? string.Empty;
    }

    public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
    {
        if (Enum.TryParse<T>(text, true, out var result)) // ignore case
        {
            return result;
        }
        throw new TypeConverterException(this, memberMapData, text, row.Context, $"Cannot convert '{text}' to {typeof(T).Name}");
    }
}