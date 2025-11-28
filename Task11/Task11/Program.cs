namespace Task11;
using System;
using System.IO;
using System.Collections.Generic;

class Program
{
    static bool IsDigit(char c)
    {
        return c >= '0' && c <= '9';
    }
    
    static bool IsValidIP(string s)
    {
        if (string.IsNullOrEmpty(s) || s[s.Length - 1] == '.' || s[0] == '.')
            return false;
            
    
        string[] parts = s.Split('.');
        if (parts.Length != 4) return false;
        

        foreach (string part in parts)
        {
            if (part.Length == 0) return false;
            
       
            if (part.Length > 1 && part[0] == '0') return false;
            
       
            foreach (char c in part)
                if (!IsDigit(c)) return false;
            
            int val = int.Parse(part);
            if (val > 255) return false;
        }
        return true;
    }
    
    static List<string> FindPotentialIPs(string str)
    {
        List<string> result = new List<string>();
        
        for (int i = 0; i < str.Length; i++)
        {
     
            if (IsDigit(str[i]) || (i > 0 && !IsDigit(str[i-1]) && str[i] == '.' && i + 1 < str.Length && IsDigit(str[i+1])))
            {
                for (int j = i + 1; j <= str.Length; j++)
                {
              
                    if (j == str.Length || (!IsDigit(str[j]) && str[j] != '.'))
                    {
                        string candidate = str.Substring(i, j - i);
                        if (IsValidIP(candidate))
                        {
                            result.Add(candidate);
                        }
                        i = j; 
                        break;
                    }
                }
            }
        }
        return result;
    }
    

    static bool NoOverlapWithNumbers(string original, string ip)
    {
        int ipIndex = original.IndexOf(ip);
        if (ipIndex == -1) return false;
        
     
        if (ipIndex > 0 && IsDigit(original[ipIndex - 1]))
            return false;
            
    
        int afterIp = ipIndex + ip.Length;
        if (afterIp < original.Length && IsDigit(original[afterIp]))
            return false;
            
        return true;
    }

    static void Main()
    {
        MyVector<string> lines = new MyVector<string>();
        
   
        using (StreamReader sr = new StreamReader("inputV.txt"))
        {
            while (!sr.EndOfStream)
            {
                string line = sr.ReadLine();
                if (!string.IsNullOrEmpty(line))
                    lines.Add(line);
            }
        }
        
        MyVector<string> ans = new MyVector<string>();
        
 
        for (int i = 0; i < lines.Size(); i++)
        {
            string line = lines[i];
            List<string> potentialIPs = FindPotentialIPs(line);
            
            foreach (string ip in potentialIPs)
            {
                if (NoOverlapWithNumbers(line, ip))
                {
                    ans.Add(ip);
                }
            }
        }
        
 
        using (StreamWriter sw = new StreamWriter("outputV.txt"))
        {
            for (int i = 0; i < ans.Size(); i++)
                sw.WriteLine(ans[i]);
        }
    }
}