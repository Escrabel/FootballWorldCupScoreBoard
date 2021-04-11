using System;
using FootballWorldCupScoreBoard;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FootballWorldCupScoreBoardTest
{
    [TestClass]
    public class ScoreBoardTests
    {
        [TestMethod]
        public void startMatchIncreasesMatchesCountBy1_ReturnsTrue()
        {
            ScoreBoard example = new ScoreBoard();
            int count1 = example.matches.Count;
            example.startMatch("A", "B");
            int count2 = example.matches.Count;
            example.startMatch("C", "D");
            int count3 = example.matches.Count;

            Assert.IsTrue(count1 + 1 == count2 && count2 + 1 == count3);
        }


        [TestMethod]
        public void finishMatchDecreasesMatchesCountBy1_ReturnsTrue()
        {
            ScoreBoard example = new ScoreBoard();
            example.startMatch("A", "B");
            example.startMatch("C", "D");
            int preCount = example.matches.Count;
            example.finishMatch("C", "D");
            int postCount = example.matches.Count;

            Assert.IsTrue(preCount == postCount + 1);
        }

        [TestMethod]
        public void ScoreBoardIsNotCaseSensitive_ReturnsTrue()
        {
            ScoreBoard example = new ScoreBoard();
            example.startMatch("   A", "B   ");
            example.updateScore("a", "b", 1, 1);

            Assert.IsTrue(example.matches[0].homeTeam.Equals("A"));
        }

        [TestMethod]
        public void finishMatchDeletesTheCorrectMatch_ReturnsTrue()
        {
            ScoreBoard example = new ScoreBoard();
            example.startMatch("A", "B");
            example.startMatch("C", "D");
            example.finishMatch("C", "D");

            Assert.IsTrue(example.matches[0].homeTeam.Equals("A") && example.matches[0].awayTeam.Equals("B"));
        }

        [DataTestMethod]
        [DataRow(2, -1)]
        [DataRow(-50, 0)]
        [DataRow(-1000, -99999)]
        [ExpectedException(typeof(ArgumentException))]
        public void NegativeScores_ReturnsException(int homeScore, int awayScore)
        {
            ScoreBoard example = new ScoreBoard();
            example.startMatch("A ", "B");
            example.updateScore("A", "B", homeScore, awayScore);
        }

        [DataTestMethod]
        [DataRow("A", "")]
        [DataRow("", "B")]
        [DataRow("", "")]
        [DataRow(" ", "")]
        [DataRow(" ", null)]
        [DataRow(null, "B")]
        [DataRow("            ", "B")]
        [ExpectedException(typeof(ArgumentException))]
        public void EmptyTeamNames_ReturnsException(string homeTeam, string awayTeam)
        {
            ScoreBoard example = new ScoreBoard();
            example.startMatch(homeTeam, awayTeam);
        }

        [DataTestMethod]
        [DataRow("A", "X")]
        [DataRow("X", "B")]
        [DataRow("X", "X")]
        [ExpectedException(typeof(NullReferenceException))]
        public void UpdateNotExintingMatch_ReturnsException(string nonExistingHomeTeam, string nonExistingAwayTeam)
        {
            ScoreBoard example = new ScoreBoard();
            example.startMatch("A", "B");
            example.updateScore(nonExistingHomeTeam, nonExistingAwayTeam, 0, 0);
        }

        [DataTestMethod]
        [DataRow("A", "X")]
        [DataRow("X", "B")]
        [DataRow("X", "X")]
        [ExpectedException(typeof(NullReferenceException))]
        public void FinishNotExintingMatch_ReturnsException(string nonExistingHomeTeam, string nonExistingAwayTeam)
        {
            ScoreBoard example = new ScoreBoard();
            example.startMatch("A", "B");
            example.finishMatch(nonExistingHomeTeam, nonExistingAwayTeam);
        }

        //This method implies that the matches are being sorted in the correct way.
        [DataTestMethod]
        public void IsToStringAndSortingWorkingFine_ReturnsTrue()
        {
            string expectedString = "1. G 10 - H 10" + Environment.NewLine +
                                    "2. C 5 - D 2" + Environment.NewLine +
                                    "3. E 3 - F 4" + Environment.NewLine +
                                    "4. A 2 - B 1" + Environment.NewLine;

            ScoreBoard example = new ScoreBoard();
            example.startMatch("A ", "B");
            Thread.Sleep(20);
            example.startMatch("C", "   D");
            Thread.Sleep(20);
            example.startMatch("E", "F");
            Thread.Sleep(20);
            example.startMatch("   G   ", "H");
            Thread.Sleep(20);
            example.updateScore("G", "H", 10, 10);
            Thread.Sleep(20);
            example.updateScore("A", "B", 2, 1);
            Thread.Sleep(20);
            example.updateScore("E", "F", 3, 4);
            Thread.Sleep(20);
            example.updateScore("C", "D", 5, 2);
            string exampleToString = example.ToString();
            Assert.IsTrue(exampleToString.Equals(expectedString));
        }
    }
}
