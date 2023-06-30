using ProbabilityCalculatorApi.Calculators;
using ProbabilityCalculatorApi.Data;
using ProbabilityCalculatorApi.Options;
using ProbabilityCalculatorApi.Repository;
using ProbabilityCalculatorApi.Service;
using ProbabilityCalculatorApi.Validation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder =>
        {
            builder
            .AllowAnyHeader()
            .AllowAnyMethod()
            .WithOrigins("http://localhost:4200");
        });
});
    

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IProbabilityCalculatorService, ProbabilityCalculatorService>();
builder.Services.AddScoped<IProbabilityCalculatorFactory, ProbabilityCalculatorFactory>();
builder.Services.AddScoped<IProbabilityCalculatorRepository, ProbabilityCalculatorRepository>();
builder.Services.AddScoped<IProbabilityCalculationDataManager, ProbabilityCalculationDataManager>();
builder.Services.AddScoped<IProbabilityCalculationValidator, ProbabilityCalculationValidator>();
builder.Services.Configure<JsonDataOptions>(
    builder.Configuration.GetSection("JsonDataOptions"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("CorsPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();
