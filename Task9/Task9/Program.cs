namespace Task9;
class Program
{
    static void Main()
    {
        string stream = "";
        using(StreamReader sw = new StreamReader("input11.txt"))
        {
            while (!sw.EndOfStream)
                stream += sw.ReadLine();
        }
        MyArrayList<string> arr = new MyArrayList<string>();
        string s = ""; bool inside = false;
        for(int i = 0; i < stream.Length; ++i)
        {
            if (stream[i] == '<')
            {
                inside = true;
                continue;
            }
            if (stream[i] == '>')
            {
                inside = false;
                arr.Add(s);
                s = "";
                continue;
            }
            if(inside)
                s += stream[i];
        }
        for (int i = 0; i < arr.Size(); ++i)
        {
            if (string.IsNullOrEmpty(arr[i])) continue;
            arr[i] = arr[i].ToLower();
            if (arr[i][0] == '/')
                arr[i] = arr[i].Remove(0, 1);
        }
        MyArrayList<string> uniq = new MyArrayList<string>();
        uniq[0] = "";
        for (int i = 0; i < arr.Size(); ++i)
        {
            if (string.IsNullOrEmpty(arr[i])) continue;
            inside = false;
            for (int j = 0; j < uniq.Size() && !inside; ++j)
                if (arr[i] == uniq[j])
                    inside = true;
            if (!inside && arr[i][0] != '!') uniq.Add(arr[i]);
        }
        arr = new MyArrayList<string>(uniq.ToArray());
        for(int i = 0; i < arr.Size(); ++i)
            Console.WriteLine(arr[i]);
    }
}