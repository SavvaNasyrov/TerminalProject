namespace Building_Console_Terminal_Helper.Builder;

public abstract class TerminalBuilder
{
    protected Terminal Terminal = new();

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

    public Terminal Build()
    {
        return Terminal;
    }
}