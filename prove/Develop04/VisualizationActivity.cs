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