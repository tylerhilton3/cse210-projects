using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

class Program {
    static void Main(string[] args) {
        Console.WriteLine("Hello, welcome to your goal keeper, Eternal Quest!");
        Thread.Sleep(1000);

        var keeper = new GoalKeeper();
        bool running = true;

        while (running) {
            Console.WriteLine("\nWhat would you like to do?");
            Console.WriteLine("1. Show simple goals");
            Console.WriteLine("2. Show eternal goals");
            Console.WriteLine("3. Show checklist goals");
            Console.WriteLine("4. Show all goals");
            Console.WriteLine("5. Add goal");
            Console.WriteLine("6. Delete goal");
            Console.WriteLine("7. Record goal completion");
            Console.WriteLine("8. Show score");
            Console.WriteLine("9. Exit");
            Thread.Sleep(1000);

            string input = Console.ReadLine();
            switch (input) {
                case "1":
                    keeper.ShowGoals(false);
                    break;
                case "2":
                    keeper.ShowGoals(true);
                    break;
                case "3":
                    keeper.ShowChecklistGoals();
                    break;
                case "4":
                    keeper.ShowAllGoals();
                    break;
                case "5":
                    AddGoal(keeper);
                    break;
                case "6":
                    DeleteGoal(keeper);
                    break;
                case "7":
                    RecordGoalCompletion(keeper);
                    break;
                case "8":
                    Console.WriteLine($"\nYour score is: {keeper.GetTotalScore()}");
                    break;
                case "9":
                    running = false;
                    break;
                default:
                    Console.WriteLine("Invalid option, try again.");
                    break;
            }
        }
    }

    static void AddGoal(GoalKeeper keeper) {
        Console.WriteLine("Is it a simple (s), eternal (e), or checklist (c) goal?");
        string type = Console.ReadLine().ToLower();

        Console.WriteLine("Enter the name of the goal:");
        string name = Console.ReadLine();

        Console.WriteLine("Enter the points for the goal:");
        int points;
        while (!int.TryParse(Console.ReadLine(), out points)) {
            Console.WriteLine("Invalid input. Please enter an integer for the points.");
        }

        if (type == "c" || type == "checklist") {
            Console.WriteLine("Enter the target count for the goal:");
            int targetCount;
            while (!int.TryParse(Console.ReadLine(), out targetCount)) {
                Console.WriteLine("Invalid input. Please enter an integer for the target count.");
            }

            Console.WriteLine("Enter the bonus points for completing the goal:");
            int bonusPoints;
            while (!int.TryParse(Console.ReadLine(), out bonusPoints)) {
                Console.WriteLine("Invalid input. Please enter an integer for the bonus points.");
            }

            keeper.AddGoal(new ChecklistGoal(name, points, targetCount, bonusPoints));
        } else if (type == "s" || type == "simple") {
            keeper.AddGoal(new SimpleGoal(name, points));
        } else if (type == "e" || type == "eternal") {
            keeper.AddGoal(new EternalGoal(name, points));
        } else {
            Console.WriteLine("Invalid goal type.");
        }
    }


    static void DeleteGoal(GoalKeeper keeper) {
        Console.WriteLine("\nEnter the name of the goal to delete:");
        string name = Console.ReadLine();
        keeper.DeleteGoal(name);
    }

    static void RecordGoalCompletion(GoalKeeper keeper) {
        Console.WriteLine("\nEnter the name of the goal you completed:");
        string name = Console.ReadLine();
        keeper.RecordGoalCompletion(name);
    }
}
