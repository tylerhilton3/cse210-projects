class Scripture {
    private List<ScriptureWord> Words;
    public ScriptureReference Reference { get; private set; }

    public Scripture(ScriptureReference reference, string text) {
        Reference = reference;
        Words = text.Split(new[] { ' ', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(word => new ScriptureWord(word)).ToList();
    }

    public void Display() {
        Console.WriteLine(Reference.ToString());
        Console.WriteLine(string.Join(" ", Words.Select(word => word.ToString())));
    }

    public void HideRandomWord() {
        var rnd = new Random();
        var visibleWords = Words.Where(word => !word.IsHidden).ToList();
        if (visibleWords.Any()) {
            var wordToHide = visibleWords[rnd.Next(visibleWords.Count)];
            wordToHide.Hide();
        }
    }

    public void UnhideRandomWord() {
        var rnd = new Random();
        var hiddenWords = Words.Where(word => word.IsHidden).ToList();
        if (hiddenWords.Any()) {
            var wordToUnhide = hiddenWords[rnd.Next(hiddenWords.Count)];
            wordToUnhide.Unhide();
        }
    }
}