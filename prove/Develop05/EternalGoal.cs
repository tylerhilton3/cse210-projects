class EternalGoal : Goal {
    public EternalGoal(string name, int points) : base(name, points) { }

    public override void RecordCompletion() {
    }

    public override string GetStatus() {
        return Name + " (" + Points + " points)";
    }
}