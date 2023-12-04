using System.Text.Json;

namespace CNET.Server;

public class Message
{
    public string Text { get; set; }
    public DateTime DateTime { get; set; }
    public string NicknameFrom { get; set; }
    public string NicknameTo { get; set; }

    public string SerializemessageToJson() => JsonSerializer.Serialize(this);

    public static Message? DeserializeFromJson(string message) => JsonSerializer.Deserialize<Message>(message);

    public void Print()
    {
        Console.WriteLine(ToString());
    }
    public override string ToString()
    {
        return $"{this.DateTime}message received {this.Text} from {this.NicknameFrom} ";
    }
}
