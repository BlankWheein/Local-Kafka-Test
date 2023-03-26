using SolTechnology.Avro;

public static class AvroSerializer
{
    //Supported encoding types:
        //Null(default)
        //Deflate
        //Snappy
        //GZip
        //Brotli

    public static byte[] GPBSerialization(object objtoSerialize)
    {
        if (objtoSerialize == null)
            return null;
        try
        {
            byte[] avroObject = AvroConvert.Serialize(objtoSerialize);
            return avroObject;
        }
        catch
        {
            throw;
        }
    }
    public static object GPBDeserialization(byte[] data)
    {
        if (data == null) return null;
        try
        {
            var result = AvroConvert.Deserialize<EventRaisedMessage>(data);
            return result;
        }
        catch { throw; }
    }
}

