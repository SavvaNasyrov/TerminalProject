namespace Building_Console_Terminal_Helper.Interfaces;

public abstract class Terminal
{
    private readonly Dictionary<string, DelegatesRepo.TerminalCommand> TerminalCommands = new(capacity:2);

    public virtual EventArgs Execute(string command)
    {
        return TerminalCommands[command](CmdEventArgs.Empty);
    }

}