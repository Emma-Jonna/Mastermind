using System.ComponentModel;
using System.Runtime.InteropServices;

namespace Mastermind
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] colorChart =
            {
                "B = Blue",
                "G = Green",
                "C = Cyan",
                "R = Red",
                "M = Magenta",
                "Y = Yellow",
            };

            int[] colorChartColorCodes = { 9, 10, 11, 12, 13, 14 };

            Game game = new Game(8, 0, false, colorChart, colorChartColorCodes, 0, 0);
         /*{
                Rounds = 8,
                CurrentRound = 0,
                Win = false,
                ColorChart = colorChart,
                ColorchartConsoleColors = colorChartColorCodes,
                CorrectColorsInWrongPlace = 0,
                CorrectColorsInRightPlace = 0,
            };*/

            game.AllUserGuesses = new string[game.Rounds];


            // press enter to start game
            while (true)
            {
                Console.Clear();
                PrintTitle(Titles("start"), 15);
                Console.WriteLine("[Press Enter To Start]");

                var pressedKey = Console.ReadKey();

                if (pressedKey.Key == ConsoleKey.Enter)
                {
                    Console.Clear();
                    printColorChart();
                    Console.WriteLine();
                    game.CorrectColors = FourCorrectColors();
                    Console.WriteLine();
                    break;
                }
            }

            while (true)
            {
                if (game.CurrentRound > 7)
                {
                    break;
                }

                Console.WriteLine("Round: " + (game.CurrentRound + 1));

                Console.WriteLine("Please enter four colors like the example, CRMY");
                game.UserGuess = Console.ReadLine();

                game.UserGuess = game.UserGuess.ToUpper();

                if (game.UserGuess == "")
                {
                    Console.WriteLine("You have to enter a guess");
                    continue;
                }
                if (game.UserGuess.Length > 4 || game.UserGuess.Length < 4)
                {
                    Console.WriteLine("You have to enter four colors");
                    continue;
                }
                if (CheckIfColorExistInColorChart(game.UserGuess) < 4)
                {
                    Console.WriteLine("Please enter colors from the chart");
                    continue;
                }

                Console.Clear();
                printColorChart();
                Console.WriteLine();

                // The guess from the user from this round is stored
                game.AllUserGuesses[game.CurrentRound] = game.UserGuess;

                for (int i = 0; i < game.AllUserGuesses.Length; i++)
                {
                    if (game.AllUserGuesses[i] is null)
                    {
                        Console.WriteLine("[ ][ ][ ][ ]");
                    }
                    else
                    {
                        for (int j = 0; j < 4; j++)
                        {
                            int colorCode = GetCorrectConsoleColor(game.AllUserGuesses[i][j], colorChartColorCodes, colorChart);
                            Console.ForegroundColor = (ConsoleColor)(colorCode);
                            Console.Write($"[{game.AllUserGuesses[i][j]}]");
                            Console.ResetColor();
                        }
                        Console.WriteLine();
                    }
                }

                for (int i = 0; i < game.AllUserGuesses[game.CurrentRound].Length; i++)
                {
                    /*int colorCode = GetCorrectConsoleColor(game.AllUserGuesses[game.CurrentRound][i], colorChartColorCodes, colorChart);
                    Console.ForegroundColor = (ConsoleColor)(colorCode);
                    Console.Write("User guess: " + game.AllUserGuesses[game.CurrentRound][i]);

                    colorCode = GetCorrectConsoleColor(game.CorrectColors[i], colorChartColorCodes, colorChart);
                    Console.ForegroundColor = (ConsoleColor)(colorCode);
                    Console.Write("Correct answer: " + game.CorrectColors[i]);
                    Console.WriteLine();*/

                    if (game.AllUserGuesses[game.CurrentRound][i] == game.CorrectColors[i])
                    {
                        game.CorrectColorsInRightPlace++;
                    }
                    else if (game.AllUserGuesses[game.CurrentRound].Contains(game.CorrectColors[i]) && game.AllUserGuesses[game.CurrentRound][i] != game.CorrectColors[i])
                    {
                        game.CorrectColorsInWrongPlace++;
                    }
                }

                Console.ResetColor();
                Console.WriteLine();
                Console.WriteLine("Correct Colors: " + game.CorrectColorsInRightPlace);
                Console.WriteLine("Correct Colors but in wrong place: " + game.CorrectColorsInWrongPlace);
                Console.WriteLine();

                if (game.CorrectColorsInRightPlace == 4)
                {
                    game.Win = true;
                    break;
                }
                game.CurrentRound++;
            }

            Console.Clear();

            if (!game.Win)
            {
                PlayAgainMenu("gameOver", 12);
            }
            else
            {
                PlayAgainMenu("gameWin", 10);
            }




            string[] Titles(string title)
            {
                string[] gameStartTitle = {
                "XX     XX     XXX      XXXXXX   XXXXXXXX  XXXXXXXX  XXXXXXXX   XX     XX  XXXX  XX    XX  XXXXXXXX",
                "XXX   XXX    XX XX    XX    XX     XX     XX        XX     XX  XXX   XXX   XX   XXX   XX  XX     XX",
                "XXXX XXXX   XX   XX   XX           XX     XX        XX     XX  XXXX XXXX   XX   XXXX  XX  XX     XX",
                "XX XXX XX  XX     XX   XXXXXX      XX     XXXXX     XXXXXXXX   XX XXX XX   XX   XX XX XX  XX     XX",
                "XX     XX  XXXXXXXXX        XX     XX     XX        XX   XX    XX     XX   XX   XX  XXXX  XX     XX",
                "XX     XX  XX     XX  XX    XX     XX     XX        XX    XX   XX     XX   XX   XX   XXX  XX     XX",
                "XX     XX  XX     XX   XXXXXX      XX     XXXXXXXX  XX     XX  XX     XX  XXXX  XX    XX  XXXXXXXX"
            };
                string[] gameWinTitle =
            {
                "XX    XX   XXXXXXX   XX     XX      XX      XX  XXXX  XX    XX",
                " XX  XX   XX     XX  XX     XX      XX  XX  XX   XX   XXX   XX",
                "  XXXX    XX     XX  XX     XX      XX  XX  XX   XX   XXXX  XX",
                "   XX     XX     XX  XX     XX      XX  XX  XX   XX   XX XX XX",
                "   XX     XX     XX  XX     XX      XX  XX  XX   XX   XX  XXXX",
                "   XX     XX     XX  XX     XX      XX  XX  XX   XX   XX   XXX",
                "   XX      XXXXXXX    XXXXXXX        XXX  XXX   XXXX  XX    XX",
            };
                string[] gameOverTitle =
            {
                " XXXXXX       XXX     XX     XX  XXXXXXXX       XXXXXXX   XX     XX  XXXXXXXX  XXXXXXXX",
                "XX    XX     XX XX    XXX   XXX  XX            XX     XX  XX     XX  XX        XX     XX",
                "XX          XX   XX   XXXX XXXX  XX            XX     XX  XX     XX  XX        XX     XX",
                "XX   XXXX  XX     XX  XX XXX XX  XXXXX         XX     XX  XX     XX  XXXXX     XXXXXXXX",
                "XX    XX   XXXXXXXXX  XX     XX  XX            XX     XX   XX   XX   XX        XX   XX",
                "XX    XX   XX     XX  XX     XX  XX            XX     XX    XX XX    XX        XX    XX",
                " XXXXXX    XX     XX  XX     XX  XXXXXXXX       XXXXXXX      XXX     XXXXXXXX  XX     XX",
            };

                if (title == "start")
                {
                    return gameStartTitle;
                }
                if (title == "gameOver")
                {
                    return gameOverTitle;
                }
                if (title == "gameWin")
                {
                    return gameWinTitle;
                }
                return gameStartTitle;
            }

            void PrintTitle(string[] title, int color)
            {
                if (color == 15)
                {
                    for (int i = 0; i < title.Length; i++)
                    {
                        Console.ForegroundColor = (ConsoleColor)(RandomColorNumber(1, 16));
                        Console.WriteLine(title[i]);
                    }
                }
                if (color < 15)
                {
                    for (int i = 0; i < title.Length; i++)
                    {
                        Console.ForegroundColor = (ConsoleColor)(color);
                        Console.WriteLine(title[i]);
                    }
                }
            }

            char[] FourCorrectColors()
            {
                char[] answerColors = new char[4];

                for (int i = 0; i < 4; i++)
                {
                    int rndColor = RandomColorNumber(0, 5);
                    Console.ForegroundColor = (ConsoleColor)(colorChartColorCodes[rndColor]);
                    Console.Write(colorChart[rndColor][0]);
                    answerColors[i] = colorChart[rndColor][0];

                }
                return answerColors;
            }

            int RandomColorNumber(int min, int max)
            {
                Random randomNumber = new Random();

                int randomColorNumber = randomNumber.Next(min, max);

                return randomColorNumber;
            }

            void printColorChart()
            {
                for (int i = 0; i < colorChart.Length; i++)
                {
                    Console.ForegroundColor = (ConsoleColor)(colorChartColorCodes[i]);
                    Console.WriteLine(colorChart[i]);
                }
            }

            int CheckIfColorExistInColorChart(string userGuess)
            {
                int matches = 0;
                for (int i = 0; i < colorChart.Length; i++)
                {
                    for (int j = 0; j < userGuess.Length; j++)
                    {
                        if (userGuess[j] == colorChart[i][0])
                        {
                            matches++;
                        }
                    }
                }
                return matches;
            }

            int GetCorrectConsoleColor(char letter, int[] colorCodes, string[] colorChart)
            {
                for (int i = 0; i < colorChart.Length; i++)
                {
                    if (letter == colorChart[i][0])
                    {
                        return colorCodes[i];
                    }
                }

                return 15;
            }

            bool PlayAgainMenu(string title, int colorCode)
            {
                string[] menuOptions = { "Play Again", "Return To Start" };

                int menuOptionIndex = 0;



                while (true)
                {
                    PrintTitle(Titles(title), colorCode);

                    Console.CursorVisible = false;

                    for (int i = 0; i < menuOptions.Length; i++)
                    {
                        if (menuOptionIndex == 0)
                        {
                            Console.WriteLine($"    --> {menuOptions[i]}");
                            menuOptionIndex++;
                        }
                        else if (menuOptionIndex == 1)
                        {
                            Console.WriteLine(menuOptions[i]);
                            menuOptionIndex--;
                        }
                    }

                    var keyPressed = Console.ReadKey();
                    Console.Clear();


                    if (keyPressed.Key == ConsoleKey.DownArrow || keyPressed.Key == ConsoleKey.RightArrow)
                    {
                        if (menuOptionIndex == 0)
                        {
                            menuOptionIndex++;
                        }
                        else
                        {
                            menuOptionIndex--;
                        }
                    }
                    if (keyPressed.Key == ConsoleKey.UpArrow || keyPressed.Key == ConsoleKey.LeftArrow)
                    {
                        if (menuOptionIndex == 1)
                        {
                            menuOptionIndex--;
                        }
                        else
                        {
                            menuOptionIndex++;
                        }
                    }
                    if (keyPressed.Key == ConsoleKey.Enter)
                    {
                        break;
                    }
                }

                if (menuOptionIndex == 0)
                {
                    return true;
                }

                return false;
            }
            void PrintGameBoard()
            {

            }
        }

        class Game
        {
            public int Rounds { get; set; }
            public int CurrentRound { get; set; }
            public bool Win { get; set; }
            public bool PlayAgain { get; set; }
            public string[] ColorChart { get; set; }
            public int[] ColorchartConsoleColors { get; set; }
            public char[] CorrectColors { get; set; }
            public string UserGuess { get; set; }
            public string[] AllUserGuesses { get; set; }
            public int CorrectColorsInWrongPlace { get; set; }
            public int CorrectColorsInRightPlace { get; set; }

            public Game(int r, int currentR, bool gameStatus, string[] colors, int[] colorIndex, int wrongPlace, int correct)
            {
                Rounds = r;
                CurrentRound = currentR;
                Win = gameStatus;
                ColorChart = colors;
                ColorchartConsoleColors = colorIndex;
                CorrectColorsInWrongPlace = wrongPlace;
                CorrectColorsInRightPlace = correct;
            }

            public void gameRound()
            {

            }
        }
    }
}