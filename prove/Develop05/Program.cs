using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

abstract class Goal {
    public string Name { get; private set; }
    public int Points { get; private set; }

    protected Goal(string name, int points) {
        Name = name;
        Points = points;
    }

    public abstract void RecordCompletion();
    public abstract string GetStatus();
}

class SimpleGoal : Goal {
    public bool IsComplete { get; internal set; }

    public SimpleGoal(string name, int points, bool isComplete = false)
        : base(name, points) {
        IsComplete = isComplete;
    }

    public override void RecordCompletion() {
        IsComplete = true;
    }

    public override string GetStatus() {
        return IsComplete ? "[X] " + Name + " (" + Points + " points)" : "[ ] " + Name + " (" + Points + " points)";
    }
}



class EternalGoal : Goal {
    public EternalGoal(string name, int points) : base(name, points) { }

    public override void RecordCompletion() {
    }

    public override string GetStatus() {
        return Name + " (" + Points + " points)";
    }
}

class GoalKeeper {
    private List<Goal> goals = new List<Goal>();
    private int totalScore = 0;
    private const string FilePath = "goals.txt";

    public GoalKeeper() {
        LoadGoals();
    }

    private void SaveGoals() {
        using (var sw = new StreamWriter(FilePath, false)) {
            sw.WriteLine($"Score:{totalScore}");

            foreach (var goal in goals) {
                var goalType = goal is SimpleGoal ? "SimpleGoal" : "EternalGoal";
                var completionStatus = goal is SimpleGoal sGoal ? sGoal.IsComplete.ToString() : "false";
                sw.WriteLine($"{goalType},{goal.Name},{goal.Points},{completionStatus}");
            }
        }
    }






    private void LoadGoals() {
    if (!File.Exists(FilePath)) {
        return;
    }

    using (var sr = new StreamReader(FilePath)) {
        var scoreLine = sr.ReadLine();
        if (scoreLine != null && scoreLine.StartsWith("Score:")) {
            var scorePart = scoreLine.Split(':')[1];
            if (int.TryParse(scorePart, out int loadedScore)) {
                totalScore = loadedScore;
            }
        }

        string line;
        while ((line = sr.ReadLine()) != null) {
            var parts = line.Split(',');
            if (parts.Length == 4) {
                var type = parts[0];
                var name = parts[1];
                var points = int.TryParse(parts[2], out int pt) ? pt : 0;
                bool isComplete = bool.TryParse(parts[3], out bool complete) && complete;
                Goal goal = null;
                if (type == "SimpleGoal") {
                    goal = new SimpleGoal(name, points, isComplete);
                } else if (type == "EternalGoal") {
                    goal = new EternalGoal(name, points);
                }

                if(goal != null) {
                    goals.Add(goal);
                }
            }
        }
    }
}



    public void AddGoal(Goal goal) {
        goals.Add(goal);
        SaveGoals();
    }

    public void DeleteGoal(string goalName) {
        var goal = goals.FirstOrDefault(g => g.Name.Equals(goalName, StringComparison.OrdinalIgnoreCase));
        if (goal != null) {
            goals.Remove(goal);
            SaveGoals();
        }
    }

    public void RecordGoalCompletion(string goalName) {
        var goal = goals.FirstOrDefault(g => g.Name.Equals(goalName, StringComparison.OrdinalIgnoreCase));
        if (goal != null) {
            goal.RecordCompletion();
            totalScore += goal.Points;
            SaveGoals();
        }
    }


    public void ShowGoals(bool showEternal) {
        var filteredGoals = goals.Where(g => showEternal ? g is EternalGoal : g is SimpleGoal).ToList();

        if (!filteredGoals.Any()) {
            Console.WriteLine($"\nYou don't have any {(showEternal ? "eternal" : "simple")} goals yet!");
            return;
        }

        foreach (var goal in filteredGoals) {
            Console.WriteLine(goal.GetStatus());
        }
    }

    public void ShowAllGoals() {
        Console.WriteLine("\nSimple Goals:");
        ShowGoals(false);
        Thread.Sleep(1000);

        Console.WriteLine("\nEternal Goals:");
        ShowGoals(true);
        Thread.Sleep(1000);
    }

    public int GetTotalScore() {
        return totalScore;
    }
}

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
            Console.WriteLine("3. Add goal");
            Console.WriteLine("4. Delete goal");
            Console.WriteLine("5. Record goal completion");
            Console.WriteLine("6. Show score");
            Console.WriteLine("7. Show all goals");
            Console.WriteLine("8. Exit");
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
                    AddGoal(keeper);
                    break;
                case "4":
                    DeleteGoal(keeper);
                    break;
                case "5":
                    RecordGoalCompletion(keeper);
                    break;
                case "6":
                    Console.WriteLine($"\nYour score is: {keeper.GetTotalScore()}");
                    break;
                case "7":
                    keeper.ShowAllGoals();
                    break;
                case "8":
                    running = false;
                    break;
                default:
                    Console.WriteLine("Invalid option, try again.");
                    break;
            }
        }
    }

    static void AddGoal(GoalKeeper keeper) {
        string type;
        do {
            Console.WriteLine("Is it a simple (s) or eternal (e) goal?");
            type = Console.ReadLine().ToLower();
        } while (type != "s" && type != "simple" && type != "e" && type != "eternal");

        Console.WriteLine("Enter the name of the goal:");
        string name = Console.ReadLine();

        int points;
        while (true) {
            Console.WriteLine("Enter the points for the goal:");
            if (int.TryParse(Console.ReadLine(), out points)) {
                break;
            }
            Console.WriteLine("Invalid input. Please enter an integer for the points.");
        }

        if (type == "s" || type == "simple") {
            keeper.AddGoal(new SimpleGoal(name, points));
        } else {
            keeper.AddGoal(new EternalGoal(name, points));
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
