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