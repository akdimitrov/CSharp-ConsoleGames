using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace Tetris
{
    public class ScoreManager
    {
        private readonly string scoresFilePath;
        private readonly int[] ScorePerLines = { 1, 40, 100, 300, 1200 };

        public ScoreManager(string scoresFilePath)
        {
            this.scoresFilePath = scoresFilePath;
            HighScore = GetHighScore();
        }
        public int Score { get; private set; }

        public int HighScore { get; private set; }

        public void AddToScore(int level, int lines)
        {
            Score += ScorePerLines[lines] * level;
            if (Score > HighScore)
            {
                HighScore = Score;
            }
        }

        public void AddToHighScore()
        {
            File.AppendAllLines(scoresFilePath, new List<string>
            {
                $"[{DateTime.UtcNow}] {Environment.UserName} => {Score}"
            });
        }

        private int GetHighScore()
        {
            var highscore = 0;

            if (File.Exists(scoresFilePath))
            {
                var allScores = File.ReadAllLines(scoresFilePath);
                foreach (var score in allScores)
                {
                    var match = Regex.Match(score, @" => (?<score>[0-9]+)");
                    highscore = Math.Max(highscore, int.Parse(match.Groups["score"].Value));
                }
            }

            return highscore;
        }
    }
}
