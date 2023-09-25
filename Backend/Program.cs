using Backend.Auth;
using Backend.TestEnpoints;
using Backend.Weather;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt => opt.AddAuth());
builder.Services.AddAuthServices(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(builder => builder.AllowAnyOrigin());
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapAuthEndpoints();

app.MapWeatherForecast();
app.MapTestEndpoints();

app.UseDefaultFiles();
app.UseStaticFiles();

app.Run();