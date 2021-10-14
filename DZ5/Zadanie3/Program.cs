using System;
using System.IO;

namespace DZ5
{
    class Program
    {
        static void Main(string[] args)
        {
            byte[] array = new byte[10];
            for (int i = 0; i < 10; i++)
                array[i] = byte.Parse(Console.ReadLine());
            File.WriteAllBytes("bytes.bin", array);
           
        }
    }
}
