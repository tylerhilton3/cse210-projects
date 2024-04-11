using System;
using System.Collections.Generic;
using System.Linq;

class Program {
    static void Main() {
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
