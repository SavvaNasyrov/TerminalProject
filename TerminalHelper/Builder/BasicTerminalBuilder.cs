using System.Runtime.CompilerServices;

namespace Building_Console_Terminal_Helper.Builder;

public class BasicTerminalBuilder : TerminalBuilder
{
    public void AddNewCommand(string command, DelegatesRepo.TerminalCommand action, string description)
    {
        Terminal.TerminalCommands[command] = action;
        Terminal.Descriptions[command] = description;
    }

    public void SetPreRunMessage(string message)
    {
        Terminal.PreMessage = message;
    }

    public void SetBreakCondition(DelegatesRepo.Condition condition)
    {
        Terminal.Condition = condition;
    }

    public void UseOfficial()
    {
        Terminal.UseOfficial = true;
    }

    public void DontUseOfficial()
    {
        Terminal.UseOfficial = false;
    }

}