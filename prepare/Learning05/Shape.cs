public abstract class Shape {
    public abstract double GetArea();
    private string _color;

    public Shape(string color) {
        _color = color;
    }

    public string GetColor() {
        return _color;
    }

    public void SetColor(string color) {
        _color = color;
    }
}