using System;
using System.Diagnostics;
class Program
{
    public struct Application
    { 
        public int number;
        public int priority;
        public int step;
        public Application(int number, int step)
        {
            this.number = number;
            this.step = step;
            Random random = new Random();
            priority = random.Next(1, 6);
        }
        public class ApplicationComparer : PriorityQueueComparer<Application>
        {
            public override int Compare(Application x, Application y)
            {
                return x.priority.CompareTo(y.priority);
            }
        }
    }
    static void Save(Application app, bool add)
    {
        try
        {
            using (StreamWriter sw = new StreamWriter("log.txt", true))
            {
                string s = add ? "ADD" : "REMOVE";
                s += " " + app.number + " " + app.priority + " " + app.step;
                sw.WriteLine(s);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
    static void Main()
    {
        int N = int.Parse(Console.ReadLine());
        MyPriorityQueue<Application> apps = new MyPriorityQueue<Application>(N, new Application.ApplicationComparer());
        Application[] generated;
        int n, i, k = 0;
        Random random = new Random();
        Stopwatch sw = new Stopwatch();
        try
        {
            using (StreamWriter sr = new StreamWriter("log.txt", true))
            {
                sr.WriteLine("\n\n" + DateTime.Now);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return;
        }
        sw.Start();
        for (i = 0; i < N; ++i)
        {
            n = random.Next(1, 11);
            generated = new Application[n];
            for (int j = 0; j < n; ++j)
            {
                generated[j] = new Application(++k, i + 1);
                Save(generated[j], true);
            }
            apps.AddAll(generated);
            Save(apps.Poll(), false);
        }
        Application app = new Application();
        while (!apps.IsEmpty())
        {
            if (apps.Size() == 1)
            {
                app = apps.Element();
                Save(apps.Poll(), false);
                sw.Stop();
            }
            else Save(apps.Poll(), false);
            ++i;
        }
        Console.WriteLine("Больше всех пострадала заявка номер " + app.number +
            "\nПриоритет " + app.priority + "\nНомер шага: " + app.step + "\nВремя ожидания: " + sw.ElapsedMilliseconds);
    }
}