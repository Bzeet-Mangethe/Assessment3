using System;
using System.Collections.Generic;
using System.Linq;

enum DuckType
{
    Rubber,
    Mallard,
    Redhead
}

interface IDuck
{
    string Name { get; set; }
    double Weight { get; set; }
    int NumberOfWings { get; set; }
    DuckType Type { get; }
    void Fly();
    void Quack();
}

class RubberDuck : IDuck
{
    public string Name { get; set; }
    public double Weight { get; set; }
    public int NumberOfWings { get; set; }
    public DuckType Type => DuckType.Rubber;

    public void Fly()
    {
        Console.WriteLine("Rubber ducks don't fly.");
    }

    public void Quack()
    {
        Console.WriteLine("Rubber ducks squeak.");
    }
}

class MallardDuck : IDuck
{
    public string Name { get; set; }
    public double Weight { get; set; }
    public int NumberOfWings { get; set; }
    public DuckType Type => DuckType.Mallard;

    public void Fly()
    {
        Console.WriteLine("Mallard ducks fly fast.");
    }

    public void Quack()
    {
        Console.WriteLine("Mallard ducks quack loud.");
    }
}

class RedheadDuck : IDuck
{
    public string Name { get; set; }
    public double Weight { get; set; }
    public int NumberOfWings { get; set; }
    public DuckType Type => DuckType.Redhead;

    public void Fly()
    {
        Console.WriteLine("Redhead ducks fly slow.");
    }

    public void Quack()
    {
        Console.WriteLine("Redhead ducks quack mild.");
    }
}

class DuckCollection : IEnumerable<IDuck>
{
    private List<IDuck> ducks;

    public DuckCollection()
    {
        ducks = new List<IDuck>();
    }

    public void AddDuck(IDuck duck)
    {
        ducks.Add(duck);
    }

    public void RemoveDuck(IDuck duck)
    {
        ducks.Remove(duck);
    }

    public void RemoveAllDucks()
    {
        ducks.Clear();
    }

    public IEnumerator<IDuck> GetEnumerator()
    {
        return ducks.OrderBy(d => d.Weight).GetEnumerator();
    }

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public IEnumerable<IDuck> GetDucksByWings()
    {
        return ducks.OrderBy(d => d.NumberOfWings);
    }
}

class Program
{
    static void Main(string[] args)
    {
        DuckCollection duckCollection = new DuckCollection();
        bool exit = false;

        while (!exit)
        {
            Console.WriteLine("Duck Simulation Game");
            Console.WriteLine("1. Show Duck Collection");
            Console.WriteLine("2. Add a Duck");
            Console.WriteLine("3. Remove a Duck");
            Console.WriteLine("4. Remove All Ducks");
            Console.WriteLine("5. Iterate Ducks by Weight");
            Console.WriteLine("6. Iterate Ducks by Number of Wings");
            Console.WriteLine("0. Exit");
            Console.Write("Enter your choice: ");

            if (int.TryParse(Console.ReadLine(), out int choice))
            {
                switch (choice)
                {
                    case 1:
                        ShowDuckCollection(duckCollection);
                        break;
                    case 2:
                        AddDuck(duckCollection);
                        break;
                    case 3:
                        RemoveDuck(duckCollection);
                        break;
                    case 4:
                        RemoveAllDucks(duckCollection);
                        break;
                    case 5:
                        IterateDucksByWeight(duckCollection);
                        break;
                    case 6:
                        IterateDucksByWings(duckCollection);
                        break;
                    case 0:
                        exit = true;
                        Console.WriteLine("Goodbye!");
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Invalid choice. Please try again.");
            }

            Console.WriteLine();
        }
    }

    static void ShowDuckCollection(DuckCollection duckCollection)
    {
        Console.WriteLine("Duck Collection:");
        foreach (IDuck duck in duckCollection)
        {
            Console.WriteLine("Name: " + duck.Name);
            Console.WriteLine("Weight: " + duck.Weight);
            Console.WriteLine("Number of Wings: " + duck.NumberOfWings);
            Console.WriteLine("Type: " + duck.Type);
            Console.WriteLine();
        }
    }

    static void AddDuck(DuckCollection duckCollection)
    {
        Console.WriteLine("Enter duck details:");
        Console.Write("Name: ");
        string name = Console.ReadLine();
        Console.Write("Weight: ");
        if (double.TryParse(Console.ReadLine(), out double weight))
        {
            Console.Write("Number of Wings: ");
            if (int.TryParse(Console.ReadLine(), out int numberOfWings))
            {
                Console.Write("Duck Type (0 - Rubber, 1 - Mallard, 2 - Redhead): ");
                if (Enum.TryParse<DuckType>(Console.ReadLine(), out DuckType type))
                {
                    IDuck duck;
                    switch (type)
                    {
                        case DuckType.Rubber:
                            duck = new RubberDuck();
                            break;
                        case DuckType.Mallard:
                            duck = new MallardDuck();
                            break;
                        case DuckType.Redhead:
                            duck = new RedheadDuck();
                            break;
                        default:
                            duck = null;
                            break;
                    }

                    if (duck != null)
                    {
                        duck.Name = name;
                        duck.Weight = weight;
                        duck.NumberOfWings = numberOfWings;
                        duckCollection.AddDuck(duck);
                        Console.WriteLine("Duck added successfully.");
                    }
                    else
                    {
                        Console.WriteLine("Invalid duck type.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid duck type.");
                }
            }
            else
            {
                Console.WriteLine("Invalid number of wings.");
            }
        }
        else
        {
            Console.WriteLine("Invalid weight.");
        }
    }

    static void RemoveDuck(DuckCollection duckCollection)
    {
        Console.Write("Enter the name of the duck to remove: ");
        string name = Console.ReadLine();
        IDuck duck = duckCollection.FirstOrDefault(d => d.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        if (duck != null)
        {
            duckCollection.RemoveDuck(duck);
            Console.WriteLine("Duck removed successfully.");
        }
        else
        {
            Console.WriteLine("Duck not found.");
        }
    }

    static void RemoveAllDucks(DuckCollection duckCollection)
    {
        duckCollection.RemoveAllDucks();
        Console.WriteLine("All ducks removed successfully.");
    }

    static void IterateDucksByWeight(DuckCollection duckCollection)
    {
        Console.WriteLine("Iterating Ducks by Weight:");
        foreach (IDuck duck in duckCollection)
        {
            Console.WriteLine("Name: " + duck.Name);
            Console.WriteLine("Weight: " + duck.Weight);
            Console.WriteLine();
        }
    }

    static void IterateDucksByWings(DuckCollection duckCollection)
    {
        Console.WriteLine("Iterating Ducks by Number of Wings:");
        foreach (IDuck duck in duckCollection.GetDucksByWings())
        {
            Console.WriteLine("Name: " + duck.Name);
            Console.WriteLine("Number of Wings: " + duck.NumberOfWings);
            Console.WriteLine();
        }
    }
}
