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
        char[] operadores = { '=', '<', '>', '+', '-', '*', '/', '&' };

        for (int i = 0; i < matriz.GetLength(0); i++)
        {
            for (int j = 0; j < matriz.GetLength(1); j++)
            {
                char currentChar = matriz[i, j];

                // Si encontramos un primer operando
                if ((currentChar >= 'a' && currentChar <= 'z') || (currentChar >= 'A' && currentChar <= 'Z'))
                {
                    char primerOperando = currentChar;
                    char operador = ' ';
                    char segundoOperador = ' ';
                    char tercerOperando = ' ';

                    for (int k = j + 1; k < matriz.GetLength(1); k++)
                    {
                        if (Array.Exists(operadores, element => element == matriz[i, k]))
                        {
                            operador = matriz[i, k];
                            // Buscar el segundo operando
                            char segundoOperando = ' ';
                            for (int l = k + 1; l < matriz.GetLength(1); l++)
                            {
                                if ((matriz[i, l] >= 'a' && matriz[i, l] <= 'z') || (matriz[i, l] >= 'A' && matriz[i, l] <= 'Z'))
                                {
                                    segundoOperando = matriz[i, l];
                                    // Buscar el tercer operando
                                    for (int m = l + 1; m < matriz.GetLength(1); m++)
                                    {
                                        if (char.IsLetterOrDigit(matriz[i, m]))
                                        {
                                            tercerOperando = matriz[i, m];
                                            break;
                                        }
                                    }
                                    break;
                                }
                            }
                            for (int l = k + 1; l < matriz.GetLength(1); l++)
                            {
                                if (char.IsLetterOrDigit(matriz[i, l]))
                                {
                                    segundoOperando = matriz[i, l];
                                    // Buscar el segundo operador
                                    for (int m = l + 1; m < matriz.GetLength(1); m++)
                                    {
                                        if (Array.Exists(operadores, elem => elem == matriz[i, m]))
                                        {
                                            segundoOperador = matriz[i, m];
                                            break;
                                        }
                                    }
                                    break;
                                }
                            }

                            if (operador == '=')
                            {
                                Console.WriteLine($"|   {instrCount++}   |   {segundoOperador}   |   {segundoOperando}  |  {tercerOperando}  |  T{tempCount}  |");
                                Console.WriteLine($"|   {instrCount++}   |   =   |   T{tempCount}  |      |   {primerOperando}  |");
                                tempCount++;
                            }
                            else
                            {
                                Console.WriteLine($"|   {instrCount++}   |   {operador}   |   {primerOperando}  |  {segundoOperando}  |  T{tempCount}  |");
                               
                                tempCount++;
                            }
                            matriz[i, k] = ' '; // Marcar el operador como espacio en blanco en la matriz

                            break;
                        }
                    }
                }
                // Si encontramos un THEN
                else if (currentChar == '|')
                {
                    // Imprimir el cuádruplo
                    Console.WriteLine($"|   {instrCount++}   |   GF   |  T{tempCount - 1}  |      |    |");
                    matriz[i, j] = ' ';
                }
            }
        }
    }
}
    

