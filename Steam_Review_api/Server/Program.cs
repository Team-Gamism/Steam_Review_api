using Server.Repository;
using Server.Repository.Interface;
using Server.Service;
using Server.Service.Interface;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IGameReviewRepository, GameReviewRepository>();
builder.Services.AddScoped<ICsvService, CsvService>();
builder.Services.AddScoped<IGameReviceService, GameRevieweService>();

builder.Services.AddControllers();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();