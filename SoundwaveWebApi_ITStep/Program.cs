
using Core.Interfaces;
using Core.MapperProfiles;
using Core.Services;
using Data.Data;
using Microsoft.EntityFrameworkCore;

namespace SoundwaveWebApi_ITStep
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            string? connectionString = builder.Configuration.GetConnectionString("LocalDb");

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<SoundwaveDbContext>(options => 
                options.UseSqlServer(connectionString)
            );

            builder.Services.AddAutoMapper(typeof(AppProfile));

            builder.Services.AddScoped<IMusicService, MusicService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
