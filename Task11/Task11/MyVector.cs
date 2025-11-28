namespace Task11;

class MyVector<T>
{
    private T[] elementData;
    private int elementCount;
    private int capacityIncrement;

    public MyVector(int initialCapacity, int capacityIncrement)
    {
        if (initialCapacity < 1 || capacityIncrement < 0) throw new VectorArgumentException();
        elementData = new T[initialCapacity];
        elementCount = 0;
        this.capacityIncrement = capacityIncrement;
    }
    public MyVector(int initialCapacity) :
        this(initialCapacity, 0)
    { }
    public MyVector() :
        this(10)
    { }
    public MyVector(T[] a)
    {
        if (a == null) throw new VectorArgumentNullException();
        elementData = a;
        elementCount = a.Length;
        capacityIncrement = 0;
    }
    public int Size() => elementCount;
    public T[] ToArray()
    {
        T[] arr = new T[elementCount];
        Array.Copy(elementData, arr, elementCount);
        return arr;
    }
    public void ToArray(T[] a) => a = ToArray();
    public bool IsEmpty() => elementCount == 0;
    public T Get(int index) => elementData[index];
    public void Set(int index, T e) => elementData[index] = e;
    public T this[int index]
    {
        get => Get(index);
        set => Set(index, value);
    }
    public T FirstElement() => elementData[0];
    public T LastElement() => elementData[elementCount - 1];

    public void Clear()
    {
        RemoveAll(elementData);
        elementCount = 0;
    }

    public void Add(T e)
    {
        if (elementCount == elementData.Length)
        {
            try
            {
                T[] newData;
                if (capacityIncrement == 0)
                    newData = new T[elementCount * 2];
                else newData = new T[elementCount + capacityIncrement];
                Array.Copy(elementData, newData, elementCount);
                elementData = newData;
            }
            catch (VectorOutOfMemoryException ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }
        }
        elementData[elementCount++] = e;
    }
    public void AddAll(T[] a)
    {
        if (a == null) throw new VectorArgumentNullException();
        foreach (T e in a)
            Add(e);
    }
    public void Add(int index, T e)
    {
        try
        {
            Add(e);
            T tmp = elementData[elementCount - 1];
            for (int i = elementCount - 1; i > index; --i)
                elementData[i] = elementData[i - 1];
            elementData[index] = tmp;
        }
        catch (VectorIndexOutOfRangeException ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
    public void AddAll(int index, T[] a)
    {
        if (a == null) throw new VectorArgumentNullException();
        for (int i = 0; i < a.Length; ++i)
            Add(index + i, a[i]);
    }

    public bool Contains(object o)
    {
        if (o == null) throw new VectorArgumentNullException();
        return IndexOf(o) != -1;
    }
    public bool ContainsAll(T[] a)
    {
        if (a == null) throw new VectorArgumentNullException();
        foreach (T e in a)
            if (!Contains(e)) return false;
        return true;
    }

    public void Remove(object o)
    {
        if (o == null) throw new VectorArgumentNullException();
        if (!Contains(o)) return;
        int index = lastIndexOf(o); --elementCount;
        while (index < elementCount)
        {
            elementData[index] = elementData[index + 1];
            ++index;
        }
    }
    public void RemoveAll(T[] a)
    {
        foreach (T e in a)
            Remove(e);
    }
    public void RetainAll(T[] a)
    {
        if (a == null) throw new VectorArgumentNullException();
        for (int i = 0; i < elementCount; ++i)
        {
            bool f = true;
            foreach (T el in a)
                if (el.Equals(elementData[i])) f = false;
            if (f) Remove(elementData[i]);
        }
    }
    public T Remove(int index)
    {
        try
        {
            T res = elementData[index]; --elementCount;
            while (index < elementCount)
            {
                elementData[index] = elementData[index + 1];
                ++index;
            }
            return res;
        }
        catch (VectorIndexOutOfRangeException ex)
        {
            Console.WriteLine(ex.Message);
            return default;
        }
    }
    public void RemoveElementAt(int pos) => Remove(pos);
    public void RemoveRange(int begin, int end)
    {
        for (int i = begin; i < end; ++i)
            RemoveElementAt(begin);
    }

    public int IndexOf(object o)
    {
        if (o == null) throw new VectorArgumentNullException();
        for (int i = 0; i < elementCount; ++i)
            if (o.Equals(elementData[i])) return i;
        return -1;
    }
    public int lastIndexOf(object o)
    {
        if (o == null) throw new VectorArgumentNullException();
        for (int i = elementCount - 1; i >= 0; --i)
            if (o.Equals(elementData[i])) return i;
        return -1;
    }

    public MyVector<T> subList(int fromIndex, int toIndex)
    {
        try
        {
            T[] vals = new T[toIndex - fromIndex];
            Array.Copy(elementData, fromIndex, vals, 0, vals.Length);
            return new MyVector<T>(vals);
        }
        catch (VectorException ex)
        {
            Console.WriteLine(ex.Message);
            return null;
        }
    }
}