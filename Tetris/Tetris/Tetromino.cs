namespace Tetris
{
    public class Tetromino
    {
        public Tetromino(bool[,] body)
        {
            Body = body;
        }

        public bool[,] Body { get; private set; }

        public int Width => Body.GetLength(0);

        public int Height => Body.GetLength(1);

        public Tetromino GetRotate()
        {
            var newFigure = new bool[Height, Width];
            for (int row = 0; row < Width; row++)
            {
                for (int col = 0; col < Height; col++)
                {
                    newFigure[col, Width - row - 1] = Body[row, col];
                }
            }

            return new Tetromino(newFigure);
        }
    }
}
