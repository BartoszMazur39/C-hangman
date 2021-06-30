using System;
using System.Collections.Generic;
using System.Text;

namespace Hangman
{
    class ReadFile
    {
        
        public string pickRandomCapital() 
        {
            string[] lines;
            try
            {
                lines = System.IO.File.ReadAllLines(@"countries_and_capitals.txt");
            } catch(Exception)
            {
                return "Failed reading from file";
            }

            Random random = new Random();
            int index = random.Next(lines.Length);
            string countryAndCapital = lines[index];
            int wallIndex = countryAndCapital.IndexOf('|');
            string capital = countryAndCapital.Substring(wallIndex + 2);
            return capital;
        }
        
}
}
