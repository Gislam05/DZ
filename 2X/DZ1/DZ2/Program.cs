using System;

namespace DZ2
{
    class Program
    {
        static void Main(string[] args)
        {
            static int StrangeSum(int[] inputArray)
            {
                int sum = 0; //О(1)
                for (int i = 0; i < inputArray.Length; i++) 
                {
                    for (int j = 0; j < inputArray.Length; j++) //O(n)
                    {
                        for (int k = 0; k < inputArray.Length; k++) //O(n)*O(n)
                        {
                            int y = 0; 

                            if (j != 0)
                            {
                                y = k / j; //O(n)*(O(n)-1)*O(n)
                            }

                            sum += inputArray[i] + i + k + j + y; //O(n)*O(n)*O(n)
                        }
                    }
                }

                return sum; //Если N - длина входного массива, то сложность получается O^3(N)
            }
        }
    }
}
