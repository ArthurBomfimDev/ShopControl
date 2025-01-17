using System.Text.Json.Serialization;

namespace ProjetoTeste.Arguments.Arguments.Order;

public class OutputBrandBestSeller
{
    public long Id { get; private set; }
    public string? Name { get; private set; }
    public string? Code { get; private set; }
    public string? Description { get; private set; }
    public int TotalSell { get; private set; }
    public decimal TotalPrice { get; private set; }

    public OutputBrandBestSeller(long id, int totalSell, decimal totalPrice)
    {
        Id = id;
        TotalSell = totalSell;
        TotalPrice = totalPrice;
    }

    [JsonConstructor]
    public OutputBrandBestSeller(long id, string name, string code, string description, int totalSell, decimal totalPrice)
    {
        Id = id;
        Name = name;
        Code = code;
        Description = description;
        TotalSell = totalSell;
        TotalPrice = totalPrice;
    }
}