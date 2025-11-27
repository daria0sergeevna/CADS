namespace VectorLength.Models
{
    public class Vector
    {
        public double[] Data { get; }
        public int Size => Data.Length;

        public Vector(double[] data)
        {
            Data = data;
        }

        public override string ToString() => $"({string.Join(", ", Data)})";
    }
}