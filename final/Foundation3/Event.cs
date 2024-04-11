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
