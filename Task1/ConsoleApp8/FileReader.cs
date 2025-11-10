using VectorLength.Models;
using VectorLength.Exceptions;

namespace VectorLength.Services
{
    public class FileReader : IReader
    {
        public (Matrix matrix, Vector vector) Read(string path)
        {
            // Проверяем что файл существует
            if (!File.Exists(path))
                throw new FileNotFoundException($"Файл не найден: {path}");

            // Читаем все строки из файла
            var lines = File.ReadAllLines(path);
            
            if (lines.Length < 2)
                throw new InvalidDataException("Файл должен содержать как минимум 2 строки");

            // Читаем размерность пространства (первая строка)
            if (!int.TryParse(lines[0].Trim(), out int n) || n <= 0)
                throw new InvalidDataException("Первая строка должна быть положительным целым числом (размерность)");

            // Создаем матрицу N x N
            var matrixData = new double[n, n];
            
            // Читаем строки матрицы (со 2-й по N+1 строку)
            for (int i = 0; i < n; i++)
            {
                if (i + 1 >= lines.Length)
                    throw new InvalidDataException($"Не хватает строк для матрицы. Ожидалось {n} строк");

                string line = lines[i + 1];
                
                // Разбиваем строку на числа
                var values = line.Split(new[] { ' ', '\t', ',', ';' }, StringSplitOptions.RemoveEmptyEntries);
                
                if (values.Length != n)
                    throw new InvalidDataException($"Строка {i + 2} должна содержать {n} чисел, но содержит {values.Length}");

                // Заполняем строку матрицы
                for (int j = 0; j < n; j++)
                {
                    // Пробуем разные форматы чисел
                    if (double.TryParse(values[j], System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out double val))
                    {
                        matrixData[i, j] = val;
                    }
                    else if (double.TryParse(values[j], System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.GetCultureInfo("ru-RU"), out val))
                    {
                        matrixData[i, j] = val;
                    }
                    else
                    {
                        throw new InvalidDataException($"Неверное число '{values[j]}' в позиции [{i},{j}]");
                    }
                }
            }

            // Читаем вектор (последняя строка)
            if (n + 1 >= lines.Length)
                throw new InvalidDataException("Не найдена строка с вектором");

            string vectorLine = lines[n + 1];
            
            var vectorValues = vectorLine.Split(new[] { ' ', '\t', ',', ';' }, StringSplitOptions.RemoveEmptyEntries);
            if (vectorValues.Length != n)
                throw new InvalidDataException($"Вектор должен содержать {n} чисел, но содержит {vectorValues.Length}");

            var vectorData = new double[n];
            for (int i = 0; i < n; i++)
            {
                if (double.TryParse(vectorValues[i], System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out double val))
                {
                    vectorData[i] = val;
                }
                else if (double.TryParse(vectorValues[i], System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.GetCultureInfo("ru-RU"), out val))
                {
                    vectorData[i] = val;
                }
                else
                {
                    throw new InvalidDataException($"Неверное число '{vectorValues[i]}' в векторе на позиции {i}");
                }
            }

            return (new Matrix(matrixData), new Vector(vectorData));
        }
    }
}