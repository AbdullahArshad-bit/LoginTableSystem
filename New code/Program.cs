using System;
using System.Collections.Generic;
using System.Threading;

class Program
{
    static void Main(string[] args)
    {
        Console.Title = "Professional Login & Table System";
        AuthManager auth = new AuthManager();
        string loggedInUser = auth.LoginScreen();

        if (!string.IsNullOrEmpty(loggedInUser))
        {
            TableApp app = new TableApp(loggedInUser);
            app.Run();
        }
    }
}

class AuthManager
{
    private Dictionary<string, string> users = new Dictionary<string, string>
    {
        { "Abdullah", "123456" }, { "Ali", "78678612" }, { "Hamza", "654321" }
    };

    public string LoginScreen()
    {
        string inputUser = "";
        while (true)
        {
           
            Console.Clear();
            int boxWidth = 40;
            int boxHeight = 12;
            int startX = (Console.WindowWidth - boxWidth) / 2;
            int startY = (Console.WindowHeight - boxHeight) / 2;

            DrawSimpleBox(startX, startY);
            Console.SetCursorPosition(startX + 12, startY + 2);
            Console.WriteLine("LOGIN PAGE");
            Console.SetCursorPosition(startX + 4, startY + 5);
            Console.Write("Username: ");
            Console.SetCursorPosition(startX + 4, startY + 7);
            Console.Write("Password: ");

            if (string.IsNullOrEmpty(inputUser))
            {
                while (true)
                {
                    ClearLine(startX + 2, startY + 9);
                    Console.SetCursorPosition(startX + 14, startY + 5);
                    Console.Write(new string(' ', 20));
                    Console.SetCursorPosition(startX + 14, startY + 5);
                    string tempUser = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(tempUser))
                        break;

                    if (users.ContainsKey(tempUser))
                    {
                        inputUser = tempUser;
                        Console.SetCursorPosition(startX + 14, startY + 5);
                        Console.ForegroundColor = ConsoleColor.Green; // Username match hone par Green color
                        Console.Write(inputUser);
                        Console.ResetColor();
                        break;
                    }
                    ShowError(startX + 2, startY + 9, "Username Ghalat hai!");
                }
                if (string.IsNullOrEmpty(inputUser)) 
                    continue;
            }
            else
            {
                Console.SetCursorPosition(startX + 14, startY + 5);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(inputUser);
                Console.ResetColor();
            }

            ClearLine(startX + 2, startY + 9);
            Console.SetCursorPosition(startX + 14, startY + 7);
            string inputPass = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(inputPass))
                continue;

            if (users[inputUser] == inputPass)
            {
                Console.SetCursorPosition(startX + 2, startY + 9);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Login Successful!");
                Console.ResetColor();
                Thread.Sleep(1500);
                return inputUser;
            }
            else
            {
                ShowError(startX + 2, startY + 9, "Password sahi nhi ha");
            }
        }
    }

    private void DrawSimpleBox(int x, int y)
    {
        Console.SetCursorPosition(x, y);
        Console.WriteLine("________________________________________");
        for (int i = 1; i <= 10; i++)
        {
            Console.SetCursorPosition(x, y + i);
            Console.WriteLine("|                                      |");
        }
        Console.SetCursorPosition(x, y + 10);
        Console.WriteLine("|______________________________________|");
    }

    private void ClearLine(int x, int y)
    {
        Console.SetCursorPosition(x, y);
        Console.Write(new string(' ', 35));
    }

    private void ShowError(int x, int y, string msg)
    {
        Console.SetCursorPosition(x, y);
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write(msg);
        Console.ResetColor();
        Thread.Sleep(1000);
    }
}

class TableApp
{
    private List<string> names = new List<string> { "Abdullah Arshad", "Muhammad Ali", "Hamza Ali", "Bilal Arshad", "Khalid Arshad", "Zainab Arshad" };
    private int max = 0;
    private bool isRunning = true;
    private string currentUser;

    public TableApp(string user)
    {
        this.currentUser = user;
    }

    public void Run()
    {
        CalculateMaxNameLength();
        while (isRunning)
        {
            Console.Clear();
            DrawWelcomeBox($"Welcome Back, {currentUser.ToUpper()}");

            Console.WriteLine();
            ShowCenteredTable();
            ShowMenu();
            HandleChoice();
        }
    }

    private void DrawWelcomeBox(string message)
    {
        int boxWidth = message.Length + 6;
        string line = new string('-', boxWidth - 2) ;

        Console.ForegroundColor = ConsoleColor.Cyan;
        PrintCentered(line);
        PrintCentered("| " + message + " |");
        PrintCentered(line);
        Console.ResetColor();
    }

    private void CalculateMaxNameLength()
    {
        max = 0;
        foreach (string n in names)
            if (n.Length > max) max = n.Length;
    }

    private void ShowCenteredTable()
    {
        string line = new string('-', max + 10);
        string header = $"| {"ID".PadRight(3)} | {"NAMES".PadRight(max)} |";
        PrintCentered(line);
        PrintCentered(header);
        PrintCentered(line);
        for (int i = 0; i < names.Count; i++)
        {
            string row = $"| {(i + 1).ToString().PadRight(3)} | {names[i].PadRight(max)} |";
            PrintCentered(row);
            PrintCentered(line);
        }
    }

    private void PrintCentered(string text)
    {
        int spaces = (Console.WindowWidth - text.Length) / 2;
        if (spaces < 0) spaces = 0;
        Console.WriteLine(new string(' ', spaces) + text);
    }

    private void ShowMenu()
    {
        int startX = (Console.WindowWidth - 30) / 2;
        Console.WriteLine();
        Console.SetCursorPosition(startX, Console.CursorTop);
        Console.WriteLine("--- OPTIONS ---");
        Console.SetCursorPosition(startX, Console.CursorTop);
        Console.WriteLine("1. Exit Program");
        Console.SetCursorPosition(startX, Console.CursorTop);
        Console.WriteLine("2. Add New Names");
        Console.SetCursorPosition(startX, Console.CursorTop);
        Console.Write("Choice: ");
    }

    private void HandleChoice()
    {
        string choice = Console.ReadLine();
        if (choice == "1") isRunning = false;
        else if (choice == "2") GetUserInput();
    }

    private void GetUserInput()
    {
        int startX = (Console.WindowWidth - 30) / 2;
        Console.WriteLine();
        Console.SetCursorPosition(startX, Console.CursorTop);
        Console.WriteLine("Enter names (Comma separated):");
        Console.SetCursorPosition(startX, Console.CursorTop);
        string input = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(input)) ProcessNames(input, startX);
    }

    private void ProcessNames(string input, int startX)
    {
        string[] newNamesArray = input.Split(',');
        foreach (string n in newNamesArray)
        {
            string cleanName = n.Trim();
            Console.SetCursorPosition(startX, Console.CursorTop);
            if (cleanName.Length >= 6 && cleanName.Length <= 20)
            {
                names.Insert(0, cleanName);
                CalculateMaxNameLength();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Added: {cleanName}");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($" Error: {cleanName} (6-20 character)");
            }
            Console.ResetColor();
        }
        Console.SetCursorPosition(startX, Console.CursorTop);
        Console.WriteLine("Press Enter to continue");
        Console.ReadLine();
    }
}