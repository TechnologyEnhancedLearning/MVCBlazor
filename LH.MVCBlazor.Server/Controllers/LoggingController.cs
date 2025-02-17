using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog.Core;
using Serilog.Events;
using Serilog;
namespace LH.MVCBlazor.Server.Controllers
{
    [Route("api/logging")]
    [ApiController]
    public class LoggingController : ControllerBase
    {
        private readonly LoggingLevelSwitch _levelSwitch;
        private readonly ILogger<LoggingController> _logger;

        public LoggingController(LoggingLevelSwitch levelSwitch, ILogger<LoggingController> logger)
        {
            _levelSwitch = levelSwitch;
            _logger = logger;
        }

        // Get current log level
        [HttpGet]
        public IActionResult GetLogLevel()
        {
            _logger.LogInformation("Fetching current log level: {Level}", _levelSwitch.MinimumLevel.ToString());
            return Ok(new { CurrentLogLevel = _levelSwitch.MinimumLevel.ToString() });
        }

        // Change log level dynamically
        [HttpPost("set-log-level")]
        public IActionResult SetLogLevel([FromBody] string level)
        {
            LogAllLevels("Before Change");
            if (string.IsNullOrWhiteSpace(level))
            {
                _logger.LogWarning("Attempted to set log level with an empty value.");
                return BadRequest(new { Message = "Log level is required." });
            }

            if (!Enum.TryParse(level, true, out LogEventLevel logLevel))
            {
                _logger.LogWarning("Invalid log level received: {Level}", level);
                return BadRequest(new { Message = "Invalid log level." });
            }

            _logger.LogInformation("Changing log level from {OldLevel} to {NewLevel}",
                                    _levelSwitch.MinimumLevel.ToString(), logLevel.ToString());

            _levelSwitch.MinimumLevel = logLevel;

            LogAllLevels("After Change");
            return Ok(new { Message = $"Log level changed to {logLevel.ToString()}" });
        }

        // Logs a message at all log levels to test visibility
        private void LogAllLevels(string phase)
        {
            _logger.LogTrace("[{Phase}] TRACE level log", phase);
            _logger.LogDebug("[{Phase}] DEBUG level log", phase);
            _logger.LogInformation("[{Phase}] INFORMATION level log", phase);
            _logger.LogWarning("[{Phase}] WARNING level log", phase);
            _logger.LogError("[{Phase}] ERROR level log", phase);
            _logger.LogCritical("[{Phase}] CRITICAL level log", phase);
        }

    }
}
