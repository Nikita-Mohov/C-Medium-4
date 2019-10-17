using System;
using System.IO;

namespace Lesson
{
    class Program
    {
        static void Main(string[] args)
        {
            var secureConsoleSender = new Sender(new SecureLog(new ConsoleLog()));
            var fileSender = new Sender(new FileLog());
            var secureFileSender = new Sender(new SecureLog(new FileLog()));
            var consoleFileSender = new Sender(new ConsoleLog(new FileLog()));
            var secureConsoleFileSender = new Sender(new SecureLog(new ConsoleLog(new FileLog())));
        }
    }

    class Sender
    {
        private ILogger _log;

        public Sender(ILogger log)
        {
            _log = log;
        }
    }

    interface ILogger
    {
        void WriteMessage(string message);
    }

    class ConsoleLog : ILogger
    {
        private ILogger _log;

        public ConsoleLog(ILogger log)
        {
            _log = log;
        }
        public ConsoleLog() { }

        public void WriteMessage(string message)
        {
            Console.WriteLine($"[{DateTime.Now}] : {message}");
        }
    }

    class FileLog: ILogger
    {
        private ILogger _log;

        public FileLog(ILogger log)
        {
            _log = log;
        }
        public FileLog() { }

        public void WriteMessage(string message)
        {
            File.WriteAllText("log.txt", $"[{DateTime.Now}] : {message}");
        }
    }

    class SecureLog : ILogger
    {
        private ILogger _logger;

        SecureLog(ILogger logger)
        {
            _logger = logger;
        }

        public void WriteError(string message)
        {
            if (DateTime.Now.DayOfWeek == DayOfWeek.Friday)
            {
                _logger.WriteMessage(message);
            }
        }
    }
}