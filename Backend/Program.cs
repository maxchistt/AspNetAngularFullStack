using Backend.Endpoints.Auth;
using Backend.Endpoints.TestEnpoints;
using Backend.Endpoints.Weather;
using Backend.ServiceConfig;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGenWithJWTAuth();
builder.Services.AddUserServices();
builder.Services.AddAuthServices(builder.Configuration);
builder.Services.AddSqlServerDbContext(builder.Configuration);

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