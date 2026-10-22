namespace Acme.Integration.Api.Models;

public class OrderPayload
{
    public string MailId { get; set; } = "";
    public string Iteration { get; set; } = "";
    public string Intent { get; set; } = "";
    public Area Area { get; set; } = new();
    public List<ProductLine> Products { get; set; } = [];
    public Header Header { get; set; } = new();
}

public class Area
{
    public string Name { get; set; } = "";
}

public class ProductLine
{
    public int LineNumber { get; set; }
    public Field ProductNumber { get; set; } = new();
    public Field UnitPrice { get; set; } = new();
    public Field DeliveryDate { get; set; } = new();
}

public class Header
{
    public Field OrderNumber { get; set; } = new();
    public Field AccountId { get; set; } = new();
}

public class Field
{
    public string Id { get; set; } = "";
    public string Value { get; set; } = "";
}
