// Clase que representa las Torres con un nombre y una pila de discos
public class Torre
{
    public string Nombre { get; }                 // Nombre de la torre (A, B o C)
    public Stack<int> Discos { get; }             // Pila que representa los discos

    public Torre(string nombre)
    {
        Nombre = nombre;
        Discos = new Stack<int>();               // Inicializa la pila de discos
    }

    // Método para mostrar los discos actualmente en la torre
    public void Mostrar()
    {
        Console.Write($"Torre {Nombre}: ");
        foreach (var disco in Discos)
        {
            Console.Write(disco + " ");
        }
        Console.WriteLine();
    }
}

// Clase principal que gestiona el proceso de las Torres de Hanoi
public class TorresDeHanoi
{
    private Torre origen;
    private Torre auxiliar;
    private Torre destino;
    private int contadorMovimientos = 0;         // Contador para llevar registro del número de movimientos realizados
    private int totalPasos = 0;                  // Total de pasos calculado según la fórmula: 2^n - 1

    public TorresDeHanoi(int numDiscos)
    {
        // Inicialización de las tres torres
        origen = new Torre("A");
        auxiliar = new Torre("B");
        destino = new Torre("C");

        // Se agregan los discos a la torre de origen en orden descendente (mayor abajo)
        for (int i = numDiscos; i >= 1; i--)
        {
            origen.Discos.Push(i);
        }

        // Cálculo del número total de pasos requeridos para resolver las Torres de Hanoi
        totalPasos = (int)Math.Pow(2, numDiscos) - 1;

        Console.WriteLine($"Estado inicial (Total de movimientos necesarios: {totalPasos}):");
        MostrarTorres();
        Console.WriteLine();

        // Inicia el algoritmo de recursividad para mover los discos
        MoverDiscos(numDiscos, origen, destino, auxiliar);
    }

    // Método recursivo que ejecuta los movimientos de discos
    private void MoverDiscos(int n, Torre origen, Torre destino, Torre auxiliar)
    {
        if (n == 1)
        {
            // Caso base: mover un solo disco directamente
            contadorMovimientos++;
            int disco = origen.Discos.Pop();            // Quitar disco de la torre de origen
            destino.Discos.Push(disco);                 // Colocar disco en la torre de destino

            // Mostrar el movimiento realizado
            Console.WriteLine($"Movimiento # {contadorMovimientos}: Mover disco {disco} de Torre {origen.Nombre} a Torre {destino.Nombre}");
            MostrarTorres();
        }
        else
        {
            // Paso 1: mover n-1 discos al auxiliar
            MoverDiscos(n - 1, origen, auxiliar, destino);

            // Paso 2: mover el disco restante al destino
            MoverDiscos(1, origen, destino, auxiliar);

            // Paso 3: mover los n-1 discos desde auxiliar al destino
            MoverDiscos(n - 1, auxiliar, destino, origen);
        }
    }

    // Muestra el estado actual de las tres torres
    private void MostrarTorres()
    {
        origen.Mostrar();
        auxiliar.Mostrar();
        destino.Mostrar();
        Console.WriteLine("----------------------------------------------------");
    }
}

// Clase principal del Programa
class ProgramaHanoi
{
    static void Main()
    {
        // Solicita al usuario que ingrese el número de discos
        Console.Write("Ingrese el número de discos (mínimo 1): ");
        
        // Verifica que la entrada sea válida y mayor que 0
        if (int.TryParse(Console.ReadLine(), out int numDiscos) && numDiscos > 0)
        {
            // Crea la instancia del juego y ejecuta el proceso
            TorresDeHanoi hanoi = new TorresDeHanoi(numDiscos);
        }
        else
        {
            Console.WriteLine("Entrada inválida. El número debe ser un entero positivo.");
        }
    }
}
