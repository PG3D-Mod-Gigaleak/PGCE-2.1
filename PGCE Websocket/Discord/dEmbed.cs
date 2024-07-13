using System;

public struct dEmbed
{
    public string title;
    public string description;
    public int color = 0xFFFFFF;
    public dField[] fields;

    public dEmbed(string title, string desc, int col, dField[] fields)
    {
        this.title = title;
        this.description = desc;
        this.color = col;
        this.fields = fields;
    }
}