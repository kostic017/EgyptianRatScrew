using Unity.Netcode;

public struct PlayerData : INetworkSerializable
{
    public ulong id;
    public int cardsCount;
    public bool isPlayersMove;
    public bool isConnected;
    public bool isInit;

    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        serializer.SerializeValue(ref id);
        serializer.SerializeValue(ref cardsCount);
        serializer.SerializeValue(ref isPlayersMove);
        serializer.SerializeValue(ref isConnected);
        serializer.SerializeValue(ref isInit);
    }
}
