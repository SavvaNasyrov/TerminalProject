namespace Building_Console_Terminal_Helper;

public class Terminal
{
    public delegate EventArgs TerminalCommand(CmdEventArgs args);

    public delegate bool Condition(string? command);

    private readonly Dictionary<string, TerminalCommand> _terminalCommands = new(capacity:2);

    public readonly Dictionary<string, string> Descriptions = new(capacity: 2);

    private readonly Dictionary<string, string> _officialDescriptions = new()
    {
        {"clear" , "{no args}clears console"}
    };

    public void AddNewCommand(string command, TerminalCommand action, string description)
    {
        _terminalCommands[command] = action;
        Descriptions[command] = description;
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

    public void Run(Condition condition, string preMessage)
    {
        Console.WriteLine("Terminal is running");
        Console.WriteLine(preMessage);
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
                foreach (var pair in Descriptions)
                {
                    Console.WriteLine($"* \"{pair.Key}\" - {pair.Value} *");
                }

                Console.WriteLine("Available official commands: ");
                foreach (var pair in _officialDescriptions)
                {
                    Console.WriteLine($"* \"{pair.Key}\" - {pair.Value} *");
                }
                Console.WriteLine("----------------");
                continue;
            }
            if (command == "clear")
            {
                Console.Clear();
                Console.WriteLine("Terminal is running");
                Console.WriteLine(preMessage);
                Console.WriteLine("Type \"help\" to see more");
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
        
        var words = new List<string>(capacity: 1);
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
            bool isKwargs = true;
            foreach (var obj in words)
            {
                if (obj[0] != '-') isKwargs = false;
            }

            if (isKwargs)
            {
                args = new CmdEventArgs(ReturnTypes.Kwargs);
                args.Kwargs = words;
            }
            else
            {
                bool isContainsKwargs = false;
                foreach (var obj in words)
                {
                    if (obj[0] == '-') isContainsKwargs = true;
                }

                if (isContainsKwargs)
                {
                    var kwargs = new List<string>();
                    foreach (var obj in words)
                    {
                        if (obj[0] == '-')
                        {
                            kwargs.Add(obj);
                        }
                    }

                    foreach (var kwarg in kwargs)
                    {
                        words.Remove(kwarg);
                    }

                    args = new CmdEventArgs(ReturnTypes.AnyAndKwargs, words, kwargs);
                }
                else
                {
                    args = new CmdEventArgs(ReturnTypes.Any, words);
                }
            }
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