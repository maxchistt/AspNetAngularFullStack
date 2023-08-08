#undef BuildFromCode
#if BuildFromCode
using Backend.BuildCommands;
#endif

using Backend.SpaExtension;
using Backend.Weather;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.ConfigAngularStaticFiles();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapWeatherForecast("/weatherforecast")
    .WithName("GetWeatherForecast")
    .WithOpenApi();

//app.UseDefaultFiles();
//app.UseStaticFiles();

app.AddDevExceptionPage();

#if BuildFromCode
app.AngularScriptsPrepare();
#endif

app.AddAngularSpa();

app.Run();