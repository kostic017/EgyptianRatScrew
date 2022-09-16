
public class NetworkPlayer : Player
{
    private readonly string id;
    public  ulong sessionId;
    public bool IsConnected;

    public string ID => id;

    public NetworkPlayer(string id, ulong sessionId)
    {
        this.id = id;
        this.sessionId = sessionId;
        this.IsConnected = true;
    }
}
