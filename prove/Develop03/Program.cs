using System;
using System.Collections.Generic;
using System.Linq;

class Program {
    static void Main() {
        // Define a scripture reference and text.
        var scriptureReference = new ScriptureReference("Proverbs", 3, 5, 6);
        var scriptureText = "Trust in the LORD with all your heart and lean not on your own understanding; " +
                            "in all your ways submit to him, and he will make your paths straight.";

        var scripture = new Scripture(scriptureReference, scriptureText);

        while (true) {
            Console.Clear();
            scripture.Display();

            Console.WriteLine("\nPress enter to hide a word or type anything and press enter to unhide a word. Type 'quit' to exit.");
            var input = Console.ReadLine();
            
            if (input?.ToLower() == "quit") {
                break;
            } else if (string.IsNullOrWhiteSpace(input)) {
                scripture.HideRandomWord();
            } else {
                scripture.UnhideRandomWord();
            }
        }
    }
}

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