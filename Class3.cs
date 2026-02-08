using System;
using System.Collections.Generic;

public class OrderItem
{
    public string Name { get; set; }
    public int Quantity { get; set; }
    public double Price { get; set; }

    public double GetTotal()
    {
        return Quantity * Price;
    }
}

public class CustomerOrder
{
    private List<OrderItem> items = new List<OrderItem>();

    public IPayment Payment { get; set; }
    public IDelivery Delivery { get; set; }

    public void AddItem(string name, int quantity, double price)
    {
        items.Add(new OrderItem
        {
            Name = name,
            Quantity = quantity,
            Price = price
        });
    }

    public double GetTotal(DiscountCalculator calculator)
    {
        double total = 0;
        foreach (var item in items)
        {
            total += item.GetTotal();
        }
        return calculator.ApplyDiscount(total);
    }
}

public interface IPayment
{
    void ProcessPayment(double amount);
}

public class CreditCardPayment : IPayment
{
    public void ProcessPayment(double amount)
    {
        Console.WriteLine("Paid by Credit Card: " + amount);
    }
}

public class PayPalPayment : IPayment
{
    public void ProcessPayment(double amount)
    {
        Console.WriteLine("Paid by PayPal: " + amount);
    }
}

public class BankTransferPayment : IPayment
{
    public void ProcessPayment(double amount)
    {
        Console.WriteLine("Paid by Bank Transfer: " + amount);
    }
}

public interface IDelivery
{
    void DeliverOrder(CustomerOrder order);
}

public class CourierDelivery : IDelivery
{
    public void DeliverOrder(CustomerOrder order)
    {
        Console.WriteLine("Delivered by courier");
    }
}

public class PostDelivery : IDelivery
{
    public void DeliverOrder(CustomerOrder order)
    {
        Console.WriteLine("Delivered by post");
    }
}

public class PickUpPointDelivery : IDelivery
{
    public void DeliverOrder(CustomerOrder order)
    {
        Console.WriteLine("Ready for pickup");
    }
}

public interface INotification
{
    void SendNotification(string message);
}

public class EmailNotification : INotification
{
    public void SendNotification(string message)
    {
        Console.WriteLine("Email: " + message);
    }
}

public class SmsNotification : INotification
{
    public void SendNotification(string message)
    {
        Console.WriteLine("SMS: " + message);
    }
}

public interface IDiscount
{
    double Apply(double total);
}

public class TenPercentDiscount : IDiscount
{
    public double Apply(double total)
    {
        return total * 0.9;
    }
}

public class DiscountCalculator
{
    private IDiscount discount;

    public DiscountCalculator(IDiscount discount)
    {
        this.discount = discount;
    }

    public double ApplyDiscount(double total)
    {
        return discount.Apply(total);
    }
}
class Program
{
    static void Main()
    {
        CustomerOrder order = new CustomerOrder();

        order.AddItem("Laptop", 1, 1000);
        order.AddItem("Mouse", 2, 50);

        order.Payment = new CreditCardPayment();
        order.Delivery = new CourierDelivery();

        DiscountCalculator calculator = new DiscountCalculator(new TenPercentDiscount());
        double total = order.GetTotal(calculator);

        order.Payment.ProcessPayment(total);
        order.Delivery.DeliverOrder(order);

        INotification notification = new EmailNotification();
        notification.SendNotification("Order total: " + total);
    }
}
