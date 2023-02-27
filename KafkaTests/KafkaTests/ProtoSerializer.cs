using ProtoBuf;

public static class ProtoSerializer
{
    public static byte[] GPBSerialization(object objtoSerialize)
    {
        if (objtoSerialize == null)
            return null;
        try
        {
            using MemoryStream stream = new();
            Serializer.Serialize(stream, objtoSerialize);
            return stream.ToArray();
        }
        catch {
            throw;
        }
    }
    public static object GPBDeserialization(byte[] data)
    {
        if (data == null) return null;
        try
        {
            using MemoryStream stream = new(data);
            return Serializer.Deserialize(typeof(EventRaisedMessage), stream);
        }
         catch { throw; }
    }
}

