namespace Acme.Integration.Api.Models;

/// <summary>
/// Explicit nulls in incoming JSON bypass the property defaults, so they are
/// replaced with empty strings and empty objects before any business logic runs.
/// </summary>
public static class PayloadNormalization
{
    public static OrderPayload Normalize(this OrderPayload payload)
    {
        payload.MailId ??= "";
        payload.Iteration ??= "";
        payload.Intent ??= "";

        payload.Area ??= new();
        payload.Area.Name ??= "";

        payload.Products = (payload.Products ?? []).OfType<ProductLine>().ToList();
        foreach (var line in payload.Products)
        {
            line.ProductNumber = Normalize(line.ProductNumber);
            line.UnitPrice = Normalize(line.UnitPrice);
            line.DeliveryDate = Normalize(line.DeliveryDate);
        }

        payload.Header ??= new();
        payload.Header.OrderNumber = Normalize(payload.Header.OrderNumber);
        payload.Header.AccountId = Normalize(payload.Header.AccountId);

        return payload;
    }

    private static Field Normalize(Field? field)
    {
        field ??= new();
        field.Id ??= "";
        field.Value ??= "";
        return field;
    }
}
