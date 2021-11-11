using System;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace DZ3
{
    class Program
    {
        static void Main(string[] args)
        {
            BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args);
        }
    }

    public class PointClass
    {
        public int X;
        public int Y;
    }

    public struct PointStruct
    {
        public int X;
        public int Y;
    }

    public class BechmarkClass
    {
        [Params(0, 1, 2)] //Новое
        public int AIdx { get; set; }

        [Params(0, 1, 2)] //Новое
        public int BIdx { get; set; }

        private PointClass[] clArray = ClassArray(); //Объявлен массив типа PointClass
        private PointStruct[] stArray = StructArray();//Объявлен массив типа PointStruct

        public static PointClass[] ClassArray()
        {
            var rand = new Random(100);
            PointClass[] arrayPointClass = new PointClass[3];
            for (int i = 0; i < 3; i++)
            {
                PointClass arrGenClass = new PointClass() { X = 1 * i, Y = 5 * i };
                arrayPointClass[i] = arrGenClass;
            }

            return arrayPointClass;
        }


        public static PointStruct[] StructArray()
        {
            var rand = new Random(100);
            PointStruct[] arrayPointStruct = new PointStruct[3];
            for (int i = 0; i < 3; i++)
            {
                PointStruct arrGenClass = new PointStruct() { X = 1 * i, Y = 5 * i };
                arrayPointStruct[i] = arrGenClass;
            }
            return arrayPointStruct;
        }


        public static float PointDistanceClass(PointClass pointOne, PointClass pointTwo)
        {
            float x = pointOne.X - pointTwo.X;
            float y = pointOne.Y - pointTwo.Y;
            return MathF.Sqrt((x * x) + (y * y));
        }
        public static float PointDistance(PointStruct pointOne, PointStruct pointTwo)
        {
            float x = pointOne.X - pointTwo.X;
            float y = pointOne.Y - pointTwo.Y;
            return MathF.Sqrt((x * x) + (y * y));
        }

        public static float PointDistanceShort(PointStruct pointOne, PointStruct pointTwo)
        {
            float x = pointOne.X - pointTwo.X;
            float y = pointOne.Y - pointTwo.Y;
            return (x * x) + (y * y);
        }

        public static double PointDistanceDouble(PointStruct pointOne, PointStruct pointTwo)
        {
            double x = pointOne.X - pointTwo.X;
            double y = pointOne.Y - pointTwo.Y;
            return Math.Sqrt((x * x) + (y * y));
        }


        [Benchmark]
        public void floatPointDistanceClass()
        {
            var pointOne = clArray[AIdx];
            var pointTwo = clArray[BIdx];
            PointDistanceClass(pointOne, pointTwo);

        }

        [Benchmark]
        public void floatPointDistance()
        {
            var pointOne = stArray[AIdx];
            var pointTwo = stArray[BIdx];
            PointDistance(pointOne, pointTwo);
        }

        [Benchmark]
        public void PointDistanceShort()
        {
            var pointOne = stArray[AIdx];
            var pointTwo = stArray[BIdx];
            PointDistanceShort(pointOne, pointTwo);
        }

        [Benchmark]
        public void PointDistanceDouble()
        {
            var pointOne = stArray[AIdx];
            var pointTwo = stArray[BIdx];
            PointDistanceDouble(pointOne, pointTwo);
        }
    }
}