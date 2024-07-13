using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

public class dWebHook: IDisposable
{
    private readonly WebClient dWebClient;
    private static Dictionary<string, object> discordValues = new Dictionary<string, object>();
    public string WebHook { get; set;}
    public string UserName { get; set; }
    public string ProfilePicture { get; set;}

    public dWebHook()
    {
        dWebClient = new WebClient();
    }
    public void SendMessage(string msgSend)
    {
        discordValues = new Dictionary<string, object>();
        discordValues.Add("username", UserName);
        discordValues.Add("avatar_url", ProfilePicture);
        discordValues.Add("content", msgSend);
        sendDiscordWebhook(WebHook, JsonConvert.SerializeObject(discordValues));
    }
    public void SendEmbed(string title, string desc, int color, dField[] fields)
    {
        discordValues = new Dictionary<string, object>();
        discordValues.Add("username", UserName);
        discordValues.Add("avatar_url", ProfilePicture);
        List<dEmbed> embed = new List<dEmbed>();
        embed.Add(new dEmbed(title, desc, color, fields));
        discordValues.Add("embeds", embed);
        sendDiscordWebhook(WebHook, JsonConvert.SerializeObject(discordValues));
    }
    public void SendEmbed(List<dEmbed> embed)
    {
        discordValues = new Dictionary<string, object>();
        discordValues.Add("username", UserName);
        discordValues.Add("avatar_url", ProfilePicture);
        discordValues.Add("embeds", JsonConvert.SerializeObject(embed));
        sendDiscordWebhook(WebHook, JsonConvert.SerializeObject(discordValues));
    }
    public static void sendDiscordWebhook(string URL, string escapedjson)
    {
        var wr = WebRequest.Create(URL);
        wr.ContentType = "application/json";
        wr.Method = "POST";
        using (var sw = new StreamWriter(wr.GetRequestStream()))
            sw.Write(escapedjson);
        wr.GetResponse();
    }
    public void Dispose()
    {
        dWebClient.Dispose();
    }
}