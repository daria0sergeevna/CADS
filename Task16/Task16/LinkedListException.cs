namespace Task16;

class LinkedListException : Exception
{
    public LinkedListException() :
        base("Ошибка!")
    { }
}
class LinkedListArgumentNullException : ArgumentNullException
{
    public LinkedListArgumentNullException() :
        base("Передаваемый объект не может быть null!")
    { }
}
class LinkedListIndexOutOfRangeException : Exception
{
    public LinkedListIndexOutOfRangeException() :
        base("Выход за границу")
    { }
}