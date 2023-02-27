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

            string[] userGuesses = new string[8];

            PrintTitle(Titles("gameStartTitle"), 15);
            Console.WriteLine("[Press Enter To Start]");

            string pressedKey = Console.ReadKey().Key.ToString();

            if (pressedKey == "Enter")
            {
                Console.WriteLine("You pressed enter!");
            }
            else
            {
                Console.WriteLine("You did not press enter!");

            }

            Console.Clear();

            printColorChart();

            char[] answerColors = FourCorrectColors();

            Console.WriteLine();

            int round = 0;

            while (true)
            {
                if (round > 7)
                {
                    Console.Clear();
                    PrintTitle(Titles("gameOverTitle"), 12);
                    break;
                }

                Console.WriteLine("Round: " + (round + 1));

                Console.WriteLine("Please enter four colors like the example, CRMY");
                string userCollorGuess = Console.ReadLine();

                Console.Clear();

                printColorChart();

                userCollorGuess = userCollorGuess.ToUpper();

                if (userCollorGuess == "")
                {
                    Console.WriteLine("You have to enter a guess");
                    continue;
                }
                if (userCollorGuess.Length > 4 || userCollorGuess.Length < 4)
                {
                    Console.WriteLine("You have to enter four colors");
                    continue;
                }

                int matches = CheckIfColorExistInColorChart(userCollorGuess);

                if (matches < 4)
                {
                    Console.WriteLine("Please enter colors from the chart");
                    continue;
                }

                userGuesses[round] = userCollorGuess;

                for (int i = 0; i < userGuesses.Length; i++)
                {
                    Console.WriteLine(userGuesses[i]);

                }

                int correctColorRightPlace = 0;
                int correctColorWrongPlace = 0;

                for (int i = 0; i < userGuesses[round].Length; i++)
                {
                    int colorCode = GetCorrectConsoleColor(userGuesses[round][i], colorChartColorCodes, colorChart);
                    Console.ForegroundColor = (ConsoleColor)(colorCode);
                    Console.Write("User guess: " + userGuesses[round][i]);

                    colorCode = GetCorrectConsoleColor(answerColors[i], colorChartColorCodes, colorChart);
                    Console.ForegroundColor = (ConsoleColor)(colorCode);
                    Console.Write("Correct answer: " + answerColors[i]);
                    Console.WriteLine();

                    if (userGuesses[round][i] == answerColors[i])
                    {
                        correctColorRightPlace++;
                    }
                    else if(userGuesses[round].Contains(answerColors[i]) && userGuesses[round][i] != answerColors[i])
                    {
                        correctColorWrongPlace++;
                    }
                }

                Console.ResetColor();
                Console.WriteLine("Correct Colors: " + correctColorRightPlace);
                Console.WriteLine("Correct Colors but in wrong place: " + correctColorWrongPlace);
                Console.WriteLine();

                if (correctColorRightPlace == 4)
                {
                    Console.Clear();
                    PrintTitle(Titles("gameWinTitle"), 10);
                    break;
                }

                round++;

                /*Console.WriteLine($"1.  [{userCollorGuess[0]}][{userCollorGuess[1]}][{userCollorGuess[2]}][{userCollorGuess[3]}]");
                Console.WriteLine("2.  [ ][ ][ ][ ]");
                Console.WriteLine("3.  [ ][ ][ ][ ]");
                Console.WriteLine("4.  [ ][ ][ ][ ]");
                Console.WriteLine("5.  [ ][ ][ ][ ]");
                Console.WriteLine("6.  [ ][ ][ ][ ]");
                Console.WriteLine("7.  [ ][ ][ ][ ]");
                Console.WriteLine("8.  [ ][ ][ ][ ]");
                Console.WriteLine("9.  [ ][ ][ ][ ]");
                Console.WriteLine("10. [ ][ ][ ][ ]");
                Console.WriteLine("11. [ ][ ][ ][ ]");
                Console.WriteLine("12. [ ][ ][ ][ ]");
                Console.WriteLine($"    [{answerColors[0]}][{answerColors[1]}][{answerColors[2]}][{answerColors[3]}]");

                Console.WriteLine(userCollorGuess);*/
            }
            string[] Titles(string titleName)
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

                if (titleName == "gameStartTitle")
                {
                    return gameStartTitle;
                }
                if (titleName == "gameOverTitle")
                {
                    return gameOverTitle;
                }
                if (titleName == "gameWinTitle")
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
        }
    }
}