using JobAgent.API.Data;
using JobAgent.API.Repositories.Implementations;
using JobAgent.API.Repositories.Interfaces;
using JobAgent.API.Services.Implementations;
using JobAgent.API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp",
        policy =>
        {
            policy.AllowAnyOrigin() // Your Angular URL
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});
// Add services to the container.

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddScoped<IResumeRepository, ResumeRepository>();
builder.Services.AddScoped<IResumeService, ResumeService>();
builder.Services.AddHttpClient<IAiService, AiService>();
builder.Services.AddScoped<IJobRepository, JobRepository>();
builder.Services.AddScoped<IJobService, JobService>();
builder.Services.AddScoped<IEmbeddingService, PythonEmbeddingService>();
builder.Services.AddScoped<IJobMatchingService,JobMatchingService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors(policy => policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
    app.MapOpenApi();
}
        
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
