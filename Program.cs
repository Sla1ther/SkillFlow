using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using SkillFlow.Data;
using SkillFlow.Models;
using SkillFlow.Services;
using SkillFlow.Services.Interfaces;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllersWithViews();

        builder.Services.AddScoped<ISkillService, SkillService>();

        builder.Services.AddScoped<IProgressService, ProgressService>();
        builder.Services.AddScoped<IAuthService, AuthService>();
        builder.Services.AddHttpContextAccessor();

        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(connectionString));

        builder.Services
            .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.Cookie.Name = "SkillFlow.Auth";
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromHours(8);
                options.LoginPath = "/Account/Login";
                options.AccessDeniedPath = "/Account/AccessDenied";
                options.SlidingExpiration = true;
            });

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        SeedDefaultAdminAsync(app).GetAwaiter().GetResult();

        app.MapStaticAssets();
        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Skills}/{action=Index}/{id?}")
            .WithStaticAssets();

        app.Run();
    }

    private static async Task SeedDefaultAdminAsync(WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        var configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();

        var adminEmail = configuration["SeedAdmin:Email"] ?? "admin@skillflow.local";
        var adminPassword = configuration["SeedAdmin:Password"] ?? "Admin123!";

        if (await db.Users.AnyAsync(user => user.Email == adminEmail))
        {
            return;
        }

        var password = AuthService.HashPassword(adminPassword);
        db.Users.Add(new User
        {
            Email = adminEmail,
            PasswordHash = password.PasswordHash,
            Salt = password.Salt,
            Role = "Admin"
        });

        await db.SaveChangesAsync();
    }

}

