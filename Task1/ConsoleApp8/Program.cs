using VectorLength.Models;
using VectorLength.Services;
using VectorLength.Exceptions;

namespace VectorLength
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Вычисление длины вектора в N-мерном пространстве ===");
            Console.WriteLine();

            try
            {
                // Запрашиваем путь к файлу с данными
                Console.Write("Введите путь к файлу с данными: ");
                string filePath = Console.ReadLine()?.Trim() ?? "";

                // Читаем матрицу и вектор из файла
                var reader = new FileReader();
                var (matrix, vector) = reader.Read(filePath);

                // Показываем введенные данные
                Console.WriteLine("\n=== Введенные данные ===");
                Console.WriteLine($"Размерность пространства: {vector.Size}");
                Console.WriteLine("\nМатрица метрического тензора G:");
                PrintMatrix(matrix);
                Console.WriteLine($"Вектор x: {vector}");

                // Вычисляем длину вектора
                var calculator = new Calculator();
                double length = calculator.ComputeLength(matrix, vector);

                // Показываем результат
                Console.WriteLine("\n=== Результат ===");
                Console.WriteLine($"Длина вектора: {length:F6}");
                Console.WriteLine($"По формуле: √(x * G * x^T)");
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine($"Ошибка: Файл не найден - {ex.Message}");
            }
            catch (InvalidDataException ex)
            {
                Console.WriteLine($"Ошибка в данных: {ex.Message}");
            }
            catch (MatrixException ex)
            {
                Console.WriteLine($"Ошибка матрицы: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Неожиданная ошибка: {ex.Message}");
            }

            Console.WriteLine("\nНажмите любую клавишу для выхода...");
            Console.ReadKey();
        }

        // Красиво выводим матрицу на экран
        private static void PrintMatrix(Matrix matrix)
        {
            for (int i = 0; i < matrix.Size; i++)
            {
                Console.Write("| ");
                for (int j = 0; j < matrix.Size; j++)
                {
                    Console.Write($"{matrix[i, j],8:F4} ");
                }
                Console.WriteLine("|");
            }
        }
    }
}