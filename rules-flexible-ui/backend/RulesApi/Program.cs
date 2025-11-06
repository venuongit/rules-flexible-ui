using Microsoft.EntityFrameworkCore;
using RulesApi.Data;
using RulesApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<RulesDbContext>(options =>
    options.UseSqlite("Data Source=rules.db"));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

var app = builder.Build();

// ensure DB + seed
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<RulesDbContext>();
    db.Database.EnsureCreated();
    if (!db.Rules.Any())
    {
        db.Rules.AddRange(
            new Rule { Name = "High-value customer", Priority = 1, IsActive = true },
            new Rule { Name = "Overdue invoice", Priority = 2, IsActive = true },
            new Rule { Name = "Fraud check", Priority = 3, IsActive = true },
            new Rule { Name = "Low priority", Priority = 4, IsActive = true }
        );
        db.SaveChanges();
    }
}

app.UseCors("AllowAll");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
