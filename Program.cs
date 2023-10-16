/*
    Developed   : V.G.A.P.Kumara (IT20068578)
    Function    : Train Reservation
    Usage       : Add Services to the Container

    */

using AspNetCore.Identity.MongoDbCore.Extensions;
using AspNetCore.Identity.MongoDbCore.Infrastructure;
using ead_rest_project.Models;
using ead_rest_project.services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using System.Text;

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

//Booking
builder.Services.Configure<BookingStoreDatabaseSettings>(
    builder.Configuration.GetSection(nameof(BookingStoreDatabaseSettings)));

builder.Services.AddSingleton<IBookingStoreDatabaseSettings>(sp =>
    sp.GetRequiredService<IOptions<BookingStoreDatabaseSettings>>().Value);

builder.Services.AddSingleton<IMongoClient>(s =>
    new MongoClient(builder.Configuration.GetValue<string>("BookingStoreDatabaseSettings:ConnectionString")));

builder.Services.AddScoped<IBookingService, BookingService>();


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

//sachini
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
