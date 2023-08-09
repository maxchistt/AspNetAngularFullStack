using Backend.SpaConfig;
using Backend.SpaExtension;
using Backend.Weather;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddCors();
builder.Services.ConfigAngularStaticFiles();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseRouting();
app.UseCors(builder => builder.AllowAnyOrigin());

app.MapWeatherForecast("api/weatherforecast")
    .WithName("GetWeatherForecast")
    .WithOpenApi();

app.MapGet("api/SpaDir", () =>
{
    return JsonConvert.SerializeObject(AngularConfig.SpaStaticRoot);
}).WithName("GetSpa")
    .WithOpenApi();

if (app.Environment.IsDevelopment()
#if DEBUG
    || true
#endif
)
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

app.AddAngularSpaStatic(
#if !DEBUG
    true
#endif
);
app.AddAngularSpa(false);

app.Run();