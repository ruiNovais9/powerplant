using PowerPlants.Interfaces;
using PowerPlants;
using Microsoft.Extensions.Logging.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;

namespace PowerPlantAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddScoped<IPowerPlantsService, PowerPlantsService>();

            IConfigurationSection pathOfLog = builder.Configuration.GetSection("Logging:RoundTheCodeFile:Options:FilePath");

            if (pathOfLog != null && !string.IsNullOrWhiteSpace(pathOfLog.Value))
            {
                Log.Logger = new LoggerConfiguration()
                 .MinimumLevel.Information()
                 .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
                 .WriteTo.File(pathOfLog.Value, rollingInterval: RollingInterval.Day)
                 .CreateLogger();

                builder.Services.AddLogging(loggingBuilder =>
                {
                    loggingBuilder.AddSerilog();
                });
            }

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}