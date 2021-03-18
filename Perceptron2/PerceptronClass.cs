using System;
using System.Collections.Generic;

namespace Nai3
{
    
    public class Perceptron
    {
         List<double> weights = new List<double>();
        private  int numberOfInputs;
        private  double lr = 5d;
        public string perceptronLanguage;
        
        
        public Perceptron(int numberOfInputs, string perceptronLanguage)
        {
            this.perceptronLanguage = perceptronLanguage;
            this.numberOfInputs = numberOfInputs;
            Random rand = new Random();

            for (int i = 0; i < numberOfInputs; i++)
            {
                weights.Add(rand.Next(-3,3));
            } 
        }

        public  int Guess(SortedDictionary<char, double> inputs)
        {
          

        double sum = 0;
            int countForWeights = 0;
            // for (int i = 0; i < weights.Count; i++)
            // {
            //     sum += inputs[i] * weights[i];
            // }

            foreach (var input in inputs)
            {
                sum += input.Value * weights[countForWeights];
                countForWeights++;
            }
            
            int output = Sign(sum);
            return output;
        }

        public  void Train(SortedDictionary<char,double> inputs, int target)
        {
            
            int guess = Guess(inputs);
            int error = target - guess;
            int countForWeights = 0;

            // for (int i = 0; i < weights.Count; i++)
            // {
            //     weights[i] += error * inputs[i] * lr;
            // }

            foreach (var input in inputs)
            {
                weights[countForWeights] += error * input.Value * lr;
                countForWeights++;
            }
        }
        
        static int  Sign(double n)
        {
            if (n >= 0)
                return 1;
            else
            {
                return -1;
            }
        }
    }
}