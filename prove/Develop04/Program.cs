using System;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;

class JournalEntry
{
    public string Prompt { get; set; }
    public string Response { get; set; }
    public DateTime Date { get; set; }

    public override string ToString()
    {
        return $"Date: {Date}\nPrompt: {Prompt}\nResponse: {Response}\n";
    }
}

class Journal
{
    private List<JournalEntry> entries = new List<JournalEntry>();
    private List<string> prompts = new List<string>
    {
        "Who was the most interesting person I interacted with today?",
        "What was the best part of my day?",
        "How did I see the hand of the Lord in my life today?",
        "What was the strongest emotion I felt today?",
        "If I had one thing I could do over today, what would it be?",
        "What was my biggest mistake today?",
        "Do I think I did better today than yesterday?",
        "What habits did I stick to today? Which ones did I not?",
        "What kept me going through my day?",
        "Which relationships did I strengthen and which did I weaken?"
    };

    public void WriteEntry()
    {
        var random = new Random();
        int index = random.Next(prompts.Count);
        string prompt = prompts[index];

        Console.WriteLine(prompt);
        string response = Console.ReadLine();

        var entry = new JournalEntry
        {
            Prompt = prompt,
            Response = response,
            Date = DateTime.Now
        };

        entries.Add(entry);
    }

    public void DisplayJournal()
    {
        foreach (var entry in entries)
        {
            Console.WriteLine(entry);
        }
    }

    public void SaveJournal(string filename)
    {
        using (StreamWriter file = new StreamWriter(filename))
        {
            foreach (var entry in entries)
            {
                file.WriteLine($"{entry.Date}|{entry.Prompt}|{entry.Response}");
            }
        }
    }
    public void LoadJournal(string filename)
    {
        // Check if the file exists before attempting to load
        if (!File.Exists(filename))
        {
            Console.WriteLine("The file does not exist.");
            return;
        }

        List<JournalEntry> loadedEntries = new List<JournalEntry>();
        try
        {
            using (StreamReader file = new StreamReader(filename))
            {
                string line;
                while ((line = file.ReadLine()) != null)
                {
                    var parts = line.Split('|');
                    if (parts.Length != 3)
                    {
                        Console.WriteLine("Skipping invalid entry.");
                        continue; // Skip any lines that do not have exactly 3 parts.
                    }

                    var entry = new JournalEntry
                    {
                        Date = DateTime.Parse(parts[0]),
                        Prompt = parts[1],
                        Response = parts[2]
                    };

                    loadedEntries.Add(entry);
                }
            }

            // Replace current entries only after successful loading
            entries = loadedEntries;
            Console.WriteLine("Journal loaded successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while loading the journal: {ex.Message}");
        }
    }

}

class Program
{
    static void Main(string[] args)
    {
        Journal journal = new Journal();
        bool running = true;

        while (running)
        {
            Console.WriteLine("1. Write new entry");
            Console.WriteLine("2. Display journal");
            Console.WriteLine("3. Save journal");
            Console.WriteLine("4. Load journal");
            Console.WriteLine("5. Open journal in Notepad");
            Console.WriteLine("6. Exit");

            string userInput = Console.ReadLine();
            switch (userInput)
            {
                case "1":
                    journal.WriteEntry();
                    break;
                case "2":
                    journal.DisplayJournal();
                    break;
                case "3":
                    Console.WriteLine("Enter filename:");
                    journal.SaveJournal(Console.ReadLine());
                    break;
                case "4":
                    Console.WriteLine("Enter filename:");
                    journal.LoadJournal(Console.ReadLine());
                    break;
                case "5":
                    Console.WriteLine("Enter filename to open:");
                    OpenInNotepad(Console.ReadLine());
                    break;
                case "6":
                    running = false;
                    break;
                default:
                    Console.WriteLine("Invalid option, please try again.");
                    break;
            }
        }
    }

    static void OpenInNotepad(string filename)
    {
        if (File.Exists(filename))
        {
            Process.Start("notepad.exe", filename);
        }
        else
        {
            Console.WriteLine("File does not exist.");
        }
    }
}

