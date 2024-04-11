public abstract class Activity {
    private DateTime date;
    private int durationInMinutes;

    protected Activity(DateTime date, int durationInMinutes) {
        this.date = date;
        this.durationInMinutes = durationInMinutes;
    }

    public abstract double GetDistance();
    public abstract double GetSpeed();
    public abstract double GetPace();

    public virtual string GetSummary() {
        return $"{date.ToString("dd MMM yyyy")} ({durationInMinutes} min) - Distance: {GetDistance()} miles, Speed: {GetSpeed()} mph, Pace: {GetPace()} min per mile";
    }

    protected int GetDurationInMinutes() => durationInMinutes;
}