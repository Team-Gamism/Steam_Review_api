namespace Server.Service.Interface;

public interface IGameReviceService
{
    Task ImportCsvToDb(string path);
}