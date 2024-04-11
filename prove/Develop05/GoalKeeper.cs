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