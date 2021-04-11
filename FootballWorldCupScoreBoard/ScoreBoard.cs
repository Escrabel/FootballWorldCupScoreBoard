using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;

namespace FootballWorldCupScoreBoard
{
    public class ScoreBoard
    {
        public ScoreBoard()
        {
            this.matches = new List<Match>();
        }

        public List<Match> matches { get; set; }

        public void startMatch(string homeTeam, string awayTeam)
        {
            checkTeams(homeTeam, awayTeam);
            Match matchFounded = getMatch(homeTeam, awayTeam);
            if (matchFounded == null)
            {
                Match newMatch = new Match(homeTeam, awayTeam);
                this.matches.Add(newMatch);
                orderMatches();
            }
            else
            {
                throw new ArgumentException("That game is already in the Score Board.");
            }
        }

        public void finishMatch(string homeTeam, string awayTeam)
        {
            checkTeams(homeTeam, awayTeam);
            Match matchFounded = getMatch(homeTeam, awayTeam);
            if (matchFounded != null)
            {
                this.matches.Remove(matchFounded);
            }
            else
            {
                throw new NullReferenceException("That game was not founded in the Score Board.");
            }
        }

        public void updateScore(string homeTeam, string awayTeam, int homeScore, int awayScore)
        {
            checkTeams(homeTeam, awayTeam);
            checkScores(homeScore, awayScore);
            Match matchFounded = getMatch(homeTeam, awayTeam);
            if (matchFounded != null)
            {
                matchFounded.updateScores(homeScore, awayScore);
                orderMatches();
            }
            else
            {
                throw new NullReferenceException("That game was not founded in the Score Board.");
            }
        }

        public override string ToString()
        {
            string scoreBoard = "";
            int i = 0;
            foreach (var match in this.matches)
            {
                i++;
                scoreBoard += i + ". " + match.ToString() + Environment.NewLine;
            }
            return scoreBoard;
        }
        private void orderMatches()
        {
            this.matches = this.matches.OrderByDescending(match => match.homeScore + match.awayScore).
                ThenByDescending(match => match.addedDateTime).ToList();
        }

        private Match getMatch(string homeTeam, string awayTeam)
        {
            checkTeams(homeTeam, awayTeam);
            return matches.FirstOrDefault(match => match.homeTeam.ToLower().Equals(homeTeam.ToLower()) &&
                match.awayTeam.ToLower().Equals(awayTeam.ToLower()));
        }

        private void checkTeams(string homeTeam, string awayTeam)
        {
            if (string.IsNullOrWhiteSpace(homeTeam) || string.IsNullOrWhiteSpace(awayTeam))
                throw new ArgumentException("The team names of a game cannot be null, empty or white spaces.");
        }

        private void checkScores(int homeScore, int awayScore)
        {
            if (homeScore < 0 || awayScore < 0)
                throw new ArgumentException("The scores of a game must be positive integers.");
        }
    }
}
