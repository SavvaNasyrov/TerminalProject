namespace Building_Console_Terminal_Helper.Builder;

public abstract class TerminalBuilder
{
    protected Terminal Terminal = new();
    
    public Terminal Build()
    {
        return Terminal;
    }
}