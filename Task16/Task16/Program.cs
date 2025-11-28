namespace Task16;

class Program
{
    static void Main()
    {
        MyLinkedList<int> a = new MyLinkedList<int>();

        a.Add(1);
        a.Add(2);
        a.Add(3);
        a.Add(23);
        a.Add(43);
        a.Add(15);
        a[0] = 4;
        Console.WriteLine(a[0]);
        Console.WriteLine(a[1]);
        Console.WriteLine(a[5]);
    }
}