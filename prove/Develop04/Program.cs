using System;
using System.Collections.Generic;
using System.Threading;

public abstract class MindfulnessActivity {
    protected string Name;
    protected string Description;
    protected int DurationInSeconds;
    protected Random random = new Random();

    public MindfulnessActivity(string name, string description) {
        Name = name;
        Description = description;
    }

    public void RunActivity() {
        StartActivityMessage();
        PerformActivity();
        EndActivityMessage();
    }

    protected void StartActivityMessage() {
        Console.WriteLine($"Activity: {Name}");
        Console.WriteLine(Description);
        Console.Write("Enter duration of the activity in seconds: ");
        DurationInSeconds = int.Parse(Console.ReadLine());
        Console.WriteLine("Prepare to begin...");
        InitialCountdown(5);
    }

    protected void EndActivityMessage() {
        Console.WriteLine("Well done!");
        PauseWithSpinner(1);
        Console.WriteLine($"You have completed the {Name} for {DurationInSeconds} seconds.");
        PauseWithSpinner(3);
    }

    protected abstract void PerformActivity();

    protected void PauseWithSpinner(int seconds) {
        var spinner = new[] { '|', '/', '-', '\\' };
        int totalIterations = seconds * 10;

        for (int i = 0; i < totalIterations; i++) {
            Console.Write($"\r{spinner[i % spinner.Length]}");
            Thread.Sleep(100);
        }

        Console.WriteLine("\r ");
    }
    protected void InitialCountdown(int seconds) {
        for (int i = seconds; i > 0; i--) {
            Console.Write($"\rStarting in {i} second(s)...");
            Thread.Sleep(1000);
        }
        Console.WriteLine("\nGo!");
    }
}

public class BreathingActivity : MindfulnessActivity {
    public BreathingActivity() : base("Breathing", "This activity will help you relax by walking you through breathing in and out slowly. Clear your mind and focus on your breathing.")
    {}

    protected override void PerformActivity() {
        int timePassed = 0;
        while (timePassed < DurationInSeconds) {
            Console.WriteLine("Breathe in...");
            PauseWithSpinner(5);
            Console.WriteLine("Breathe out...");
            PauseWithSpinner(5);
            timePassed += 10;
        }
    }
}

public class ReflectionActivity : MindfulnessActivity {
    private List<string> reflectionPrompts = new List<string> {
        "Think of a time when you stood up for someone else.",
        "Think of a time when you did something really difficult.",
        "Think of a time when you helped someone in need.",
        "Think of a time when you did something truly selfless."
    };

    private List<string> reflectionQuestions = new List<string> {
        "Why was this experience meaningful to you?",
        "Have you ever done anything like this before?",
        "How did you get started?",
        "How did you feel when it was complete?",
        "What made this time different than other times when you were not as successful?",
        "What is your favorite thing about this experience?",
        "What could you learn from this experience that applies to other situations?",
        "What did you learn about yourself through this experience?",
        "How can you keep this experience in mind in the future?"
    };

    public ReflectionActivity() : base("Reflection", "This activity will help you reflect on times in your life when you have shown strength and resilience. This will help you recognize the power you have and how you can use it in other aspects of your life.")
    {}

    protected override void PerformActivity() {
        Console.WriteLine(reflectionPrompts[random.Next(reflectionPrompts.Count)]);
        
        reflectionQuestions = reflectionQuestions.OrderBy(q => random.Next()).ToList();
        
        int questionIndex = 0;
        int timePassed = 0;
        
        while (timePassed < DurationInSeconds && questionIndex < reflectionQuestions.Count) {
            Console.WriteLine(reflectionQuestions[questionIndex]);
            PauseWithSpinner(10);
            timePassed += 10;
            questionIndex++;
        }
    }
}

public class ListingActivity : MindfulnessActivity {
    private readonly List<string> listingPrompts = new List<string> {
        "Who are people that you appreciate?",
        "What are personal strengths of yours?",
        "Who are people that you have helped this week?",
        "When have you felt the Holy Ghost this month?",
        "Who are some of your personal heroes?"
    };

    private List<string> itemsListed = new List<string>();

    public ListingActivity() : base("Listing", "This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area.")
    {}

    protected override void PerformActivity() {
        var prompt = listingPrompts[random.Next(listingPrompts.Count)];
        Console.WriteLine(prompt);

        InitialCountdown(5); 

        Console.WriteLine("Start listing (type an item and hit Enter):");

        DateTime startTime = DateTime.Now;
        while ((DateTime.Now - startTime).TotalSeconds < DurationInSeconds) {
            string input = Console.ReadLine();
            if (!string.IsNullOrEmpty(input)) {
                itemsListed.Add(input);
            }
        }

        Console.WriteLine($"You have listed {itemsListed.Count} item(s).");
    }
}


public class VisualizationActivity : MindfulnessActivity {
    private readonly List<string> visualizationScenarios = new List<string> {
        "Imagine you're walking on a peaceful beach. Feel the warm sand under your feet.",
        "Picture yourself in a quiet forest. Hear the birds singing and the leaves rustling in the gentle breeze.",
        "Visualize achieving one of your personal goals. Experience the sense of accomplishment and joy.",
        "Envision yourself on top of a mountain, gazing at the panoramic view beneath a clear blue sky.",
        "Imagine walking through a lush garden filled with the most fragrant flowers in full bloom.",
        "Visualize sitting by a tranquil lake at sunset, watching the colors of the sky reflecting on the water.",
        "Picture yourself at a cozy fireplace, feeling the warmth and comfort of the crackling fire.",
        "See yourself surrounded by loved ones in a scene of joyous celebration, feeling the happiness and connection.",
        "Conjure the image of an open field under the night sky, stargazing at a tapestry of stars.",
        "Imagine the sensation of a gentle breeze on your face while walking through a quiet meadow on a spring day.",
        "Visualize yourself achieving a life-long dream and the steps you took to reach that success.",
        "Picture a day of perfect health, feeling energized and revitalized in every part of your being.",
        "Imagine an encounter with your ideal self, engaging in a conversation about your aspirations and life's purpose."
    };

    public VisualizationActivity() : base("Visualization", "This activity will help you relax and focus your mind by visualizing peaceful scenes or achieving future goals. Picture the details in your mind's eye and let the positive feelings arise.")
    {}

    protected override void PerformActivity() {
        Console.WriteLine("Close your eyes and take a deep breath. Let's begin the visualization.");
        int timePassed = 0;
        while (timePassed < DurationInSeconds) {
            var scenario = visualizationScenarios[random.Next(visualizationScenarios.Count)];
            Console.WriteLine(scenario);
            PauseWithSpinner(15);
            timePassed += 15;
        }
    }
}

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
