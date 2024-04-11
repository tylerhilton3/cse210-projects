using System;


public class Program {
    public static void Main() {
        var lectureEvent = new Lecture(
            "Science Talk", 
            "A talk on the impact of climate change", 
            new DateTime(2023, 4, 22), 
            "15:00", 
            new Address("123 Elm St", "Springfield", "IL", "USA"), 
            "Dr. Jane Goodall", 
            100);

        var receptionEvent = new Reception(
            "Gallery Opening", 
            "An opening reception for the new art exhibit", 
            new DateTime(2023, 5, 15), 
            "19:00", 
            new Address("456 Oak St", "Somewhere", "CA", "USA"), 
            "rsvp@gallery.com");

        var outdoorEvent = new OutdoorGathering(
            "Community Picnic", 
            "An outdoor picnic for the local community", 
            new DateTime(2023, 6, 10), 
            "12:00", 
            new Address("789 Pine St", "Elsewhere", "TX", "USA"), 
            "Sunny with a chance of showers");

        Console.WriteLine(lectureEvent.GetStandardDetails());
        Console.WriteLine(lectureEvent.GetFullDetails());
        Console.WriteLine(lectureEvent.GetShortDescription());

        Console.WriteLine(receptionEvent.GetStandardDetails());
        Console.WriteLine(receptionEvent.GetFullDetails());
        Console.WriteLine(receptionEvent.GetShortDescription());

        Console.WriteLine(outdoorEvent.GetStandardDetails());
        Console.WriteLine(outdoorEvent.GetFullDetails());
        Console.WriteLine(outdoorEvent.GetShortDescription());
    }
}
