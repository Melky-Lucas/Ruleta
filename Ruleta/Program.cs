bool salir = false;
Random random = new();


string[] carreras = ["Desarrollo de Software", "Multimedia"];
string[] roles = ["Desarrollador en Vivo", "Facilitador en vivo"];
string[][] estudiantes = new string[5][];
for (int i = 0; i < estudiantes.Length; i++)
{
    estudiantes[i] = new string[4];
}
estudiantes[0] = ["Darlyn", "12345678", $"{carreras[0]}", $""];
estudiantes[1] = ["Erick", "12245632", $"{carreras[0]}", $""];
estudiantes[2] = ["Tatiana", "12545623", $"{carreras[0]}", $""];
estudiantes[3] = ["Juan", "12325623", $"{carreras[0]}", $""];
estudiantes[4] = ["Samara", "12425624", $"{carreras[1]}", $""];


string[][][] historialRuleta = [];

//Animacion
void ruletaAnimacion(int indexElegido)
{
    string flecha = "<--------";

    int apuntadorFlecha = 0;
    int contador = (random.Next(3, 8) * estudiantes.Length) + indexElegido;
    int delay = 300;

    while (contador > 0)
    {
        Console.Clear();
        Console.WriteLine($"Eligiendo al azar...");

        for (int i = 0; i < estudiantes.Length; i++)
        {


            string[] estudiante = estudiantes[i];

            Console.WriteLine($"- {estudiante[0]} {(apuntadorFlecha == i ? flecha : " ")}");
            Console.WriteLine();

            if (estudiantes.Length == 1)
            {
                contador = 0;
                break;
            }
            
        }

        Thread.Sleep(delay);
        apuntadorFlecha = apuntadorFlecha < estudiantes.Length - 1 ? apuntadorFlecha + 1 : 0;
        contador--;


        if (contador < 9)
            delay += 50;
    }
}

void presioneParaContinuar()
{
    Console.Write("Presione cualquier tecla para continuar...");
    Console.ReadKey();
}

int obtenerOpcionNumerica(string mensaje)
{
    bool esValido;

    Console.Write(mensaje);
    esValido = int.TryParse(Console.ReadLine(), out int num);

    if (!esValido)
    {
        num--;
    }

    return num;
}

string obtenerNombre(string mensaje)
{
    string nombre;
    bool esValido;

    do
    {
        Console.Clear();
        Console.Write(mensaje);
        nombre = Console.ReadLine() ?? "";

        esValido = string.Empty != nombre.Trim() && nombre.Trim().Length > 2;

        if (!esValido)
        {
            Console.Clear();
            Console.WriteLine("Ingrese un nombre valido\n");

            presioneParaContinuar();
        }

    } while (!esValido);

    return nombre;
}

string[] obtenerMatriculas()
{
    string[] matriculas = [];

    for (int i = 0; i < estudiantes.Length; i++)
    {
        string[] estudiante = estudiantes[i];

        matriculas = agregarItem(matriculas, estudiante[1]);
    }

    return matriculas;
}

string obtenerMatricula(string mensaje)
{
    string matricula;
    bool esValida;
    string[] matriculas = obtenerMatriculas();

    do
    {
        Console.Clear();
        Console.Write(mensaje);
        matricula = Console.ReadLine() ?? "";

        esValida = matricula.Length > 7 && !matriculas.Contains(matricula);

        Console.Clear();
        if (!esValida)
        {
            Console.WriteLine("La matricula debe por lo menos 8 caracteres y no puede repetirse.\n");

            presioneParaContinuar();
        }

    } while (!esValida);

    return matricula;
}

bool confirmarAccion(string mensaje)
{
    string? opcion;
    bool eleccion = false;

    do
    {
        Console.Clear();
        Console.Write($"{mensaje}(S/N): ");
        opcion = Console.ReadLine()?.ToUpper();

        Console.Clear();
        if (opcion == "S") {
            eleccion = true;
            Console.WriteLine("Cargando... \n");
        }
        else if (opcion == "N")
            Console.WriteLine("Operacion cancelada\n");

        else
        {
            Console.WriteLine("Opcion no valida\n");

            presioneParaContinuar();
        }
    } while (opcion != "S" && opcion != "N");

    return eleccion;
}

bool todosEstTienenRoles()
{
    bool todosTienesRoles = true;

    for (int i = 0; i < estudiantes.Length; i++)
    {
        string[] estudiante = estudiantes[i];
        string[] rolesEstudiante = estudiante[3].Split(",");

        for (int j = 0; j < roles.Length; j++)
        {
            if (!rolesEstudiante.Contains(roles[j]))
            {
                todosTienesRoles = false;
                break;
            }
        }

        if (!todosTienesRoles) break;
    }

    return todosTienesRoles;
}

string[][] agregarArray(string[][] array, string[] item)
{
/*  string[][] newArray = new string[array.Length + 1][];

    for (int i = 0; i < newArray.Length; i++)
    {
        newArray[i] = new string[array[0].Length];
    } */

    string[][] newArray = [.. array, item];
    
    return newArray;
}

string[] agregarItem(string[] array, string item)
{
    string[] newArray = [.. array, item];

    return newArray;
}

string[][] eliminarArray(string[][] array, int index)
{

    array[index] = [];

    string[][] arrayCopy = array;

    array = Array.Empty<string[]>(); // En caso de estar vacio el array dentro del array de arrays

    for (int i = 0; i < arrayCopy.Length; i++)
    {
        if (arrayCopy[i] == Array.Empty<string>())
            continue;

        array = agregarArray(array, arrayCopy[i]);
    }

    return array;
}

string[] eliminarItem(string[] array, int index)
{
    array[index] = "";

    string[] newArray = array;

    array = [];

    for (int i = 0; i < newArray.Length; i++)
    {
        if (string.Empty == newArray[i])
            continue;

        array = agregarItem(array, newArray[i]);
    }

    return array;
}

void mostrarEstudiantes()
{
    if (estudiantes.Length == 0)
    {
        Console.WriteLine("No hay estudiantes registrados.\n");
        return;
    }

    Console.WriteLine("Lista de estudiantes:");

    for (int i = 0; i < estudiantes.Length; i++)
    {
        if (estudiantes[i] == Array.Empty<string>()) continue;

        string[] estudiante = estudiantes[i];
        string[] rolesEstudiante = estudiantes[i][3].Split(",");

        Console.WriteLine($"Estudiante {i + 1}:");
        Console.WriteLine($"Nombre: \t{estudiante[0]}\nMatrícula: \t{estudiante[1]}\nCarrera: \t{estudiante[2]}\n");

        Console.WriteLine("Roles asignados: ");

        if (rolesEstudiante == Array.Empty<string>() || rolesEstudiante[0] == string.Empty)
            Console.WriteLine("No hay roles asignados a este estudiante.");

        else
        {
            for (int j = 0; j < rolesEstudiante.Length; j++)
            {
                Console.WriteLine($"- {rolesEstudiante[j]}");
            }
        }
        Console.WriteLine("\n");
    }
}

void mostrarEstudiantesSinRoles()
{
    Console.WriteLine("Lista de Estudiantes");

    for (int i = 0; i < estudiantes.Length; i++)
    {
        string[] estudiante = estudiantes[i];
        Console.WriteLine($"Matricula: {estudiante[1]} Nombre: {estudiante[0]}");
    }
}

void mostrarRoles()
{
    if (roles.Length == 0)
    {
        Console.WriteLine("No hay roles registrados.\n");
        return;
    }

    Console.WriteLine("Roles\t\t\t Hay estudiantes con este rol?\n");

    for (int i = 0; i < roles.Length; i++)
    {
        string hayRoles = hayEstConEsteRol(roles[i]) ? "Si" : "No";
        Console.WriteLine($"- {roles[i]}\t\t\t {hayRoles}");
    }
    Console.WriteLine();
}

void mostrarRolesNumerados()
{
    Console.WriteLine("Lista de Roles");

    for (int i = 0; i < roles.Length; i++)
    {
        string rol = roles[i];

        Console.WriteLine($"{i + 1}- {rol}");
    }
}

void mostrarCarreras()
{
    Console.WriteLine("Lista de Carreras");

    for (int i = 0; i < carreras.Length; i++)
    {
        string carrera = carreras[i];

        Console.WriteLine($"{i + 1}- {carrera}");
    }
}

void mostrarHistorial()
{
    if (historialRuleta.Length == 0)
    {
        Console.WriteLine("Todavia no has hecho tiradas de ruleta\n");
        return;
    }

    Console.WriteLine("Historial de tiradas");
    for (int i = 0; i < historialRuleta.Length; i++)
    {
        Console.WriteLine($"Tirada #{i + 1}");
        int cantEstElegidos = historialRuleta[i].Length;

        for (int j = 0; j < cantEstElegidos; j++)
        {
            string[] historialRegistro = historialRuleta[i][j];
            string[] estudiante = estudiantes[buscarEstPorMatricula(historialRegistro[0])];

            Console.WriteLine($"-El estudiante {estudiante[0]} con matricula {estudiante[1]} fue elegido con el rol de {historialRegistro[1]}");
        }

        Console.WriteLine();        
    }
}

bool hayEstConEsteRol(string rol)
{
    bool rolExiste = false;

    for (int i = 0; i < estudiantes.Length; i++)
    {
        string[] estudiante = estudiantes[i];
        string[] rolesEstudiante = estudiante[3].Split(",");

        if (rolesEstudiante.Contains(rol))
        {
            rolExiste = true;
            break;
        }
    }

    return rolExiste;
}

string[][] filtrarEstudiantesPorRol(string rol)
{
    string[][] estudiantesSinRol = [];

    for (int i = 0; i < estudiantes.Length; i++)
    {
        string[] estudianteRoles = estudiantes[i][3].Split(",");

        if (!estudianteRoles.Contains(rol))
        {
            estudiantesSinRol = agregarArray(estudiantesSinRol, estudiantes[i]);
        }
    }

    return estudiantesSinRol;
}

int buscarEstPorMatricula(string matricula)
{
    int estudianteIndex = -1;

    for (int i = 0; i < estudiantes.Length; i++)
    {
        if (matricula == estudiantes[i][1])
        {
            estudianteIndex = i;
        }
    }

    return estudianteIndex;
}

void agregarEstudiante()
{
    string[] est = estudianteForm();

    bool realizarAccion = confirmarAccion("Seguro que desea agregar el estudiante? ");

    if (realizarAccion)
    {
        estudiantes = agregarArray(estudiantes, est);

        Console.WriteLine("Estudiante agregado exitosamente.\n");
    }
}

void agregarRoles()
{
    string rolNombre = obtenerNombre("Ingrese el nombre del rol? ");

    bool realizarAccion = confirmarAccion("Estas seguro de que quieres agregarlo? ");

    if (realizarAccion)
    {
        roles = agregarItem(roles, rolNombre);
        Console.WriteLine("Se ha agregado correctamente\n");
    }
}

void actualizarEstudiante()
{
    if (estudiantes.Length == 0)
    {
        Console.WriteLine("No hay estudiantes para actualizar.\n");
        return;
    }

    string matricula;
    string[] matriculas = obtenerMatriculas();

    do
    {
        Console.Clear();
        mostrarEstudiantesSinRoles();
        Console.WriteLine();

        Console.WriteLine("Solo ingrese 0 para salir");
        Console.Write("Ingrese la matrícula del estudiante para actualizar: ");
        matricula = Console.ReadLine() ?? "";

        if (matricula == "0")
        {
            Console.Clear();
            return;
        }
        if (!matriculas.Contains(matricula))
        {
            Console.Clear();
            Console.WriteLine("No se encontro un estudiante con esa matricula.\n");

            presioneParaContinuar();
        }

    } while (!matriculas.Contains(matricula));

    Console.Clear();

    for (int i = 0; i < estudiantes.Length; i++)
    {
        if (estudiantes[i][1] == matricula)
        {
            Console.WriteLine($"Actualizando datos del estudiante con nombre {estudiantes[i][0]}...");

            string[] nuevoEstudiante = estudianteForm();

            bool realizarAccion = confirmarAccion("Estas seguro de actualizar el estudiante? ");

            if (realizarAccion)
            {
                estudiantes[i] = nuevoEstudiante;

                Console.WriteLine("Datos actualizados exitosamente.\n");
            }

            return;
        }
    }
}

void actualizarRoles()
{
    if (roles.Length == 0)
    {
        Console.WriteLine("No hay roles para actualizar\n");
        return;
    }

    int opcion;
    bool opcionValida;

    do
    {
        Console.Clear();
        mostrarRolesNumerados();
        Console.WriteLine();

        Console.WriteLine("Solo ingrese 0 para salir");
        opcion = obtenerOpcionNumerica($"Elija el rol que va actualizar (1 - {roles.Length}): ");


        opcionValida = opcion > 0 && opcion <= roles.Length;

        if (opcion == 0)
        {
            Console.Clear();
            return;
        }

        if (!opcionValida)
        {
            Console.Clear();
            Console.WriteLine("Porfavor elija entre las opciones disponibles.\n");

            presioneParaContinuar();            
        }

    } while (!opcionValida);

    int rolIndex = opcion - 1;

    string antiguoRol = roles[rolIndex];
    string nuevoNombreRol = obtenerNombre("Actualize el nombre del rol: ");

    if (confirmarAccion("Estas seguro de actualizar? "))
    {
        roles[rolIndex] = nuevoNombreRol;
        cambiarRolEnEstudiantes(antiguoRol, nuevoNombreRol);
        cambiarRolEnHistorial(antiguoRol, nuevoNombreRol);
        Console.WriteLine("Actualizado correctamente\n");
    }

}

void eliminarEstudiante()
{
    if (estudiantes.Length == 0)
    {
        Console.WriteLine("No hay estudiantes para eliminar.\n");
        return;
    }

    string matricula;
    string[] matriculas = obtenerMatriculas();

    do
    {
        Console.Clear();
        mostrarEstudiantesSinRoles();
        Console.WriteLine();

        Console.WriteLine("Solo ingrese 0 para salir");
        Console.Write("Ingrese la matrícula del estudiante para eliminar: ");
        matricula = Console.ReadLine() ?? "";

        if (matricula == "0")
        {
            Console.Clear();
            return;
        }
        if (!matriculas.Contains(matricula))
        {
            Console.Clear();
            Console.WriteLine("No se encontro un estudiante con esa matricula.\n");

            presioneParaContinuar();
        }

    } while (!matriculas.Contains(matricula));

    Console.Clear();

    for (int i = 0; i < estudiantes.Length; i++)
    {
        if (estudiantes[i][1] == matricula)
        {
            Console.WriteLine($"Eliminando datos del estudiante con nombre {estudiantes[i][0]}...");

            bool realizarAccion = confirmarAccion("Estas seguro de eliminar el estudiante? ");

            if (realizarAccion)
            {
                estudiantes = eliminarArray(estudiantes, i);

                Console.WriteLine("Datos actualizados exitosamente.\n");
            }

            return;
        }
    }
}

void eliminarRoles()
{
    if (roles.Length == 0)
    {
        Console.WriteLine("No hay roles para eliminar.\n");
        return;
    }

    int opcion;
    bool opcionValida;

    do
    {
        Console.Clear();
        mostrarRolesNumerados();
        Console.WriteLine();

        Console.WriteLine("Solo ingrese 0 para salir");
        opcion = obtenerOpcionNumerica($"Elija el rol que va a eliminar (1 - {roles.Length}): ");

        opcionValida = opcion > 0 && opcion <= roles.Length;
        
        if (opcion == 0)
        {
            Console.Clear();
            return;
        }

        if (!opcionValida)
        {
            Console.Clear();
            Console.WriteLine("Porfavor elija entre las opciones disponibles.\n");

            presioneParaContinuar();
        }

    } while (!opcionValida);

    int rolIndex = opcion - 1;
    string antiguoRol = roles[rolIndex];

    if (confirmarAccion("Estas seguro de eliminarlo? "))
    {
        roles = eliminarItem(roles, rolIndex);
        cambiarRolEnEstudiantes(antiguoRol, "");
        cambiarRolEnHistorial(antiguoRol, "");
        
        Console.WriteLine("Eliminado correctamente\n");
    }
}

string[] estudianteForm()
{
    string[] result;

    string nombre = obtenerNombre("Ingrese su nombre: ");
    string matricula = obtenerMatricula("Ingrese la matricula: ");


    int opcion;
    bool opcionValida;
    do
    {
        Console.Clear();
        mostrarCarreras();
        Console.WriteLine();

        opcion = obtenerOpcionNumerica($"Elija su carrera de estudio (1 - {carreras.Length}): ");
        opcionValida = opcion > 0 && opcion <= carreras.Length;

        if (!opcionValida)
        {
            Console.Clear();
            Console.WriteLine("Porfavor elija entre las opciones disponibles.\n");

            presioneParaContinuar();
        }

    } while (!opcionValida);

    int carreraIndex = opcion - 1;
    string carrera = carreras[carreraIndex];

    result = [nombre, matricula, carrera, $""];

    return result;
}

void ruleta()
{
    if (estudiantes.Length == 0 || roles.Length == 0)
    {
        if (estudiantes.Length == 0)
            Console.WriteLine("No hay estudiantes registrados para jugar a la ruleta.\n");
        if (roles.Length == 0)
            Console.WriteLine("No hay roles registrados para jugar a la ruleta.\n");
        return;
    }

    if (todosEstTienenRoles())
    {
        Console.WriteLine("Todos los estudiantes tienen sus roles asignados.\n");
        return;
    }

    bool salirRuleta;
    do
    {
        string[] estudiantesElegidos = [];
        
        string[][] elecciones = [];

        for (int i = 0; i < roles.Length; i++)
        {
            Console.Clear();

            string[][] estudiantesSinRol = filtrarEstudiantesPorRol(roles[i]);
            int cantestudiantesSinRol = estudiantesSinRol.Length;

            for (int j = cantestudiantesSinRol - 1; j >= 0; j--)
            {
                if (estudiantesElegidos.Contains(estudiantesSinRol[j][1]))
                {
                    estudiantesSinRol = eliminarArray(estudiantesSinRol, j);
                }
            }

            if (estudiantesSinRol == Array.Empty<string[]>())
            {
                Console.WriteLine($"Todos los estudiantes tienen asignado este rol ({roles[i]}) o ya han sido elegido en otras tiradas.\n");

                presioneParaContinuar();
                continue;
            }

            int estudianteRamdomIndex = random.Next(estudiantesSinRol.Length);
            string[] estudianteRamdom = estudiantesSinRol[estudianteRamdomIndex];

            int estudianteIndex = buscarEstPorMatricula(estudianteRamdom[1]);
            string[] estudiante = estudiantes[estudianteIndex];

            estudiantesElegidos = agregarItem(estudiantesElegidos, estudiante[1]);

            asignarRoles(estudianteIndex, roles[i]);

            //Animacion
            ruletaAnimacion(estudianteIndex + 1);

            Console.WriteLine($"El alumno seleccionado es: {estudiante[0]} con matrícula {estudiante[1]} y carrera {estudiante[2]}");
            Console.WriteLine($"Y se la ha asignado el rol de {roles[i]}\n");

            //Se agrega la matricula entre los elegidos para luego aparecer en el historial
            elecciones = agregarArray(elecciones, [estudiante[1], roles[i]]);

            presioneParaContinuar();
        }

        // Se agregan los elegidos al historial
        historialRuleta = [.. historialRuleta, elecciones];

        salirRuleta = confirmarAccion("Quieres salir de la ruleta? ");
        Console.Clear();

        if (!salirRuleta)
        {
            if (todosEstTienenRoles())
            {
                salirRuleta = true;
                Console.Clear();
                Console.WriteLine("Todos los estudiantes tienen sus roles asignados.\n");

                presioneParaContinuar();
            }
        }

    } while (!salirRuleta);

}

void asignarRoles(int estudianteIndex, string rol)
{
    string separador = string.IsNullOrEmpty(estudiantes[estudianteIndex][3]) ? string.Empty : ",";
    
    estudiantes[estudianteIndex][3] += separador + rol;
}

void cambiarRolEnEstudiantes(string rolAntiguo, string rolNuevo)
{
    for (int i = 0; i < estudiantes.Length; i++)
    {
        string[] estudianteRoles = estudiantes[i][3].Split(",");

        if (!estudianteRoles.Contains(rolAntiguo))
            continue;

        estudiantes[i][3] = "";

        for (int j = 0; j < estudianteRoles.Length; j++)
        {
            if (estudianteRoles[j] == rolAntiguo)
            {
                if (rolNuevo == string.Empty)
                    continue;

                estudianteRoles[j] = rolNuevo;
            }

            asignarRoles(i, estudianteRoles[j]);
        }
    }
}

void cambiarRolEnHistorial(string rolAntiguo, string rolNuevo)
{
    if (rolNuevo == string.Empty)
        rolNuevo = rolAntiguo + " (eliminado)";

    for (int i = 0; i < historialRuleta.Length; i++)
    {
        for (int j = 0; j < historialRuleta[i].Length; j++)
        {
            string rol = historialRuleta[i][j][1];

            if (rol == rolAntiguo)
                historialRuleta[i][j][1] = rolNuevo;
        }
    }
}

void menuRuleta()
{
    bool salirMenu = false;

    while (!salirMenu)
    {
        // Mostrar el menú de ruleta
        Console.Clear();
        Console.WriteLine("Menú de Ruleta");
        Console.WriteLine("Seleccione una opción:");
        Console.WriteLine($"1- Tirar ruleta{(roles.Length > 1 ? "s" : "")}");
        Console.WriteLine("2- Historial de juegos");
        Console.WriteLine("0- Volver al Menú Principal");
        
        Console.Write("\nSeleccione una opción: ");

        string? opcion = Console.ReadLine();

        Console.Clear();
        switch (opcion)
        {
            case "1":
                ruleta();
                break;
            case "2":
                mostrarHistorial();
                break;
            case "0":
                salirMenu = true;
                break;
            default:
                Console.WriteLine("Opción no válida, por favor intente de nuevo.");

                Console.Write("\nPresione cualquier tecla para continuar...");
                Console.ReadKey();
                break;
        }

        presioneParaContinuar();
    }
}

void menuEst()
{
    bool salirMenu = false;

    while (!salirMenu)
    {
        Console.Clear();
        Console.WriteLine("=== Menú de estudiantes ===");
        Console.WriteLine("Seleccione una opción:");
        Console.WriteLine("1- Consultar estudiantes");
        Console.WriteLine("2- Agregar estudiantes");
        Console.WriteLine("3- Actualizar estudiantes");
        Console.WriteLine("4- Eliminar estudiantes");
        Console.WriteLine("0- Volver al Menú Principal");

        Console.Write("\nSeleccione una opción: ");

        string opcion = Console.ReadLine() ?? "";

        Console.Clear();
        switch (opcion)
        {
            case "1":
                mostrarEstudiantes();
                break;
            case "2":
                agregarEstudiante();
                break;
            case "3":
                actualizarEstudiante();
                break;
            case "4":
                eliminarEstudiante();
                break;
            case "0":
                salirMenu = true;
                break;
            default:
                Console.WriteLine("Opción no válida, por favor intente de nuevo.\n");
                break;
        }

        presioneParaContinuar();
    }
}

void menuRoles()
{
    bool salirMenu = false;

    while (!salirMenu)
    {
        Console.Clear();
        Console.WriteLine("=== Menú de roles ===");
        Console.WriteLine("Seleccione una opción:");
        Console.WriteLine("1- Consultar roles");
        Console.WriteLine("2- Agregar roles");
        Console.WriteLine("3- Actualizar roles");
        Console.WriteLine("4- Eliminar roles");
        Console.WriteLine("0- Volver al Menú Principal");

        Console.Write("\nSeleccione una opción: ");

        string opcion = Console.ReadLine() ?? "";

        Console.Clear();
        switch (opcion)
        {
            case "1":
                mostrarRoles();
                break;
            case "2":
                agregarRoles();
                break;
            case "3":
                actualizarRoles();
                break;
            case "4":
                eliminarRoles();
                break;
            case "0":
                salirMenu = true;
                break;
            default:
                Console.WriteLine("Opción no válida, por favor intente de nuevo.\n");
                break;
        }

        presioneParaContinuar();
    }    
}

//Inicio del programa principal
while (!salir)
{
    Console.Clear();
    Console.WriteLine("=== Bienvenido a la Ruleta ===");
    Console.WriteLine("1- Jugar");
    Console.WriteLine("2- Roles");
    Console.WriteLine("3- Estudiantes");
    Console.WriteLine("0- Salir");
    Console.Write("\nSeleccione una opción: ");

    string opcion = Console.ReadLine() ?? "";

    Console.Clear();
    switch (opcion)
    {
        case "1":
            menuRuleta();
            break;
        case "2":
            menuRoles();
            break;
        case "3":
            menuEst();
            break;
        case "0":
            if (confirmarAccion("Seguro que quiere salir? "))
            {
                salir = true;
                Console.Clear();
                Console.WriteLine("Gracias por jugar a la Ruleta. ¡Hasta luego!");
            }
            break;
        default:
            Console.WriteLine("Opción no válida, por favor intente de nuevo.\n");

            presioneParaContinuar();
            break;
    }
}




 