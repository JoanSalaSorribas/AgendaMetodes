using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;

class Program
{
    private const string FILE_PATH = @"C:\M03\crearieliminar\crearieliminar\bin\Debug\net7.0\agenda.txt";
    private const string TEMP_FILE_PATH = @"C:\M03\crearieliminar\crearieliminar\bin\Debug\net7.0\temp.txt";

    static void Main(string[] args)
    {
        bool continuar = true;

        while (continuar)
        {
            Console.WriteLine("Selecciona una opció:\n1. Alta usuari\n2. Eliminar usuari\n3. Mostrar agenda\n4. Sortir");
            string opció = Console.ReadLine();

            switch (opció)
            {
                case "1":
                    AltaUsuari();
                    break;
                case "2":
                    EliminarUsuari();
                    break;
                case "3":
                    MostrarAgenda();
                    break;
                case "4":
                    continuar = false;
                    break;
                default:
                    Console.WriteLine("Opció no vàlida.");
                    break;
            }
        }
    }

    static void AltaUsuari()
    {
        Console.WriteLine("Escriu el teu DNI:");
        string dni;
        while (true)
        {
            dni = Console.ReadLine();
            if (ValidarDNI(dni))
                break;
            else
                Console.WriteLine("DNI no vàlid. Torna-ho a intentar:");
        }

        Console.WriteLine("Escriu el teu nom:");
        string nom = Console.ReadLine();
        nom = Char.ToUpper(nom[0]) + nom.Substring(1).ToLower();

        Console.WriteLine("Escriu el teu cognom:");
        string cognom = Console.ReadLine();
        cognom = Char.ToUpper(cognom[0]) + cognom.Substring(1).ToLower();

        Console.WriteLine("Escriu el teu telèfon:");
        string telefon;
        while (true)
        {
            telefon = Console.ReadLine();
            if (ValidarTelefon(telefon))
                break;
            else
                Console.WriteLine("Telèfon no vàlid. Torna-ho a intentar:");
        }

        Console.WriteLine("Escriu la teva data de naixement (dd/mm/aaaa):");
        string dataNaixement = Console.ReadLine();
        DateTime naixement;
        while (!DateTime.TryParse(dataNaixement, out naixement))
        {
            Console.WriteLine("Data no vàlida. Torna-ho a intentar:");
            dataNaixement = Console.ReadLine();
        }

        Console.WriteLine("Escriu el teu correu electrònic:");
        string correu;
        while (true)
        {
            correu = Console.ReadLine().ToLower();
            if (ValidarCorreu(correu))
                break;
            else
                Console.WriteLine("Correu electrònic no vàlid. Torna-ho a intentar:");
        }

        using (StreamWriter sw = new StreamWriter(FILE_PATH, true))
        {
            sw.WriteLine("DNI: " + dni);
            sw.WriteLine("Nom: " + nom + " " + cognom);
            sw.WriteLine("Telèfon: " + telefon);
            sw.WriteLine("Data naixement: " + naixement.ToString("dd/MM/yyyy"));
            sw.WriteLine("Correu: " + correu);
            sw.WriteLine();
        }

        Console.WriteLine("\nDades registrades.");
    }

    static void EliminarUsuari()
    {
        Console.Clear();
        Console.WriteLine("Introdueix el DNI de l'usuari a eliminar:");
        string dniAEliminar = Console.ReadLine();
        bool found = false;

        if (!File.Exists(FILE_PATH))
        {
            Console.WriteLine("L'agenda està buida.");
            return;
        }

        bool isUserBlock = false;

        using (StreamWriter sw = new StreamWriter(TEMP_FILE_PATH))
        {
            foreach (var line in File.ReadAllLines(FILE_PATH))
            {
                if (!isUserBlock && line.StartsWith("DNI: ") && line.Equals($"DNI: {dniAEliminar}"))
                {
                    found = true;
                    isUserBlock = true;
                    continue;
                }

                if (isUserBlock && string.IsNullOrWhiteSpace(line))
                {
                    isUserBlock = false;
                    continue;
                }

                if (!isUserBlock)
                {
                    sw.WriteLine(line);
                }
            }
        }

        if (found)
        {
            File.Delete(FILE_PATH);
            File.Move(TEMP_FILE_PATH, FILE_PATH);
            Console.WriteLine("Usuari eliminat amb èxit.");
        }
        else
        {
            File.Delete(TEMP_FILE_PATH);
            Console.WriteLine("Usuari no trobat.");
        }
    }

    static void MostrarAgenda()
    {
        if (!File.Exists(FILE_PATH))
        {
            Console.WriteLine("L'agenda està buida.");
            return;
        }

        var usuaris = new List<(string nom, string telefon)>();

        using (StreamReader sr = new StreamReader(FILE_PATH))
        {
            string nom = "";
            string telefon = "";
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                if (line.StartsWith("Nom: "))
                {
                    nom = line.Substring(5);
                }
                else if (line.StartsWith("Telèfon: "))
                {
                    telefon = line.Substring(9);
                    usuaris.Add((nom, telefon));
                }
            }
        }

        foreach (var usuari in usuaris.OrderBy(u => u.nom))
        {
            Console.WriteLine($"Nom: {usuari.nom}, Telèfon: {usuari.telefon}");
        }
    }

    static bool ValidarDNI(string dni)
    {
        if (string.IsNullOrWhiteSpace(dni) || dni.Length != 9)
            return false;

        string numero = dni.Substring(0, dni.Length - 1);
        char lletra = dni[dni.Length - 1];

        if (int.TryParse(numero, out int num))
        {
            string lletresValides = "TRWAGMYFPDXBNJZSQVHLCKE";
            int index = num % 23;
            if (lletresValides[index] == lletra)
                return true;
        }
        return false;
    }

    static bool ValidarTelefon(string telefon)
    {
        return Regex.IsMatch(telefon, @"^(\+34|0034|34)?[6789]\d{8}$");
    }

    static bool ValidarCorreu(string correu)
    {
        return Regex.IsMatch(correu, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
    }
}
