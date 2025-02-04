using System;
using System.Collections.Generic;

class Program
{
    static Dictionary<int, (string nombre, double precio, int cantidad)> inventario = new Dictionary<int, (string, double, int)>();

    static void Main()
    {
        int opcion;
        do
        {
            Console.WriteLine("\n--- Menú ---");
            Console.WriteLine("1. Agregar artículo");
            Console.WriteLine("2. Facturar");
            Console.WriteLine("3. Reporte de artículos con baja cantidad");
            Console.WriteLine("4. Salir");
            Console.Write("Seleccione una opción: ");
            opcion = int.Parse(Console.ReadLine());

            switch (opcion)
            {
                case 1:
                    AgregarArticulo();
                    break;
                case 2:
                    Facturar();
                    break;
                case 3:
                    ReporteFaltante();
                    break;
                case 4:
                    Console.WriteLine("Saliendo...");
                    break;
                default:
                    Console.WriteLine("Opción inválida. Intente nuevamente.");
                    break;
            }
        } while (opcion != 4);
    }

    static void AgregarArticulo()
    {
        Console.Write("Ingrese código del artículo: ");
        int codigo = int.Parse(Console.ReadLine());

        if (inventario.ContainsKey(codigo))
        {
            Console.WriteLine("Error: Código ya registrado.");
            return;
        }

        Console.Write("Ingrese el nombre del artículo: ");
        string nombre = Console.ReadLine();

        Console.Write("Ingrese el precio: ");
        double precio = double.Parse(Console.ReadLine());

        Console.Write("Ingrese la cantidad en inventario: ");
        int cantidad = int.Parse(Console.ReadLine());

        inventario[codigo] = (nombre, precio, cantidad);
        Console.WriteLine("Artículo agregado correctamente.");
    }

    static void Facturar()
    {
        Console.Write("Ingrese código del artículo: ");
        int codigo = int.Parse(Console.ReadLine());

        if (!inventario.ContainsKey(codigo))
        {
            Console.WriteLine("Error: Código no encontrado.");
            return;
        }

        Console.Write("Ingrese cantidad a comprar: ");
        int cantidad = int.Parse(Console.ReadLine());

        if (cantidad > inventario[codigo].cantidad)
        {
            Console.WriteLine("Error: Cantidad insuficiente en inventario.");
            return;
        }

        double subtotal = inventario[codigo].precio * cantidad;
        double iva = subtotal * 0.1; // IVA del 10%
        double total = subtotal + iva;

        inventario[codigo] = (inventario[codigo].nombre, inventario[codigo].precio, inventario[codigo].cantidad - cantidad);

        Console.WriteLine($"Subtotal: {subtotal}");
        Console.WriteLine($"IVA: {iva}");
        Console.WriteLine($"Total a pagar: {total}");
    }

    static void ReporteFaltante()
    {
        Console.WriteLine("\n--- Reporte de artículos con baja cantidad ---");
        foreach (var item in inventario)
        {
            if (item.Value.cantidad <= 5) // Umbral de stock bajo
            {
                Console.WriteLine($"Código: {item.Key}, Nombre: {item.Value.nombre}, Cantidad: {item.Value.cantidad}");
            }
        }
    }
}
