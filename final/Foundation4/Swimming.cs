public class Swimming : Activity {
    private int laps;
    private const double lapLengthInMeters = 50;
    private const double metersToMiles = 0.000621371;

    public Swimming(DateTime date, int durationInMinutes, int laps)
        : base(date, durationInMinutes) {
        this.laps = laps;
    }

    public override double GetDistance() => laps * lapLengthInMeters * metersToMiles;

    public override double GetSpeed() => GetDistance() / GetDurationInMinutes() * 60;

    public override double GetPace() => GetDurationInMinutes() / GetDistance();
}