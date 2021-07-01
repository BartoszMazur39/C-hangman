using System;
using System.Text.RegularExpressions;

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

            UI(randomCapital, hiddenAnswer);
        }
        public static void UI(string randomCapital, string hiddenAnswer)
        {
            // Checking if the file has been properly read
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
                // Letter or entire word
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


                    guessLetter(ref hiddenAnswer, guessedLetter, ref player, ref capitalWithGuessedLetters, lowerGuessedLetter, upperGuessedLetter);


                } else if (decisionInput == "2")
                {
                    // Guessing word

                    Console.WriteLine("Type the word u think is a good answer");

                    guessedWord = Console.ReadLine();

                    guessWord(guessedWord, randomCapital, ref hiddenAnswer, ref player);

                } else
                {
                    Console.WriteLine("Unsupported Input. Try again \n");
                }

                Console.WriteLine($"{hiddenAnswer} lifes: {player.lifes}");

                if (hiddenAnswer.IndexOf("_") == -1)
                {
                    //Win

                    Console.WriteLine("Congratulatin you won this game :)");
                    break;
                }

                if (player.lifes < 1)
                {
                    //Lose

                    Console.WriteLine("No more lifes, you lose :(");
                    break;
                }

            }

            static void guessLetter(ref string hiddenAnswer, char guessedLetter, ref Player player, ref string capitalWithGuessedLetters, char lowerGuessedLetter, char upperGuessedLetter)
            {
                if (capitalWithGuessedLetters.IndexOf(lowerGuessedLetter) == -1 && capitalWithGuessedLetters.IndexOf(upperGuessedLetter) == -1)
                {
                    //Letter bad answer

                    Console.WriteLine("Bad answer");
                    player.lifes -= 1;
                    
                } else
                {
                    //Letter good answer

                    Console.WriteLine("Good answer");

                    int index;
                    while (true)
                    {
                        //Replacing '_' into letters in hidden answer

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

            }

            static void guessWord(string guessedWord, string randomCapital, ref string hiddenAnswer, ref Player player)
            {
                // making sure if player type word witch CapsLock or forgot upper cases the program will accept this

                string lowerGuessedWord = guessedWord.ToLower();
                string lowerWord = randomCapital.ToLower();

                if(lowerGuessedWord == lowerWord)
                {
                    // Whole word good answer

                    Console.WriteLine("Good answer");

                    hiddenAnswer = randomCapital;
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
