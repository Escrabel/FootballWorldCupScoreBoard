using System;
using FootballWorldCupScoreBoard;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FootballWorldCupScoreBoardTest
{
    [TestClass]
    public class MatchTest
    {
        //When the score is updated, the internal datetime property "addedDateTime" is updated also.
        [TestMethod]
        public void IsAddedDatedUpdated_ReturnsTrue()
        {
            Match example = new Match("A", "B");
            DateTime creationTime = example.addedDateTime;
            Thread.Sleep(20);
            example.updateScores(0, 0);
            DateTime modificationTime = example.addedDateTime;
            Assert.IsTrue(modificationTime > creationTime);
        }

        //Negative scores are not allowed
        [DataTestMethod]
        [DataRow(-1, 4)]
        [DataRow(0, -15)]
        [DataRow(-1451, -540)]
        [ExpectedException(typeof(ArgumentException))]
        public void NegativeScoreMatch_ReturnsException(int homeScore, int awayScore)
        {
            Match example = new Match("A", "B");
            example.updateScores(homeScore, awayScore);
        }

        //Empty string are not allowed
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
            new Match(homeTeam, awayTeam);
        }

        [TestMethod]
        public void IsToStringWorkingFine_ReturnsTrue()
        {
            string expectedString = "A 14 - B 9";

            Match example = new Match("A", "B");
            Thread.Sleep(20);
            example.updateScores(14, 9);
            Assert.IsTrue(example.ToString().Equals(expectedString));
        }
    }
}
