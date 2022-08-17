using System;

namespace Tetris
{
    public class TetrisConsoleWriter
    {
        private int tetrisRows;
        private int tetrisCols;
        private int infoCols;
        private int consoleRows;
        private int consoleCols;
        private char tetrisCharacter;

        public TetrisConsoleWriter(int tetrisRows, int tetrisCols, char tetrisCharacter = '*', int infoCols = 10)
        {
            this.tetrisRows = tetrisRows;
            this.tetrisCols = tetrisCols;
            this.tetrisCharacter = tetrisCharacter;
            this.infoCols = infoCols;
            this.consoleRows = 1 + tetrisRows + 1;
            this.consoleCols = 1 + tetrisCols + 1 + infoCols + 1;
            this.Frame = 0;
            FramesToMoveFigure = 16;

            Console.SetWindowSize(consoleCols, consoleRows);
            Console.SetBufferSize(consoleCols, consoleRows);

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Title = "Tetris";
            Console.CursorVisible = false;
        }

        public int Frame { get; set; }

        public int FramesToMoveFigure { get; private set; }

        public void DrawAll(ITetrisGame state, ScoreManager scoreManager)
        {
            DrawBorder();
            DrawGameState(3 + tetrisCols, state, scoreManager);
            DrawTetrisField(state.TetrisField);
            DrawCurrentFigure(state.CurrentFigure, state.CurrentFigureRow, state.CurrentFigureCol);
        }

        public void DrawGameState(int startCol, ITetrisGame state, ScoreManager scoreManager)
        {
            Write($"Level:", 1, startCol);
            Write(state.Level.ToString(), 2, startCol);

            Write($"Sore:", 4, startCol);
            Write(scoreManager.Score.ToString(), 5, startCol);

            Write($"Best:", 7, startCol);
            Write(scoreManager.HighScore.ToString(), 8, startCol);

            Write($"Frame:", 10, startCol);
            Write(Frame.ToString() + " / " + (FramesToMoveFigure - state.Level).ToString(), 11, startCol);

            Write($"Position:", 13, startCol);
            Write($"{state.CurrentFigureRow}, {state.CurrentFigureCol}", 14, startCol);

            Write($"Keys:", 16, startCol);
            Write("  ^  ", 18, startCol);
            Write("<   >", 19, startCol);
            Write("  v  ", 20, startCol);
        }

        public void WriteGameOver(int score)
        {
            int row = tetrisRows / 2 - 2;
            int col = (tetrisCols + 3 + infoCols) / 2 - 5;
            var scoreAsString = score.ToString();
            scoreAsString = new string(' ', 7 - scoreAsString.Length) + scoreAsString;
            Write("╔═════════╗", row, col);
            Write("║ Game    ║", row + 1, col);
            Write("║   over! ║", row + 2, col);
            Write($"║ {scoreAsString} ║", row + 3, col);
            Write("╚═════════╝", row + 4, col);
        }

        public void DrawTetrisField(bool[,] tetrisField)
        {
            for (int row = 0; row < tetrisField.GetLength(0); row++)
            {
                string line = "";
                for (int col = 0; col < tetrisField.GetLength(1); col++)
                {
                    line += tetrisField[row, col] ? tetrisCharacter : " ";
                }

                Write(line, row + 1, 1);
            }
        }

        public void DrawCurrentFigure(Tetromino currentFigure, int currentFigureRow, int currentFigureCol)
        {
            for (int row = 0; row < currentFigure.Width; row++)
            {
                for (int col = 0; col < currentFigure.Height; col++)
                {
                    if (currentFigure.Body[row, col])
                    {
                        Write(tetrisCharacter.ToString(), row + 1 + currentFigureRow, col + 1 + currentFigureCol);
                    }
                }
            }
        }

        public void DrawBorder()
        {
            Console.SetCursorPosition(0, 0);

            string topLine = "╔";
            topLine += new string('═', tetrisCols);
            topLine += "╦";
            topLine += new string('═', infoCols);
            topLine += "╗";
            Console.Write(topLine);

            for (int i = 0; i < tetrisRows; i++)
            {
                string middleLine = "║";
                middleLine += new string(' ', tetrisCols);
                middleLine += "║";
                middleLine += new string(' ', infoCols);
                middleLine += "║";
                Console.Write(middleLine);
            }

            string bottomLine = "╚";
            bottomLine += new string('═', tetrisCols);
            bottomLine += "╩";
            bottomLine += new string('═', infoCols);
            bottomLine += "╝";
            Console.Write(bottomLine);
        }

        private void Write(string text, int row, int col)
        {
            Console.SetCursorPosition(col, row);
            Console.Write(text);
        }
    }
}
