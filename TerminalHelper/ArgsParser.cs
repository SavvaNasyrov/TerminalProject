using System.Collections;

namespace Building_Console_Terminal_Helper;

internal static class ArgsParser
{
    public static CmdEventArgs ParseEventArgs(string command)
    {
        var words = DetectWords(command);

        CmdEventArgs args;

        if (words.Count == 0)
        {
            args = CmdEventArgs.Empty;
        }
        else
        {
            bool isKwargs = true;
            foreach (var obj in words)
            {
                if (obj[0] != '-') isKwargs = false;
            }

            if (isKwargs)
            {
                args = new CmdEventArgs(ReturnTypes.Kwargs);
                args.Kwargs = words;
            }
            else
            {
                bool isContainsKwargs = false;
                foreach (var obj in words)
                {
                    if (obj[0] == '-') isContainsKwargs = true;
                }

                if (isContainsKwargs)
                {
                    var kwargs = DivideArgs(ref words);

                    args = new CmdEventArgs(ReturnTypes.AnyAndKwargs, words, kwargs);
                }
                else
                {
                    args = new CmdEventArgs(ReturnTypes.Any, words);
                }
            }
        }

        return args;
    }

    private static List<string> DetectWords(string command)
    {
        var words = new List<string>(capacity: 1);
        string word = "";
        foreach (var symbol in command)
        {
            if (symbol != ' ')
            {
                word += symbol;
            }
            else
            {
                words.Add(word);
                word = "";
            }
        }
        words.Add(word);
        
        words.RemoveAt(0);

        return words;
    }

    private static List<string> DivideArgs(ref List<string> words)
    {
        var kwargs = new List<string>();
        foreach (var obj in words)
        {
            if (obj[0] == '-')
            {
                kwargs.Add(obj);
            }
        }

        foreach (var kwarg in kwargs)
        {
            words.Remove(kwarg);
        }

        return kwargs;
    }

    public static string FirstWord(string command)
    {
        string word = "";
        foreach (var symbol in command)
        {
            if (symbol != ' ')
            {
                word += symbol;
            }
            else
            {
                return word;
            }
        }

        return word;
    }
}