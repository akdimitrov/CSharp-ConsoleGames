namespace Tetris
{
    public interface ITetrisGame
    {
        Tetromino CurrentFigure { get; set; }
        int CurrentFigureCol { get; set; }
        int CurrentFigureRow { get; set; }
        int Level { get; }
        int TetrisCols { get; }
        bool[,] TetrisField { get; }
        int TetrisRows { get; }

        void AddCurrentFigureToTetrisField();
        int CheckForFullLines();
        bool Collision(Tetromino figure);
        void NewRandomFigure();
        void UpdateLevel(int score);
        bool CanMoveToLeft();
        bool CanMoveToRight();
    }
}