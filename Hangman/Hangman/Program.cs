using System;
using System.Text.RegularExpressions;

namespace Hangman
{
    class Program
    {

        static void Main(string[] args)
        {
            ReadFile readFile = new ReadFile();
            string[] capitalAndCountry = readFile.pickRandomCapital();
            string randomCapital = capitalAndCountry[0];
            string randomCountry = capitalAndCountry[1];
            string hiddenAnswer = randomCapital;

            Regex rgx = new Regex("[a-zA-Z]");
            hiddenAnswer = rgx.Replace(hiddenAnswer, "_");

            UI(randomCapital, hiddenAnswer);
        }
        public static void UI(string randomCapital, string hiddenAnswer)
        {
            Console.WriteLine(randomCapital);
            if (randomCapital == "Failed reading from file")
            {
                return;
            }
            Player player = new Player();
            Console.WriteLine($"{hiddenAnswer} lifes: {player.lifes}");


            string decisionInput;
            char guessedLetter = '1';
            string guessedWord;
            string capitalWithGuessedLetters = randomCapital;
            while (true)
            {
                Console.WriteLine("Would you like to guess a letter (type 1) or whole word (type 2) ?");
                decisionInput = Console.ReadLine();
                if (decisionInput == "1")
                {
                    Console.WriteLine("Type letter u think is hiding in this word");

                    string temp = Console.ReadLine();
                    if (temp.Length == 1)
                    {
                        guessedLetter = Convert.ToChar(temp);

                    }

                    char lowerGuessedLetter = Char.ToLower(guessedLetter);
                    char upperGuessedLetter = Char.ToUpper(guessedLetter);


                    guessLetter(ref hiddenAnswer, guessedLetter, ref player, ref capitalWithGuessedLetters, lowerGuessedLetter, upperGuessedLetter);


                } else if (decisionInput == "2")
                {

                } else
                {
                    Console.WriteLine("Unsupported Input. Try again \n");
                }
                if (hiddenAnswer.IndexOf("_") == -1)
                {
                    Console.WriteLine("Congratulatin you won this game!");
                    break;
                }
                if (player.lifes < 1)
                {
                    Console.WriteLine("No more lifes, you lose");
                    break;
                }

            }

            static void guessLetter(ref string hiddenAnswer, char guessedLetter, ref Player player, ref string capitalWithGuessedLetters, char lowerGuessedLetter, char upperGuessedLetter)
            {
                if (capitalWithGuessedLetters.IndexOf(lowerGuessedLetter) == -1 && capitalWithGuessedLetters.IndexOf(upperGuessedLetter) == -1)
                {
                    Console.WriteLine("Bad answer");
                    player.lifes -= 1;
                    
                } else
                {
                    Console.WriteLine("Good answer");

                    int index;
                    while (true)
                    {
                        if (capitalWithGuessedLetters.IndexOf(lowerGuessedLetter) > -1)
                        {
                            index = capitalWithGuessedLetters.IndexOf(lowerGuessedLetter);
                            hiddenAnswer = hiddenAnswer.Remove(index, 1).Insert(index, "" + lowerGuessedLetter);
                            capitalWithGuessedLetters = capitalWithGuessedLetters.Remove(index, 1).Insert(index, "" + "_");
                            Console.WriteLine(capitalWithGuessedLetters);
                        } else if (capitalWithGuessedLetters.IndexOf(upperGuessedLetter) > -1)
                        {
                            index = capitalWithGuessedLetters.IndexOf(upperGuessedLetter);
                            hiddenAnswer = hiddenAnswer.Remove(index, 1).Insert(index, "" + upperGuessedLetter);
                            capitalWithGuessedLetters = capitalWithGuessedLetters.Remove(index, 1).Insert(index, "" + "_");
                            Console.WriteLine(capitalWithGuessedLetters);
                        } else
                        {
                            break;
                        }
                    }






                }
                Console.WriteLine($"{hiddenAnswer} lifes: {player.lifes}");


            }

        }
    }
}
