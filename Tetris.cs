using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris_game
{
    internal class Tetris
    {
        Random rnd = new Random();

        int width;
        int height;
        int speed;
        int[,] grid;
        bool alive;
        List<int[,]> tetrominoes = new List<int[,]>();
        int[,] fallingTetromiod;
        private int tetrominoX;
        private int tetrominoY;

        public Tetris(int width, int height, int speed)
        {
            this.width = width;
            this.height = height;
            this.speed = speed;
            this.alive = true;
            this.tetrominoY = 0;
            this.tetrominoX = 0;
            this.grid = new int[height, width];
            TetromoniesListGenerator();
        }

        private void TetromoniesListGenerator()
        {
            tetrominoes.Add(new int[,] {
                { 1, 0, 0, 0 },
                { 1, 0, 0, 0 },
                { 1, 0, 0, 0 },
                { 1, 0, 0, 0 }
                });

            //Add the "J" Tetromino to the list
            tetrominoes.Add(new int[,] {
                { 0, 1, 0 },
                { 0, 1, 0 },
                { 1, 1, 0 }
                });
            // Add the "L" Shape
            tetrominoes.Add(new int[,] {
                {0, 1, 0},
                {0, 1, 0},
                {0, 1, 1}
                });
            // Add the "O" Shape
            tetrominoes.Add(new int[,] {
                {1, 1},
                {1, 1}
                });

            //tetrominoes.Add(new int[,] {
            //    { 1 },

            //    });
        }
        private void NewTetromiod()
        {
            var random = rnd.Next(0, tetrominoes.Count());
            fallingTetromiod = tetrominoes[random];
        }

        public void Game()
        {
            NewTetromiod();
            int i = 0;
            while (alive)
            {
                OneRound();
                Console.WriteLine(i);
                i++;
            }
            Console.WriteLine("game ended");
        }

        private void OneRound()
        {
            Clearing();
            InputHandle();
            
            TetromiodPositionig();
            ColideChecker();
            Draw();
            MatrixReader(grid);
            tetrominoX++;
            
        }

        private void InputHandle()
        {
            System.Threading.Thread.Sleep(speed);
            if (Console.KeyAvailable) {
                Console.WriteLine("high!!!!!");
                ConsoleKeyInfo key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.LeftArrow && IfCanmoveLeft())
                {
                    if (tetrominoY > 0)
                    {
                        tetrominoY--;
                    }
                }
                else if (key.Key == ConsoleKey.RightArrow && IfCanmoveRight())
                {
                    if (tetrominoY < width)
                    {
                        tetrominoY++;
                    }
                }
            }
        }   // moves the tetroid left to right acording to input
        private void FixingPieces()
        {
            for (int x = 0; x < grid.GetLength(0); x++)
            {
                string outPut = "";
                for (int y = 0; y < grid.GetLength(1); y++)
                {
                    if (grid[x, y] == 1)
                    {
                        grid[x, y] = 2;
                    }
                }
            }
        }

        private void Clearing()
        {
            for (int x = 0; x < grid.GetLength(0); x++)
            {
                for (int y = 0; y < grid.GetLength(1); y++)
                {
                    if (grid[x, y] == 1)
                    {
                        grid[x, y] = 0;
                    }
                }
            }
        }

       
        private void TetromiodPositionig()
        {
            for (int x = 0; x < fallingTetromiod.GetLength(0); x++)
            {
                for (int y = 0; y < fallingTetromiod.GetLength(1); y++)
                {
                    if (fallingTetromiod[x, y] != 0)
                    {
                        grid[tetrominoX + x, tetrominoY +  y] = 1;
                    }
                }
            }
        }  // moves the falling tetroid on the board

        // ======  Hit situation handle =======

        private bool IfCanmoveRight()
        {
            for (int x = 0; x < fallingTetromiod.GetLength(0); x++)
            {
                for (int y = 0; y < fallingTetromiod.GetLength(1); y++)
                {
                    if (fallingTetromiod[x,y] == 1  && tetrominoY + y + 1 < width && grid[tetrominoX +  x,tetrominoY + y + 1] == 2)
                    {
                        return false;
                    }
                    if (fallingTetromiod[x,y] == 1 && tetrominoY + y + 1 == width)
                    {
                        return false;
                    }
                }
            }

            return true;
        }
        private bool IfCanmoveLeft()
        {
            for (int x = 0; x < fallingTetromiod.GetLength(0); x++)
            {
                for (int y = 0; y < fallingTetromiod.GetLength(1); y++)
                {
                    if (fallingTetromiod[x, y] == 1 && tetrominoY + y > 0 && grid[tetrominoX + x, tetrominoY + y - 1] == 2)
                    {
                        return false;
                    }
                    //if (fallingTetromiod[x, y] == 1 && tetrominoY + y - 1 == 0) 
                    //{
                    //    return false;
                    //}
                }
            }

            return true;
        }
       

        private void ColideChecker()
        {
            bool isCollision = false;

            for (int x = 0; x < grid.GetLength(0); x++)
            {
                for (int y = 0; y < grid.GetLength(1); y++)
                {
                    if (grid[x, y] == 1)
                    {
                        int boardX = tetrominoX + x;
                        int boardY = tetrominoY + y;

                        if (x + 1 == height && tetrominoX > fallingTetromiod.GetLength(0))
                        {
                            // The Tetromino has hit the botom of the game board
                            isCollision = true;
                            break;
                        }

                        if (grid[x + 1, y] == 2)
                        {
                            // The Tetromino has hit another Tetromino on the game board
                            isCollision = true;
                            break;
                        }
                    }
                }
                if (isCollision) break;
            }

            if (isCollision)
            {
                FixingPieces();
                NewTetromiod();
                tetrominoX = 0;
                tetrominoY = 0;
                IfCanSpawnTetroid();
                FullRowSearch();
                FallingStatPieces();
            }
        }   // checks if colided with bottom or another tetroid
        // and spawns the next tetrimo


        private void IfCanSpawnTetroid()
        {
            for (int x = 0; x < fallingTetromiod.GetLength(1); x++)
            {
                for (int y = 0; y < fallingTetromiod.GetLength(1); y++)
                {
                    if (grid[tetrominoX + x, tetrominoY + y] == 2 && fallingTetromiod[x,y] != 0)
                    {
                        Dead();
                        return;
                    }
                }
            }
        }

        private void RowDemolisher(int n)
        {
            for (int x = 0; x < grid.GetLength(0); x++)
            {
                if (x == n)
                {
                    for (int y = 0; y < grid.GetLength(1); y++)
                    {
                        grid[n, y] = 0;
                    }
                }
            }
        } // emolished the given row
        private void FullRowSearch()
        {
            for (int x = 0; x < grid.GetLength(0); x++)
            {
                bool rowIsFull = true;
                for (int y = 0; y < grid.GetLength(1); y++)
                {
                    if (grid[x,y] != 2)
                    {
                        rowIsFull = false;
                    }
                }
                if (rowIsFull)
                {
                    RowDemolisher(x);
                }
            }
        } // searches  for full row

        private void FallingStatPieces()
        {
            for (int x = 0; x < grid.GetLength(0); x++)
            {
                for (int y = 0; y < grid.GetLength(1); y++)
                {
                    if (grid[x,y] == 2 && x + 1 < height && grid[x + 1, y] == 0)
                    {
                        grid[x, y] = 0;
                        grid[x + 1, y] = 2;
                    }
                }
            }
        }    //manages the floating pieces

        private void Dead()
        {
            alive = false;
            Console.Clear();
            Console.WriteLine("===========================================================");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Game End!");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("===========================================================");
        }
        // ========= Visual ===============

        private void Draw()
        {
            Console.Clear();

            //Console.BackgroundColor = ConsoleColor.Blue;
            for (int x = 0; x < grid.GetLength(0); x++)
            {
                string outPut = "";
                for (int y = 0; y < grid.GetLength(1); y++)
                {
                    switch (grid[x, y])
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
        }  // Draws The grid

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
    }
}
