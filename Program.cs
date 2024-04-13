using System;

class Program
{
    static void Main(string[] args)
    {
        // codigin
        string codigo = "IF	y<c	then\n\tIF	a>b	then\n\t\tIF	c>d	then\n\t\t\ty=y+1\n\t\t\tA=a+b\n\t\t\tc=d+1";

        //matriz
        char[,] matriz = GenerarMatriz(codigo);

        Console.WriteLine($"Codigo: {matriz.GetLength(0)} filas x {matriz.GetLength(1)} columnas");
        
        MostrarMatriz(matriz);

        Console.WriteLine($"Cuadruplos");

        GenerarCuadruplos(matriz);
    }

    static char[,] GenerarMatriz(string codigo)
    {
        // separar por linea
        string[] lineasCodigo = codigo.Split('\n');

        // lomngitud matriz xd
        int longitudMaxima = 0;
        foreach (string linea in lineasCodigo)
        {
            if (linea.Length > longitudMaxima)
                longitudMaxima = linea.Length;
        }

        // crear matriz
        char[,] matriz = new char[lineasCodigo.Length, longitudMaxima];

        // recorrer matriz por lineaaaaaas
        for (int i = 0; i < lineasCodigo.Length; i++)
        {
            string linea = lineasCodigo[i];

            // recorrecr caractres
            for (int j = 0; j < linea.Length; j++)
            {
                char caracter = linea[j];
                char caracterAlmacenado = caracter;

                //cambiar if por caracter porque me marca error con dos
                if (caracter == 'I' && j + 1 < linea.Length && linea[j + 1] == 'F')
                {
                    caracterAlmacenado = '¿'; 
                    j++; 
                }
                else if (caracter == 't' && j + 3 < linea.Length && linea.Substring(j, 4) == "then")
                {
                    caracterAlmacenado = '|';
                    j += 3; 
                }

                matriz[i, j] = caracterAlmacenado; // almacenar en la matriz
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
            Console.WriteLine();
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
                    
                    char operador = matriz[i, j + 1]; 
                   
                    char segundoOperando = matriz[i, j + 2];

                    char operador2 = matriz[i, j + 3];
                   
                    char tercerOperando = matriz[i, j + 4];

                    if (operador == '=')
                    {
                        Console.WriteLine($"|   {instrCount++}   |   {operador2}   |   {segundoOperando}  |  {tercerOperando}  |  T{tempCount}  |");
                        Console.WriteLine($"|   {instrCount++}   |   =   |   T{tempCount}  |      |   {primerOperando}  |");
                        tempCount++;
                    }
                    else
                    {
                        Console.WriteLine($"|   {instrCount++}   |   {operador}   |   {primerOperando}  |  {segundoOperando}  |  T{tempCount}  |");
                        tempCount++;
                    }
                 
                    j += 4;
                }
               
                else if (currentChar == '|')
                {
                 
                    Console.WriteLine($"|   {instrCount++}   |   GF   |  T{tempCount - 1}  |      |    |");
                }
            }
        }
    }

}


