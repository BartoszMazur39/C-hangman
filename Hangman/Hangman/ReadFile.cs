using System;
using System.Collections.Generic;
using System.Text;

namespace Hangman
{
    class ReadFile
    {
        
        public string[] pickRandomCapital() 
        {
            string[] lines;
            string[] output = new string[2];
            try
            {
                lines = System.IO.File.ReadAllLines(@"countries_and_capitals.txt");
            } catch(Exception)
            {
                output[0] = "Failed reading from file";
                return output;
            }

            Random random = new Random();
            int index = random.Next(lines.Length);
            string countryAndCapital = lines[index];
            int wallIndex = countryAndCapital.IndexOf('|');
            string capital = countryAndCapital.Substring(wallIndex + 2);
            string country = countryAndCapital.Substring(0, wallIndex - 2);
            output[0] = capital;
            output[1] = country;
            return output;
        }
        
}
}
