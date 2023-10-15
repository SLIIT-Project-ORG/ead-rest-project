using ead_rest_project.Models;
using ead_rest_project.services;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);


//Train
// Add services to the container.
builder.Services.Configure<TrainStoreDatabaseSettings>(
    builder.Configuration.GetSection(nameof(TrainStoreDatabaseSettings)));

builder.Services.AddSingleton<ITrainStoreDatabaseSettings>(sp =>
    sp.GetRequiredService<IOptions<TrainStoreDatabaseSettings>>().Value);

builder.Services.AddSingleton<IMongoClient>(s =>
    new MongoClient(builder.Configuration.GetValue<string>("TrainStoreDatabaseSettings:ConnectionString")));

builder.Services.AddScoped<ITrainService, TrainService>();


//User
// Add services to the container.
builder.Services.Configure<AuthStoreDatabaseSettings>(
    builder.Configuration.GetSection(nameof(AuthStoreDatabaseSettings)));

builder.Services.AddSingleton<AuthStoreDatabaseSettings>(sp =>
    sp.GetRequiredService<IOptions<AuthStoreDatabaseSettings>>().Value);

builder.Services.AddSingleton<IMongoClient>(s =>
    new MongoClient(builder.Configuration.GetValue<string>("AuthStoreDatabaseSettings:ConnectionString")));

builder.Services.AddScoped<IAuthService, AuthService>();

string[] origins = [];

origins = ["http://localhost:3000"];

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder
            .WithOrigins(origins)
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();


//sachini
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
