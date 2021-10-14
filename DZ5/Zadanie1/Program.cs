using System;
using System.IO;

namespace DZ5
{
    class Program
    {
        static void Main(string[] args)
        {
            File.AppendAllText("startup.txt", "New Text");
            
        }
    }
}
