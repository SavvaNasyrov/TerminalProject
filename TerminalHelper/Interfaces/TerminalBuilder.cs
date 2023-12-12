namespace Building_Console_Terminal_Helper.Interfaces;

public interface ITerminalBuilder<out TTerminal>
{
    public TTerminal Build();
}