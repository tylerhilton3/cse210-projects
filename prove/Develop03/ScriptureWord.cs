class ScriptureWord {
    private string Word;
    public bool IsHidden { get; private set; }

    public ScriptureWord(string word) {
        Word = word;
        IsHidden = false;
    }

    public void Hide() {
        IsHidden = true;
    }

    public void Unhide() {
        IsHidden = false;
    }

    public override string ToString() {
        return IsHidden ? "____" : Word;
    }
}