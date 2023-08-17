namespace WebApplicationL5.Models;

public class Orders
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public int ProductId { get; set; }
    public int Amount { get; set; }
}