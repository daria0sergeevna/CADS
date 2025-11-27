namespace Task9;
class MyArrayList<T>
{
    private T[] elementD;
    private int size;
    public MyArrayList()
    {
        elementD = new T[1];
        size = 0;
    }
    public MyArrayList(T[] a)
    {
        if (a == null) throw new ArrayListArgumentNullException();
        elementD = a;
        size = a.Length;
    }
    public MyArrayList(int capacity)
    {
        if (capacity < 1) throw new ArrayListArgumentException();
        try
        {
            elementD = new T[capacity];
        }
        catch (OutOfMemoryException ex)
        {
            Console.WriteLine(ex.Message);
            elementD = new T[1];
        }
        size = 0;
    }

    public int Size() => size;
    public T[] ToArray()
    {
        T[] arr = new T[size];
        Array.Copy(elementD, arr, size);
        return arr;
    }
    public void ToArray(T[] a) => a = ToArray();
    public bool IsEmpty() => size == 0;
    public T Get(int index) => elementD[index];
    public void Set(int index, T e) => elementD[index] = e;
    public T this[int index]
    {
        get => Get(index);
        set => Set(index, value);
    }

    public void Clear()
    {
        RemoveAll(elementD);
        size = 0;
    }

    public void Add(T e)
    {
        if (size == elementD.Length)
        {
            try
            {
                T[] newData = new T[(int)(elementD.Length * 1.5 + 1)];
                Array.Copy(elementD, newData, size);
                elementD = newData;
            }
            catch (ArrayListOutOfMemoryException ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }
        }
        elementD[size++] = e;
    }
    public void AddAll(T[] a)
    {
        if (a == null) throw new ArrayListArgumentNullException();
        foreach (T e in a)
            Add(e);
    }
    public void Add(int index, T e)
    {
        try
        {
            Add(e);
            T tmp = elementD[size - 1];
            for (int i = size - 1; i > index; --i)
                elementD[i] = elementD[i - 1];
            elementD[index] = tmp;
        }
        catch (ArrayListIndexOutOfRangeException ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
    public void AddAll(int index, T[] a)
    {
        if (a == null) throw new ArrayListArgumentNullException();
        for (int i = 0; i < a.Length; ++i)
            Add(index + i, a[i]);
    }

    public bool Contains(object o)
    {
        if (o == null) throw new ArrayListArgumentNullException();
        return IndexOf(o) != -1;
    }
    public bool ContainsAll(T[] a)
    {
        if (a == null) throw new ArrayListArgumentNullException();
        foreach (T e in a)
            if (!Contains(e)) return false;
        return true;
    }

    public void Remove(object o)
    {
        if (o == null) throw new ArrayListArgumentNullException();
        if (!Contains(o)) return;
        int index = lastIndexOf(o); --size;
        while (index < size)
        {
            elementD[index] = elementD[index + 1];
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
        if (a == null) throw new ArrayListArgumentNullException();
        for (int i = 0; i < size; ++i) 
        {
            bool f = true;
            foreach (T el in a)
                if (el.Equals(elementD[i])) f = false;
            if (f) Remove(elementD[i]);
        }
    }
    public T Remove(int index)
    {
        try
        {
            T res = elementD[index]; --size;
            while (index < size)
            {
                elementD[index] = elementD[index + 1];
                ++index;
            }
            return res;
        }
        catch (ArrayListIndexOutOfRangeException ex)
        {
            Console.WriteLine(ex.Message);
            return default;
        }
    }

    public int IndexOf(object o)
    {
        if (o == null) throw new ArrayListArgumentNullException();
        for (int i = 0; i < size; ++i)
            if (o.Equals(elementD[i])) return i;
        return -1;
    }
    public int lastIndexOf(object o)
    {
        if (o == null) throw new ArrayListArgumentNullException();
        for (int i = size - 1; i >= 0; --i)
            if (o.Equals(elementD[i])) return i;
        return -1;
    }

    public MyArrayList<T> subList(int fromIndex, int toIndex)
    {
        try
        {
            T[] vals = new T[toIndex - fromIndex];
            Array.Copy(elementD, fromIndex, vals, 0, vals.Length);
            return new MyArrayList<T>(vals);
        }
        catch (ArrayListException ex)
        {
            Console.WriteLine(ex.Message);
            return null;
        }
    }
}