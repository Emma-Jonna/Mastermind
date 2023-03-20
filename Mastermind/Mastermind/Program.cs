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

            Game game = new Game()
            {
                Rounds = 8,
                CurrentRound = 0,
                ColorChart = colorChart,
                ColorchartConsoleColors = colorChartColorCodes
            };

            while (true)
            {
                game.CurrentRound = 0;
                game.Win = false;
                game.ColorChart = colorChart;
                game.ColorchartConsoleColors = colorChartColorCodes;
                game.AllUserGuesses = new string[game.Rounds];
                game.CorrectColorsInWrongPlace = 0;
                game.CorrectColorsInRightPlace = 0;
                game.CorrectPlace = new int[game.Rounds];
                game.WrongPlace = new int[game.Rounds];

                game.gameRound();

                if (game.Win)
                {
                    if (!game.PlayAgainMenu("gameWin", 10))
                    {
                        break;
                    }
                }
                else
                {
                    if (!game.PlayAgainMenu("gameOver", 12))
                    {
                        break;
                    }
                }
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
            public int[] WrongPlace { get; set; }
            public int CorrectColorsInRightPlace { get; set; }
            public int[] CorrectPlace { get; set; }
            public int[] Clues { get; set; }

            public void gameRound()
            {

                StartMenu();

                Console.CursorVisible = true;

                while (true)
                {
                    if (CurrentRound > 7)
                    {
                        break;
                    }

                    Console.ResetColor();
                    Console.WriteLine("Round: " + (CurrentRound + 1));


                    Console.WriteLine("Please enter four colors like the example, CRMY");
                    UserGuess = Console.ReadLine();

                    UserGuess = UserGuess.ToUpper();

                    if (UserGuess == "")
                    {
                        Console.WriteLine("You have to enter a guess");
                        continue;
                    }
                    if (UserGuess.Length > 4 || UserGuess.Length < 4)
                    {
                        Console.WriteLine("You have to enter four colors");
                        continue;
                    }
                    if (CheckIfColorExistInColorChart(UserGuess, ColorChart) < 4)
                    {
                        Console.WriteLine("Please enter colors from the chart");
                        continue;
                    }

                    Console.Clear();
                    PrintColorChart(ColorChart, ColorchartConsoleColors);
                    Console.WriteLine();

                    Console.ResetColor();
                    Console.SetCursorPosition(14, 6);
                    Console.WriteLine("Correct Colors | Correct Colors but in wrong place");

                    CompareUserColorsWithCorrectAnswer();

                    PrintGameBoard();

                    if (CorrectColorsInRightPlace == 4)
                    {
                        Win = true;
                        break;
                    }

                    CurrentRound++;
                }

                Console.Clear();
            }
            public string[] Titles(string title)
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

            public void PrintTitle(string[] title, int color)
            {
                if (color == 15)
                {
                    for (int i = 0; i < title.Length; i++)
                    {
                        Console.ForegroundColor = (ConsoleColor)(RandomConsoleColorNumber(1, 16));
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

            public int RandomConsoleColorNumber(int min, int max)
            {
                Random randomIndex = new Random();

                int randomColorIndex = randomIndex.Next(min, max);

                return randomColorIndex;
            }

            public char[] FourCorrectColors(string[] colorChart, int[] colorChartColorCodes)
            {
                char[] answerColors = new char[4];

                for (int i = 0; i < 4; i++)
                {
                    int rndColor = RandomConsoleColorNumber(0, 5);
                    Console.ForegroundColor = (ConsoleColor)(colorChartColorCodes[rndColor]);
                    answerColors[i] = colorChart[rndColor][0];

                }
                return answerColors;
            }

            public void PrintColorChart(string[] colorChart, int[] colorChartColorCodes)
            {
                for (int i = 0; i < colorChart.Length; i++)
                {
                    Console.ForegroundColor = (ConsoleColor)(colorChartColorCodes[i]);
                    Console.WriteLine(colorChart[i]);
                }
            }

            public int CheckIfColorExistInColorChart(string userGuess, string[] colorChart)
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

            public int GetCorrectConsoleColor(char letter, int[] colorCodes, string[] colorChart)
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

            public bool PlayAgainMenu(string title, int colorCode)
            {
                string[] menuOptions = { "Play Again", "Quit" };
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

            public void StartMenu()
            {
                while (true)
                {
                    Console.Clear();
                    PrintTitle(Titles("start"), 15);
                    Console.WriteLine("[Press Enter To Start]");
                    Console.CursorVisible = false;

                    var pressedKey = Console.ReadKey();

                    if (pressedKey.Key == ConsoleKey.Enter)
                    {
                        Console.Clear();
                        PrintColorChart(ColorChart, ColorchartConsoleColors);
                        Console.WriteLine();
                        CorrectColors = FourCorrectColors(ColorChart, ColorchartConsoleColors);
                        Console.WriteLine();
                        break;
                    }
                }
            }

            public void PrintGameBoard()
            {                               
                for (int i = 0; i < AllUserGuesses.Length; i++)
                {
                    if (AllUserGuesses[i] is null)
                    {
                        Console.WriteLine("[ ][ ][ ][ ]");
                    }
                    else
                    {
                        for (int j = 0; j < 4; j++)
                        {
                            int colorCode = GetCorrectConsoleColor(AllUserGuesses[i][j], ColorchartConsoleColors, ColorChart);
                            Console.ForegroundColor = (ConsoleColor)(colorCode);
                            Console.Write($"[{AllUserGuesses[i][j]}]");
                            Console.ResetColor();
                        }
                        
                        Console.WriteLine("   " + CorrectPlace[i] + " | " + WrongPlace[i]);
                    }
                }
                Console.WriteLine();
            }

            public void CompareUserColorsWithCorrectAnswer()
            {                
                // The guess from the user from this round is stored 
                AllUserGuesses[CurrentRound] = UserGuess;

                CorrectColorsInWrongPlace = 0;
                CorrectColorsInRightPlace = 0;

                //char[] userTemp = new char[4];
                //char[] answerTemp = CorrectColors;

                var userTemp = new List<char>();
                var answerTemp = new List<char>();

                for (int i = 0; i < AllUserGuesses[CurrentRound].Length; i++)
                {
                    userTemp.Add(AllUserGuesses[CurrentRound][i]);
                }
                for (int i = 0; i < AllUserGuesses[CurrentRound].Length; i++)
                {
                    answerTemp.Add(CorrectColors[i]);
                }

                Console.Write("Correct answer:  ");
                foreach (var item in answerTemp)
                {
                    Console.Write(item);
                }

                Console.Write($" User guess: ");
                foreach (var item in userTemp)
                {
                    Console.Write(item);
                }
                Console.WriteLine();


                for (int i = 0; i < answerTemp.Count; i++)
                {
                    if (userTemp[i] == answerTemp[i])
                    {
                        CorrectColorsInRightPlace++;
                        //answerTemp.RemoveAt(answerTemp[i]);
                    }
                    /*else if (answerTemp.Contains(userTemp[i]))
                    {
                        CorrectColorsInWrongPlace++;
                        answerTemp[i] = 'e';
                    }*/
                }

                /*foreach (var item in answerTemp)
                {
                    Console.Write(item);
                }*/

                WrongPlace[CurrentRound] = CorrectColorsInWrongPlace;
                CorrectPlace[CurrentRound] = CorrectColorsInRightPlace;
            }
        }
    }
}