using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hangman
{
    class Program
    {
        static int lifePoints = 0;
        static string wrongLetters = "";
        static string goodLetters = " ";
        static string capitalCity = "";
        
        static void Exit()
        {
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("                 OK");
            System.Threading.Thread.Sleep(2000);
            Console.WriteLine("                 ...");
            System.Threading.Thread.Sleep(2000);
            Console.WriteLine("                 bye");
            System.Threading.Thread.Sleep(1000);
            Environment.Exit(0);
        }

        static void IfGameEnds()
        {
            if (Program.lifePoints == 0)
            {
                Console.WriteLine("\nGame Over");
                Console.WriteLine("the answer: " + Program.capitalCity);
            }
            else
            {
                DrawRebus();
            }
        }
        static void ShowLife()
        {
            int lifeCount = 0;
            int lifePoints = Program.lifePoints;
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            string lifeIcon = "\x2665";
            Console.Write("                 You have " + lifePoints + " life points: ");
            while (lifeCount < lifePoints)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(lifeIcon + " ");
                lifeCount++;
            }
            Console.ResetColor();
            Console.WriteLine();
            if(Program.wrongLetters != null)
            {
                LettersNotInWord();
            }
        }
        static void CheckPhrase()
        {
            string correct = Program.capitalCity;
            Console.WriteLine("                 Type phrase:");
            string answer = Console.ReadLine();
            if (correct == answer.ToLower())
            {
                Console.WriteLine("                 " + answer + "is correct! Well done!");
            }
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("                 " + answer.ToUpper() + " is not correct. U lose 1 life point.\n");
                Console.ResetColor();
                Program.lifePoints--;
                ShowLife();
                IfGameEnds();
            }
        }
        static void LettersNotInWord()
        {
            Console.Write("\n                 Letters not-in-phrase: ");
            int count = 0;
            while (count < Program.wrongLetters.Length)
            {
                Console.Write(Program.wrongLetters.ToUpper()[count] + ", ");
                count++;
            }
            Console.WriteLine("");
        }
        static void CheckLetter()
        {
            string correct = Program.capitalCity;
            Console.WriteLine("                 Type letter:");
            string typing = Console.ReadLine();
            char answer = typing[0];
            if (correct.ToLower().Contains(answer))
            {
                Console.WriteLine("                 Good! " + answer.ToString().ToUpper() + " exist in this phrase.");
                Program.goodLetters = Program.goodLetters + answer.ToString();
                DrawRebus();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("                 " + answer.ToString().ToUpper() + " doesn't exist in this word. U lose 1 life point.\n");
                Console.ResetColor();
                Program.lifePoints--;
                Program.wrongLetters = Program.wrongLetters + answer.ToString();
                ShowLife();
                IfGameEnds();
            }
        }
        static void LetterOrPhrase()
        {
            string letter = "l";
            string phrase = "p";
            Console.WriteLine("\n\n                 Letter (L) or phrase (P)?");
            string x = Console.ReadLine();
            if (phrase == x.ToLower())
            {
                CheckPhrase();
            }
            else if (letter == x.ToLower())
            {
                CheckLetter();
            }
            else
            {
                LetterOrPhrase();
            }
        }
        static void SetCapital()
        {
            string line = RandomLineFromFile();
            string[] splitLine = line.Split('|');
            string sourceCapital = splitLine[1];
            string capital = sourceCapital.Substring(1);
            Program.capitalCity = capital;
        }
        static void DrawRebus()
        {
            string phrase = Program.capitalCity;
            int count = 0;
            Console.WriteLine("\n                 Phrase to guess:\n");
            Console.Write("                 ");
            string phraseL = phrase.ToLower();
            string goodLetters = Program.goodLetters.ToLower();
            while (count < phrase.Length)
            {
                char space = ' ';
                if (phrase[count] == space)
                {
                    Console.Write("  ");
                    count++;
                }
                else if (goodLetters.Contains(phraseL[count]))
                {
                    Console.Write(phraseL[count].ToString().ToUpper() + " ");
                    count++;
                }
                else
                {
                    Console.Write("_ ");
                    count++;
                }
            }
            Console.WriteLine();
            LetterOrPhrase();
        }
        static void Game()
        {
            Program.lifePoints = 5;
            SetCapital();
            ShowLife();
            DrawRebus();
        }
        static void Manual()
        {
            Console.WriteLine("                 Guess the name of one of the capitals. ");
            Console.WriteLine("                 Type 'L' to guess one letter or 'P' to guess phrase\n");
        }
        static int RandomNumberGenerator()
        {
            int randomCounter = 0;
            string dataSourcePath = DataSourcePath();
            using (var reader = File.OpenText(dataSourcePath))
            {
                while (reader.ReadLine() != null)
                {
                    randomCounter++;
                }
            }
            Random rand = new Random();
            int draw = rand.Next(randomCounter++);
            return draw;
        }
        static string DataSourcePath()
        {
            string dataSourcePath = @"countries_and_capitals.txt";
            return dataSourcePath;
        }
        static string RandomLineFromFile()
        {
            int lineCounter = 0;
            string line;
            string dataSourcePath = DataSourcePath();
            System.IO.StreamReader file = new System.IO.StreamReader(dataSourcePath);
            int draw = RandomNumberGenerator();
            while ((line = file.ReadLine()) != null)
            {
                if (lineCounter == draw)
                {
                    break;
                }

                lineCounter++;
            }
            return line;
        }
        static void Main(string[] args)
        {   
            Console.WriteLine();
            Console.WriteLine("     ██╗  ██╗ █████╗ ███╗   ██╗ ██████╗ ███╗   ███╗ █████╗ ███╗   ██╗");
            Console.WriteLine("     ██║  ██║██╔══██╗████╗  ██║██╔════╝ ████╗ ████║██╔══██╗████╗  ██║");
            Console.WriteLine("     ███████║███████║██╔██╗ ██║██║  ███╗██╔████╔██║███████║██╔██╗ ██║");
            Console.WriteLine("     ██╔══██║██╔══██║██║╚██╗██║██║   ██║██║╚██╔╝██║██╔══██║██║╚██╗██║");
            Console.WriteLine("     ██║  ██║██║  ██║██║ ╚████║╚██████╔╝██║ ╚═╝ ██║██║  ██║██║ ╚████║");
            Console.WriteLine("     ╚═╝  ╚═╝╚═╝  ╚═╝╚═╝  ╚═══╝ ╚═════╝ ╚═╝     ╚═╝╚═╝  ╚═╝╚═╝  ╚═══╝");
            Console.WriteLine();
            Console.WriteLine();
            string start;
            string startLower = "x";
            string yes = "y";
            string no = "n";
            Console.WriteLine("                 Start the game?     (Y/N)");
            while (true)
            {
                start = Console.ReadLine();
                startLower = start.ToLower();
                if (startLower == yes)
                {
                    Manual();
                    Game();
                    //LetterOrPhrase();


                }
                else if (startLower == no)
                {
                    Exit();
                }
                else
                {
                    Console.WriteLine("                 Type 'Y' to start or 'N' to exit ");
                    Console.ReadKey();
                }
            }
        }
    }
}
