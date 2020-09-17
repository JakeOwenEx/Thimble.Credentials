using System;
using Serilog;
using Serilog.Formatting.Compact;
using Thimble.Credentials.logging;

namespace Thimble.Credentials.logging
{
    public class ThimbleLogger : IThimbleLogger
    {
        private string _traceId;
        private string _userId;
        private readonly ILogger _log;

        public ThimbleLogger()
        {
            _log = new LoggerConfiguration()
                .WriteTo.Console(new CompactJsonFormatter())
                .CreateLogger();
        }

        public void Log(string userId, string action)
        {
            _userId = userId;
            _log.Information("{service} - {userId} - {action} - {traceId}", 
                "Credentials", 
                _userId, 
                action,
                _traceId
            );
        }
        
        public void Log(Exception exception, string action)
        {
            _log.Information("{service} - {userId} - {action} - {traceId} - {errorType} - {errorMessage}",
                "Credentials", 
                _userId, 
                action,
                _traceId,
                exception.GetType().ToString(),
                exception.Message
            );
        }
        
        public void Log(string action)
        {
            _log.Information("{service} - {userId} - {action} - {traceId}",
                "Credentials", 
                _userId, 
                action,
                _traceId
            );
        }

        public void SetTraceId(string traceId)
        {
            _traceId = traceId;
        }
    }
}