using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Collections;

namespace Hangman
{
    class Program
    {

        static void Main(string[] args)
        {
            // Reading from file and picking random country and capital
            ReadFile readFile = new ReadFile();
            string[] capitalAndCountry = readFile.pickRandomCapital();
            string randomCapital = capitalAndCountry[0];
            string randomCountry = capitalAndCountry[1];
            string hiddenAnswer = randomCapital;

            // Creating hidden answer
            Regex rgx = new Regex("[a-zA-Z]");
            hiddenAnswer = rgx.Replace(hiddenAnswer, "_");

            UI(randomCapital, hiddenAnswer, randomCountry);
        }
        public static void UI(string randomCapital, string hiddenAnswer, string randomCountry)
        {
            // Checking if the file has been properly read
            
            if (randomCapital == "Failed reading from file")
            {
                return;
            }

            Stopwatch stopwatch = new Stopwatch();
            string decisionInput;
            char guessedLetter = '1';
            string guessedWord;
            string capitalWithGuessedLetters = randomCapital;
            string notInWord = "Not in word letters:";
            int guessCounts = 0;
            int savedTime = 0;
            string hangman = "";

            Player player = new Player();
            Console.WriteLine($"{hiddenAnswer}   lifes: {player.lifes}");
            stopwatch.Start();

            while (true)
            {
                // Letter or entire word

                if(player.lifes < 4)
                {
                    Console.WriteLine($"Hint: The capital of {randomCountry}");
                }

                Console.WriteLine("Would you like to guess a letter (type 1) or whole word (type 2) ?");
                decisionInput = Console.ReadLine();

                if (decisionInput == "1")
                {
                    // Guessing letter

                    Console.WriteLine("Type the letter u think is hiding in this word");

                    string temp = Console.ReadLine();
                    if (temp.Length == 1)
                    {
                        guessedLetter = Convert.ToChar(temp);

                    }

                    char lowerGuessedLetter = Char.ToLower(guessedLetter);
                    char upperGuessedLetter = Char.ToUpper(guessedLetter);


                    guessLetter(ref hiddenAnswer, guessedLetter, ref player, ref capitalWithGuessedLetters, lowerGuessedLetter, upperGuessedLetter, ref notInWord, ref guessCounts);


                } else if (decisionInput == "2")
                {
                    // Guessing word

                    Console.WriteLine("Type the word u think is a good answer");

                    guessedWord = Console.ReadLine();

                    guessWord(guessedWord, randomCapital, ref hiddenAnswer, ref player, ref savedTime);

                } else
                {
                    Console.WriteLine("Unsupported Input. Try again \n");
                }

                //ASCII

                if (player.lifes == 4)
                {
                    hangman = "  _______ \n |/      | \n | \n | \n | \n | \n | \n_|__ ";
                }else if (player.lifes == 3)
                {
                    hangman = "  _______ \n |/      | \n |      (_) \n | \n | \n | \n | \n_|__ ";
                }else if (player.lifes == 2)
                {
                    hangman = "  _______ \n |/      | \n |      (_) \n |      \\|/ \n | \n | \n | \n_|__ ";
                }else if (player.lifes == 1)
                {
                    hangman = "  _______ \n |/      | \n |      (_) \n |      \\|/ \n |       | \n | \n | \n_|__ ";
                }else if (player.lifes == 0)
                {
                    hangman = "  _______ \n |/      | \n |      (_) \n |      \\|/ \n |       | \n |      / \\ \n | \n_|__ ";
                }


                if (hiddenAnswer.IndexOf("_") == -1)
                {
                    //Win

                    stopwatch.Stop();
                    Console.WriteLine($"{hiddenAnswer}   lifes: {player.lifes}    {notInWord}\n {hangman}");
                    Console.WriteLine("Congratulatin you won this game :)");
                    Console.WriteLine($"You guessed the capital after {guessCounts} letters. It took you {Math.Round(stopwatch.Elapsed.TotalSeconds, 0) - savedTime} seconds");

                    //Adding high score

                    Console.WriteLine("Whats your name?");
                    string name = Console.ReadLine();
                    DateTime date = DateTime.Now;

                    using (StreamWriter writeHighScore = File.AppendText("high_score.txt"))
                    {
                        writeHighScore.WriteLine($"{name} | {date} | {Math.Round(stopwatch.Elapsed.TotalSeconds, 0) - savedTime} | {guessCounts} | {hiddenAnswer}");
                        
                    }

                    //high scores
                    string temp;
                    string[] result = File.ReadAllLines("high_score.txt");
                    for(int i = 0; i < result.Length; i++)
                    {
                        for(int j = 0; j < result.Length; j++)
                        {
                            if(Convert.ToInt32(result[j].Split('|')[2]) > Convert.ToInt32(result[i].Split('|')[2]))
                            {
                                temp = result[i];
                                result[i] = result[j];
                                result[j] = temp;
                            }
                        }
                    }

                    int topScores;
                    if(result.Length < 10)
                    {
                        topScores = result.Length;
                    } else
                    {
                        topScores = 10;
                    }
                    Console.WriteLine("Top ten players: ");
                    for(int i = 0; i < topScores; i++)
                    {
                        Console.WriteLine(result[i]);
                    }


                    // Asking about restart after wining

                    while (true)
                    {
                        Console.WriteLine("Do you want to restart the game? 'y' if yes 'n' if no");
                        string restart = Console.ReadLine();
                        if (restart == "y")
                        {
                            stopwatch.Reset();
                            Main(null);
                            break;
                        }else if (restart == "n")
                        {
                            break;
                        }
                    }
                    break;


                }

                if (player.lifes < 1)
                {
                    //Lose

                    player.lifes = 0;
                    Console.WriteLine($"{hiddenAnswer}   lifes: {player.lifes}    {notInWord}\n {hangman}");

                    Console.WriteLine("No more lifes, you lose :(");
                    Console.WriteLine($"Correct answer: {randomCapital}");

                    //Asking about restart after losing

                    while (true)
                    {
                        Console.WriteLine("Do you want to restart the game? 'y' if yes 'n' if no");
                        string restart = Console.ReadLine();
                        if (restart == "y")
                        {
                            Main(null);
                            break;
                        } else if (restart == "n")
                        {
                            break;
                        }
                    }
                    break;

                }

                


                Console.WriteLine($"{hiddenAnswer}   lifes: {player.lifes}    {notInWord}\n {hangman}");

            }

            static void guessLetter(ref string hiddenAnswer, char guessedLetter, ref Player player, ref string capitalWithGuessedLetters, char lowerGuessedLetter, char upperGuessedLetter, ref string notInWord, ref int guessCounts)
            {
                if (capitalWithGuessedLetters.IndexOf(lowerGuessedLetter) == -1 && capitalWithGuessedLetters.IndexOf(upperGuessedLetter) == -1)
                {
                    //Letter bad answer

                    Console.WriteLine("Bad answer");

                    guessCounts++;
                    player.lifes -= 1;
                    
                    notInWord = notInWord + " " + guessedLetter;
                    
                } else
                {
                    //Letter good answer

                    Console.WriteLine("Good answer");

                    guessCounts++;

                    int index;
                    while (true)
                    {
                        //Replacing '_' into letters in hidden answer

                        if (capitalWithGuessedLetters.IndexOf(lowerGuessedLetter) > -1)
                        {
                            index = capitalWithGuessedLetters.IndexOf(lowerGuessedLetter);
                            hiddenAnswer = hiddenAnswer.Remove(index, 1).Insert(index, "" + lowerGuessedLetter);
                            capitalWithGuessedLetters = capitalWithGuessedLetters.Remove(index, 1).Insert(index, "" + "_");
                        } else if (capitalWithGuessedLetters.IndexOf(upperGuessedLetter) > -1)
                        {
                            index = capitalWithGuessedLetters.IndexOf(upperGuessedLetter);
                            hiddenAnswer = hiddenAnswer.Remove(index, 1).Insert(index, "" + upperGuessedLetter);
                            capitalWithGuessedLetters = capitalWithGuessedLetters.Remove(index, 1).Insert(index, "" + "_");
                        } else
                        {
                            break;
                        }
                    }
                }

            }

            static void guessWord(string guessedWord, string randomCapital, ref string hiddenAnswer, ref Player player, ref int savedTime)
            {
                // making sure if player type word witch CapsLock or forgot upper cases the program will accept this

                string lowerGuessedWord = guessedWord.ToLower();
                string lowerWord = randomCapital.ToLower();

                if(lowerGuessedWord == lowerWord)
                {
                    // Whole word good answer

                    Console.WriteLine("Good answer");

                    hiddenAnswer = randomCapital;
                    savedTime ++;
                } else
                {
                    // Whole word bad answer

                    Console.WriteLine("Bad answer");

                    player.lifes -= 2;
                }
            }

        }
    }
}
