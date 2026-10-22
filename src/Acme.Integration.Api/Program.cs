using System.Text.Json;
using System.Text.Json.Serialization;
using Acme.Integration.Api.Models;
using Acme.Integration.Api.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower;
    options.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});

builder.Services.AddSingleton<IOrderTransformer, OrderTransformer>();

var app = builder.Build();

app.MapPost("/transform", async (OrderPayload payload, IOrderTransformer transformer) =>
    Results.Ok(await transformer.TransformAsync(payload)));

app.Run();

public partial class Program;
