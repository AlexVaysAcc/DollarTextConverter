
using Converter.Core;
using Serilog;

namespace Converter.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.File(Path.Combine(AppContext.BaseDirectory, "logs", "server_log-.txt"), rollingInterval: RollingInterval.Day)
                .CreateLogger();

            builder.Host.UseSerilog();

            builder.Services.AddControllers();

            // Add services to the container.
            builder.Services.AddAuthorization();
            builder.Services.AddTransient<EnglishConverter>();
            builder.Services.AddTransient<DeutschConverter>();
            builder.Services.AddSingleton<ConverterFactory>();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

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
