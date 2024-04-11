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