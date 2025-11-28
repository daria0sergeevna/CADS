namespace Task15;
using System;
using System.IO;
class Program
{
    static int DigitCount(string s)
    {
        int count = 0;
        foreach(char c in s) 
            if(c >= '0' && c <= '9')
                ++count;
        return count;
    }
    static int SpacesCount(string s)
    {
        int count = 0;
        foreach (char c in s)
            if (c == ' ')
                ++count;
        return count;
    }
    static void Main()
    {
        MyArrayDeque<string> strings = new MyArrayDeque<string>();
        using (StreamReader sr = new StreamReader("inputD.txt"))
        {
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                if (strings.IsEmpty())
                {
                    strings.Add(line);
                }
                else
                {
                    int currentDigitCount = DigitCount(line);
                    int firstDigitCount = DigitCount(strings.GetFirst());

                    if (currentDigitCount > firstDigitCount)
                        strings.AddLast(line);
                    else
                        strings.AddFirst(line);
                }
            }
        }
        using (StreamWriter sw = new StreamWriter("sortedD.txt", false))
        {
            string[] str = strings.ToArray();
            foreach(string s in str)
                sw.WriteLine(s);
        }
        int n = int.Parse(Console.ReadLine());
        List<string> toRemove = new List<string>();
        foreach (string s in strings.ToArray())
        {
            if (s != null && SpacesCount(s) > n)
                toRemove.Add(s);
        }
        foreach (string s in toRemove)
        {
            strings.Remove(s);
        }
        foreach (string s in strings.ToArray())
            Console.WriteLine(s);
    }
}