using Microsoft.Extensions.Logging;
using Package.Shared.Entities.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Package.Shared.Services.StateServices.T_Services.T_StateCounterTestService;
using static System.Net.WebRequestMethods;


namespace Package.Shared.Services.StateServices.T_Services
{
    public class T_ServiceWithLogger: IT_ServiceWithLogger
    {
        private readonly ILogger _logger;   
      
        public T_ServiceWithLogger(ILogger<T_ServiceWithLogger> logger) 
        {
            _logger = logger;
        }
        private void IAmPrivateUseTheLoggerToUnitTestMe() 
        {
            //Do some work

            string aStringMessage = "I am a string message";
            _logger.LogInformation(aStringMessage);

            string[] Summaries = new[]
            {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
            };
            var rng = new Random();
            var shouldShowNicelyqqqq = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();

            _logger.LogInformation("More complex object should display nicely: {@result}", (object)shouldShowNicelyqqqq);
            //qqqqqqqqq

        }
    }
}
