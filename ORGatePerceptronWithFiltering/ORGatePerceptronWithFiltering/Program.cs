using System;

using PerceptronHillClimberLibrary;

namespace ORGatePerceptron
{
    class Program
    {
        public static double MeanSquaredError(double output, double desiredOutput)
        {
            return Math.Pow((desiredOutput - output), 2);
        }

        public static (double[][] inputs, double[] outputs) OrInOut()
        {
            double[][] inputs = new double[][] { new double[] { 0, 0 }, new double[] { 1, 0 }, new double[] { 0, 1 }, new double[] { 1, 1 } };
            double[] outputs = new double[] { 0, 1, 1, 1};
            return (inputs, outputs);
        }

        static void Main(string[] args)
        {
            Random random = new Random();

            Perceptron gate = new Perceptron(2, 0.001, random, MeanSquaredError);

            gate.Randomize(random, -1, 1);

            (double[][] inputs, double[] outputs) = OrInOut();

            //Time to make the magic happen:

            double currentError = gate.GetError(inputs, outputs);
            while (currentError > 0)
            {
                Console.SetCursorPosition(0, 0);
                for (int i = 0; i < inputs.Length; i++)
                {
                    Console.Write("Inputs: ");
                    for (int j = 0; j < inputs[i].Length; j++)
                    {
                        if (j != 0)
                        {
                            Console.Write(", ");
                        }
                        Console.Write(inputs[i][j]);
                    }

                    Console.Write(" Output: " + Math.Round(gate.Compute(inputs[i]), 0) + "                ");
                    Console.WriteLine();
                }
                Console.WriteLine("Error: " + Math.Round(currentError, 3) + "                ");
                currentError = gate.TrainWithHillClimbing(inputs, outputs, currentError);
            }
            
        }
    }
}
