using AutoFixture;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Test.BUnit.UnitTests.TestDoubles;
using Package.Shared.Entities.Communication;
using Package.Shared.Entities.Models;
using Package.Shared.Services.ComponentServices;
using Package.Shared.Services.StateServices.CharacterStateServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.IO;
using Microsoft.Extensions.Logging;
using Serilog;
//using Serilog.Sinks.XUnit; //This defines testoutput
using Microsoft.Extensions.Logging.Abstractions;
using Serilog.Events;
using Serilog.Templates;
using Xunit.Abstractions;
using Serilog.Sinks.InMemory;
using Serilog.Formatting.Json;
using Serilog.Formatting.Compact;
using Serilog.Core;

namespace Test.BUnit.UnitTests.DependencyInjection
{

    public static class DependencyInjection
    {
        public static IServiceCollection AddTestDouble_GS_StateServices(this IServiceCollection services)
        {
            /*Mocks, Stubs, Fakes here*/

            FakeGS_CharactersStateService FakeGS_CharactersStateService = new FakeGS_CharactersStateService();
            services.AddScoped<IGS_CharactersStateService, FakeGS_CharactersStateService>(serviceProvider => FakeGS_CharactersStateService);

            return services;
        }


        /// <summary>
        /// AddLogging XUNIT this is Test Explorer logging, not for Asserts
        /// InMemory for Asserting
        /// </summary>
        /// <param name="services"></param>
        /// <param name="outputHelper"></param>
        /// <returns></returns>
        public static IServiceCollection AddLogging(this IServiceCollection services, ITestOutputHelper outputHelper,InMemorySink  inMemorySink)
        {
            // Create configuration from appsettings.json
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

          
            Log.Logger = new LoggerConfiguration()
                .WriteTo.TestOutput(outputHelper)
                .WriteTo.Sink(inMemorySink)
                //.WriteTo.TestOutput(testOutputHelper: outputHelper, //WORKS if we want to specify
                //    formatter: new ExpressionTemplate("[{UtcDateTime(@t):mm:ss.ffffff} | {@l:u3} | {Substring(SourceContext, LastIndexOf(SourceContext, '.') + 1)} | {Coalesce(EventId.Name, '<none>')}] {@m}\n{@x}"),
                //    restrictedToMinimumLevel: LogEventLevel.Verbose)
                 //.WriteTo.Sink<InMemorySink>(inMemorySink, // DOESNT WORK
                 //   new ExpressionTemplate("[{UtcDateTime(@t):mm:ss.ffffff} | {@l:u3} | {Substring(SourceContext, LastIndexOf(SourceContext, '.') + 1)} | {Coalesce(EventId.Name, '<none>')}] {@m}\n{@x}"),
                 //   LogEventLevel.Verbose)
                .ReadFrom.Configuration(configuration) //qqqq ths is doing nothing
                .CreateLogger();


           
            services.AddSingleton<ILoggerFactory>(_ => new LoggerFactory().AddSerilog(Log.Logger, dispose: true));
            services.AddSingleton(typeof(ILogger<>), typeof(Logger<>));
            return services;
        }

    }  
}

