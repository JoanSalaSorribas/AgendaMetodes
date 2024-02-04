internal class Program
{
    static string MenuText()
    {


        string menu =

            "----------Menu----------\n" +
            "|                      |\n" +
            "| 1.Alta usuari        |\n" +
            "| 2.Rec. usuari        |\n" +
            "| 3.Mod. usuari        |\n" +
            "| 4.Eliminar usuari    |\n" +
            "| 5.Mostrar agenda     |\n" +
            "| 6.Ordenar agenda     |\n" +
            "|                      |\n" +
            "| 0.Sortir             |\n" +
            "------------------------\n";
        return menu;
    }
    static string AltaUsuari()
    {

    }
    static void RecuperarUsuari()
    {
        Console.WriteLine("Introdueix el nom de l'usuari a buscar:");
        string nomUsuari = Console.ReadLine();

        // Obtenir totes les línies del fitxer agenda.txt
        string[] línies = File.ReadAllLines("agenda.txt");

        // Comprovar si l'usuari existeix i mostrar-lo
        bool usuariTrobat = false;
        foreach (string linia in línies)
        {
            string[] dades = linia.Split(',');
            string nom = dades[0].Trim();

            // Comprovem si el nom coincideix amb la entrada de l'usuari
            if (EsCoincidencia(nom, nomUsuari))
            {
                MostrarUsuari(dades);
                usuariTrobat = true;
                break;
            }
        }

        // Mostrar missatge si l'usuari no s'ha trobat
        if (!usuariTrobat)
        {
            Console.WriteLine("L'usuari no existeix.");

            // Demanar si es vol buscar un altre usuari
            Console.WriteLine("Vols buscar un altre usuari? (si/no)");
            string resposta = Console.ReadLine().ToLower();

            if (resposta == "si")
            {
                RecuperarUsuari(); // Tornar a cridar la funció per a una nova cerca
            }
        }
    }
    static void MostrarUsuari(string[] dades)
    {
        Console.WriteLine("Usuari trobat:");
        Console.WriteLine($"Nom: {dades[0].Trim()}");
        // Aquí pots afegir altres dades de l'usuari
        Console.WriteLine("Mostrant durant 5 segons...");
        Thread.Sleep(5000); // Esperar 5 segons
    }

    static bool EsCoincidencia(string nomFitxer, string entradaUsuari)
    {
        // Si l'entrada d'usuari conté l'asterisc (*) la tractarem com a comodí
        if (entradaUsuari.Contains("*"))
        {
            // Verificar si el nom del fitxer coincideix amb la part de l'entrada de l'usuari abans de l'asterisc
            return nomFitxer.StartsWith(entradaUsuari.Substring(0, entradaUsuari.IndexOf("*")));
        }
        else
        {
            // Comparar el nom del fitxer amb l'entrada de l'usuari sense comodins
            return nomFitxer.Equals(entradaUsuari);
        }
    }
    static void ModificarUsuari()
    {
        Console.WriteLine("----- Modificar usuari -----");

        string nomUsuari;
        Console.WriteLine("Introdueix el nom de l'usuari a modificar:");
        nomUsuari = Console.ReadLine();

        List<string[]> usuarisTroba = BuscarUsuaris(nomUsuari);

        if (usuarisTroba.Count > 0)
        {
            Console.WriteLine("Usuaris trobats:");
            int contador = 1;

            foreach (string[] usuari in usuarisTroba)
            {
                Console.WriteLine($"{contador}. {usuari[0]} {usuari[1]}");
                contador++;
            }

            Console.WriteLine("Selecciona el número de l'usuari a modificar:");
            int opcioUsuari;

            while (!int.TryParse(Console.ReadLine(), out opcioUsuari) || opcioUsuari < 1 || opcioUsuari > usuarisTroba.Count)
            {
                Console.WriteLine("Opció no vàlida. Torna a intentar-ho:");
            }

            // Reduir en 1 per obtenir l'índex correcte a la llista
            opcioUsuari--;

            // Obté les dades de l'usuari seleccionat
            string[] usuariSeleccionat = usuarisTroba[opcioUsuari];

            // Mostra les dades actuals de l'usuari seleccionat
            MostrarDadesUsuari(usuariSeleccionat[0], usuariSeleccionat[1], usuariSeleccionat[2], usuariSeleccionat[3], usuariSeleccionat[4], usuariSeleccionat[5], Convert.ToInt32(usuariSeleccionat[6]));

            // Bucle per a modificar dades fins que l'usuari ho decideixi
            bool modificarMes = true;
            while (modificarMes)
            {
                // Pregunta quina dada volem modificar
                Console.WriteLine("Quina dada vols modificar? (nom/cognom/dni/telefon/dataNaix/correu)");
                string opcioModificar = Console.ReadLine().ToLower();

                switch (opcioModificar)
                {
                    case "nom":
                    case "cognom":
                    case "dni":
                    case "telefon":
                    case "dataNaix":
                    case "correu":
                        ModificarDadaUsuari(usuariSeleccionat, opcioModificar);
                        break;

                    default:
                        Console.WriteLine("Opció no vàlida.");
                        break;
                }

                // Pregunta si vol modificar més dades
                Console.WriteLine("Vols modificar una altra dada? (si/no)");
                string resposta = Console.ReadLine().ToLower();
                modificarMes = (resposta == "si");
            }

            // Mostra les dades actualitzades de l'usuari i espera 5 segons
            Console.WriteLine("\nDades de l'usuari actualitzades. Mostrant durant 5 segons...");
            MostrarDadesUsuari(usuariSeleccionat[0], usuariSeleccionat[1], usuariSeleccionat[2], usuariSeleccionat[3], usuariSeleccionat[4], usuariSeleccionat[5], Convert.ToInt32(usuariSeleccionat[6]));
            Thread.Sleep(5000);
        }
        else
        {
            Console.WriteLine("L'usuari no existeix.");
        }
    }

    static void ModificarDadaUsuari(string[] usuari, string opcioModificar)
    {
        string novaDada;

        switch (opcioModificar)
        {
            case "nom":
            case "cognom":
                Console.WriteLine($"Introdueix el nou {opcioModificar}:");
                novaDada = Console.ReadLine();
                ValidarNom(ref novaDada);
                break;

            case "dni":
                Console.WriteLine("Introdueix el nou DNI:");
                novaDada = Console.ReadLine();
                ValidarDNI(novaDada);
                break;

            case "telefon":
                Console.WriteLine("Introdueix el nou telèfon:");
                novaDada = Console.ReadLine();
                ValidarTelefon(novaDada);
                break;

            case "dataNaix":
                Console.WriteLine("Introdueix la nova data de naixement (format: dd/MM/yyyy):");
                novaDada = Console.ReadLine();
                ValidarDataNaixement(novaDada, out DateTime dataNaix);
                break;

            case "correu":
                Console.WriteLine("Introdueix el nou correu electrònic:");
                novaDada = Console.ReadLine();
                ValidarCorreu(novaDada);
                break;

            default:
                novaDada = string.Empty;
                break;
        }

        // Actualitza la dada a l'array usuari
        switch (opcioModificar)
        {
            case "nom":
                usuari[0] = novaDada;
                break;

            case "cognom":
                usuari[1] = novaDada;
                break;

            case "dni":
                usuari[2] = novaDada;
                break;

            case "telefon":
                usuari[3] = novaDada;
                break;

            case "dataNaix":
                usuari[4] = novaDada;
                break;

            case "correu":
                usuari[5] = novaDada;
                break;
        }
    }

    static void OrdenarAgenda()
    {
        List<string> llistatUsuaris = File.ReadAllLines("agenda.txt").ToList();
        llistatUsuaris.Sort();

        using (StreamWriter sw = new StreamWriter("agenda.txt"))
        {
            foreach (string usuari in llistatUsuaris)
            {
                sw.WriteLine(usuari);
            }
        }

        Console.WriteLine("Agenda ordenada alfabèticament.");
    }
    private static void Main(string[] args)
    {
        int opcio, num;
        string menuText;

        StreamWriter fitxer;
        fitxer = new StreamWriter(@".\altaUsuari.txt");

        Console.WriteLine("Selecciona una de les següents opcions");
        opcio = Convert.ToInt32(Console.ReadLine());

        menuText = MenuText();
        Console.WriteLine(menuText);

        while (int.TryParse(Console.ReadLine(), out opcio))
        {
            switch (opcio)
            {
                case 0:
                    Console.WriteLine("Final del programa");
                    return;

                case 2:
                    RecuperarUsuari();
                    break;

                default:
                    Console.WriteLine("Opció no vàlida. Torna a intentar-ho.");
                    break;
            }

            Console.WriteLine($"\nSelecciona una de les següents opcions:\n{menuText}");
        }
        fitxer.Close();
    }
}