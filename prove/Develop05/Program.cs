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

class ChecklistGoal : Goal {
    public bool IsComplete { get; internal set; }
    public int CompletionCount { get; internal set; }
    public int TargetCount { get; private set; }
    public int BonusPoints { get; private set; }

    public ChecklistGoal(string name, int points, int targetCount, int bonusPoints)
        : base(name, points) {
        TargetCount = targetCount;
        BonusPoints = bonusPoints;
        CompletionCount = 0;
        IsComplete = false;
    }

    public override void RecordCompletion() {
        CompletionCount++;
        if (CompletionCount >= TargetCount) {
            IsComplete = true;
        }
    }

    public override string GetStatus() {
        return IsComplete
            ? $"[X] {Name} ({Points} points each, {BonusPoints} bonus points on completion, Completed {CompletionCount}/{TargetCount} times)"
            : $"[ ] {Name} ({Points} points each, {BonusPoints} bonus points on completion, Completed {CompletionCount}/{TargetCount} times)";
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
    public void ShowChecklistGoals() {
        var checklistGoals = goals.OfType<ChecklistGoal>().ToList();
        if (checklistGoals.Any()) {
            foreach (var goal in checklistGoals) {
                Console.WriteLine(goal.GetStatus());
            }
        } else {
            Console.WriteLine("\nYou don't have any checklist goals yet!");
        }
    }

    private void SaveGoals() {
        using (var sw = new StreamWriter(FilePath, false)) {
            sw.WriteLine($"Score:{totalScore}");

            foreach (var goal in goals) {
                var goalType = goal.GetType().Name;
                var completionStatus = goal is SimpleGoal sGoal ? sGoal.IsComplete.ToString() : "false";
                
                string line = goalType switch
                {
                    "SimpleGoal" => $"{goalType},{goal.Name},{goal.Points},{completionStatus}",
                    "EternalGoal" => $"{goalType},{goal.Name},{goal.Points},{completionStatus}",
                    "ChecklistGoal" => goal is ChecklistGoal cGoal
                        ? $"{goalType},{goal.Name},{goal.Points},{completionStatus},{cGoal.CompletionCount},{cGoal.TargetCount},{cGoal.BonusPoints}"
                        : throw new InvalidOperationException("ChecklistGoal expected"),
                    _ => throw new InvalidOperationException("Unknown goal type")
                };

                sw.WriteLine(line);
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
                if (parts.Length >= 4) {
                    var type = parts[0];
                    var name = parts[1];
                    var points = int.TryParse(parts[2], out int pt) ? pt : 0;
                    Goal goal = null;
                    bool isComplete = false;
                    switch (type) {
                        case "SimpleGoal":
                            isComplete = bool.TryParse(parts[3], out bool simpleComplete) && simpleComplete;
                            goal = new SimpleGoal(name, points, isComplete);
                            break;
                        case "EternalGoal":
                            goal = new EternalGoal(name, points);
                            break;
                        case "ChecklistGoal":
                            if (parts.Length >= 7) {
                                isComplete = bool.TryParse(parts[3], out bool checklistComplete) && checklistComplete;
                                var completionCount = int.TryParse(parts[4], out int compCount) ? compCount : 0;
                                var targetCount = int.TryParse(parts[5], out int targCount) ? targCount : 0;
                                var bonusPoints = int.TryParse(parts[6], out int bonPoints) ? bonPoints : 0;
                                goal = new ChecklistGoal(name, points, targetCount, bonusPoints) {
                                    IsComplete = isComplete,
                                    CompletionCount = completionCount
                                };
                            }
                            break;
                    }

                    if (goal != null) {
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
            if (goal is ChecklistGoal checklistGoal && checklistGoal.CompletionCount == checklistGoal.TargetCount) {
                totalScore += checklistGoal.BonusPoints;
            }
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

        Console.WriteLine("\nChecklist Goals:");
        ShowChecklistGoals();
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
