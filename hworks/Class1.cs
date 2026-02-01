using System;

namespace DesignPrinciples
{
    public class Logger
    {
        public void Log(string type, string message)
        {
            Console.WriteLine($"{type}: {message}");
        }
    }

    public static class Config
    {
        public static string ConnectionString =
            "Server=myServer;Database=myDb;User Id=myUser;Password=myPass;";
    }

    public class DatabaseService
    {
        public void Connect()
        {
            string connectionString = Config.ConnectionString;
        }
    }

    public class LoggingService
    {
        public void Log(string message)
        {
            string connectionString = Config.ConnectionString;
        }
    }

    public class NumberProcessor
    {
        public void ProcessNumbers(int[] numbers)
        {
            if (numbers == null)
                return;

            foreach (var number in numbers)
            {
                if (number > 0)
                    Console.WriteLine(number);
            }
        }

        public void PrintPositiveNumbers(int[] numbers)
        {
            if (numbers == null)
                return;

            foreach (var number in numbers)
            {
                if (number > 0)
                    Console.WriteLine(number);
            }
        }

        public int Divide(int a, int b)
        {
            if (b == 0)
                return 0;

            return a / b;
        }
    }

    public class User
    {
        public string Name { get; set; }
        public string Email { get; set; }
    }

    public class FileReader
    {
        public string ReadFile(string filePath)
        {
            return "file content";
        }
    }

    public class ReportGenerator
    {
        public void GeneratePdfReport()
        {
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Logger logger = new Logger();
            logger.Log("INFO", "Start");
            logger.Log("WARNING", "Test");
            logger.Log("ERROR", "Fail");

            NumberProcessor processor = new NumberProcessor();
            int[] numbers = { -1, 0, 3, 7 };
            processor.ProcessNumbers(numbers);
            processor.PrintPositiveNumbers(numbers);
            processor.Divide(10, 2);

            User user = new User { Name = "Ivan", Email = "ivan@mail.com" };

            FileReader reader = new FileReader();
            reader.ReadFile("test.txt");

            ReportGenerator report = new ReportGenerator();
            report.GeneratePdfReport();
        }
    }
}


