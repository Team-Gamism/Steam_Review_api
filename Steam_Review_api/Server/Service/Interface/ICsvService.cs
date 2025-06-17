using Server.Model;

namespace Server.Service.Interface;

public interface ICsvService
{
    IEnumerable<GameReview> ReadAll(string path);
}