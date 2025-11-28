namespace Task13;

class MyStack<T> : MyVector<T>
{
    public void Push(T item) => Add(item);
    public T Pop()
    {
        if (Empty()) return default;
        T tmp = Peek();
        Remove(lastIndexOf(tmp));
        return tmp;
    }
    public T Peek()
    {
        if (Empty()) throw new VectorArgumentException();
        return LastElement();
    }
    public bool Empty() => IsEmpty();
    public int Search(T item)
    {
        if (!Contains(item)) return -1;
        return Size() - lastIndexOf(item);
    }
}