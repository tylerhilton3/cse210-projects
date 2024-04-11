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