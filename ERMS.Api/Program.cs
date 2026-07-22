using ERMS.Infrastructure;
using ERMS.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// DbContext Yapılandırması
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Altyapı ve Servis Kayıtları
builder.Services.AddInfrastructure();
builder.Services.AddControllers();

// Swagger / OpenAPI Yapılandırması
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    // Çakışan şema/model isimleri hatasını engeller (Failed to load API definition çözümüdür)
    c.CustomSchemaIds(type => type.FullName);

    // Swagger UI üzerinden JWT Token test edebilmek için "Authorize" butonu
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Token değerinizi girin. Örnek: Bearer {token}"
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
            Array.Empty<string>()
        }
    });
});

// JWT Authentication Yapılandırması
var jwtSettings = builder.Configuration.GetSection("Jwt");
var secretKey = Encoding.UTF8.GetBytes(jwtSettings["Key"]!);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(secretKey),
        ValidateIssuer = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidateAudience = true,
        ValidAudience = jwtSettings["Audience"],
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
    };
});

// Scoped Servis Kayıtları
builder.Services.AddScoped<ERMS.Application.Common.Interfaces.IApplicationDbContext>(provider =>
    provider.GetRequiredService<ERMS.Infrastructure.Persistence.AppDbContext>());

builder.Services.AddScoped<ERMS.Application.Services.AuthService>();
builder.Services.AddScoped<ERMS.Application.Services.AuditLogService>();
builder.Services.AddScoped<ERMS.Application.Services.RequestService>();
builder.Services.AddScoped<ERMS.Application.Services.ApprovalService>();
builder.Services.AddScoped<ERMS.Application.Services.DepartmentService>();

var app = builder.Build();

// Development Ortamı Middleware Ayarları
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "ERMS API v1");
    });
}

app.UseHttpsRedirection();

// Kimlik Doğrulama ve Yetkilendirme
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// Otomatik Migration Uygulama Bloğu
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<ERMS.Infrastructure.Persistence.AppDbContext>();
        context.Database.Migrate();
    }
    catch (Exception ex)
    {
        Console.WriteLine($"DB Kurulurken hata oluştu : {ex.Message}");
    }
}

app.Run();