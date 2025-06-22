using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo_prueba
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const int max_encargos = 25;
            string[,] encargos = new string[max_encargos, 5];
            int cantidad_registrados = 0;
            bool continuar = true;

            while (continuar)
            {
                Console.WriteLine("==== Argentinian Simulator ====");
                Console.WriteLine("1. Crear nuevo encargo");
                Console.WriteLine("2. Mostrar todos los encargos");
                Console.WriteLine("3. Asignar camión a encargo");
                Console.WriteLine("4. Mostrar encargos asignados");
                Console.WriteLine("5. Promedio de ganancia por sede");
                Console.WriteLine("6. Encargo con mayor distancia");
                Console.WriteLine("7. Filtrar encargos por código de camión");
                Console.WriteLine("8. Salir");
                Console.Write("Ingrese una opción: ");
                string opcion = Console.ReadLine();

                switch(opcion)
                {
                    case "1":
                        if (cantidad_registrados > max_encargos)
                        {
                            Console.WriteLine("\nNo se pueden ingresar mas encargos. Presione la tecla Enter. ");
                            Console.ReadLine();
                        }
                        else
                        {
                            int distancia = 0;
                            bool distanciaValida = false;

                            while (!distanciaValida)
                            {
                                Console.WriteLine("Ingrese la distancia en kilometros del encargo: ");
                                distancia = int.Parse(Console.ReadLine());

                                if (distancia > 0)
                                {
                                    distanciaValida = true;
                                }
                                else
                                {
                                    Console.WriteLine("La distancia no puede ser negativa. Es ilegal que los camiones circulen en reversa");
                                }
                            }

                            string sede = "";
                            bool sedeValida = false;
                            while (!sedeValida)
                            {
                                Console.Write("Ingrese la sede (1 = BSAS, 2 = BB, 3 = MDQ): ");
                                sede = Console.ReadLine();
                                if (sede == "1" || sede == "2" || sede == "3")
                                {
                                    sedeValida = true;
                                }
                                else
                                {
                                    Console.WriteLine("Sede inválida. Ingrese 1, 2 o 3.");
                                }
                            }

                            Console.WriteLine("Ingrese la ganancia esperada ($): ");
                            string ganancia = Console.ReadLine();

                            encargos[cantidad_registrados, 0] = ""; // Cod camion
                            encargos[cantidad_registrados, 1] = distancia.ToString();
                            encargos[cantidad_registrados, 2] = sede;
                            encargos[cantidad_registrados, 3] = ganancia;
                            encargos[cantidad_registrados, 4] = "0"; // Asignado

                            cantidad_registrados++;

                            Console.WriteLine("Encargo registrado exitosamente. Presione la tecla Enter.");
                            Console.ReadLine();
                        }
                            break;

                    case "2":
                        Console.WriteLine("\nTodos los encargos:");
                        Console.WriteLine("\n| Fila | Codcamion | Distancia | Sede | Ganancia | Asignado |");

                        for (int i = 0; i < cantidad_registrados; i++)
                        {
                            Console.WriteLine("| " + (i+1) + "   | " + encargos[i, 0] + "\t| " + encargos[i, 1] + "   | " + encargos[i, 2] + "   | " +    encargos[i, 3] + "   | " + encargos[i, 4] + "   |");
                        }
                        Console.WriteLine("\nPresione Enter para continuar.");
                        Console.ReadLine();
                        break;

                    case "3":
                        Console.WriteLine("\nEncargos no asignados: ");
                        Console.WriteLine("\n| Fila | Distancia | Sede | Ganancia |");
                        for (int i = 0;i  < cantidad_registrados; i++)
                        {
                            if(encargos[i, 4] == "0")
                            {
                                Console.WriteLine("| " + (i + 1) + "   | " + encargos[i, 1] + "\t| " + encargos[i, 2] + "   | " + encargos[i, 3] + "   |");
                            }
                        }

                        bool asignarValida = true;
                        while (asignarValida)
                        {
                            Console.WriteLine("Seleccione el numero del encargo a asignar: ");
                            int indexAsignar = int.Parse(Console.ReadLine());

                            if (encargos[indexAsignar - 1, 4] == "1")
                            {
                                Console.WriteLine("El encargo seleccionado ya tiene un camión asignado");
                            }
                            else
                            {
                                Console.WriteLine("Ingrese el codigo del camion: ");
                                string codcamion = Console.ReadLine();
                                encargos[indexAsignar - 1, 0] = codcamion;
                                encargos[indexAsignar - 1, 4] = "1";
                                Console.WriteLine("Camion asignado correctamente.");
                                asignarValida = false;
                            }

                        }
                        
                        Console.WriteLine("\nPresione Enter para continuar.");
                        Console.ReadLine();
                        break;

                    case "4":
                        Console.WriteLine("\nTodos los encargos:");
                        Console.WriteLine("\n| Fila | Codcamion | Distancia | Sede | Ganancia |");

                        for (int i = 0; i < cantidad_registrados; i++)
                        {
                            if (encargos[i, 4] == "1")
                            {
                                Console.WriteLine("| " + (i + 1) + "   | " + encargos[i, 0] + "\t| " + encargos[i, 1] + "   | " + encargos[i, 2] + "   | " + encargos[i, 3] + "   |");
                            }
                            
                        }
                        Console.WriteLine("\nPresione Enter para continuar.");
                        Console.ReadLine();
                        break;

                    case "5":
                        double[] suma = new double[3];
                        int[] cantidad = new int[3];

                        
                        for (int i = 0; i < cantidad_registrados; i++)
                        {
                            int sedeIndex = int.Parse(encargos[i, 2]) - 1;
                            suma[sedeIndex] += double.Parse(encargos[i, 3]);
                            cantidad[sedeIndex]++;
                        }

                        string[] sedes = { "BSAS", "BB", "MDQ" };
                        for(int i = 0; i < 3; i++)
                        {
                            double promedio = 0;
                            if (cantidad[i] > 0)
                            {
                                promedio = suma[i] / cantidad[i];
                            }
                            Console.WriteLine("Promedio de ganancia en " + sedes[i] + ": $" + promedio);
                        }
                        Console.WriteLine("\nPresione Enter para continuar.");
                        Console.ReadLine();
                        break;

                    case "6":
                        int maxDist = 0;
                        for (int i = 0; i < cantidad_registrados; i++)
                        {
                            int dist = int.Parse(encargos[i, 2]);
                            if (dist > maxDist)
                            {
                                maxDist = dist;
                            }
                        }

                        Console.WriteLine("\nEncargos con la mayor distancia (Si hay varios encargos con la misma distancia máxima, Se mostraran en pantalla):");
                        for (int i = 0; i < cantidad_registrados; i++)
                        {
                            if (int.Parse(encargos[i, 1]) == maxDist)
                            {
                                Console.WriteLine(i + ". Distancia: " + encargos[i, 1] + " km, Ganancia: $" + encargos[i, 3]);
                            }

                        }
                        Console.WriteLine("\nPresione Enter para continuar.");
                        Console.ReadLine();
                        break;

                    case "7":
                        Console.WriteLine("Ingrese el codigo del camion a buscar: ");
                        string codigo = Console.ReadLine();
                        Console.WriteLine("Encargos del camion " + codigo + ": ");

                        for(int i = 0; i < cantidad_registrados; i++)
                        {
                            if (encargos[i,0] == codigo)
                            {
                                Console.WriteLine("| " + (i + 1) + "   | " + encargos[i, 1] + "   | " + encargos[i, 2] + "   | " + encargos[i, 3] + "   |");
                            }
                        }
                        break;

                    case "8":
                        continuar = false;
                        break;

                    default:
                        Console.WriteLine("Opción no válida. Presione Enter.");
                        break;
                }
            }
            Console.ReadKey();
        }
    }
}