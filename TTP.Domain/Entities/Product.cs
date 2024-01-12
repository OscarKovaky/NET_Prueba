namespace TTP.Domain.Entities;

public class Product : Base.EntityBase
{
    public string Name { get; set; } = string.Empty;
    public double Price { get; set; }
}