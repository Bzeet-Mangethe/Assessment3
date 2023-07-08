using System;
using System.Collections.Generic;
using System.Linq;

// Enum to represent the type of equipment
enum EquipmentType
{
    Mobile,
    Immobile
}

// Base Equipment class
abstract class Equipment
{
    public string Name { get; set; }
    public string Description { get; set; }
    public double DistanceMovedTillDate { get; protected set; }
    public decimal MaintenanceCost { get; protected set; }
    public EquipmentType Type { get; protected set; }

    public abstract void MoveBy(double distance);
    public abstract void ShowDetails();
}

// Mobile Equipment class
class MobileEquipment : Equipment
{
    public int NumberOfWheels { get; set; }

    public MobileEquipment()
    {
        Type = EquipmentType.Mobile;
    }

    public override void MoveBy(double distance)
    {
        DistanceMovedTillDate += distance;
        MaintenanceCost += NumberOfWheels * (decimal)distance;
    }

    public override void ShowDetails()
    {
        Console.WriteLine("Type: Mobile Equipment");
        Console.WriteLine("Name: " + Name);
        Console.WriteLine("Description: " + Description);
        Console.WriteLine("Distance Moved Till Date: " + DistanceMovedTillDate + " km");
        Console.WriteLine("Maintenance Cost: $" + MaintenanceCost);
        Console.WriteLine("Number of Wheels: " + NumberOfWheels);
    }
}

// Immobile Equipment class
class ImmobileEquipment : Equipment
{
    public double Weight { get; set; }

    public ImmobileEquipment()
    {
        Type = EquipmentType.Immobile;
    }

    public override void MoveBy(double distance)
    {
        DistanceMovedTillDate += distance;
        MaintenanceCost += (decimal)(Weight * distance);
    }

    public override void ShowDetails()
    {
        Console.WriteLine("Type: Immobile Equipment");
        Console.WriteLine("Name: " + Name);
        Console.WriteLine("Description: " + Description);
        Console.WriteLine("Distance Moved Till Date: " + DistanceMovedTillDate + " km");
        Console.WriteLine("Maintenance Cost: $" + MaintenanceCost);
        Console.WriteLine("Weight: " + Weight + " kg");
    }
}

class Program
{
    static List<Equipment> equipmentList = new List<Equipment>();

    static void Main()
    {
        bool exit = false;
        while (!exit)
        {
            Console.WriteLine("========== Equipment Inventory ==========");
            Console.WriteLine("1. Create an equipment");
            Console.WriteLine("2. Delete an equipment");
            Console.WriteLine("3. Move an equipment");
            Console.WriteLine("4. List all equipment (basic details)");
            Console.WriteLine("5. Show details of an equipment");
            Console.WriteLine("6. List all mobile equipment");
            Console.WriteLine("7. List all immobile equipment");
            Console.WriteLine("8. List all equipment that have not been moved till now");
            Console.WriteLine("9. Delete all equipment");
            Console.WriteLine("10. Delete all immobile equipment");
            Console.WriteLine("11. Delete all mobile equipment");
            Console.WriteLine("0. Exit");
            Console.WriteLine("=========================================");
            Console.Write("Enter your choice: ");

            if (int.TryParse(Console.ReadLine(), out int choice))
            {
                switch (choice)
                {
                    case 1:
                        CreateEquipment();
                        break;
                    case 2:
                        DeleteEquipment();
                        break;
                    case 3:
                        MoveEquipment();
                        break;
                    case 4:
                        ListAllEquipmentBasicDetails();
                        break;
                    case 5:
                        ShowEquipmentDetails();
                        break;
                    case 6:
                        ListAllMobileEquipment();
                        break;
                    case 7:
                        ListAllImmobileEquipment();
                        break;
                    case 8:
                        ListNotMovedEquipment();
                        break;
                    case 9:
                        DeleteAllEquipment();
                        break;
                    case 10:
                        DeleteAllImmobileEquipment();
                        break;
                    case 11:
                        DeleteAllMobileEquipment();
                        break;
                    case 0:
                        exit = true;
                        Console.WriteLine("Exiting the program...");
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid choice.");
            }

            Console.WriteLine();
        }
    }

    static void CreateEquipment()
    {
        Console.WriteLine("Enter the details of the equipment:");
        Console.Write("Name: ");
        string name = Console.ReadLine();
        Console.Write("Description: ");
        string description = Console.ReadLine();

        Equipment equipment;
        Console.WriteLine("Choose the type of equipment:");
        Console.WriteLine("1. Mobile Equipment");
        Console.WriteLine("2. Immobile Equipment");
        Console.Write("Enter your choice: ");
        if (int.TryParse(Console.ReadLine(), out int typeChoice))
        {
            switch (typeChoice)
            {
                case 1:
                    equipment = new MobileEquipment();
                    Console.Write("Number of Wheels: ");
                    if (int.TryParse(Console.ReadLine(), out int numberOfWheels))
                    {
                        ((MobileEquipment)equipment).NumberOfWheels = numberOfWheels;
                        equipmentList.Add(equipment);
                        Console.WriteLine("Mobile Equipment created successfully.");
                    }
                    else
                    {
                        Console.WriteLine("Invalid input for number of wheels.");
                    }
                    break;
                case 2:
                    equipment = new ImmobileEquipment();
                    Console.Write("Weight (in kg): ");
                    if (double.TryParse(Console.ReadLine(), out double weight))
                    {
                        ((ImmobileEquipment)equipment).Weight = weight;
                        equipmentList.Add(equipment);
                        Console.WriteLine("Immobile Equipment created successfully.");
                    }
                    else
                    {
                        Console.WriteLine("Invalid input for weight.");
                    }
                    break;
                default:
                    Console.WriteLine("Invalid type choice.");
                    break;
            }
        }
        else
        {
            Console.WriteLine("Invalid input for type choice.");
        }
    }

    static void DeleteEquipment()
    {
        Console.Write("Enter the name of the equipment to delete: ");
        string name = Console.ReadLine();
        Equipment equipment = equipmentList.FirstOrDefault(e => e.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        if (equipment != null)
        {
            equipmentList.Remove(equipment);
            Console.WriteLine("Equipment deleted successfully.");
        }
        else
        {
            Console.WriteLine("Equipment not found.");
        }
    }

    static void MoveEquipment()
    {
        Console.Write("Enter the name of the equipment to move: ");
        string name = Console.ReadLine();
        Equipment equipment = equipmentList.FirstOrDefault(e => e.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        if (equipment != null)
        {
            Console.Write("Enter the distance to move (in km): ");
            if (double.TryParse(Console.ReadLine(), out double distance))
            {
                equipment.MoveBy(distance);
                Console.WriteLine("Equipment moved successfully.");
            }
            else
            {
                Console.WriteLine("Invalid input for distance.");
            }
        }
        else
        {
            Console.WriteLine("Equipment not found.");
        }
    }

    static void ListAllEquipmentBasicDetails()
    {
        Console.WriteLine("Listing all equipment (basic details):");
        foreach (Equipment equipment in equipmentList)
        {
            Console.WriteLine("Name: " + equipment.Name);
            Console.WriteLine("Description: " + equipment.Description);
            Console.WriteLine();
        }
    }

    static void ShowEquipmentDetails()
    {
        Console.Write("Enter the name ofthe equipment to show details: ");
        string name = Console.ReadLine();
        Equipment equipment = equipmentList.FirstOrDefault(e => e.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        if (equipment != null)
        {
            equipment.ShowDetails();
        }
        else
        {
            Console.WriteLine("Equipment not found.");
        }
    }

    static void ListAllMobileEquipment()
    {
        Console.WriteLine("Listing all mobile equipment:");
        var mobileEquipment = equipmentList.Where(e => e.Type == EquipmentType.Mobile);
        foreach (Equipment equipment in mobileEquipment)
        {
            Console.WriteLine("Name: " + equipment.Name);
            Console.WriteLine("Description: " + equipment.Description);
            Console.WriteLine();
        }
    }

    static void ListAllImmobileEquipment()
    {
        Console.WriteLine("Listing all immobile equipment:");
        var immobileEquipment = equipmentList.Where(e => e.Type == EquipmentType.Immobile);
        foreach (Equipment equipment in immobileEquipment)
        {
            Console.WriteLine("Name: " + equipment.Name);
            Console.WriteLine("Description: " + equipment.Description);
            Console.WriteLine();
        }
    }

    static void ListNotMovedEquipment()
    {
        Console.WriteLine("Listing all equipment that have not been moved till now:");
        var notMovedEquipment = equipmentList.Where(e => e.DistanceMovedTillDate == 0);
        foreach (Equipment equipment in notMovedEquipment)
        {
            Console.WriteLine("Name: " + equipment.Name);
            Console.WriteLine("Description: " + equipment.Description);
            Console.WriteLine();
        }
    }

    static void DeleteAllEquipment()
    {
        equipmentList.Clear();
        Console.WriteLine("All equipment deleted successfully.");
    }

    static void DeleteAllImmobileEquipment()
    {
        equipmentList.RemoveAll(e => e.Type == EquipmentType.Immobile);
        Console.WriteLine("All immobile equipment deleted successfully.");
    }

    static void DeleteAllMobileEquipment()
    {
        equipmentList.RemoveAll(e => e.Type == EquipmentType.Mobile);
        Console.WriteLine("All mobile equipment deleted successfully.");
    }
}
