namespace Task17_;

class MyArrayList<T>
{
    private T[] elementData;
    private int size;
    public MyArrayList()
    {
        elementData = new T[1];
        size = 0;
    }
    public MyArrayList(T[] a)
    {
        if (a == null) throw new ArrayListArgumentNullException();
        elementData = a;
        size = a.Length;
    }
    public MyArrayList(int capacity)
    {
        if (capacity < 1) throw new ArrayListArgumentException();
        try
        {
            elementData = new T[capacity];
        }
        catch (OutOfMemoryException ex)
        {
            Console.WriteLine(ex.Message);
            elementData = new T[1];
        }
        size = 0;
    }

    public int Size() => size;
    public T[] ToArray()
    {
        T[] arr = new T[size];
        Array.Copy(elementData, arr, size);
        return arr;
    }
    public void ToArray(T[] a) => a = ToArray();
    public bool IsEmpty() => size == 0;
    public T Get(int index) => elementData[index];
    public void Set(int index, T e) => elementData[index] = e;
    public T this[int index]
    {
        get => Get(index);
        set => Set(index, value);
    }

    public void Clear()
    {
        RemoveAll(elementData);
        size = 0;
    }

    public void Add(T e)
    {
        if (size == elementData.Length)
        {
            try
            {
                T[] newData = new T[(int)(elementData.Length * 1.5 + 1)];
                Array.Copy(elementData, newData, size);
                elementData = newData;
            }
            catch (ArrayListOutOfMemoryException ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }
        }
        elementData[size++] = e;
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
            T tmp = elementData[size - 1];
            for (int i = size - 1; i > index; --i)
                elementData[i] = elementData[i - 1];
            elementData[index] = tmp;
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
                if (el.Equals(elementData[i])) f = false;
            if (f) Remove(elementData[i]);
        }
    }
    public T Remove(int index)
    {
        try
        {
            T res = elementData[index]; --size;
            while (index < size)
            {
                elementData[index] = elementData[index + 1];
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
    public void Remove(object o)
    {
        if (o == null) throw new ArrayListArgumentNullException();
        if (!Contains(o)) return;
        int index = lastIndexOf(o); --size;
        while (index < size)
        {
            elementData[index] = elementData[index + 1];
            ++index;
        }
    }

    public int IndexOf(object o)
    {
        if (o == null) throw new ArrayListArgumentNullException();
        for (int i = 0; i < size; ++i)
            if (o.Equals(elementData[i])) return i;
        return -1;
    }
    public int lastIndexOf(object o)
    {
        if (o == null) throw new ArrayListArgumentNullException();
        for (int i = size - 1; i >= 0; --i)
            if (o.Equals(elementData[i])) return i;
        return -1;
    }

    public MyArrayList<T> subList(int fromIndex, int toIndex)
    {
        try
        {
            T[] vals = new T[toIndex - fromIndex];
            Array.Copy(elementData, fromIndex, vals, 0, vals.Length);
            return new MyArrayList<T>(vals);
        }
        catch (ArrayListException ex)
        {
            Console.WriteLine(ex.Message);
            return null;
        }
    }
}