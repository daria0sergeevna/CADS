using VectorLength.Models;

namespace VectorLength.Services
{
    public interface IReader
    {
        (Matrix matrix, Vector vector) Read(string path);
    }
}