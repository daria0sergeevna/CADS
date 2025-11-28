namespace Task14;

class ArrayDequeException : Exception
{
    public ArrayDequeException() :
        base("Ошибка!")
    { }
}
class ArrayDequeArgumentNullException : ArgumentNullException
{
    public ArrayDequeArgumentNullException() :
        base("Передаваемый объект не может быть null!")
    { }
}
class ArrayDequeOutOfMemoryException : OutOfMemoryException
{
    public ArrayDequeOutOfMemoryException() :
        base("Слишком большой дек!")
    { }
}