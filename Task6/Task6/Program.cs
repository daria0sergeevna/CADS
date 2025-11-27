using System;
class Program
{
    static void Main()
    {
        MyPriorityQueue<int> q = new MyPriorityQueue<int>();
        int[] a = { 7, 15, 3, 4, 60, 12, 3, 1, 12, 3, 123, 64, -6, -4, 32, -6 };
        q.AddAll(a);
        Console.WriteLine(q.Element());
        Console.WriteLine(q.Size()); int[] b = { 13, 11, 123, -4 };
        q.RemoveAll(b);
        Console.WriteLine(q.Element());
        Console.WriteLine(q.Size());

        Random rand = new Random();
        int n = rand.Next(0, 256);
        string[] s = new string[n]; 
        for (int i = 0; i < n; ++i)
        {
            int m = rand.Next(0, 256);
            for (int j = 0; j < m; ++j)
                s[i] += (char)rand.Next(0, 256);
        }
        MyPriorityQueue<string> strQ = new MyPriorityQueue<string>(s);
        Console.WriteLine(strQ.Element());
        Console.WriteLine(strQ.Size());
    }
}