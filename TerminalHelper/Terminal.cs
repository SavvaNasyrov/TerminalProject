namespace Building_Console_Terminal_Helper;

public class Terminal
{
    public delegate EventArgs TerminalCommand(CmdEventArgs args);

    public delegate bool Condition(string? command);

    private readonly Dictionary<string, TerminalCommand> _terminalCommands = new(capacity:2);

    private readonly Dictionary<string, string> _descriptions = new(capacity: 2);

    public void AddNewCommand(string command, TerminalCommand action, string description)
    {
        _terminalCommands[command] = action;
        _descriptions[command] = description;
    }

    public void RemoveCommand(string command)
    {
        _terminalCommands.Remove(command);
    }

    public EventArgs Execute(string command)
    {
        var args = ParseEventArgs(command);
        command = FirstWord(command);
        return _terminalCommands[command ?? throw new InvalidOperationException()](args);
    }

    public void Run(Condition condition)
    {
        Console.WriteLine("Terminal is running");
        Console.WriteLine("Type \"help\" to see more");
        string? command;
        while (true)
        {
            Console.Write(">>> ");
            command = Console.ReadLine();

            if (condition(command) == false) break;

            if (command == "help")
            {
                Console.WriteLine("Available commands: ");
                foreach (var pair in _descriptions)
                {
                    Console.WriteLine($"* \"{pair.Key}\" - {pair.Value} *");
                }
                Console.WriteLine("----------------");
                continue;
            }

            try
            {
                var args = ParseEventArgs(command);
                command = FirstWord(command);
                _terminalCommands[command ?? throw new InvalidOperationException()](args);
            }
            catch
            {
                Console.WriteLine("Unknown command. Please retype");
            }
        }

        Console.WriteLine("Aborting runtime...");
    }

    private CmdEventArgs ParseEventArgs(string? command)
    {
        if (command is null) throw new ArgumentNullException();
        
        var words = new List<object>(capacity: 1);
        string word = "";
        foreach (var symbol in command)
        {
            if (symbol != ' ')
            {
                word += symbol;
            }
            else
            {
                words.Add(word);
                word = "";
            }
        }
        words.Add(word);
        
        words.RemoveAt(0);

        CmdEventArgs args;

        if (words.Count == 0)
        {
            args = new CmdEventArgs(ReturnTypes.Nothing);
        }
        else
        {
            args = new CmdEventArgs(ReturnTypes.Kwargs, words);
        }

        return args;
    }

    private string FirstWord(string? command)
    {
        if (command is null) throw new ArgumentNullException();
        
        string word = "";
        foreach (var symbol in command)
        {
            if (symbol != ' ')
            {
                word += symbol;
            }
            else
            {
                return word;
            }
        }

        return word;
    }
}