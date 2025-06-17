using System.Globalization;
using CsvHelper;
using Server.Model;
using Server.Model.Entity;
using Server.Service.Interface;

namespace Server.Service;

public class CsvService :  ICsvService
{
    public IEnumerable<GameReview> ReadAll(string path)
    {
        using var reader = new StreamReader(path);
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
        
        csv.Context.RegisterClassMap<GameReviewMap>();
        var records = csv.GetRecords<GameReview>().ToList();
        
        Console.WriteLine($"총 {records.Count}개의 리뷰를 읽었습니다.");
        
        return records;
    }
}