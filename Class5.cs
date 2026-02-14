using System;

interface Document
{
    void Open();
}

class Report : Document
{
    public void Open()
    {
        Console.WriteLine("Report opened.");
    }
}

class Resume : Document
{
    public void Open()
    {
        Console.WriteLine("Resume opened.");
    }
}

class Letter : Document
{
    public void Open()
    {
        Console.WriteLine("Letter opened.");
    }
}

class Invoice : Document
{
    public void Open()
    {
        Console.WriteLine("Invoice opened.");
    }
}


abstract class DocumentCreator
{
    public abstract Document CreateDocument();
}

class ReportCreator : DocumentCreator
{
    public override Document CreateDocument()
    {
        return new Report();
    }
}

class ResumeCreator : DocumentCreator
{
    public override Document CreateDocument()
    {
        return new Resume();
    }
}

class LetterCreator : DocumentCreator
{
    public override Document CreateDocument()
    {
        return new Letter();
    }
}

class InvoiceCreator : DocumentCreator
{
    public override Document CreateDocument()
    {
        return new Invoice();
    }
}


class Program
{
    static void Main()
    {
        Console.WriteLine("Choose document type:");
        Console.WriteLine("1 - Report");
        Console.WriteLine("2 - Resume");
        Console.WriteLine("3 - Letter");
        Console.WriteLine("4 - Invoice");

        string choice = Console.ReadLine();
        DocumentCreator creator = null;

        if (choice == "1")
        {
            creator = new ReportCreator();
        }
        else if (choice == "2")
        {
            creator = new ResumeCreator();
        }
        else if (choice == "3")
        {
            creator = new LetterCreator();
        }
        else if (choice == "4")
        {
            creator = new InvoiceCreator();
        }
        else
        {
            Console.WriteLine("Wrong choice.");
            return;
        }

        Document doc = creator.CreateDocument();
        doc.Open();
    }
}
