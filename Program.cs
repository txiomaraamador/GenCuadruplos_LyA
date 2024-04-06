using System;

class Program
{
    static void Main(string[] args)
    {
        // Código proporcionado
        string codigo = "IF	y<c	then\n\tIF	a>b	then\n\t\tIF	c>d	then\n\t\t\ty=y+1\n\t\t\tA=a+b\n\t\t\tc=d+1";

        // Generar matriz
        char[,] matriz = GenerarMatriz(codigo);

        Console.WriteLine($"Codigo: {matriz.GetLength(0)} filas x {matriz.GetLength(1)} columnas");

        // Mostrar la matriz
        MostrarMatriz(matriz);

        Console.WriteLine($"Cuadruplos");

        // Generar cuádruplos
        GenerarCuadruplos(matriz);
    }

    static char[,] GenerarMatriz(string codigo)
    {
        // Separar el código por saltos de línea
        string[] lineasCodigo = codigo.Split('\n');

        // Encontrar la longitud máxima de línea
        int longitudMaxima = 0;
        foreach (string linea in lineasCodigo)
        {
            if (linea.Length > longitudMaxima)
                longitudMaxima = linea.Length;
        }

        // Crear matriz para almacenar los caracteres
        char[,] matriz = new char[lineasCodigo.Length, longitudMaxima];

        // Recorrer cada línea del código
        for (int i = 0; i < lineasCodigo.Length; i++)
        {
            string linea = lineasCodigo[i];

            // Recorrer cada caracter de la línea
            for (int j = 0; j < linea.Length; j++)
            {
                char caracter = linea[j];
                char caracterAlmacenado = caracter;

                // Reemplazar 'IF' y 'then' por caracteres especiales
                if (caracter == 'I' && j + 1 < linea.Length && linea[j + 1] == 'F')
                {
                    caracterAlmacenado = '¿'; // Carácter especial para IF
                    j++; // Saltar el siguiente caracter ('F')
                }
                else if (caracter == 't' && j + 3 < linea.Length && linea.Substring(j, 4) == "then")
                {
                    caracterAlmacenado = '|'; // Carácter especial para then
                    j += 3; // Saltar los siguientes caracteres ('h', 'e', 'n')
                }

                matriz[i, j] = caracterAlmacenado; // Almacenar caracter en matriz
            }
        }

        return matriz;
    }

    static void MostrarMatriz(char[,] matriz)
    {
        // Mostrar la matriz
        for (int i = 0; i < matriz.GetLength(0); i++)
        {
            for (int j = 0; j < matriz.GetLength(1); j++)
            {
                Console.Write(matriz[i, j]);
            }
            Console.WriteLine(); // Agregar salto de línea después de cada fila
        }
    }

    static void GenerarCuadruplos(char[,] matriz)
    {
        int tempCount = 1;
        int instrCount = 0;
        char[] operadores = { '=', '<', '>', '+' };

        for (int i = 0; i < matriz.GetLength(0); i++)
        {
            for (int j = 0; j < matriz.GetLength(1); j++)
            {
                char currentChar = matriz[i, j];

                // Si encontramos un IF
                if (currentChar == '¿')
                {
                    // Buscar el siguiente operador matemático
                    char operador = ' ';
                    char primerOperando = ' ';
                    char segundoOperando = ' ';
                    int temp = tempCount;

                    for (int k = j + 1; k < matriz.GetLength(1); k++)
                    {
                        if (Array.Exists(operadores, element => element == matriz[i, k]))
                        {
                            operador = matriz[i, k];
                            // Buscar el primer operando
                            for (int l = k - 1; l >= 0; l--)
                            {
                                if (matriz[i, l] != ' ' && matriz[i, l] != '\t')
                                {
                                    primerOperando = matriz[i, l];
                                    break;
                                }
                            }
                            // Buscar el segundo operando
                            for (int l = k + 1; l < matriz.GetLength(1); l++)
                            {
                                if (matriz[i, l] != ' ' && matriz[i, l] != '\t')
                                {
                                    segundoOperando = matriz[i, l];
                                    break;
                                }
                            }
                            break;
                        }
                    }

                    // Imprimir el cuádruplo
                    Console.WriteLine($"|   {instrCount++}   |   {operador}   |   {primerOperando}  |  {segundoOperando}  |  T{temp}  |");

                    // Incrementar el contador de temporales
                    tempCount++;
                }
                // Si encontramos un THEN
                else if (currentChar == '|')
                {
                    // Imprimir el cuádruplo
                    Console.WriteLine($"|   {instrCount++}   |   GF   |  T{tempCount - 1}  |      |    |");
                }
            }
        }
    }
}

