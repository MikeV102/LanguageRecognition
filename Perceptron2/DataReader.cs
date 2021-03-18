using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Nai3
{
    public class DataReader
    {
      
        
        public static SortedDictionary<char,double> CountLettersOccurence(FileInfo fileInfo)
        {
          var groupedLetters =   ParseFileLetters(fileInfo).GroupBy(c => c).Select(c => new { Char = c.Key, Count = c.Count()}); // TODO: counting letters occurrences 

          SortedDictionary<char, double> dictionary = new SortedDictionary<char, double>();
          double allLettersCount = 0;
          
          foreach (var letter in groupedLetters)
          {
              allLettersCount += letter.Count;
          }

          foreach (var letter in groupedLetters)
          {
             double temp = letter.Count / allLettersCount; // TODO: passing counted letters to the dictionary
             dictionary[letter.Char] = temp;

          }

          return dictionary;
        }
        
        public static SortedDictionary<char,double> CountLettersOccurence(string text)
        {
            var groupedLetters =   ParseFileLetters(text).GroupBy(c => c).Select(c => new { Char = c.Key, Count = c.Count()}); // TODO: counting letters occurrences 

            SortedDictionary<char, double> dictionary = new SortedDictionary<char, double>();
         
            double allLettersCount = 0;
          
            foreach (var letter in groupedLetters)
            {
                allLettersCount += letter.Count;
            }
            
            foreach (var letter in groupedLetters)
            {
                dictionary[letter.Char] = letter.Count / allLettersCount; // TODO: passing counted letters to the dictionary
             
            }

            return dictionary;
        }

        public static string ParseFileLetters(FileInfo fileInfo)
        {
            using (var stream = new StreamReader(fileInfo.OpenRead())) //TODO: reading file
            {
                StringBuilder builder = new StringBuilder();//TODO: builder to be returned , file with only A-Z letters

                string line = null;    
                while ((line = stream.ReadLine()) != null)
                {
                    
                    builder.Append(Regex.Replace(line.ToUpper(), @"[^A-Z]+", String.Empty));//TODO: replacing non A-Z characters with empty string
                }

                return builder.ToString();
            }
        }
        
        public static string ParseFileLetters(string text)
        {
           
                StringBuilder builder = new StringBuilder();//TODO: builder to be returned , file with only A-Z letters

                builder.Append(Regex.Replace(text.ToUpper(), @"[^A-Z]+", String.Empty));//TODO: replacing non A-Z characters with empty string
                

                return builder.ToString();
            
        }
    }
    
}