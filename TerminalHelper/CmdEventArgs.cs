namespace Building_Console_Terminal_Helper;

public class CmdEventArgs : EventArgs
{
    public ReturnTypes DataType;

    public List<string>? Data;

    public List<string> Kwargs;

    public CmdEventArgs(ReturnTypes message)
    {
        DataType = message;
    }
    
    public CmdEventArgs(ReturnTypes message, List<string> data)
    {
        DataType = message;
        Data = data;
    }
    
    public CmdEventArgs(ReturnTypes message, List<string> data, List<string> kwargs)
    {
        DataType = message;
        Data = data;
        Kwargs = kwargs;
    }
}