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