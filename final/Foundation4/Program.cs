using System;
using System.Collections.Generic;

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

public class Running : Activity {
    private double distanceInMiles;

    public Running(DateTime date, int durationInMinutes, double distanceInMiles)
        : base(date, durationInMinutes) {
        this.distanceInMiles = distanceInMiles;
    }

    public override double GetDistance() => distanceInMiles;

    public override double GetSpeed() => (distanceInMiles / GetDurationInMinutes()) * 60;

    public override double GetPace() => GetDurationInMinutes() / distanceInMiles;
}

public class Cycling : Activity {
    private double speedInMph;

    public Cycling(DateTime date, int durationInMinutes, double speedInMph)
        : base(date, durationInMinutes) {
        this.speedInMph = speedInMph;
    }

    public override double GetDistance() => (speedInMph / 60) * GetDurationInMinutes();

    public override double GetSpeed() => speedInMph;

    public override double GetPace() => 60 / speedInMph;
}

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

public class Program {
    public static void Main() {
        var activities = new List<Activity> {
            new Running(new DateTime(2022, 11, 3), 30, 3.0),
            new Cycling(new DateTime(2022, 11, 4), 45, 20.0),
            new Swimming(new DateTime(2022, 11, 5), 30, 20)
        };

        foreach (var activity in activities) {
            Console.WriteLine(activity.GetSummary());
        }
    }
}
