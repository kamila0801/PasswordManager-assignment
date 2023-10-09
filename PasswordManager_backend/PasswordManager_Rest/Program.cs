using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PasswordManager_Main.IRepository;
using PasswordManager_Main.IService;
using PasswordManager_Main.Repository;
using PasswordManager_Main.Service;
using PasswordManager_Security.IRepository;
using PasswordManager_Security.IService;
using PasswordManager_Security.Repository;
using PasswordManager_Security.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme."
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });

    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "My API",
        Version = "v1"
    });
});

// CORS config

builder.Services.AddCors(option =>
{
    option.AddPolicy("eventsTeam-development",
        builder =>
        {
            builder
                .WithOrigins("http://localhost:4200")
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

builder.Services.AddAuthentication(authentificationOptions =>
    {
        authentificationOptions.DefaultAuthenticateScheme =
            JwtBearerDefaults.AuthenticationScheme;

        authentificationOptions.DefaultChallengeScheme =
            JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey =
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(builder.Configuration["jwtConfig:secret"])),
            ValidateIssuer = true,
            ValidIssuer = builder.Configuration["jwtConfig:issuer"],
            ValidateAudience = true,
            ValidAudience = builder.Configuration["jwtConfig:audience"]
        };
    });

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAuthDbSeeder, AuthDbSeeding>();
builder.Services.AddScoped<ISecurityService, SecurityService>();
builder.Services.AddScoped<IAuthUserService, AuthUserService>();
builder.Services.AddScoped<IMainDbSeeding, MainDbSeeding>();
builder.Services.AddScoped<IUnitRepository, PasswordUnitRepository>();
builder.Services.AddScoped<IPasswordUnitService, PasswordUnitService>();


var loggerFactory = LoggerFactory.Create(builder =>
{
    builder.AddConsole();
});
// Auth Database init setup SQLite
builder.Services.AddDbContext<AuthDbContext>(
    opt =>
    {
        opt
            .UseLoggerFactory(loggerFactory)
            .UseSqlite("Data Source=PasswordManagerAuth.db");
    }, ServiceLifetime.Transient);

//Database init setup SQLite
builder.Services.AddDbContext<MainDbContext>(
    opt =>
    {
        opt
            .UseLoggerFactory(loggerFactory)
            .UseSqlite("Data Source=PasswordManagementMain.db");
    }, ServiceLifetime.Transient);

builder.Services.AddControllers();

// builder.Services.AddTransient<DbSeeding>();
builder.Services.AddTransient<AuthDbSeeding>();
builder.Services.AddTransient<MainDbSeeding>();

var app = builder.Build();

SeedAuthDevelopment(app);
SeedMainDevelopment(app);

void SeedAuthDevelopment(IHost app)
{
    var scopedFactory = app.Services.GetService<IServiceScopeFactory>();

    using (var scope = scopedFactory.CreateScope())
    {
        var service = scope.ServiceProvider.GetService<AuthDbSeeding>();
        service.SeedDevelopment();
    }
}

void SeedMainDevelopment(IHost app)
{
    var scopedFactory = app.Services.GetService<IServiceScopeFactory>();

    using (var scope = scopedFactory.CreateScope())
    {
        var service = scope.ServiceProvider.GetService<MainDbSeeding>();
        service.SeedDevelopment();
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors("eventsTeam-development");

app.MapControllers();

app.Run();