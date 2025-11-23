using DotNetEnv;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Serilog;
using WebApi.Database;
using WebApi.Repositories;
using WebApi.Repositories.Interfaces;

// ==================================================
// CRIA O BUILDER
// ==================================================
var builder = WebApplication.CreateBuilder(args);

// ==================================================
// CARREGA .ENV APENAS EM DESENVOLVIMENTO
// ==================================================
if (builder.Environment.IsDevelopment())
{
    Console.WriteLine("[DEV] Carregando .env da raiz...");

    var root = Directory
        .GetParent(AppContext.BaseDirectory)!
        .Parent!.Parent!.Parent!.Parent!.Parent!.FullName;
    var envPath = Path.Combine(root, ".env");

    Console.WriteLine($"[DEV] Caminho .env: {envPath}");

    if (File.Exists(envPath))
        Env.Load(envPath);
    else
        Console.WriteLine("[DEV] ⚠️ Arquivo .env não encontrado!");
}

// ==================================================
// GARANTE LEITURA DAS VARIÁVEIS DO RENDER
// ==================================================
builder.Configuration.AddEnvironmentVariables();

// ==================================================
// LOGGING (SERILOG)
// ==================================================
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .WriteTo.Console()
    .CreateLogger();

builder.Host.UseSerilog();

// ==================================================
// CARREGA STRING DE CONEXÃO ORACLE
// ==================================================
var rawOracle = Environment.GetEnvironmentVariable("ORACLE_DB");

// Se vier null → falha imediatamente
if (string.IsNullOrWhiteSpace(rawOracle))
{
    Console.WriteLine("❌ ERRO FATAL: Variável ORACLE_DB não encontrada!");
    throw new Exception("Variável de ambiente ORACLE_DB não definida.");
}

// Ajusta possíveis '\n' enviados literalmente pelo Render
var oracleConnectionString = rawOracle.Replace("\\n", "\n");

Console.WriteLine("✅ ORACLE_DB carregada com sucesso!");

// ==================================================
// DB CONTEXT (WITH ORACLE)
// ==================================================
builder.Services.AddDbContext<SkillBridgeContext>(options =>
    options.UseOracle(oracleConnectionString)
);

// ==================================================
// DEPENDENCY INJECTION
// ==================================================
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ISkillRepository, SkillRepository>();
builder.Services.AddScoped<ICourseRepository, CourseRepository>();
builder.Services.AddScoped<IWorkRepository, WorkRepository>();
builder.Services.AddScoped<ILearningPathRepository, LearningPathRepository>();
builder.Services.AddScoped<IRecommendationRepository, RecommendationRepository>();

// ==================================================
// VERSIONAMENTO
// ==================================================
builder.Services.AddApiVersioning(options =>
{
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.ReportApiVersions = true;
});

// ==================================================
// SWAGGER (AGORA FUNCIONA EM PRODUÇÃO)
// ==================================================
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

// ==================================================
// HEALTHCHECK (NÃO QUEBRA A API SE O ORACLE CAIR)
// ==================================================
builder
    .Services.AddHealthChecks()
    .AddOracle(
        oracleConnectionString,
        name: "oracle",
        failureStatus: HealthStatus.Degraded // evita travar swagger
    );

// ==================================================
// APP
// ==================================================
var app = builder.Build();

app.UseSerilogRequestLogging();

app.UseSwagger();
app.UseSwaggerUI();

app.UseRouting();
app.UseAuthorization();

app.MapControllers();
app.MapHealthChecks("/healthz");

app.Run();

public partial class Program { }
