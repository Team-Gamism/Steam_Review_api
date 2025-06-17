using System.Globalization;
using CsvHelper;
using Server.Model;
using Server.Service.Interface;

namespace Server.Service;

public class CsvService :  ICsvService
{
    public IEnumerable<GameReview> ReadAll(string path)
    {
        using var reader = new StreamReader(path);
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
        
        var records = csv.GetRecords<GameReview>().ToList();
        
        return records;
    }
}