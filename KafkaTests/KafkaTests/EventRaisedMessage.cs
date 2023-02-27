using ProtoBuf;

[ProtoContract]
public class EventRaisedMessage
{
    [ProtoMember(1)]
    public DateTime Date { get; set; }
    public EventRaisedMessage() {}

    public EventRaisedMessage(DateTime date)
    {
        Date = date;
    }
    public override string ToString()
    {
        return $"{Date}";
    }
}

