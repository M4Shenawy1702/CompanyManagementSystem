using Application.Services;
using Domain.Entities;
using Domain.IRepositories;
using Domain.IServices;
using Domain.Mapping;
using Domain.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistance.DbContext;
using Persistance.Healper;
using Persistance.Repositories;
using Stripe;
using System.Reflection;

internal static class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add DbContext with EF Core and Lazy Loading
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseLazyLoadingProxies()
                   .UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

        // Add Identity
        builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                        .AddEntityFrameworkStores<ApplicationDbContext>();

        // Add AutoMapper
        builder.Services.AddAutoMapper(Assembly.GetAssembly(typeof(MappingProfile)));

        // Add Stripe configuration
        builder.Services.Configure<StripeSettings>(builder.Configuration.GetSection("Stripe"));
        StripeConfiguration.ApiKey = builder.Configuration["Stripe:SecretKey"];

        // Add JWT configuration
        builder.Services.Configure<JWT>(builder.Configuration.GetSection("JWT"));

        // Add Repositories & Services
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
        builder.Services.AddScoped<IAuthRepository, AuthRepository>();
        builder.Services.AddScoped<IAuthSevice, AuthService>();
        builder.Services.AddScoped<IDepartmentService, DepartmentService>();
        builder.Services.AddScoped<IHolidayService, HolidayService>();
        builder.Services.AddScoped<IProjectService, ProjectService>();
        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddScoped<IJWTTokenGenerator, JWTTokenGenerator>();
        builder.Services.AddScoped<IPayrollService, PayrollService>();

        // Add Middleware Requirements
        builder.Services.AddHttpContextAccessor();

        // Add Controllers
        builder.Services.AddControllers();

        // Swagger
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        // Add CORS (Optional if needed)
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowAll", policy =>
            {
                policy.AllowAnyHeader()
                      .AllowAnyMethod()
                      .AllowAnyOrigin();
            });
        });

        var app = builder.Build();

        // Development environment configuration
        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        // Custom Exception Middleware
        app.UseMiddleware<ExceptionHandlingMiddleware>();

        // Optional: Static files if needed
        app.UseStaticFiles();

        app.UseHttpsRedirection();

        app.UseCors("AllowAll");

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
