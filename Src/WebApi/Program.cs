using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using DotNetEnv;
using Serilog;
using WebApi.Database;
using WebApi.Repositories.Interfaces;
using WebApi.Repositories;

// ========================================
// LOAD .ENV (que está na raiz do projeto)
// ========================================

// Caminho absoluto até a pasta raiz
var projectRoot = Directory.GetParent(Directory.GetCurrentDirectory())!.Parent!.FullName;
var envFile = Path.Combine(projectRoot, ".env");

Console.WriteLine($"[ENV] Carregando arquivo: {envFile}");

// Carrega o arquivo .env
Env.Load(envFile);

var builder = WebApplication.CreateBuilder(args);

// ========================================
// LOGGING (Serilog)
// ========================================
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .WriteTo.Console()
    .CreateLogger();

builder.Host.UseSerilog();

// ========================================
// ORACLE CONNECTION
// ========================================

var oracleConnectionString = Environment.GetEnvironmentVariable("ORACLE_DB");

if (string.IsNullOrEmpty(oracleConnectionString))
{
    throw new Exception("❌ ERRO: A variável de ambiente ORACLE_DB não foi carregada do arquivo .env. Verifique o .env na raiz do projeto.");
}

// DbContext com Oracle
builder.Services.AddDbContext<SkillBridgeContext>(options =>
    options.UseOracle(oracleConnectionString));


// ========================================
// DEPENDENCY INJECTION
// ========================================
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ISkillRepository, SkillRepository>();
builder.Services.AddScoped<ICourseRepository, CourseRepository>();
builder.Services.AddScoped<IWorkRepository, WorkRepository>();
builder.Services.AddScoped<ILearningPathRepository, LearningPathRepository>();
builder.Services.AddScoped<IRecommendationRepository, RecommendationRepository>();

// ========================================
// API VERSIONING
// ========================================
builder.Services.AddApiVersioning(options =>
{
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.ReportApiVersions = true;
});

// ========================================
// SWAGGER
// ========================================
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ========================================
// HEALTHCHECKS ORACLE
// ========================================
builder.Services.AddHealthChecks()
    .AddOracle(
        oracleConnectionString,
        name: "oracle",
        failureStatus: HealthStatus.Unhealthy
    );

var app = builder.Build();

app.UseSerilogRequestLogging();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.UseAuthorization();

app.MapControllers();
app.MapHealthChecks("/healthz");

app.Run();

public partial class Program { }