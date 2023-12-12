using System.Runtime.CompilerServices;
using Building_Console_Terminal_Helper.Interfaces;

namespace Building_Console_Terminal_Helper.Builder;

public class BasicTerminalBuilder : ITerminalBuilder<BasicTerminal>
{
    private readonly BasicTerminal _terminal = new();

    public void AddNewCommand(string command, DelegatesRepo.TerminalCommand action, string description)
    {
        _terminal.TerminalCommands[command] = action;
        _terminal.Descriptions[command] = description;
    }

    public void SetPreRunMessage(string message)
    {
        _terminal.PreMessage = message;
    }

    public void SetBreakCondition(DelegatesRepo.Condition condition)
    {
        _terminal.Condition = condition;
    }

    public void UseOfficial()
    {
        _terminal.UseOfficial = true;
    }

    public void DontUseOfficial()
    {
        _terminal.UseOfficial = false;
    }

    public BasicTerminal Build()
    {
        return _terminal;
    }
}