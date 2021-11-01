using System;

namespace DZ3
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(F(int.Parse(Console.ReadLine())));
            ulong F(int n)
            {
                if (n == 1 || n == 2)
                {
                    return 1;
                }
                return F(n-1)+F(n-2);
            }
            Console.WriteLine("Теперь без использования функции. Введите номер: ");
            int n = int.Parse(Console.ReadLine());
            int i = 1, j=1, k=0;
            for (; k < n/2; )
            {
                i += j; j += i;
                k += 1;
                Console.WriteLine(i + ", "+j + ", ");
            }
            Console.WriteLine("Номер Фибоначчи: "+j);

        }
    }
}
