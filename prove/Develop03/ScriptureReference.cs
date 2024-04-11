class ScriptureReference {
    public string Book { get; private set; }
    public int Chapter { get; private set; }
    public int StartingVerse { get; private set; }
    public int EndingVerse { get; private set; }

    public ScriptureReference(string book, int chapter, int startVerse, int endVerse) {
        Book = book;
        Chapter = chapter;
        StartingVerse = startVerse;
        EndingVerse = endVerse;
    }

    public override string ToString() {
        return StartingVerse == EndingVerse ? $"{Book} {Chapter}:{StartingVerse}" :
            $"{Book} {Chapter}:{StartingVerse}-{EndingVerse}";
    }
}