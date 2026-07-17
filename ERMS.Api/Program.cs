using ERMS.Infrastructure;
using ERMS.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add services to the container.
builder.Services.AddInfrastructure();
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ERMS.Application.Common.Interfaces.IApplicationDbContext>(provider =>
    provider.GetRequiredService<ERMS.Infrastructure.Persistence.AppDbContext>());
builder.Services.AddScoped<ERMS.Application.Services.AuthService>();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<ERMS.Infrastructure.Persistence.AppDbContext>();
        // Veritabanı yoksa sıfırdan oluşturur, migration'ları otomatik uygular bra!
        context.Database.Migrate();
    }
    catch (Exception ex)
    {
        Console.WriteLine($"DB Kurulurken hata oluştu : {ex.Message}");
    }
}

app.Run();
