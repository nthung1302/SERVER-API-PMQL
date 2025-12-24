using Files.Data;
using Files.Helpers;
using Files.Models.Interfaces;
using Files.Repositories;
using Files.Services;

var builder = WebApplication.CreateBuilder(args);

// Init DB
dbConnect.Initialize(builder.Configuration);

// DI
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<LoginService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();
app.MapControllers();

app.Run();
