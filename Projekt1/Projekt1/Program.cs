using DoctorAppointmentDemo.Data.Configuration;
using DoctorAppointmentDemo.Data.Interfaces;
using DoctorAppointmentDemo.Domain.Entities;
using DoctorAppointmentDemo.Domain.Enums;
using DoctorAppointmentDemo.Service.Interfaces;
using DoctorAppointmentDemo.Service.Service;
using System.Runtime.Intrinsics.Arm;

public class DoctorAppointment
{
    private readonly IDoctorService _doctorService;
    private readonly ISerializationService _serializationService;
    private readonly string _filePath;

    public DoctorAppointment(ISerializationService serializationService, string filePath)
    {
        _serializationService = serializationService;
        _filePath = filePath;
        _doctorService = new DoctorService();

    }
    public enum MenuOption
    {
        ShowDoctors = 1,
        AddDoctor,
        DeleteDoctor,
        Exit
    }

    public void Menu()
    {
        while (true)
        {
            Console.WriteLine("\n--- Doctor Appointment Menu ---");
            Console.WriteLine("1. Show all doctors");
            Console.WriteLine("2. Add a new doctor");
            Console.WriteLine("3. Delete a doctor");
            Console.WriteLine("4. Exit");
            Console.Write("Choose an option: ");

            if (Enum.TryParse(Console.ReadLine(), out MenuOption choice) && Enum.IsDefined(typeof(MenuOption), choice))
            {
                switch (choice)
                {
                    case MenuOption.ShowDoctors:
                        ShowDoctors();
                        break;
                    case MenuOption.AddDoctor:
                        AddDoctor();
                        break;
                    case MenuOption.DeleteDoctor:
                        DeleteDoctor();
                        break;
                    case MenuOption.Exit:      
                        return;
                    default:
                        Console.WriteLine("Invalid Oprion try again");
                        break;

                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid number.");
            }
        }
    }
    private void ShowDoctors()
    {
        Console.WriteLine("\n--- Doctors List ---");
        var doctors = _doctorService.GetAll();

        if(!doctors.Any())
        {
            Console.WriteLine("No doctors available.");
            return;
        }

        foreach (var doctor in doctors)
        {
            Console.WriteLine($"Name: {doctor.Name} {doctor.Surname}, Experience: {doctor.Experience} years");
        }
    }

    private void AddDoctor()
    {
        Console.WriteLine("\n--- Add New Doctor ---");

        Console.WriteLine("Enter first name: ");
        string name = Console.ReadLine() ??"Unknow";
        Console.WriteLine("Enter second name: ");
        string surname = Console.ReadLine() ?? "Unknow";

        Console.WriteLine("Chose doctor type: 1 - Dentist, 2 - Dermatologist, 3 - FamilyDoctor, 4 - Paramedic ");
        if (!Enum.TryParse(Console.ReadLine(), out DoctorTypes doctorType) || !Enum.IsDefined(typeof(DoctorTypes), doctorType))
        {
            Console.WriteLine("Invalid doctor type. Defaulting to Dentist.");
            doctorType = DoctorTypes.Dentist;
        }
        var newDoctor = new Doctor
        {
            Name = name,
            Surname = surname,
            DoctorType = doctorType
        };

        _doctorService.Create(newDoctor);
        Console.WriteLine("Doctor added successfully!");
    }

    private void DeleteDoctor()
    {
        Console.WriteLine("\n--- Delete Doctor ---");
        ShowDoctors();
        Console.Write("Enter the ID of the doctor to delete: ");
        
        if (int.TryParse(Console.ReadLine(), out int doctorId))
        {
            var doctor = _doctorService.Get(doctorId);
            if (doctor != null)
            {
                _doctorService.Delete(doctorId);
                Console.WriteLine("Doctor deleted successfully!");
            }
            else
            {
                Console.WriteLine("Doctor not found.");
            }
        }
        else
        {
            Console.WriteLine("Invalid input. Please enter a valid ID.");
        }
    }
   
}




public static class Program
{
    public static void Main()
    {
        Console.WriteLine("Choose storage format: (1) JSON, (2) XML");
        string choice = Console.ReadLine()!;


        ISerializationService serializationService = choice switch
        {
            "1" => new JsonDataSerializerService(),
            "2" => new XmlDataSerializerService(),
            _ => throw new ArgumentException("Invalid choice, please choose 1 or 2.")
        };
        string filePath = choice switch
        {
            "1" => Constants.JsonAppSettingsPath,
            "2" => Constants.XmlAppSettingsPath, 
            _ => throw new Exception("Invalid choice.")
        };
        Console.WriteLine($"File path selected: {filePath}");

        var doctorAppointment = new DoctorAppointment(serializationService, filePath);
        doctorAppointment.Menu();
    }
}