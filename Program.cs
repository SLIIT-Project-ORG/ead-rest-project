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

// Add services to the container.
//sachini
BsonSerializer.RegisterSerializer(new GuidSerializer(MongoDB.Bson.BsonType.String));
BsonSerializer.RegisterSerializer(new DateTimeSerializer(MongoDB.Bson.BsonType.String));
BsonSerializer.RegisterSerializer(new DateTimeOffsetSerializer(MongoDB.Bson.BsonType.String));

// MongoIdentity Configuration.
//sachini
var mongoDbIdentityConfig = new MongoDbIdentityConfiguration
{
    MongoDbSettings = new MongoDbSettings
    {
        ConnectionString = "mongodb+srv://admin:admin@ead-db.fn9jqdj.mongodb.net/?retryWrites=true&w=majority",

		DatabaseName = "ead-db"
	},
    IdentityOptionsAction = Options =>
    {
        Options.Password.RequireDigit = false;
        Options.Password.RequiredLength = 8;
        Options.Password.RequireNonAlphanumeric = true;
        Options.Password.RequireLowercase = false;

        //lockout
        Options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
        Options.Lockout.MaxFailedAccessAttempts = 5;
        Options.User.RequireUniqueEmail = true;
    }
};

builder.Services.ConfigureMongoDbIdentity<ApplicationUser, ApplicationRole, Guid>(mongoDbIdentityConfig)

    .AddUserManager<UserManager<ApplicationUser>>()
    .AddSignInManager<SignInManager<ApplicationUser>>()
    .AddRoleManager<RoleManager<ApplicationRole>>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

}).AddJwtBearer(x =>{

    x.RequireHttpsMetadata = true;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidIssuer = "https://localhost:5001",
        ValidAudience = "https://localhost:5001",
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("1swek3u4uo2u4a6e")),
        ClockSkew = TimeSpan.Zero,



    };
});






// Add services to the container.
builder.Services.Configure<TrainStoreDatabaseSettings>(
    builder.Configuration.GetSection(nameof(TrainStoreDatabaseSettings)));

builder.Services.AddSingleton<ITrainStoreDatabaseSettings>(sp =>
    sp.GetRequiredService<IOptions<TrainStoreDatabaseSettings>>().Value);

builder.Services.AddSingleton<IMongoClient>(s =>
    new MongoClient(builder.Configuration.GetValue<string>("TrainStoreDatabaseSettings:ConnectionString")));

builder.Services.AddScoped<ITrainService, TrainService>();

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
