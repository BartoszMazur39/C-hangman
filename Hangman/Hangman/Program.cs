using System;
using System.Text.RegularExpressions;

namespace Hangman
{
class Program
{
static void Main(string[] args)
{
    ReadFile readFile = new ReadFile();
    string randomCapital = readFile.pickRandomCapital();
    string hiddenAnswer = randomCapital;

    Regex rgx = new Regex("[a-zA-Z]");
    hiddenAnswer = rgx.Replace(hiddenAnswer, "_ ");

    StartUI(randomCapital, hiddenAnswer);
            
}
public static void StartUI(string randomCapital, string hiddenAnswer)
{
    Console.WriteLine(randomCapital);
    if (randomCapital == "Failed reading from file")
    {
        return;
    }
    Console.WriteLine(hiddenAnswer);
    
    
    bool rightInput = false;
    while(rightInput == false)
    {
        Console.WriteLine("Would you like to guess a letter (type 1) or whole word (type 2) ?");
        string decisionInput = Console.ReadLine();
        if(decisionInput == "1" || decisionInput == "2")
        {
            rightInput = true;
        }
        else
        {
            Console.WriteLine("Unsupported Input. Try again \n");
        }
    }

}
}
}
