using PGCE;
using WebSocketSharp;
using WebSocketSharp.Server;

public struct PlayerSession
{
    public IWebSocketSession Session;
    public AccountParameters? AccountParameters;
}