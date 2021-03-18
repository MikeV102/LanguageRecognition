using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Drawing;
using System.Text;


namespace Nai3
{
    public class MainClass
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Training Perceptrons..");
            Console.WriteLine();
            List<Perceptron> perceptrons = new List<Perceptron>();
            List<string> languages = FolderTraverse.GetLanugages(@"Training");


            List<string> trainingPathsList = FolderTraverse.FilesPaths(@"Test");
            List<string> testingPathsList = FolderTraverse.FilesPaths(@"Training");




            foreach (var language in languages)
            {
                perceptrons.Add(new Perceptron(26, language));
            }


            foreach (var perceptron in perceptrons)
            {
                int count = 0;
                double precision = 0;
                double correctlyClassified = 0;
                int countForPrecision = 0;
                double previousPrecision = 0;

                while (countForPrecision < 50 && precision < 1)
                {
                    if (previousPrecision == precision)
                    {
                        countForPrecision++;
                    }
                    else
                    {
                        countForPrecision = 0;
                    }

                    previousPrecision = precision;

                    correctlyClassified = 0;
                    count = 0;

                    foreach (var path in trainingPathsList)
                    {
                        //TODO: to get path language
                        string pathLanguage = new DirectoryInfo(System.IO.Path.GetDirectoryName(path)).Name;

                        //TODO: get dictionary with letters and their occurrences 
                        SortedDictionary<char, double> dictionary = DataReader.CountLettersOccurence(new FileInfo(path));


                        TrainPerceptron(perceptron, dictionary, perceptron.perceptronLanguage, pathLanguage);
                    }

                    foreach (var path in testingPathsList)
                    {
                        string pathLanguage = new DirectoryInfo(System.IO.Path.GetDirectoryName(path)).Name;
                        SortedDictionary<char, double> dictionary = DataReader.CountLettersOccurence(new FileInfo(path));

                        int answer = 0;

                        if (perceptron.perceptronLanguage == pathLanguage)
                            answer = 1;
                        else
                        {
                            answer = -1;
                        }

                        int guess = perceptron.Guess(dictionary);

                        if (guess == answer)
                        {

                            correctlyClassified++;
                            count++;
                        }
                        else
                        {

                            count++;
                        }
                    }

                    precision = (correctlyClassified / count);
                    // Console.WriteLine(precision + " correctyly classified " + correctlyClassified + " out of " + count);
                    // Console.WriteLine("previous precison" + previousPrecision);
                    // Console.WriteLine(countForPrecision + " cfp");

                }

                Console.WriteLine();
                Console.WriteLine("for perceptorn " + perceptron.perceptronLanguage);
                Console.WriteLine("number of correctly classified samples: " + correctlyClassified + " out of 30");
                Console.WriteLine($"percentage correctness: { precision * 100}%");
            }


            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("type \"finish\" to finish or \"example\" to give another example");
                string userInput = Console.ReadLine();
                if ("finish".Equals(userInput))
                {
                    break;
                }
                else if ("example".Equals(userInput))
                {
                    Console.WriteLine("input your example and type \"endOfInput\" as the last line");
                    Console.WriteLine();

                    string line;
                    StringBuilder builder = new StringBuilder();

                    while ((line = Console.ReadLine()) != null)
                    {
                        if (line == "endOfInput")
                            break;
                        builder.Append(line);
                    }

                    string parsedString = DataReader.ParseFileLetters(builder.ToString());

                    SortedDictionary<char, double> dictionary = DataReader.CountLettersOccurence(parsedString);
                    int guess = 0;

                    foreach (var perceptron in perceptrons)
                    {
                        guess = perceptron.Guess(dictionary);
                        if (guess == 1)
                        {
                            Console.WriteLine();
                            Console.WriteLine("---------------------The input text is in: " + perceptron.perceptronLanguage + " language---------------------");
                            break;
                        }
                    }

                    if (guess == 0 || guess == -1)
                    {
                        Console.WriteLine("unknown language");
                    }



                }
                else
                {
                    Console.WriteLine("wrong command");
                }

            }
        }




        public static void TrainPerceptron(Perceptron perceptron, SortedDictionary<char, double> trainingSet, string trainingLanguage, string pathLanguage)
        {
            int answer = trainingLanguage == pathLanguage ? 1 : -1;

            perceptron.Train(trainingSet, answer);
        }




    }
}