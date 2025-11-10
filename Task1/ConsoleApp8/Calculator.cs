using VectorLength.Models;
using VectorLength.Validation;

namespace VectorLength.Services
{
    public class Calculator
    {
        public double ComputeLength(Matrix g, Vector x)
        {
            // Проверяем что матрица симметричная
            MatrixValidator.CheckSymmetric(g);
            // Проверяем что размерности совпадают
            MatrixValidator.CheckDimensions(g, x);

            // Вычисляем произведение вектора на матрицу: x * G
            var xG = MultiplyVectorByMatrix(x, g);
            
            // Вычисляем скалярное произведение: (x * G) * x^T
            double dotProduct = DotProduct(xG, x);
            
            // Возвращаем квадратный корень по формуле √(x × G × x^T)
            return Math.Sqrt(dotProduct);
        }

        // Умножение вектора на матрицу (x * G)
        private double[] MultiplyVectorByMatrix(Vector v, Matrix m)
        {
            var result = new double[v.Size];
            
            for (int i = 0; i < v.Size; i++)
            {
                double sum = 0;
                // Вычисляем i-ю компоненту результата
                for (int j = 0; j < v.Size; j++)
                {
                    // Суммируем произведения компонент вектора на элементы матрицы
                    sum += v.Data[j] * m[j, i];
                }
                result[i] = sum;
            }
            
            return result;
        }

        // Скалярное произведение двух векторов
        private double DotProduct(double[] v1, Vector v2)
        {
            double result = 0;
            for (int i = 0; i < v1.Length; i++)
            {
                // Суммируем произведения соответствующих компонент
                result += v1[i] * v2.Data[i];
            }
            return result;
        }
    }
}