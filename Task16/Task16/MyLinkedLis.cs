namespace Task16;

class MyLinkedList<T>
{
    private MyNode<T>? first;
    private MyNode<T>? last;
    private int size;

    public MyLinkedList()
    {
        first = null;
        last = null;
        size = 0;
    }
    public MyLinkedList(T[] a)
    {
        if (a == null) throw new LinkedListArgumentNullException();
        if (a.Length == 0)
        {
            first = null;
            last = null;
            size = 0;
            return;
        }
        size = a.Length;
        first = new MyNode<T>(a[0]);
        MyNode<T>? p = first;
        for (int i = 1; i < a.Length; i++)
        {
            MyNode<T>? q = new MyNode<T>(a[i]);
            q.Prev = p; p.Next = q; p = q;
        }
        last = p;
    }

    public void Add(T e) => AddLast(e);
    public void AddAll(T[] a)
    {
        if (a == null) throw new LinkedListArgumentNullException();
        foreach (T e in a)
            Add(e);
    }
    public void Clear()
    {
        first = null; last = null;
        size = 0;
    }
    public bool Contains(object o)
    {
        if (o == null) throw new LinkedListArgumentNullException();
        for (MyNode<T> p = first; p != null; p = p.Next)
            if (o.Equals(p.Value)) return true;
        return false;
    }
    public bool ContainsAll(T[] a)
    {
        if (a == null) throw new LinkedListArgumentNullException();
        foreach (T e in a)
            if (!Contains(e)) return false;
        return true;
    }
    public bool IsEmpty() => size == 0;
    public void Remove(object o) => RemoveFirstOccurrence(o);
    public void RemoveAll(T[] a)
    {
        if (a == null) throw new LinkedListArgumentNullException();
        foreach (T e in a)
            Remove(e);
    }
    public void RetainAll(T[] a)
    {
        if (a == null) throw new LinkedListArgumentNullException();
        for (MyNode<T> p = first; p != null; p = p.Next)
        {
            bool f = true;
            foreach (T el in a)
                if (p.Value.Equals(el)) f = false;
            if (f) Remove(p.Value);
        }
    }
    public int Size() => size;
    public T[] ToArray()
    {
        T[] a = new T[size]; int i = 0;
        for (MyNode<T> p = first; p != null; p = p.Next, ++i)
            a[i] = p.Value;
        return a;
    }
    public void ToArray(T[] a) => a = ToArray();
    public void Add(int index, T e)
    {
        MyNode<T>? p = first;
        for (int i = 0; i < index && p.Next != null; ++i)
            p = p.Next;
        MyNode<T>? q = new MyNode<T>(e);
        q.Next = p; q.Prev = p.Prev;
        if (p.Prev != null) p.Prev.Next = q;
        p.Prev = q; ++size;
    }
    public void AddAll(int index, T[] a)
    {
        if (a == null) throw new LinkedListArgumentNullException();
        foreach (T e in a)
            Add(index++, e);
    }
    public T Get(int index)
    {
        if (index < 0 || index >= size) throw new LinkedListIndexOutOfRangeException();
        MyNode<T>? p = first;
        for (int i = 0; i < index && p.Next != null; ++i)
            p = p.Next;
        return p.Value;
    }
    public void Set(int index, T e)
    {
        if (index < 0 || index >= size) throw new LinkedListIndexOutOfRangeException();
        MyNode<T>? p = first;
        for (int i = 0; i < index; ++i)
            p = p.Next;
        p.Value = e;
    }
    public T this[int index]
    {
        get => Get(index);
        set => Set(index, value);
    }
    public int IndexOf(object o)
    {
        if (o == null) throw new LinkedListArgumentNullException();
        int i = 0;
        for (MyNode<T> p = first; p != null; p = p.Next, ++i)
            if (o.Equals(p.Value)) return i;
        return -1;
    }
    public int LastIndexOf(object o)
    {
        if (o == null) throw new LinkedListArgumentNullException();
        int i = size - 1;
        for (MyNode<T> p = last; p != null; p = p.Prev, --i)
            if (o.Equals(p.Value)) return i;
        return -1;
    }
    public T Remove(int index)
    {
        MyNode<T>? p = first;
        for (int i = 0; i < index; ++i)
            p = p.Next;
        if (p.Prev != null) p.Prev.Next = p.Next;
        if (p.Next != null) p.Next.Prev = p.Prev;
        --size; return p.Value;
    }

    public T[] SubList(int fromIndex, int toIndex)
    {
        MyNode<T>? start = first;
        for (int i = 0; i < fromIndex; ++i)
            start = start.Next;
        T[] a = new T[toIndex - fromIndex];
        for (int i = 0; i < fromIndex - toIndex; start = start.Next, ++i)
            a[i] = start.Value;
        return a;
    }
    public T Element() => last.Value;
    public bool Offer(T obj) => OfferLast(obj);
    public T? Peek() => IsEmpty() ? default : last.Value;
    public T? Poll() => PollLast();
    public void AddFirst(T obj)
    {
        MyNode<T>? newFirst = new MyNode<T>(obj);
        newFirst.Next = first;
        if (first != null) first.Prev = newFirst;
        first = newFirst;
        if (IsEmpty()) last = first; 
        ++size;
    }
    public void AddLast(T obj)
    {
        MyNode<T>? newLast = new MyNode<T>(obj);
        newLast.Prev = last;
        if (last != null) last.Next = newLast;
        last = newLast;
        if (IsEmpty()) first = last;
        ++size;
    }
    public T GetFirst() => first.Value;
    public T GetLast() => last.Value;
    public bool OfferFirst(T obj)
    {
        try
        {
            AddFirst(obj);
        }
        catch (LinkedListException ex)
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
            AddLast(obj);
        }
        catch (LinkedListException ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
        return true;
    }
    public T? Pop() => PollLast();
    public void Push(T obj) => AddLast(obj);
    public T? PeekFirst() => IsEmpty() ? default : GetFirst();
    public T? PeekLast() => IsEmpty() ? default : GetLast();
    public T? PollFirst()
    {
        if (IsEmpty()) return default;
        T ono = first.Value;
        if (first.Next != null) first.Next.Prev = null;
        first = first.Next; --size;
        return ono;
    }
    public T? PollLast()
    {
        if (IsEmpty()) return default;
        T ono = last.Value;
        if (last.Prev != null) last.Prev.Next = null;
        last = last.Prev; --size;
        return ono;
    }
    public T RemoveFirst() => PollFirst();
    public T? RemoveLast() => PollLast();
    public bool RemoveLastOccurrence(object obj)
    {
        if (obj == null) throw new LinkedListArgumentNullException();
        try
        {
            if (!Contains(obj)) return false;
            for (MyNode<T> p = last; p != null; p = p.Prev)
                if (obj.Equals(p.Value))
                {
                    if (p.Prev != null) p.Prev.Next = p.Next;
                    if (p.Next != null) p.Next.Prev = p.Prev;
                    --size; return true;
                }
        }
        catch (LinkedListException ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
        return true;
    }
    public bool RemoveFirstOccurrence(object obj)
    {
        if (obj == null) throw new LinkedListArgumentNullException();
        try
        {
            if (!Contains(obj)) return false;
            for (MyNode<T> p = first; p != null; p = p.Next)
                if (obj.Equals(p.Value))
                {
                    if (p.Prev != null) p.Prev.Next = p.Next;
                    if (p.Next != null) p.Next.Prev = p.Prev;
                    --size; return true;
                }
        }
        catch (LinkedListException ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
        return true;
    }
}