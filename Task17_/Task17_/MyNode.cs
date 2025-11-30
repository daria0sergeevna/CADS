namespace Task17_;

class MyNode<T>
{
    public T Value { get; set; }
    public MyNode<T>? Next;
    public MyNode<T>? Prev;
    public MyNode(T value)
    {
        Value = value;
        Next = null; Prev = null;
    }
}