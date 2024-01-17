using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Menu_Matematic
{
    internal class Program
    {
        static string MenuText()
        {


            string menu =

                "----------Menu----------\n" +
                "|                      |\n" +
                "| 1.Màxim              |\n" +
                "| 2.Mcd                |\n" +
                "| 3.Mcm                |\n" +
                "| 4.Factorial          |\n" +
                "| 5.Combinatori        |\n" +
                "| 6.MostrarDivisorMajor|\n" +
                "| 7.EsPrimer           |\n" +
                "| 8.NPrimersPrimers    |\n" +
                "|                      |\n" +
                "| 0.Sortir             |\n" +
                "------------------------\n";
            return menu;
        }
        static int Maxim()
        {
            int valor, major = int.MinValue;
            Console.WriteLine("Digues un valor");
            valor = Convert.ToInt32(Console.ReadLine());

            //Bucle per a buscar el major d'una sèrie de nums
            do
            {
                if (valor > major)
                {
                    major = valor;
                }
                Console.WriteLine("Torna a escriure un valor (per acabar escriu un -1)");
                valor = Convert.ToInt32(Console.ReadLine());
            } while (valor != -1);
            return major;
        }

        static int Mcd()
        {
            int divisor1, divisor2, mcd = 0, i = 1;

            Console.WriteLine("Digues un número");
            divisor1 = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Digues un altre número");
            divisor2 = Convert.ToInt32(Console.ReadLine());

            //Bucle per a veure quin num coincideix en ser el divisor dels dos nums entrats
            while (i <= divisor1 && i <= divisor2)
            {
                if (divisor1 % i == 0 && divisor2 % i == 0)
                {
                    mcd = i;
                }
                i++;
            }
            return mcd;
        }
        static int Mcm()
        {
            int mcm, num1, num2;

            Console.WriteLine("Escriu el primer num");
            num1 = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Escriu el segon num");
            num2 = Convert.ToInt32(Console.ReadLine());

            //comprovació de grandària
            if (num2 > num1)
            {
                int aux = num1;
                num1 = num2;
                num2 = aux;
            }

            int i = num1;

            //Bucle per a trobar el mcm
            while (i % num1 != i % num2)
            {
                i++;
            }
            mcm = i;
            return mcm;
        }
        static int Factorial()
        {
            int num, factorial = 1;
            Console.WriteLine("Digues un num");
            num = Convert.ToInt32(Console.ReadLine());

            //Bucle per fer el factorial
            for (int i = 1; i < num; i++)
            {
                factorial = i * factorial;
            }
            return factorial;
        }
        static double Factorial(double valor) 
        {
            double num = valor;
            for (int i = 1; i < num; i++)
            {
                num = i * num;
            }
            return num;
        }
        static double Factorial(double valor, double valor2) 
        {
            
            double res = 0;
            for (int i = 0; i < valor; i++)
            {
                res = i * (valor - valor2);
            }
            return res;
        }
        static double Combinatori()
        {
            double operació, n, m, resta, facN, facM, facResta;

            Console.WriteLine("Digues el valor de n");
            n = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Digues el valor de m");
            m = Convert.ToInt32(Console.ReadLine());

            //Comprovar si n és més gran que m
            if (n < m)
            {
                double aux = n;
                n = m;
                m = aux;
            }
            resta = n - m;
            //Factoritzar els numeros
            facN = Factorial(n);
            facM = Factorial(m);
            facResta = Factorial(n, m);
            //fer la operació n! / (n-m)!*m!
            operació = n / m * resta;
            return operació;
        }
        static int DivisorMajor()
        {
            int num, divisorMajor = 0;
            Console.WriteLine("Digues el número del que vols saber el seu divisor major");
            num = Convert.ToInt32(Console.ReadLine());
            for (int i = 1; i < num / 2; i++)
            {
                if (num % i == 0)
                {
                    divisorMajor = i;
                }
            }
            return divisorMajor;
        }

        static void EsPrimer()
        {
            int primer, divisors = 0;

            Console.WriteLine("Digues un número");
            primer = Convert.ToInt32(Console.ReadLine());

            // Bucle per a saber el nombre de divisors total
            for (int i = 2; i <= primer / 2; i++)
            {
                if (primer % i == 0)
                {
                    divisors++;
                }
            }

            // Serà primer si només té 2 divisors, és a dir, 1 i ell mateix
            if (divisors == 0)
            {
                Console.WriteLine("És primer");
            }
            else
            {
                Console.WriteLine("No és primer");
            }
        }
        static bool EsPrimer(int valor)
        {
            int num = valor;
            bool primer = true;
            for (int i = 2; i <= num / 2; i++)
            {
                if (num % i == 0)
                {
                    primer = false;
                }
            }
            return primer;
        }

        static void NPrimersPrimers()
        {
            int n, primers = 0;
            int actual = 2; // Iniciem amb el primer nombre primer

            Console.WriteLine("Digues un num");
            n = Convert.ToInt32(Console.ReadLine());

            // Bucle per a repetir fins a trobar els primers n nombres primers
            while (primers < n)
            {
                int divisors = 0;

                // Bucle per provar si el nombre actual és primer
                for (int i = 2; i <= actual / 2; i++)
                {
                    if (actual % i == 0)
                    {
                        divisors++;
                    }
                }

                if (divisors == 0)
                {
                    Console.Write($"{actual} ");
                    primers++;
                }

                // Passar al següent nombre
                actual++;
            }

            Console.WriteLine(); // Nova línia després d'imprimir els primers n nombres primers
        }

        private static void Main(string[] args)
        {
            int opcio, num;
            double num2;
            string menuText = MenuText();
            Console.WriteLine("Selecciona una de les següents opcions:\n" + menuText);
            opcio = Convert.ToInt32(Console.ReadLine());

            
                switch (opcio)
                {
                    case 0:
                        Console.WriteLine("Final del programa");
                        break;

                    case 1:
                        num = Maxim();
                        Console.WriteLine("El màxim és " + num);
                        break;

                    case 2:
                        num = Mcd();
                        Console.WriteLine("El mcd és " + num);
                        break;

                    case 3:
                        num = Mcm();
                        Console.WriteLine("El mcm és " + num);
                        break;

                    case 4:
                        num = Factorial();
                        Console.WriteLine("El factorial és " + num);
                        break;

                    case 5:
                        num2 = Combinatori();
                        Console.WriteLine("El resultat final és " + num2);
                        break;

                    case 6:
                        num = DivisorMajor();
                        Console.WriteLine("El divisor major és " + num);
                        break;

                    case 7:
                        EsPrimer();
                        break;

                    case 8:
                        NPrimersPrimers();
                        break;

                    default:
                        Console.WriteLine("ERROR");
                        break;
                }
            
            
        }
    }
}
