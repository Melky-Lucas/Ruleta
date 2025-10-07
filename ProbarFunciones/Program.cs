
namespace ProbarFunciones
{
    class Program
    {
        static void Main(string[] args)
        {
            string flecha = "<--------";
            Random random = new();

            string[][] estudiantes = [["Juan", "Perez", "12345678"],
                          ["Maria", "Gomez", "87654321"],
                          ["Luis", "Martinez", "11223344"]];

            int apuntadorFlecha = 0;
            int contador = (random.Next(3, 11) * estudiantes.Length) + random.Next(1, 3);

            int indexElegido = (contador % estudiantes.Length) - 1;
            string[] estElegido = estudiantes[indexElegido];

            int delay = 300;

            while (contador > 0)
            {
                Console.Clear();
                Console.WriteLine("Lista de Estudiantes:");

                for (int i = 0; i < estudiantes.Length; i++)
                {
                    string[] estudiante = estudiantes[i];

                    Console.WriteLine($"-{estudiante[0]} {(apuntadorFlecha == i ? flecha : "")}");
                    Console.WriteLine();
                }

                Thread.Sleep(delay);
                apuntadorFlecha = apuntadorFlecha < estudiantes.Length - 1 ? apuntadorFlecha + 1 : 0;
                contador--;


                if (contador < 9)
                    delay += 50;
            }

            Console.WriteLine($"{estElegido[0]} {estElegido[1]} ha sido elegido");


        }
    }
}