using System;

namespace Hangman
{
    class Program
    {
        static void Main(string[] args)
        {
            ReadFile readFile = new ReadFile();
            string randomCapital = readFile.pickRandomCapital();
            Console.WriteLine(randomCapital);
            
        }
    }
}
