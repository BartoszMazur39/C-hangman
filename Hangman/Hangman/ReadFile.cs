using System;
using System.Collections.Generic;
using System.Text;

namespace Hangman
{
    class ReadFile
    {
        string[] lines = System.IO.File.ReadAllLines(@"C:\Users\barto\source\repos\Hangman\Hangman\countries_and_capitals.txt");
        public string pickRandomCapital() 
        {
            Random random = new Random();
            int index = random.Next(lines.Length);
            string countryAndCapital = lines[index];
            int wallIndex = countryAndCapital.IndexOf('|');
            string capital = countryAndCapital.Substring(wallIndex + 1);
            return capital;
        }
        
}
}
