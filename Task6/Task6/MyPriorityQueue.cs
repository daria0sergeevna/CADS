class MyPriorityQueue<T>
{
    private T[] queue;
    private int size;
    private PriorityQueueComparer<T> comparator;
    public int Size() => size;
    public T[] ToArray() => queue;
    public void ToArray(T[] a) => a = ToArray();
    public T Element() => queue[0];
    public bool IsEmpty() => size == 0;

    public MyPriorityQueue()
    {
        queue = new T[11]; size = 0;
        comparator = new PriorityQueueComparer<T>();
    }
    public MyPriorityQueue(T[] a)
    {
        if (a == null) throw new PriorityQueueArgumentNullException();
        queue = a; size = a.Length;
        comparator = new PriorityQueueComparer<T>();
        int index, parent; T tmp;
        for (int i = queue.Length / 2; i >= 0; --i)
        {
            index = i;
            while (index > 0)
            {
                parent = (index - 1) / 2;
                if (comparator.Compare(queue[parent], queue[index]) >= 0)
                    break;
                tmp = queue[index];
                queue[index] = queue[parent];
                queue[parent] = tmp;
                index = parent;
            }
        }
    }
    public MyPriorityQueue(int initialCapacity)
    {
        if (initialCapacity < 1) throw new PriorityQueueArgumentException();
        try
        {
            queue = new T[initialCapacity];
        }
        catch(PriorityQueueOutOfMemoryException ex)
        {
            Console.WriteLine(ex.Message);
            queue = new T[11];
        }
        size = 0;
        comparator = new PriorityQueueComparer<T>();
    }
    public MyPriorityQueue(int initialCapacity, PriorityQueueComparer<T> comparator)
    {
        if (initialCapacity < 1) throw new PriorityQueueArgumentException();
        queue = new T[initialCapacity];
        size = 0;
        this.comparator = comparator;
    }
    public MyPriorityQueue(MyPriorityQueue<T> c)
    {
        queue = c.ToArray();
        size = c.Size();
        comparator = new PriorityQueueComparer<T>();
    }

    public void Clear()
    {
        RemoveAll(queue);
        size = 0;
    }

    public void Add(T e)
    {
        if (size == queue.Length)
        {
            try
            {
                T[] newQueue;
                if (queue.Length < 64)
                    newQueue = new T[queue.Length + 2];
                else
                    newQueue = new T[queue.Length + queue.Length / 2];
                Array.Copy(queue, newQueue, queue.Length);
                queue = newQueue;
            }
            catch(PriorityQueueOutOfMemoryException ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }
        }
        queue[size++] = e;
        int parent, index = size - 1; T tmp;
        while (index > 0)
        {
            parent = (index - 1) / 2;
            if (comparator.Compare(queue[parent], queue[index]) >= 0)
                break;
            tmp = queue[index];
            queue[index] = queue[parent];
            queue[parent] = tmp;
            index = parent;
        }
    }
    public void AddAll(T[] a)
    {
        if (a == null) throw new PriorityQueueArgumentNullException();
        foreach (T e in a)
            Add(e);
    }
    public bool Offer(T obj)
    {
        try
        {
            Add(obj);
        }
        catch(PriorityQueueException ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
        return true;
    }

    public bool Contains(object o)
    {
        if (o == null) throw new PriorityQueueArgumentNullException();
        try
        {
            foreach (T e in queue)
                if (o.Equals(e)) return true;
            return false;
        }
        catch (PriorityQueueException ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
    }
    public bool ContainsAll(T[] a)
    {
        if (a == null) throw new PriorityQueueArgumentNullException();
        foreach (T e in a)
            if (!Contains(e)) return false;
        return true;
    }

    public void Remove(object o)
    {
        if (o == null) throw new PriorityQueueArgumentNullException();
        if (!Contains(o)) return;
        int index = -1;
        for (int i = 0; i < size && index == -1; ++i)
            if (o.Equals(queue[i])) index = i;
        queue[index] = queue[--size];
        int minChild; T tmp;
        while (2 * index + 1 < size + 1)
        {
            minChild = 2 * index + 1;
            if (minChild + 1 < size && comparator.Compare(queue[minChild + 1], queue[minChild]) > 0)
                ++minChild;
            if (comparator.Compare(queue[index], queue[minChild]) >= 0)
                break;
            tmp = queue[index];
            queue[index] = queue[minChild];
            queue[minChild] = tmp;
            index = minChild;
        }
    }
    public void RemoveAll(T[] a)
    {
        if (a == null) throw new PriorityQueueArgumentNullException();
        foreach (T e in a)
            Remove(e);
    }
    public void RetainAll(T[] a)
    {
        if (a == null) throw new PriorityQueueArgumentNullException();
        foreach (T e in queue)
        {
            bool f = true;
            foreach (T el in a)
                if (e.Equals(el)) f = false;
            if (f) Remove(e);
        }
    }

    public T? Peek() => !IsEmpty() ? Element() : default;
    public T? Poll()
    {
        if (IsEmpty()) return default;
        T result = Element();
        Remove(result);
        return result;
    }
}