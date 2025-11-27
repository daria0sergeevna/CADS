class ArrayListException : Exception
{
    public ArrayListException() : 
        base("Ошибка!")
    { }
}
class ArrayListArgumentNullException : ArgumentNullException
{
    public ArrayListArgumentNullException() :
        base("Передаваемый объект не может быть null!")
    { }
}
class ArrayListArgumentException : ArgumentException
{
    public ArrayListArgumentException() :
        base("Неверный тип объекта!")
    { }
}
class ArrayListOutOfMemoryException : OutOfMemoryException
{
    public ArrayListOutOfMemoryException() :
        base("Слишком большая очередь!")
    { }
}
class ArrayListIndexOutOfRangeException : Exception
{
    public ArrayListIndexOutOfRangeException() :
        base("Индекс улетел...") 
    { }
}