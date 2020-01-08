using System;
using System.Drawing;
using System.Linq;

namespace Matrix
{
    class Program
    {
        public static Point sizeMatrix = new Point();
        public static float[,] matrix;
        private static int longestFloat;

        static void Main(string[] args)
        {
            Restart();
        }

        public static void Restart()
        {
            Console.Clear();
            Console.WriteLine("Create array type: array[x][y]. Please write x and y separated by space");
            string input = Console.ReadLine();
            bool isValidInput = input.All(c => char.IsDigit(c) || char.IsWhiteSpace(c));

            if (!isValidInput)
            {
                Console.WriteLine("Error. Please write only numbers. Without chars");
                Console.ReadKey();
                Restart();
            }

            if (input.Split().Length != 2)
            {
                Console.WriteLine("Error, please write only TWO numbers. Not more");
                Console.ReadKey();
                Restart();
            }

            try
            {
                sizeMatrix = new Point(Int32.Parse(input.Split()[0]),
                    Int32.Parse(input.Split()[1]));
            }
            catch (System.OverflowException e)
            {
                Console.WriteLine(e.Message);
                Console.ReadKey();
                Restart();
            } 

            matrix = new float[sizeMatrix.X, sizeMatrix.Y];

            for (int i = 0; i < sizeMatrix.X; i++)
                for (int j = 0; j < sizeMatrix.Y; j++)
                {
                    float n;
                    Console.Write($"\narray[{i}][{j}] = ");
                    var tempNumber = Console.ReadLine();

                    if (float.TryParse(tempNumber, out n))
                    {
                        if (tempNumber.Length > longestFloat)
                            longestFloat = tempNumber.Length;

                        matrix[i, j] = float.Parse(tempNumber);
                    }
                    else
                    {
                        Console.WriteLine("Write correct numbers. AnyKey to restart");
                        Console.ReadKey();
                        Restart();
                    }
                }

            MatrixShow();

            while (true)
            {
                Console.WriteLine("\nWrite 1 to sort ascending\n" +
                                  "Write 2 to sort descending\n" +
                                  "Write 3 to find count numbers>0\n" +
                                  "Write 4 to find count numbers <0\n" +
                                  "Write 5 to inverse line\n" +
                                  "Write 6 to show matrix");

                switch (Console.ReadLine())
                {
                    case "1":
                        Console.WriteLine("Write line number to sort");
                        int line;
                        if (int.TryParse(Console.ReadLine(), out line) && line <= sizeMatrix.X && line > 0)
                        {
                            Sort(line, 1);
                            MatrixShow();
                            continue;
                        }
                        else
                            Console.WriteLine("Error");

                        break;
                    case "2":
                        Console.WriteLine("Write line number to sort");
                        if (int.TryParse(Console.ReadLine(), out line) && line <= sizeMatrix.X && line > 0)
                        {
                            Sort(line, 2);
                            MatrixShow();
                            continue;
                        }
                        else
                        {
                            Console.WriteLine("Error");
                            continue;
                        }

                        break;
                    case "3":
                        int count = 0;

                        foreach (var number in matrix)
                            if (number > 0) count++;

                        Console.WriteLine($"Count numbers >0 = {count}");
                        continue;
                    case "4":
                        count = 0;

                        foreach (var number in matrix)
                            if (number < 0) count++;

                        Console.WriteLine($"Count numbers <0 = {count}");
                        continue;
                    case "5":
                        Console.WriteLine("Write line number to inverse");

                        if (int.TryParse(Console.ReadLine(), out line) && line <= sizeMatrix.X && line > 0)
                        {
                            InverseMatrix(line);
                            MatrixShow();
                            continue;
                        }
                        else
                        {
                            Console.WriteLine("Error");
                            continue;
                        }
                    case "6":
                        MatrixShow();
                        continue;
                    default:
                        Console.WriteLine("Error. Write only 1-6");
                        continue;
                }
            }
        }

        public static void MatrixShow()
        {
            try
            {
                int cursorX = 0;
                if (longestFloat > 13) longestFloat = 13;
                for (int i = 0; i < sizeMatrix.X; i++)
                {
                    for (int j = 0; j < sizeMatrix.Y; j++)
                    {
                        cursorX = (j * longestFloat) + 5;
                        Console.SetCursorPosition(cursorX, Console.CursorTop);
                        Console.Write($" | {matrix[i, j]}");
                    }
                    Console.WriteLine();
                }
            }
            catch (System.ArgumentOutOfRangeException e)
            {
                Console.WriteLine("\nMatrix is more then console size, please enter smaller numbers ");
                Console.ReadKey();
                Restart();
            }
          
        }

        public static void InverseMatrix(int line)
        {
            line -= 1;
            float[] inverseMatrix = new float[sizeMatrix.Y];
            for (int i = 0; i < sizeMatrix.Y; i++)
                inverseMatrix[i] = matrix[line, sizeMatrix.Y - 1 - i];
            for (int i = 0; i < sizeMatrix.Y; i++)
                matrix[line, i] = inverseMatrix[i];
        }

        public static void Sort(int line, int type)
        {
           float temp;
            line -= 1;
            switch (type)
            {
                case 1: // ascending 
                    for (int i = 0; i < sizeMatrix.Y - 1; i++)
                    {
                        for (int j = i + 1; j < sizeMatrix.Y; j++)
                        {
                            if (matrix[line, i] > matrix[line, j])
                            {
                                temp = matrix[line, i];
                                matrix[line, i] = matrix[line, j];
                                matrix[line, j] = temp;
                            }
                        }
                    }

                    break;
                case 2: // descending
                    for (int i = 0; i < sizeMatrix.Y - 1; i++)
                    {
                        for (int j = i + 1; j < sizeMatrix.Y; j++)
                        {
                            if (matrix[line, i] < matrix[line, j])
                            {
                                temp = matrix[line, i];
                                matrix[line, i] = matrix[line, j];
                                matrix[line, j] = temp;
                            }
                        }
                    }

                    break;
            }
        }
    }
}
