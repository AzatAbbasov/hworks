using System;
using System.Collections.Generic;

class Task2_Builder_Prototype
{
    static void Main()
    {
        Console.WriteLine("=== BUILDER ===");

        ReportDirector director = new ReportDirector();

        TextReportBuilder textBuilder = new TextReportBuilder();
        director.ConstructReport(textBuilder);
        Console.WriteLine(textBuilder.GetReport());

        HtmlReportBuilder htmlBuilder = new HtmlReportBuilder();
        director.ConstructReport(htmlBuilder);
        Console.WriteLine(htmlBuilder.GetReport());


        Console.WriteLine("\n=== PROTOTYPE ===");

        Order order1 = new Order();
        order1.Products.Add(new Product("Ноутбук", 500000, 1));
        order1.Discounts.Add(new Discount("Скидка 10%", 50000));
        order1.DeliveryCost = 7000;
        order1.PaymentMethod = "Kaspi";

        Order order2 = (Order)order1.Clone();
        order2.PaymentMethod = "Карта";

        Console.WriteLine("Оригинал способ оплаты: " + order1.PaymentMethod);
        Console.WriteLine("Клон способ оплаты: " + order2.PaymentMethod);
    }
}

/////////////////////////////////////////////////////////
// BUILDER
/////////////////////////////////////////////////////////

class Report
{
    public string Header;
    public string Content;
    public string Footer;

    public override string ToString()
    {
        return Header + "\n" + Content + "\n" + Footer;
    }
}

interface IReportBuilder
{
    void SetHeader(string header);
    void SetContent(string content);
    void SetFooter(string footer);
    Report GetReport();
}

class TextReportBuilder : IReportBuilder
{
    private Report report = new Report();

    public void SetHeader(string header)
    {
        report.Header = "=== " + header + " ===";
    }

    public void SetContent(string content)
    {
        report.Content = content;
    }

    public void SetFooter(string footer)
    {
        report.Footer = "--- " + footer + " ---";
    }

    public Report GetReport()
    {
        return report;
    }
}

class HtmlReportBuilder : IReportBuilder
{
    private Report report = new Report();

    public void SetHeader(string header)
    {
        report.Header = "<h1>" + header + "</h1>";
    }

    public void SetContent(string content)
    {
        report.Content = "<p>" + content + "</p>";
    }

    public void SetFooter(string footer)
    {
        report.Footer = "<footer>" + footer + "</footer>";
    }

    public Report GetReport()
    {
        return report;
    }
}

class ReportDirector
{
    public void ConstructReport(IReportBuilder builder)
    {
        builder.SetHeader("Отчет за месяц");
        builder.SetContent("Продажи выросли");
        builder.SetFooter("2026 год");
    }
}

/////////////////////////////////////////////////////////
// PROTOTYPE
/////////////////////////////////////////////////////////

class Product : ICloneable
{
    public string Name;
    public double Price;
    public int Quantity;

    public Product(string name, double price, int quantity)
    {
        Name = name;
        Price = price;
        Quantity = quantity;
    }

    public object Clone()
    {
        return new Product(Name, Price, Quantity);
    }
}

class Discount : ICloneable
{
    public string Description;
    public double Amount;

    public Discount(string description, double amount)
    {
        Description = description;
        Amount = amount;
    }

    public object Clone()
    {
        return new Discount(Description, Amount);
    }
}

class Order : ICloneable
{
    public List<Product> Products = new List<Product>();
    public List<Discount> Discounts = new List<Discount>();
    public double DeliveryCost;
    public string PaymentMethod;

    public object Clone()
    {
        Order newOrder = new Order();
        newOrder.DeliveryCost = DeliveryCost;
        newOrder.PaymentMethod = PaymentMethod;

        foreach (var product in Products)
        {
            newOrder.Products.Add((Product)product.Clone());
        }

        foreach (var discount in Discounts)
        {
            newOrder.Discounts.Add((Discount)discount.Clone());
        }

        return newOrder;
    }
}
