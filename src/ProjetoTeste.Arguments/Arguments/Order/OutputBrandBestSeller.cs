using System.Text.Json.Serialization;

namespace ProjetoTeste.Arguments.Arguments.Order;

[method: JsonConstructor]
public class OutputBrandBestSeller(long id, string name, string code, string description, int totalSell, decimal totalPrice)
{
    public long Id { get; private set; } = id;
    public string Name { get; private set; } = name;
    public string Code { get; private set; } = code;
    public string Description { get; private set; } = description;
    public int TotalSell { get; private set; } = totalSell;
    public decimal TotalPrice { get; private set; } = totalPrice;
}