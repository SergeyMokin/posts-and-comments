using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace Company.PostsAndCommentsServices.Logger
{
    public class Logger : ILogger
    {
        private static readonly List<string> NotWrittenExceptions
            = new List<string>();

        private static readonly object Lock = new object();

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception ex, Func<TState, Exception, string> formatter)
        {
            if(logLevel < LogLevel.Error) return;
            
            if (ex == null) throw new ArgumentNullException();

            var date = DateTime.Now.ToString("dd/MM/yy HH:mm:ss");
            var stackTrace = ex.StackTrace;
            var res = $"---!--- {logLevel} -|- {eventId} -|- {date} -|- {formatter(state, ex)} -|- {ex.Message}";

            while (ex.InnerException != null)
            {
                ex = ex.InnerException;
                res += ex.Message;
            }

            res += Environment.NewLine + stackTrace;

            var path = Path.Combine(
                Directory.GetCurrentDirectory(),
                "wwwroot",
                DateTime.Now.ToString("dd_MM_yy") + "_log.txt");

            lock (Lock)
            {
                try
                {
                    if (NotWrittenExceptions.Count > 0)
                    {
                        while (NotWrittenExceptions.Count > 0)
                        {
                            File.AppendAllText(path, NotWrittenExceptions.Last() + Environment.NewLine);
                            NotWrittenExceptions.Remove(NotWrittenExceptions.Last());
                        }
                    }

                    File.AppendAllText(path, res + Environment.NewLine);
                }
                catch
                {
                    NotWrittenExceptions.Add(res + Environment.NewLine);
                }
            }
        }

        public bool IsEnabled(LogLevel logLevel) => logLevel == LogLevel.Error;
        public IDisposable BeginScope<TState>(TState _) => null;
    }
}
