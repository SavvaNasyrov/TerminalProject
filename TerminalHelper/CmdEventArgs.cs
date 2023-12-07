namespace Building_Console_Terminal_Helper;

public class CmdEventArgs : EventArgs
{
    public ReturnTypes DataType;

    public List<object>? Data;

    public CmdEventArgs(ReturnTypes message)
    {
        DataType = message;
    }
    
    public CmdEventArgs(ReturnTypes message, List<object> data)
    {
        DataType = message;
        Data = data;
    }
}