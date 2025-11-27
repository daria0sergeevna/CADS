namespace VectorLength.Models
{
    public class Matrix
    {
        public double[,] Data { get; }
        public int Size { get; }

        public Matrix(double[,] data)
        {
            Data = data;
            Size = data.GetLength(0);
        }

        public double this[int i, int j]
        {
            get => Data[i, j];
            set => Data[i, j] = value;
        }
    }
}