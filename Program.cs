using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris_game
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Tetris_Game game = new Tetris_Game(10, 20);
            //game.Game();
            Tetris Test = new Tetris(5, 10, 700);
            //Test.Game();

            int[,] test = new int[,]
            {
                {0, 1, 0, 0},
                {0, 1, 0, 0},
                {0, 1, 0, 0},
                {0, 1, 0, 0}
            };
            
                
            Console.BackgroundColor = ConsoleColor.Green;
            Console.WriteLine(" ");

            //MatrixReader(test);
            MatrixDrawer2(test);

            Console.Read();
        }

        public static void MatrixReader(int[,] mx)
        {
            Console.WriteLine();
            Console.WriteLine();
            for (int i = 0; i < mx.GetLength(0); i++)
            {
                string output = ""; 
                for (int j = 0; j < mx.GetLength(1); j++)
                {
                    output = output + mx[i, j];
                }
                Console.WriteLine(output);
            }
        }

        public static void MatrisDrawer(int[,] mx)
        {
            
            for (int x = 0; x < mx.GetLength(0); x++)
            {
                for (int y = 0; y < mx.GetLength(1); y++)
                {
                    if (mx[x, y] == 0)
                    {
                        Console.BackgroundColor = ConsoleColor.Red;
                        Console.SetCursorPosition( x, y);
                        Console.Write(" ");
                    }
                    else
                    {
                        Console.BackgroundColor = ConsoleColor.Blue;
                        Console.SetCursorPosition(x, y);
                        Console.Write(" ");
                    }
                }
            }
        }

        public static void MatrixDrawer2(int[,] mx)
        {
            for (int i = 0; i < mx.GetLength(0); i++)
            {
                string outPut = "";
                for (int j = 0; i < mx.GetLength(1); i++)
                {
                    switch (mx[i, j])
                    {
                        case 0:
                            Console.BackgroundColor = ConsoleColor.Gray;
                            outPut = outPut + " ";
                            break;
                        case 1:
                            Console.BackgroundColor = ConsoleColor.Blue;
                            outPut = outPut + " ";
                            break;
                        case 2:
                            Console.BackgroundColor = ConsoleColor.Green;
                            outPut = outPut + " ";
                            break;
                    }
                }
                Console.WriteLine(outPut);
            }
        }
    }
}
