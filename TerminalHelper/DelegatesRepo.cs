namespace Building_Console_Terminal_Helper;

public static class DelegatesRepo
{
    public delegate EventArgs TerminalCommand(CmdEventArgs args);
    public delegate bool Condition(string? command, CmdEventArgs args);
}