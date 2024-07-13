using System;
using System.Text;
using System.Text.RegularExpressions;

public static class Utility
{
    public static string RemoveColorCode(this string var)
    {
        string editedVar = var;
        Regex rg = new Regex(@"(\[([a-f|A-F]*)\]*)");
        editedVar = rg.Replace(editedVar, string.Empty);
        return editedVar;
    }
}