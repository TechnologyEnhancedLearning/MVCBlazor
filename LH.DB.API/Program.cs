using LH.DB.API.Data;
using LH.DB.API.DependencyInjection;
using Serilog;
using Microsoft.Extensions.Logging;
using Serilog.Events;

try { 
    var builder = WebApplication.CreateBuilder(args);

    builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
    builder.Logging.ClearProviders();
    Log.Logger = new LoggerConfiguration()
        .ReadFrom.Configuration(builder.Configuration)
        .CreateLogger();

    // Add Serilog to logging providers
    builder.Logging.AddSerilog(Log.Logger, dispose: true);
    builder.Host.UseSerilog();

    // Add services to the container.

    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    var allowedOrigins = builder.Configuration.GetSection("AllowedOrigins").Get<string[]>();


    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowLHMVCBlazorServer",
          builder => builder
              .WithOrigins(allowedOrigins) //Server SSL //"https://localhost:44343"
              .AllowAnyHeader()
          .AllowAnyMethod()
          .AllowCredentials());
    });

    builder.Services.AddSingleton<ISimulatedDatabase, SimulatedDatabase>();

    //DI Collections
    builder.Services.LHAPI_AddDbServices();


    var app = builder.Build();

    app.UseSerilogRequestLogging();

    Log.Information("API program.cs");

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();
    app.UseCors("AllowLHMVCBlazorServer"); // Apply CORS policy
    app.UseAuthorization();

    app.MapControllers();

    app.Run();

}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush(); // Ensure logs are flushed before exit
}
