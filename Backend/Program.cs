using Backend.Auth;
using Backend.TestEnpoints;
using Backend.Weather;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGenWithAuth();
builder.Services.AddAuth(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwaggerAndUI();
}

app.UseCors(builder => builder.AllowAnyOrigin());
app.UseRouting();

app.UseAuthAndMapEndpoints();

app.MapWeatherForecast();
app.MapTestEndpoints();

app.UseDefaultFiles();
app.UseStaticFiles();

app.Run();