﻿using Backend.DTOs.Other;

namespace Backend.Endpoints.Weather
{
    public static class WeatherForecastApi
    {
        private static string[] summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        public static RouteHandlerBuilder MapWeatherForecast(this WebApplication app, string route = "/api/weatherforecast")
        {
            return app.MapGet(route, () =>
            {
                var forecast = Enumerable.Range(1, 5).Select(index =>
                    new WeatherForecastDTO
                    (
                        DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                        Random.Shared.Next(-20, 55),
                        summaries[Random.Shared.Next(summaries.Length)]
                    ))
                    .ToArray();
                return forecast;
            })
                .WithName("GetWeatherForecast")
                .WithTags("weather")
                .WithOpenApi();
        }
    }
}