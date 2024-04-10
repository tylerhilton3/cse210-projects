using System;

public class Address {
    private string street;
    private string city;
    private string state;
    private string country;

    public Address(string street, string city, string state, string country) {
        this.street = street;
        this.city = city;
        this.state = state;
        this.country = country;
    }

    public override string ToString() {
        return $"{street}, {city}, {state}, {country}";
    }
}

public abstract class Event {
    private string title;
    private string description;
    private DateTime date;
    private string time;
    private Address address;

    protected Event(string title, string description, DateTime date, string time, Address address) {
        this.title = title;
        this.description = description;
        this.date = date;
        this.time = time;
        this.address = address;
    }

    public string GetStandardDetails() {
        return $"{title} - {description}. Date: {date.ToShortDateString()}, Time: {time} at {address}";
    }

    public abstract string GetFullDetails();
    public string GetShortDescription() {
        return $"{GetType().Name} - {title} on {date.ToShortDateString()}";
    }
}

public class Lecture : Event {
    private string speaker;
    private int capacity;

    public Lecture(string title, string description, DateTime date, string time, Address address, string speaker, int capacity)
        : base(title, description, date, time, address) {
        this.speaker = speaker;
        this.capacity = capacity;
    }

    public override string GetFullDetails() {
        return $"{GetStandardDetails()}, Speaker: {speaker}, Capacity: {capacity}";
    }
}

public class Reception : Event {
    private string rsvpEmail;

    public Reception(string title, string description, DateTime date, string time, Address address, string rsvpEmail)
        : base(title, description, date, time, address) {
        this.rsvpEmail = rsvpEmail;
    }

    public override string GetFullDetails() {
        return $"{GetStandardDetails()}, RSVP at {rsvpEmail}";
    }
}

public class OutdoorGathering : Event {
    private string weather;

    public OutdoorGathering(string title, string description, DateTime date, string time, Address address, string weather)
        : base(title, description, date, time, address) {
        this.weather = weather;
    }

    public override string GetFullDetails() {
        return $"{GetStandardDetails()}, Weather forecast: {weather}";
    }
}

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
