namespace Task15;

class MyArrayDeque<T>
{
    private T[] elements;
    private int head;
    private int tail;

    private int Next(int index)
    {
        if (index == elements.Length - 1)
            return 0;
        return index + 1;
    }
    private int Prev(int index)
    {
        if (index == 0)
            return elements.Length - 1;
        return index - 1;
    }
    public MyArrayDeque()
    {
        elements = new T[16];
        tail = 0; head = 0;
    }
    public MyArrayDeque(T[] a)
    {
        if (a == null) throw new ArrayDequeArgumentNullException();
        elements = a;
        tail = 0; head = a.Length - 1;
    }
    public MyArrayDeque(int numElements)
    {
        try
        {
            elements = new T[numElements];
        }
        catch (ArrayDequeOutOfMemoryException ex)
        {
            Console.WriteLine(ex.Message);
            elements = new T[16];
        }
        tail = 0; head = 0;
    }

    public void Add(T e)
    {
        if (Size() == elements.Length - 1)
        {
            try
            {
                T[] newElements = new T[elements.Length * 2];
                Array.Copy(elements, newElements, elements.Length);
                elements = newElements;
            }
            catch (ArrayDequeOutOfMemoryException ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }
        }
        head = Next(head); elements[head] = e;
        
        
    }
    public void AddAll(T[] a)
    {
        if (a == null) throw new ArrayDequeArgumentNullException();
        foreach (T e in a)
            Add(e);
    }
    public void Clear()
    {
        RemoveAll(elements);
        head = 0; tail = 0;
    }
    public bool Contains(object o)
    {
        if (o == null) throw new ArrayDequeArgumentNullException();
        for (int i = tail; i != Next(head); i = Next(i))
            if (o.Equals(elements[i])) return true;
        return false;
    }
    public bool ContainsAll(T[] a)
    {
        if (a == null) throw new ArrayDequeArgumentNullException();
        foreach (T e in a)
            if (!Contains(e)) return false;
        return true;
    }
    public bool IsEmpty() => tail == head;
    public void Remove(object o) => RemoveFirstOccurrence(o);
    public void RemoveAll(T[] a)
    {
        if (a == null) throw new ArrayDequeArgumentNullException();
        foreach (T e in a)
            Remove(e);
    }
    public void RetailAll(T[] a)
    {
        if (a == null) throw new ArrayDequeArgumentNullException();
        for (int i = tail; i != Next(head); i = Next(i))
        {
            bool f = true;
            foreach (T el in a)
                if (elements[i].Equals(el)) f = false;
            if (f) Remove(elements[i]);
        }
    }
    public int Size()
    {
        if (tail == head) return 0;
        if (head < tail) return head + elements.Length - tail;
        else return head - tail;
    }
    public T[] ToArray()
    {
        T[] a = new T[Size() + 1]; int k = 0;
        for (int i = tail; i != Next(head); i = Next(i))
            a[k++] = elements[i];
        return a;
    }
    public void ToArray(T[] a) => a = ToArray();
    public T Element() => IsEmpty() ? default : elements[head];
    public bool Offer(T obj)
    {
        try
        {
            Add(obj);
        }
        catch (ArrayDequeException ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
        return true;
    }
    public T? Peek() => PeekFirst();
    public T? Poll() => PollFirst();
    public void AddFirst(T obj) => Add(obj);
    public void AddLast(T obj)
    {
        if (Size() == elements.Length - 1)
        {
            try
            {
                T[] newElements = new T[elements.Length * 2];
                Array.Copy(elements, newElements, elements.Length);
                elements = newElements;
            }
            catch (ArrayDequeOutOfMemoryException ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }
        }
        elements[tail] = obj; tail = Prev(tail);
    }
    public T GetFirst() => IsEmpty() ? default : elements[head];
    public T GetLast() => IsEmpty() ? default : elements[tail];
    public bool OfferFirst(T obj)
    {
        try
        {
            head = Next(head); elements[head] = obj;
        }
        catch (ArrayDequeException ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
        return true;
    }
    public bool OfferLast(T obj)
    {
        try 
        {
            tail = Prev(tail); elements[tail] = obj;
        }
        catch (ArrayDequeException ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
        return true;
    }
    public T Pop() => PollFirst();
    public void Push(T obj) => Add(obj);
    public T PeekFirst() => GetFirst();
    public T PeekLast() => GetLast();
    public T? PollFirst()
    {
        if (IsEmpty()) return default;
        T el = Element();
        head = Prev(head);
        return el;
    }
    public T? PollLast()
    {
        if (IsEmpty()) return default;
        T el = GetLast();
        tail = Next(tail);
        return el;
    }
    public T RemoveFirst() => PollFirst();
    public T RemoveLast() => PollLast();
    public bool RemoveFirstOccurrence(object obj)
    {
        if (obj == null) throw new ArrayDequeArgumentNullException();
        for (int i = tail; i != Next(head); i = Next(i))
            if (obj.Equals(elements[i]))
            {
                for (int j = i; j != Prev(tail); j = Prev(j))
                    elements[j] = elements[Prev(j)];
                tail = Next(tail);
                return true;
            }
        return false;
    }
    public bool RemoveLastOccurrence(object obj)
    {
        if (obj == null) throw new ArrayDequeArgumentNullException();
        for (int i = head; i != Prev(tail); i = Prev(i))
            if (obj.Equals(elements[i]))
            {
                for (int j = i; j != Next(head); j = Next(j))
                    elements[j] = elements[Next(j)];
                tail = Prev(tail);
                return true;
            }
        return false;
    }
}