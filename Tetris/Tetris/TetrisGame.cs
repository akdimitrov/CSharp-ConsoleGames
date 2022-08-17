using System;
using System.Collections.Generic;

namespace Tetris
{
    public class TetrisGame : ITetrisGame
    {
        private readonly List<Tetromino> TetrisFigures = new List<Tetromino>()
        {
            new Tetromino(new bool[,] // I
            {
                {true, true, true, true}
            }),

            new Tetromino(new bool[,] // O 
            {
                { true, true },
                { true, true }
            }),

            new Tetromino(new bool[,] // T
            {
                { false, true, false },
                { true, true, true }
            }),

            new Tetromino(new bool[,] // S
            {
                { false, true, true },
                { true, true, false }
            }),

            new Tetromino(new bool[,] // Z
            {
                { true, true, false },
                { false, true, true }
            }),

            new Tetromino(new bool[,] // J
            {
                { true, false, false },
                { true, true, true }
            }),

            new Tetromino(new bool[,] // L
            {
                { false, false, true },
                { true, true, true }
            }),
        };
        private Random random;

        public TetrisGame(int tetrisRows, int tetrisCols)
        {
            TetrisRows = tetrisRows;
            TetrisCols = tetrisCols;
            TetrisField = new bool[tetrisRows, tetrisCols];
            Level = 1;
            CurrentFigure = null;
            CurrentFigureRow = 0;
            CurrentFigureCol = 0;
            random = new Random();
            NewRandomFigure();
        }

        public int TetrisRows { get; }

        public int TetrisCols { get; }

        public bool[,] TetrisField { get; private set; }

        public int Level { get; private set; }

        public Tetromino CurrentFigure { get; set; }

        public int CurrentFigureRow { get; set; }

        public int CurrentFigureCol { get; set; }

        public void UpdateLevel(int score)
        {
            if (score <= 0)
            {
                Level = 1;
                return;
            }

            Level = (int)Math.Log10(score) - 1;
            if (Level < 1)
            {
                Level = 1;
            }

            if (Level > 10)
            {
                Level = 10;
            }
        }

        public void NewRandomFigure()
        {
            CurrentFigure = TetrisFigures[random.Next(0, TetrisFigures.Count)];
            CurrentFigureRow = 0;
            CurrentFigureCol = this.TetrisCols / 2 - this.CurrentFigure.Width / 2 - 1;
        }

        public void AddCurrentFigureToTetrisField()
        {
            for (int row = 0; row < CurrentFigure.Width; row++)
            {
                for (int col = 0; col < CurrentFigure.Height; col++)
                {
                    if (CurrentFigure.Body[row, col])
                    {
                        TetrisField[CurrentFigureRow + row, CurrentFigureCol + col] = true;
                    }
                }
            }
        }

        public int CheckForFullLines()
        {
            int lines = 0;

            for (int row = 0; row < TetrisField.GetLength(0); row++)
            {
                bool rowIsFull = true;
                for (int col = 0; col < TetrisField.GetLength(1); col++)
                {
                    if (!TetrisField[row, col])
                    {
                        rowIsFull = false;
                        break;
                    }
                }

                if (rowIsFull)
                {
                    for (int rowToMove = row; rowToMove >= 1; rowToMove--)
                    {
                        for (int col = 0; col < TetrisField.GetLength(1); col++)
                        {
                            TetrisField[rowToMove, col] = TetrisField[rowToMove - 1, col];
                        }
                    }

                    lines++;
                }
            }

            return lines;
        }

        public bool Collision(Tetromino figure)
        {
            if (CurrentFigureCol > TetrisCols - figure.Height)
            {
                return true;
            }

            if (CurrentFigureRow + figure.Width == TetrisRows)
            {
                return true;
            }

            for (int row = 0; row < figure.Width; row++)
            {
                for (int col = 0; col < figure.Height; col++)
                {
                    if (figure.Body[row, col] && TetrisField[CurrentFigureRow + row + 1, CurrentFigureCol + col])
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public bool CanMoveToLeft()
        {
            return (this.CurrentFigureCol >= 1 && !CheckForCollision(-1));
        }

        public bool CanMoveToRight()
        {
            return (this.CurrentFigureCol < this.TetrisCols - this.CurrentFigure.Height)
                && !CheckForCollision(1);
        }
        private bool CheckForCollision(int direction) //direction = -1 left, = 1 right
        {
            for (int row = 0; row < CurrentFigure.Width; row++)
            {
                for (int col = 0; col < CurrentFigure.Height; col++)
                {
                    if (CurrentFigure.Body[row, col] &&
                        this.TetrisField[this.CurrentFigureRow + row, this.CurrentFigureCol + col + direction])
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
