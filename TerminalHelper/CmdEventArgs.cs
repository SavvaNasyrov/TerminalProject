namespace Building_Console_Terminal_Helper;

public class CmdEventArgs : EventArgs
{
    public ReturnTypes DataType;

    public List<string>? Args;

    public List<string>? Kwargs;

    public new static CmdEventArgs Empty = new (ReturnTypes.Nothing);

    public CmdEventArgs(ReturnTypes message)
    {
        DataType = message;
    }
    
    public CmdEventArgs(ReturnTypes message, List<string> args)
    {
        DataType = message;
        Args = args;
    }
    
    public CmdEventArgs(ReturnTypes message, List<string> args, List<string> kwargs)
    {
        DataType = message;
        Args = args;
        Kwargs = kwargs;
    }
}