using System;

class Program {
    static void Main(string[] args) {
        List<int> numbers = new List<int>();
        
        Console.WriteLine("Enter a series of numbers, enter 0 when finished.");
        int inputNum = -1;
        while (inputNum != 0) {
            string input = Console.ReadLine();
            inputNum = int.Parse(input);
            
            if (inputNum != 0) {
                numbers.Add(inputNum);
            }
        }


        int tot = 0;
        foreach (int number in numbers) {
            tot += number;
        }
        Console.WriteLine($"The total is: {tot}");


        float average = ((float)tot) / numbers.Count;
        Console.WriteLine($"The average is: {average}");
        

        int max = numbers[0];
        foreach (int number in numbers) {
            if (number > max) {
                max = number;
            }
        }
        Console.WriteLine($"The max is: {max}");
    }
}