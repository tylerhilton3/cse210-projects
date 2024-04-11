using System;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;



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

