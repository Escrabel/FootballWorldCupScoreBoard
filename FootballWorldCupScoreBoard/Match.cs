using System;

namespace FootballWorldCupScoreBoard
{
    public class Match
    {
        public Match(string homeTeam, string awayTeam)
        {
            if (string.IsNullOrWhiteSpace(homeTeam) || string.IsNullOrWhiteSpace(awayTeam))
                throw new ArgumentException("The team names of a game cannot be null, empty or white spaces.");

            this.homeTeam = homeTeam.Trim();
            this.awayTeam = awayTeam.Trim();
            this.homeScore = 0;
            this.awayScore = 0;
            this.addedDateTime = DateTime.Now;
        }

        public string homeTeam { get; }
        public string awayTeam { get; }
        public int homeScore { get; set; }
        public int awayScore { get; set; }
        public DateTime addedDateTime { get; set; }

        public void updateScores(int homeScore, int awayScore)
        {
            if (homeScore < 0 || awayScore < 0)
                throw new ArgumentException("The scores of a game must be positive integers.");

            this.homeScore = homeScore;
            this.awayScore = awayScore;
            this.addedDateTime = DateTime.Now;
        }

        public override string ToString() => $"{homeTeam} {homeScore} - {awayTeam} {awayScore}";
    }
}
