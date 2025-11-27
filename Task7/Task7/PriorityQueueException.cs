class PriorityQueueException : Exception
{
    public PriorityQueueException()
        : base("Ошибка!") { }
}
class PriorityQueueArgumentNullException : ArgumentNullException
{
    public PriorityQueueArgumentNullException() :
        base("Передаваемый объект не может быть null!")
    { }
}
class PriorityQueueArgumentException : ArgumentException
{
    public PriorityQueueArgumentException() :
        base("Неверный тип объекта!")
    { }
}
class PriorityQueueOutOfMemoryException : OutOfMemoryException
{
    public PriorityQueueOutOfMemoryException() :
        base("Слишком большая очередь!")
    { }
}