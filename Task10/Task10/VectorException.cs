namespace Task10;

class VectorException : Exception
{
    public VectorException() :
        base("Oшибка!")
    { }
}
class VectorArgumentNullException : ArgumentNullException
{
    public VectorArgumentNullException() :
        base("Передаваемый объект не может быть null!")
    { }
}
class VectorArgumentException : ArgumentException
{
    public VectorArgumentException() :
        base("Неверный тип объекта!")
    { }
}
class VectorOutOfMemoryException : OutOfMemoryException
{
    public VectorOutOfMemoryException() :
        base("Слишком большой вектор!")
    { }
}
class VectorIndexOutOfRangeException : Exception
{
    public VectorIndexOutOfRangeException() :
        base("Выход за границы")
    { }
}