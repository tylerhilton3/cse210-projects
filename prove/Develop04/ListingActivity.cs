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