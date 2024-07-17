using BlogLiteAPI.DataAccess;
using BlogLiteAPI.Infrastructure.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.ConfigureServices();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();

app.MapEndpoints();

app.Run();