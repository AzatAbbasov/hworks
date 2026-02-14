using System;

interface IVehicle
{
    void Drive();
    void Refuel();
}

class Car : IVehicle
{
    public string Brand;
    public string Model;
    public string FuelType;

    public Car(string brand, string model, string fuelType)
    {
        Brand = brand;
        Model = model;
        FuelType = fuelType;
    }

    public void Drive()
    {
        Console.WriteLine("Car " + Brand + " " + Model + " is driving.");
    }

    public void Refuel()
    {
        Console.WriteLine("Car refueled with " + FuelType + ".");
    }
}

class Motorcycle : IVehicle
{
    public string Type;
    public int EngineVolume;

    public Motorcycle(string type, int engineVolume)
    {
        Type = type;
        EngineVolume = engineVolume;
    }

    public void Drive()
    {
        Console.WriteLine("Motorcycle (" + Type + ") is driving.");
    }

    public void Refuel()
    {
        Console.WriteLine("Motorcycle refueled.");
    }
}

class Truck : IVehicle
{
    public int LoadCapacity;
    public int Axles;

    public Truck(int loadCapacity, int axles)
    {
        LoadCapacity = loadCapacity;
        Axles = axles;
    }

    public void Drive()
    {
        Console.WriteLine("Truck with capacity " + LoadCapacity + " kg is driving.");
    }

    public void Refuel()
    {
        Console.WriteLine("Truck refueled.");
    }
}

class Bus : IVehicle
{
    public int PassengerCapacity;
    public string FuelType;

    public Bus(int passengerCapacity, string fuelType)
    {
        PassengerCapacity = passengerCapacity;
        FuelType = fuelType;
    }

    public void Drive()
    {
        Console.WriteLine("Bus with " + PassengerCapacity + " passengers is driving.");
    }

    public void Refuel()
    {
        Console.WriteLine("Bus refueled with " + FuelType + ".");
    }
}

abstract class VehicleFactory
{
    public abstract IVehicle CreateVehicle();
}

class CarFactory : VehicleFactory
{
    string brand;
    string model;
    string fuel;

    public CarFactory(string brand, string model, string fuel)
    {
        this.brand = brand;
        this.model = model;
        this.fuel = fuel;
    }

    public override IVehicle CreateVehicle()
    {
        return new Car(brand, model, fuel);
    }
}

class MotorcycleFactory : VehicleFactory
{
    string type;
    int volume;

    public MotorcycleFactory(string type, int volume)
    {
        this.type = type;
        this.volume = volume;
    }

    public override IVehicle CreateVehicle()
    {
        return new Motorcycle(type, volume);
    }
}

class TruckFactory : VehicleFactory
{
    int capacity;
    int axles;

    public TruckFactory(int capacity, int axles)
    {
        this.capacity = capacity;
        this.axles = axles;
    }

    public override IVehicle CreateVehicle()
    {
        return new Truck(capacity, axles);
    }
}

class BusFactory : VehicleFactory
{
    int passengers;
    string fuel;

    public BusFactory(int passengers, string fuel)
    {
        this.passengers = passengers;
        this.fuel = fuel;
    }

    public override IVehicle CreateVehicle()
    {
        return new Bus(passengers, fuel);
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("Choose vehicle:");
        Console.WriteLine("1 - Car");
        Console.WriteLine("2 - Motorcycle");
        Console.WriteLine("3 - Truck");
        Console.WriteLine("4 - Bus");

        string choice = Console.ReadLine();
        VehicleFactory factory = null;

        if (choice == "1")
        {
            Console.Write("Brand: ");
            string brand = Console.ReadLine();

            Console.Write("Model: ");
            string model = Console.ReadLine();

            Console.Write("Fuel type: ");
            string fuel = Console.ReadLine();

            factory = new CarFactory(brand, model, fuel);
        }
        else if (choice == "2")
        {
            Console.Write("Motorcycle type: ");
            string type = Console.ReadLine();

            Console.Write("Engine volume: ");
            int volume = int.Parse(Console.ReadLine());

            factory = new MotorcycleFactory(type, volume);
        }
        else if (choice == "3")
        {
            Console.Write("Load capacity: ");
            int capacity = int.Parse(Console.ReadLine());

            Console.Write("Axles: ");
            int axles = int.Parse(Console.ReadLine());

            factory = new TruckFactory(capacity, axles);
        }
        else if (choice == "4")
        {
            Console.Write("Passenger capacity: ");
            int passengers = int.Parse(Console.ReadLine());

            Console.Write("Fuel type: ");
            string fuel = Console.ReadLine();

            factory = new BusFactory(passengers, fuel);
        }
        else
        {
            Console.WriteLine("Wrong choice.");
            return;
        }

        IVehicle vehicle = factory.CreateVehicle();
        vehicle.Drive();
        vehicle.Refuel();
    }
}

