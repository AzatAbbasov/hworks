using System;

abstract class ReportGenerator
{
    public void GenerateReport()
    {
        GetData();
        FormatData();
        CreateHeader();

        if (CustomerWantsSave())
        {
            Save();
        }
    }

    void GetData()
    {
        Console.WriteLine("Получение данных");
    }

    protected abstract void FormatData();
    protected abstract void CreateHeader();

    protected virtual bool CustomerWantsSave()
    {
        Console.Write("Сохранить отчет? (y/n): ");
        string answer = Console.ReadLine();

        return answer.ToLower() == "y";
    }

    protected virtual void Save()
    {
        Console.WriteLine("Отчет сохранен");
    }
}

class PdfReport : ReportGenerator
{
    protected override void FormatData()
    {
        Console.WriteLine("Форматирование PDF отчета");
    }

    protected override void CreateHeader()
    {
        Console.WriteLine("Создание PDF заголовка");
    }
}

class ExcelReport : ReportGenerator
{
    protected override void FormatData()
    {
        Console.WriteLine("Форматирование Excel отчета");
    }

    protected override void CreateHeader()
    {
        Console.WriteLine("Создание Excel таблицы");
    }

    protected override void Save()
    {
        Console.WriteLine("Excel файл сохранен");
    }
}

class HtmlReport : ReportGenerator
{
    protected override void FormatData()
    {
        Console.WriteLine("Форматирование HTML отчета");
    }

    protected override void CreateHeader()
    {
        Console.WriteLine("<html><head></head>");
    }
}

class CsvReport : ReportGenerator
{
    protected override void FormatData()
    {
        Console.WriteLine("Форматирование CSV отчета");
    }

    protected override void CreateHeader()
    {
        Console.WriteLine("CSV Header");
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("Template Method пример");

        ReportGenerator pdf = new PdfReport();
        pdf.GenerateReport();

        Console.WriteLine();

        ReportGenerator excel = new ExcelReport();
        excel.GenerateReport();

        Console.WriteLine();

        ReportGenerator html = new HtmlReport();
        html.GenerateReport();
    }
}