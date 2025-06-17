using System.Text.Json.Serialization;
using Server.Repository;
using Server.Repository.Interface;
using Server.Service;
using Server.Service.Interface;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IGameReviewRepository, GameReviewRepository>();
builder.Services.AddScoped<ICsvService, CsvService>();
builder.Services.AddScoped<IGameReviewService, GameReviewService>();

builder.Services
    .AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

var app = builder.Build();

app.UseHttpsRedirection();
app.UseRouting();
app.MapControllers();

app.Run();