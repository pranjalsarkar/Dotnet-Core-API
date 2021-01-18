using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.WebAPI
{
    public class LoggerDatabaseProvider : ILoggerProvider
    {
        private static string _dbCon;

        public LoggerDatabaseProvider(string dbCon)
        {
            _dbCon = dbCon;
        }

        public ILogger CreateLogger(string categoryName)
        {
            return new Logger(categoryName, _dbCon);
        }

        public void Dispose()
        {
        }

        public class Logger : ILogger
        {
            private readonly string _categoryName;
            private readonly string _dbCon;

            public Logger(string categoryName, string dbConnectionString)
            {
                _categoryName = categoryName;
                _dbCon = dbConnectionString;
            }

            public bool IsEnabled(LogLevel logLevel)
            {
                return true;
            }

            public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
            {
                if (logLevel == LogLevel.Critical || logLevel == LogLevel.Error || logLevel == LogLevel.Warning)
                    RecordMsg(logLevel, eventId, state, exception, formatter);
            }

           
            private void RecordMsg<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
            {
                //_repo.Log(new Log
                //{
                //    LogLevel = logLevel.ToString(),
                //    CategoryName = _categoryName,
                //    Msg = formatter(state, exception),
                //    User = "username",
                //    Timestamp = DateTime.Now
                //});

                //sql db Entry
            }

            public IDisposable BeginScope<TState>(TState state)
            {
                return new NoopDisposable();
            }

            private class NoopDisposable : IDisposable
            {
                public void Dispose()
                {
                }
            }
        }
    }
}
