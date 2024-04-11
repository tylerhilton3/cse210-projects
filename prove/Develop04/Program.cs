using System;
using System.Collections.Generic;
using System.Threading;


class Program {
    static void Main(string[] args) {
        Console.WriteLine("Welcome to the Mindfulness Activity Program.");
        Console.WriteLine("Please select an activity to begin:");
        Console.WriteLine("1. Breathing");
        Console.WriteLine("2. Reflection");
        Console.WriteLine("3. Listing");
        Console.WriteLine("4. Visualization");
        Console.WriteLine("Enter your choice (1-4):");

        string choice = Console.ReadLine();
        MindfulnessActivity activity;

        switch (choice) {
            case "1":
                activity = new BreathingActivity();
                break;
            case "2":
                activity = new ReflectionActivity();
                break;
            case "3":
                activity = new ListingActivity();
                break;
            case "4":
                activity = new VisualizationActivity();
                break;
            default:
                Console.WriteLine("Invalid choice. Exiting program.");
                return;
        }

        activity.RunActivity();
    }
}
