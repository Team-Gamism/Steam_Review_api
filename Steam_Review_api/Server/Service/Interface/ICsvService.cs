using Server.Model;
using Server.Model.Entity;

namespace Server.Service.Interface;

public interface ICsvService
{
    IEnumerable<GameReview> ReadAll(string path);
}