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