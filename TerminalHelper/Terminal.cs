using System.ComponentModel.Design;

namespace Building_Console_Terminal_Helper;

public class Terminal
{
    internal readonly Dictionary<string, DelegatesRepo.TerminalCommand> TerminalCommands = new(capacity:2);

    internal readonly Dictionary<string, string> Descriptions = new(capacity: 2);

    internal string PreMessage = "";

    internal DelegatesRepo.Condition Condition = (command, args) => true;

    internal bool UseOfficial = true;

    private readonly Dictionary<string, string> _officialDescriptions = new()
    {
        {"clear" , "{no args}clears console"}
    };

    /// <summary>Use it to build your own runtime. Command can contain args or kwargs(Auto-Parsing)</summary>
    public EventArgs Execute(string command)
    {
        var args = ArgsParser.ParseEventArgs(command);
        command = ArgsParser.FirstWord(command);
        return TerminalCommands[command ?? throw new InvalidOperationException()](args);
    }
    
    /// <summary>Use it to build your own runtime.</summary>
    public EventArgs Execute(string command, CmdEventArgs args)
    {
        return TerminalCommands[command ?? throw new InvalidOperationException()](args);
    }

    /// <summary>Basic runtime. Reads and executes all added functions. Official functions is additional.</summary> 
    public void Run()
    {
        Console.WriteLine("Terminal is running");
        Console.WriteLine(PreMessage);
        Console.WriteLine("Type \"help\" to see more");
        string? command;
        while (true)
        {
            Console.Write(">>> ");
            command = Console.ReadLine();
            
            var args = ArgsParser.ParseEventArgs(command ?? throw new InvalidOperationException());
            command = ArgsParser.FirstWord(command);

            if (Condition(command, args) == false) break;
            
            if (command == "help")
            {
                Help();
                continue;
            }

            if (UseOfficial)
            {
                if (command == "clear")
                {
                    Console.Clear();
                    Console.WriteLine("Terminal is running");
                    Console.WriteLine(PreMessage);
                    Console.WriteLine("Type \"help\" to see more");
                    continue;
                }
            }

            try
            {
                TerminalCommands[command ?? throw new InvalidOperationException()](args);
            }
            catch
            {
                Console.WriteLine("Unknown command. Please retype");
            }
        }

        Console.WriteLine("Aborting runtime...");
    }
    private void Help()
    {
        Console.WriteLine("Available commands: ");
        foreach (var pair in Descriptions)
        {
            Console.WriteLine($"* \"{pair.Key}\" - {pair.Value} *");
        }

        if (UseOfficial)
        {
            Console.WriteLine("Available official commands: ");
            foreach (var pair in _officialDescriptions)
            {
                Console.WriteLine($"* \"{pair.Key}\" - {pair.Value} *");
            }
        }

        Console.WriteLine("----------------");
    }
}