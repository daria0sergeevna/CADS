namespace Task10;

using System;
class Program
{
    static void Main()
    {
        int N = int.Parse(Console.ReadLine());
        MyVector<MyVector<int>> arr = new MyVector<MyVector<int>>(N);
        Random random = new Random();
        for (int i = 0; i < N; ++i)
        {
            int n = random.Next(100);
            arr[i] = new MyVector<int>(n);
            for (int j = 0; j < n; j++)
                arr[i].Add(random.Next());
        }
        arr[N / 2].Clear(); arr[N/2+1].Clear();
        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j < arr[i].Size(); j++)
                Console.Write(arr[i][j] + " ");
            Console.WriteLine();
        }
    }
}