using Acme.Integration.Api.Models;

namespace Acme.Integration.Api.Services;

public interface IOrderTransformer
{
    Task<OrderPayload> TransformAsync(OrderPayload payload);
}

public class OrderTransformer : IOrderTransformer
{
    public Task<OrderPayload> TransformAsync(OrderPayload payload)
    {
        // Acme's requests are not implemented yet. The payload is returned unchanged.
        return Task.FromResult(payload);
    }
}
