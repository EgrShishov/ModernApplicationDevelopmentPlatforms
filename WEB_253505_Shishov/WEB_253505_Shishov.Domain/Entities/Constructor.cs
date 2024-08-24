namespace WEB_253505_Shishov.Domain.Entities;

public class Constructor : Entity
{
    public short Picies { get; set; }
    public Category? Category { get; set; }
    public int CategoryId { get; set; }
    public string Description { get; set; }
    public string? Image { get; set; }
    public decimal Price { get; set; }
}
