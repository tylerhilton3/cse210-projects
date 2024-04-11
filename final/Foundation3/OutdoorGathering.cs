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