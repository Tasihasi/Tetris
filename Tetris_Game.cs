using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris_game
{
    internal class Tetris_Game
    {
        Random random = new Random();

        int gridWidth;
        int gridHeight;
        int[,] board;
        int[,] fallingPiece;
        bool dead;

        List<int[,]> pieces = new List<int[,]>();
        public Tetris_Game(int gridWidth, int gridHeight)
        {
            this.gridWidth = gridWidth;
            this.gridHeight = gridHeight;
            this.dead = false;
            this.board = new int[gridWidth, gridHeight];
            SettingTetrisPiecesList();
        }

        private  void SettingTetrisPiecesList()
        {
            int[,] piece = new int[,] {
                    { 0, 5, 0 },
                    { 5, 5, 5 },
                                };
           this.pieces.Add(piece);

            piece = new int[,] {
                    { 1, 1, 0 },
                    { 0, 1, 1 },
                                };
            this.pieces.Add(piece);

            piece = new int[,] {
                    { 2, 2},
                    { 2, 2}
                                };
            this.pieces.Add(piece);

            piece = new int[,] {
                    { 3, 3, 3 , 3},
                                };
            this.pieces.Add(piece);

            piece = new int[,] {
                    { 0, 4, 4 },
                    { 4, 4, 0 },
                                };
            this.pieces.Add(piece);

            piece = new int[,] {
                    { 6, 6, 6 },
                    { 0, 6, 0 },
                                };
            this.pieces.Add(piece);
        }  // sets the list of pieces in the constructor
        private  void FallingPieceGenerator()
        {
            var idx = random.Next(0, 6);
            fallingPiece = pieces[idx];
        }   // sets the current fallling piece to random

        public void Game()
        {
            FallingPieceGenerator();
            PieceOnBoardPlacer();
            BoardDrawer();
            for (int i = 0; i < 25; i++)
            {
                OneRound();
                Console.WriteLine(i);
            }
        }
        private void OneRound()
        {
            DeathChecker();
            Input(500);
            MoveDown();
            BoardDrawer();
            Console.WriteLine();
            Console.WriteLine("\t" + "one Round");
        }
        // =============== Input Handle ===========

        private void MoveSide(int s)
        {
            
                for (int i = 0; i < gridWidth; i++)
                {
                    for (int j = 0; j < gridHeight; j++)
                    {
                        if (i - 1 > 0 && i + 1 < gridWidth && board[i, j] != 0 && board[i,j] < 10)
                        {
                            if (s == 0)
                            {
                                board[i - 1, j] = board[i, j];
                                board[i, j] = 0;
                            }
                            else if(s == 1)
                            {
                                board[i + 1, j] = board[i,j];
                                board[i, j] = 0;
                            }
                        }
                    }
                }
            
        } // moves the falling piece 0 - left 1 - right

        protected void Input(int speed)
        {
            System.Threading.Thread.Sleep(speed);
            if (Console.KeyAvailable)
            {
                var input = Console.ReadKey();
                if (input.KeyChar == 'a')
                {
                    MoveSide(0);
                }
                else if(input.KeyChar == 'd')
                {
                    MoveSide(1);
                }
            }
        }  // handles the human input
        // ================ Death Handle ============
        private void DeathChecker()
        {
            for (int i = 0; i < gridWidth; i++)
            {
                if (board[0,i] != 0)
                {
                    dead = true;
                }
            }
        }  // hecks if the death


        // ============ Move Segment ==============

        private void PieceOnBoardPlacer()
        {
            for (int i = 0; i < gridWidth; i++)
            {
                for (int j = 0; j < gridHeight; j++)
                {
                    if (i < fallingPiece.GetLength(0) && j < fallingPiece.GetLength(1) && board[i,j] != fallingPiece[i,j])
                    {
                        board[i, j] = fallingPiece[i, j];
                    }
                }
            }
        }  // places the falling piece on board at first

        private void Placed()
        {
            for (int i = 0; i < gridWidth; i++)
            {
                for (int j = 0; j < gridHeight; j++)
                {
                    if (board[i,j] != 0 && board[i,j] < 10)
                    {
                        board[i, j] += 10;
                    }
                }
            }
            FallingPieceGenerator();
        }  // calls when placed
        private bool IfPlaced(int y)
        {
            if (y + 1 == gridHeight)
            {
                return true;
            }

            for (int i = 0; i < gridWidth; i++)
            {
                for (int j = 0; j < gridHeight; j++)
                {
                    if (j + 1 != gridHeight &&  board[i, j] < 10 && board[i, j+1] >  10)
                    {
                        return true;
                    }
                }
            }

            return false;
        }  //check if placed
        private void MoveDown()
        {
            Console.Clear();

            for (int i = 0; i < gridWidth; i++)
            {
                for (int j = 0; j < gridHeight; j++)
                {
                    if (IfPlaced(j))
                    {
                        Placed();
                    }

                    else if (j + 1 != gridHeight && board[i,j] != 0 && board[i, j] < 10)
                    {
                        board[i, j + 1] = board[i, j];
                        board[i, j] = 0;
                    }
                }
            }
        }

        private void BoardDrawer()
        {
            Console.Clear();
            for (int x = 0; x < gridWidth; x++)
            {
                for (int y = 0; y < gridHeight; y++)
                {
                    // Set the console foreground color based on the value in the grid array
                    switch (board[x, y])
                    {
                        case 0:
                            Console.BackgroundColor = ConsoleColor.Black;
                            break;
                        case 1:
                            Console.BackgroundColor = ConsoleColor.Green;
                            break;
                        //case 2:
                        //    Console.BackgroundColor = ConsoleColor.Red;
                        //    break;
                        //case 3:
                        //    Console.BackgroundColor = ConsoleColor.Cyan;
                        //    break;
                        //case 4:
                        //    Console.BackgroundColor = ConsoleColor.Magenta;
                        //    break;
                        //case 5:
                        //    Console.BackgroundColor = ConsoleColor.Yellow;
                        //    break;
                        //case 6:
                        //    Console.BackgroundColor = ConsoleColor.Blue;
                        //    break;
                        //case 11:
                        //    Console.BackgroundColor = ConsoleColor.Green;
                        //    break;
                        //case 12:
                        //    Console.BackgroundColor = ConsoleColor.Red;
                        //    break;
                        //case 13:
                        //    Console.BackgroundColor = ConsoleColor.Cyan;
                        //    break;
                        //case 14:
                        //    Console.BackgroundColor = ConsoleColor.Magenta;
                        //    break;
                        //case 15:
                        //    Console.BackgroundColor = ConsoleColor.Yellow;
                        //    break;
                        //case 16:
                        //    Console.BackgroundColor = ConsoleColor.Blue;
                        //    break;
                    }

                    // Write a single space character at the current position
                    Console.SetCursorPosition(x , y );
                    Console.Write(" ");
                }
            }
        } // draws the game 


    }
}
