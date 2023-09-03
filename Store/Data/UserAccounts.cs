namespace WebApplicationL5.Data;

public class UserAccounts
{
    public static Dictionary<string, string> Users { get; } = new ()
    {
        { "admin", "admin" },
        { "guest", "123" }
    };
}