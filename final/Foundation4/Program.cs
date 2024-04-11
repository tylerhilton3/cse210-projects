using System;
using System.Collections.Generic;

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
