using Microsoft.Extensions.Logging.Configuration;
using TestApi.CustomLogger;
using TestApi.CustomService;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders();
builder.Logging.Services.AddScoped<ICustomService, CustomService>();
builder.Logging.AddProvider(new CustomLoggerProvider(builder.Logging.Services.BuildServiceProvider().GetRequiredService<ICustomService>()));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ICustomService, CustomService>();

//builder.Logging.AddProvider(new CustomLoggerProvider(builder.Services.BuildServiceProvider().GetRequiredService<ICustomService>()));
/*builder.Logging.Services.AddScoped<ICustomService, CustomService>();
var test = builder.Logging.Services.BuildServiceProvider().GetRequiredService<ICustomService>();
var test2 = builder.Services.BuildServiceProvider().GetRequiredService<ICustomService>();*/

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
