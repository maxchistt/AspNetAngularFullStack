using Backend.AngularSpa;
using Backend.Shared;
using Backend.Weather;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.ConfigAngularStaticFiles();

var app = builder.Build();

if (app.Environment.IsDevelopment() || EnvConfig.IsDebug)
    app.UseDeveloperExceptionPage();

app.UseCors(builder => builder.SetIsOriginAllowed(_ => true).AllowCredentials().AllowAnyHeader().AllowAnyMethod());
app.UseRouting();

if (app.Environment.IsDevelopment() || EnvConfig.IsDebug)
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapWeatherForecast("/api/weatherforecast")
    .WithName("GetWeatherForecast")
    .WithOpenApi();

app.MapSpaInfo("/api/SpaDir")
    .WithName("GetSpaDir")
    .WithOpenApi();

app.UseStaticFiles();
app.AddAngularSpaStatic(/*!EnvConfig.IsDebug*/);
app.AddAngularSpa(/*false*/);

app.Run();