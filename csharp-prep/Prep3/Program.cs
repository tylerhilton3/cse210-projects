using System;

class Program
{
    static void Main(string[] args)
    {
        Random randomGenerator = new Random();
        int magicNumber = randomGenerator.Next(1, 101);
        int guess = -1;

        while (guess != magicNumber) {
            Console.Write("Guess a number between 1 and 100: ");
            guess = int.Parse(Console.ReadLine());

            if (guess < magicNumber) {
                Console.WriteLine("Higher...");
            } else if (guess > magicNumber) {
                Console.WriteLine("Lower...");
            } else {
                Console.WriteLine("Correct!");
            }
        }                    
    }
}