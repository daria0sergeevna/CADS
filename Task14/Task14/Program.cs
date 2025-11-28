namespace Task14;
class Program
{
    static void Main()
    {
        MyArrayDeque<int> myArrayDeque = new MyArrayDeque<int>();
        for(int i = 0; i < 10; i++)
            myArrayDeque.Add(i);
        Console.WriteLine(myArrayDeque.IsEmpty());
    }
};