
using Microsoft.EntityFrameworkCore;
using Infrastructure.Persistence;
using Infrastructure.ExternalServices;
using Application.Interfaces;
using Infrastructure.Services;
using API.Middleware;

namespace API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services

            // (SQLite)
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

            //HttpClient 
            builder.Services.AddHttpClient<BlockCypherClient>();

            //Interface
            builder.Services.AddScoped<IBlockchainService, BlockchainService>();

            //Health Checks
            builder.Services.AddHealthChecks()
                .AddDbContextCheck<ApplicationDbContext>();

            // 3. Register CORS
            builder.Services.AddCors(options => {
                options.AddPolicy("AllowAll", policy =>
                    policy.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader());
            });

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            app.UseMiddleware<ExceptionMiddleware>();

            //HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.MapHealthChecks("/health");

           

            app.UseHttpsRedirection();
            app.UseCors("AllowAll");
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}
