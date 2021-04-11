using System;
using System.Threading;

namespace FootballWorldCupScoreBoard
{
    class Program
    {
        private static ScoreBoard scoreBoard = null;

        static void Main(string[] args)
        {
            startFootballWorldCupScoreBoard();

        }

        private static void startFootballWorldCupScoreBoard()
        {
            scoreBoard = new ScoreBoard();
            InitialState();
        }

        private static void InitialState(string messageError = "")
        {
            Write(messageError + "Use numbers 1 to 5 to continue...");
            int action = 0;
            bool validInput = int.TryParse(Console.ReadLine(), out action) && action > 0 && action <= 5;
            while (!validInput)
            {
                Write("You must use numbers from 1 to 5.");
                validInput = int.TryParse(Console.ReadLine(), out action) && action > 0 && action <= 5;
            }
            try
            {
                switch (action)
                {
                    case 1:
                        startGame();
                        InitialState();
                        break;
                    case 2:
                        finishGame();
                        InitialState();
                        break;
                    case 3:
                        UpdateScore();
                        InitialState();
                        break;
                    //case 4:
                    //GetSummary();
                    //InitialState();
                    //break;
                    case 4:
                        PresetState();
                        InitialState();
                        break;
                    case 5:
                        //Nothing, the Program ENDS
                        break;
                    default:
                        //Unreachable code
                        break;
                }
            }
            catch (Exception e)
            {
                //Write(e.Message);
                InitialState(e.Message + " ");
            }
        }

        private static void PresetState()
        {
            ScoreBoard sc = new ScoreBoard();
            sc.startMatch("Mexico", "Canada"); Thread.Sleep(20);
            sc.startMatch("Spain", "Brazil"); Thread.Sleep(20);
            sc.startMatch("Germany", "France"); Thread.Sleep(20);
            sc.startMatch("Uruguay", "Italy"); Thread.Sleep(20);
            sc.startMatch("Argentina", "Australia"); Thread.Sleep(20);
            sc.updateScore("Mexico", "Canada", 0, 5); Thread.Sleep(20);
            sc.updateScore("Spain", "Brazil", 10, 2); Thread.Sleep(20);
            sc.updateScore("Germany", "France", 2, 2); Thread.Sleep(20);
            sc.updateScore("Uruguay", "Italy", 6, 6); Thread.Sleep(20);
            sc.updateScore("Argentina", "Australia", 3, 1);
            scoreBoard = sc;
        }

        private static void startGame()
        {
            string homeTeam = IntroduceName("HOME");
            string awayTeam = IntroduceName("AWAY");
            scoreBoard.startMatch(homeTeam, awayTeam);
            Write("Game started.");
        }

        private static void finishGame()
        {
            string homeTeam = IntroduceName("HOME");
            string awayTeam = IntroduceName("AWAY");
            scoreBoard.finishMatch(homeTeam, awayTeam);
            Write("Game finished.");
        }

        private static void UpdateScore()
        {
            string homeTeam = IntroduceName("HOME");
            int homeScore = IntroduceScore("HOME");
            string awayTeam = IntroduceName("AWAY");
            int awayScore = IntroduceScore("AWAY");
            scoreBoard.updateScore(homeTeam, awayTeam, homeScore, awayScore);
            Write("Score's game updated.");
        }

        private static int IntroduceScore(string side)
        {
            Write("Introduce " + side + " score:");
            int score = 0;
            bool validInput = int.TryParse(Console.ReadLine(), out score) && score >= 0;
            while (!validInput)
            {
                Write("You must use positive numbers.");
                validInput = int.TryParse(Console.ReadLine(), out score) && score >= 0;
            }
            return score;
        }

        private static string IntroduceName(string side)
        {
            Write("Introduce " + side + " team name:");
            string team = Console.ReadLine().Trim();
            bool validInput = !string.IsNullOrWhiteSpace(team);
            while (!validInput)
            {
                Write("The name cannot be empty or white spaces.");
                team = Console.ReadLine().Trim();
                validInput = !string.IsNullOrWhiteSpace(team);
            }
            return team;
        }

        private static void GetSummary()
        {
            Write(scoreBoard.ToString());
        }

        private static void Write(string message)
        {
            Console.Clear();
            Console.WriteLine("Welcome to Football World Cup Score Board:" + Environment.NewLine +
                "To interact with the score board you have to press the desired action:" + Environment.NewLine +
                "\t1 to START a game." + Environment.NewLine +
                "\t2 to FINISH a game." + Environment.NewLine +
                "\t3 to UPDATE the score of a game." + Environment.NewLine +
                //"\t4 to SEE the actual score board." + Environment.NewLine +
                "\t4 to SET the Score Board to a PRESET state." + Environment.NewLine +
                "\t5 to EXIT from Football World Cup Score Board.");
            Console.WriteLine(Environment.NewLine + "Actual Score Board:" + Environment.NewLine + scoreBoard.ToString());
            Console.WriteLine(message);
        }

    }
}
