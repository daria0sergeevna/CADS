using VectorLength.Exceptions;
using VectorLength.Models;

namespace VectorLength.Validation
{
    public static class MatrixValidator
    {
        // Проверяем что матрица симметричная (G[i,j] == G[j,i])
        public static void CheckSymmetric(Matrix matrix)
        {
            for (int i = 0; i < matrix.Size; i++)
            {
                for (int j = i + 1; j < matrix.Size; j++)
                {
                    if (Math.Abs(matrix[i, j] - matrix[j, i]) > 1e-10)
                    {
                        throw new MatrixException(
                            $"Матрица не симметрична: G[{i},{j}] = {matrix[i, j]} ≠ G[{j},{i}] = {matrix[j, i]}");
                    }
                }
            }
        }

        // Проверяем что размерности матрицы и вектора совпадают
        public static void CheckDimensions(Matrix matrix, Vector vector)
        {
            if (matrix.Size != vector.Size)
            {
                throw new MatrixException(
                    $"Размерности не совпадают: матрица {matrix.Size}x{matrix.Size}, вектор размерности {vector.Size}");
            }
        }
    }
}