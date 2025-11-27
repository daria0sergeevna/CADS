class Program
{
    static void Main()
    {
        int N = int.Parse(Console.ReadLine());
        MyArrayList<MyArrayList<int>> arr = new MyArrayList<MyArrayList<int>>(N);
        Random random = new Random();
        for(int i = 0; i < N; ++i)
        {
            int n = random.Next(1000);
            arr[i] = new MyArrayList<int>(n);
            for (int j = 0; j < n; ++j)
                arr[i].Add(random.Next());
        }
        arr[N / 2].Clear();
        for (int i = 0; i < N; ++i)
        {
            for (int j = 0; j < arr[i].Size(); ++j)
                Console.Write(arr[i][j] + " ");
            Console.WriteLine();
        }
    }
}