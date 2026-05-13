using System;
using System.Collections.Generic;

class Event
{
    public string Name;

    public Event(string name)
    {
        Name = name;
    }
}

class DocumentUpdatedEvent : Event
{
    public DocumentUpdatedEvent() : base("Документ обновлен")
    {
    }
}

class TaskStatusChangedEvent : Event
{
    public TaskStatusChangedEvent() : base("Статус задачи изменен")
    {
    }
}

class SystemNotificationEvent : Event
{
    public SystemNotificationEvent() : base("Системное уведомление")
    {
    }
}

interface IObserver
{
    void Update(Event ev);
}

interface ISubject
{
    void Attach(IObserver observer);
    void Detach(IObserver observer);
    void Notify(Event ev);
}

class Manager : IObserver
{
    private string name;

    public Manager(string name)
    {
        this.name = name;
    }

    public void Update(Event ev)
    {
        Console.WriteLine("Менеджер " + name +
                          " получил уведомление: " + ev.Name);
    }
}

class Employee : IObserver
{
    private string name;

    public Employee(string name)
    {
        this.name = name;
    }

    public void Update(Event ev)
    {
        Console.WriteLine("Сотрудник " + name +
                          " получил уведомление: " + ev.Name);
    }
}

class Client : IObserver
{
    private string name;

    public Client(string name)
    {
        this.name = name;
    }

    public void Update(Event ev)
    {
        if (ev is SystemNotificationEvent)
        {
            Console.WriteLine("Клиент " + name +
                              " получил уведомление: " + ev.Name);
        }
    }
}

class EmailNotification
{
    public void Send(string message)
    {
        Console.WriteLine("[EMAIL] " + message);
    }
}

class PushNotification
{
    public void Send(string message)
    {
        Console.WriteLine("[PUSH] " + message);
    }
}

class InAppNotification
{
    public void Send(string message)
    {
        Console.WriteLine("[IN-APP] " + message);
    }
}

class NotificationSystem : ISubject
{
    private List<IObserver> observers = new List<IObserver>();

    private EmailNotification email = new EmailNotification();
    private PushNotification push = new PushNotification();
    private InAppNotification inApp = new InAppNotification();

    public void Attach(IObserver observer)
    {
        observers.Add(observer);
    }

    public void Detach(IObserver observer)
    {
        observers.Remove(observer);
    }

    public void Notify(Event ev)
    {
        foreach (IObserver observer in observers)
        {
            observer.Update(ev);
        }

        email.Send(ev.Name);
        push.Send(ev.Name);
        inApp.Send(ev.Name);

        Console.WriteLine();
    }
}

class Program
{
    static void Main(string[] args)
    {
        NotificationSystem system = new NotificationSystem();

        Manager manager = new Manager("Дастан");
        Employee employee = new Employee("Абылайхан");
        Client client = new Client("Пархат");

        system.Attach(manager);
        system.Attach(employee);
        system.Attach(client);

        Event event1 = new DocumentUpdatedEvent();
        system.Notify(event1);

        Event event2 = new TaskStatusChangedEvent();
        system.Notify(event2);

        Event event3 = new SystemNotificationEvent();
        system.Notify(event3);

        system.Detach(employee);

        Console.WriteLine("Сотрудник Расул отписался\n");

        system.Notify(new TaskStatusChangedEvent());
    }
}
