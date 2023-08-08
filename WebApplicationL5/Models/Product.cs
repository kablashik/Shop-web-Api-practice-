using System.Text.Json.Serialization;
using WebApplicationL5.JsonSettings.Converters;

namespace WebApplicationL5.Models;

public class Product
{
    [JsonPropertyName("Id")]
    public int ProductId { get; set; }
    
    
    [JsonConverter(typeof(StringConverter))]
    [JsonPropertyName("Name")]
    public string? ProductName { get; set; } 
    
    [JsonPropertyName("Description")]
    public string? ProductDescription { get; set; } 
    
    [JsonPropertyName("Price")]
    public double ProductPrice { get; set; }
        
    [JsonPropertyName("Amount")]
    public int ProductAmount { get; set; }
    
}
