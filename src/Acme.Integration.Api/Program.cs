using System.Text.Json;
using Acme.Integration.Api.Models;
using Acme.Integration.Api.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureHttpJsonOptions(options =>
    options.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower);

builder.Services.AddSingleton<IOrderTransformer, OrderTransformer>();

var app = builder.Build();

app.MapPost("/transform", async (OrderPayload payload, IOrderTransformer transformer) =>
    Results.Ok(await transformer.TransformAsync(payload.Normalize())));

app.Run();

public partial class Program;
