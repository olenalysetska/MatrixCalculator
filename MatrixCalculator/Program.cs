using System;

class MatrixCalculator
{
    static void Main()
    {
        Console.WriteLine("=== Kalkulator wyznacznika macierzy ===");
        
        int size = ReadMatrixSize();
        double[,] matrix = ReadMatrix(size);
        double result = ComputeDeterminant(matrix, size);
        
        Console.WriteLine($"\nWyznacznik = {result}");
    }

    static int ReadMatrixSize()
    {
        int n;
        while (true)
        {
            Console.Write("Podaj rozmiar macierzy (1-10): ");
            if (int.TryParse(Console.ReadLine(), out n) && n >= 1 && n <= 10)
                return n;
            Console.WriteLine("Niepoprawny rozmiar. Spróbuj ponownie.");
        }
    }

    static double[,] ReadMatrix(int n)
    {
        double[,] matrix = new double[n, n];
        Console.WriteLine("Wprowadz elementy macierzy (wiersz po wierszu, oddzielone spacją):");

        for (int i = 0; i < n; i++)
        {
            while (true)
            {
                Console.Write($"Wiersz {i + 1}: ");
                string[] parts = Console.ReadLine()
                    .Split(' ', StringSplitOptions.RemoveEmptyEntries);

                if (parts.Length != n)
                {
                    Console.WriteLine($"Wymagane {n} elementów. Spróbuj ponownie.");
                    continue;
                }

                bool ok = true;
                for (int j = 0; j < n; j++)
                {
                    if (!double.TryParse(parts[j], out matrix[i, j]))
                    {
                        ok = false;
                        break;
                    }
                }

                if (ok) break;
                Console.WriteLine("Błąd w danych. Spróbuj ponownie.");
            }
        }

        return matrix;
    }

    
    static double ComputeDeterminant(double[,] original, int n)
    {
       
        double[,] m = (double[,])original.Clone();
        double det = 1.0;

        for (int col = 0; col < n; col++)
        {
         
            int pivotRow = -1;
            double maxVal = 0;
            for (int row = col; row < n; row++)
            {
                if (Math.Abs(m[row, col]) > maxVal)
                {
                    maxVal = Math.Abs(m[row, col]);
                    pivotRow = row;
                }
            }

            if (pivotRow == -1 || maxVal == 0)
                return 0; 

            
            if (pivotRow != col)
            {
                SwapRows(m, col, pivotRow, n);
                det *= -1; 
            }

            det *= m[col, col];

         
            for (int row = col + 1; row < n; row++)
            {
                double factor = m[row, col] / m[col, col];
                for (int j = col; j < n; j++)
                    m[row, j] -= factor * m[col, j];
            }
        }

        return det;
    }

    static void SwapRows(double[,] m, int r1, int r2, int n)
    {
        for (int j = 0; j < n; j++)
            (m[r1, j], m[r2, j]) = (m[r2, j], m[r1, j]);
    }
}