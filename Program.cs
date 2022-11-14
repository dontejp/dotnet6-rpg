global using dotnet_rpg.Models;
using dotnet_rpg.Services.CharacterService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(Program).Assembly);                          //allows us to use automapper after downloading the Nuget "dotnet add package AutoMapper.Extensions.Microsoft.DependencyInjection"
builder.Services.AddScoped<ICharacterService, CharacterService>();              //lets the webapi know it has to use the CharacterService class whenever it wants to use the ICharacterService

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
