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
    public Field? ProductNumber { get; set; }
    public Field? UnitPrice { get; set; }
    public Field? DeliveryDate { get; set; }
}

public class Header
{
    public Field? OrderNumber { get; set; }
    public Field? AccountId { get; set; }
}

public class Field
{
    public string? Id { get; set; }
    public string? Value { get; set; }
}
