using System;

public struct dField
{
    public string name;
    public string value;
    public bool inline;
    public dField(string theName, string theValue, bool isInline)
    {
        this.name = theName;
        this.value = theValue;
        this.inline = isInline;
    }
}