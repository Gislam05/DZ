using System;
using System.Diagnostics;

namespace DZ6
{
    class Program
    {
        static void Main(string[] args)
        {
            Process[] processes = Process.GetProcesses();
            for (int i = 0; i < processes.Length; i++)
            {
                Console.WriteLine(processes[i].Id + " "+ processes[i]);
            }
            Console.WriteLine();
            Console.WriteLine("Какой процесс хочешь убить? Введи его ID");
            int id = int.Parse (Console.ReadLine());
            Process.GetProcessById(id).Kill();
            Console.WriteLine($"{Process.GetProcessById(id).ProcessName} завершен");
        }
    }
}
