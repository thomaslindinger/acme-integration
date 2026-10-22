using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json.Nodes;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Acme.Integration.Tests;

public class TransformEndpointTests(WebApplicationFactory<Program> factory)
    : IClassFixture<WebApplicationFactory<Program>>
{
    [Theory]
    [InlineData("order-denmark.json")]
    [InlineData("quote-denmark.json")]
    [InlineData("quote-with-freight.json")]
    [InlineData("order-sweden-refresh.json")]
    public async Task Transform_ReturnsPayloadInSameStructure(string sample)
    {
        var client = factory.CreateClient();
        var json = await File.ReadAllTextAsync(SamplePath(sample));
        var content = new StringContent(json, Encoding.UTF8);
        content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

        var response = await client.PostAsync("/transform", content);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var expected = JsonNode.Parse(json);
        var actual = JsonNode.Parse(await response.Content.ReadAsStringAsync());
        Assert.True(JsonNode.DeepEquals(expected, actual));
    }

    [Fact]
    public async Task Transform_ReplacesNullsWithEmptyStrings()
    {
        var client = factory.CreateClient();
        const string json = """
            {
              "mail_id": null,
              "iteration": "first",
              "intent": null,
              "area": null,
              "products": [
                { "line_number": 1, "product_number": { "id": null, "value": "BP1" }, "unit_price": null },
                null
              ],
              "header": { "order_number": null }
            }
            """;
        var content = new StringContent(json, Encoding.UTF8);
        content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

        var response = await client.PostAsync("/transform", content);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var body = JsonNode.Parse(await response.Content.ReadAsStringAsync())!;
        Assert.Equal("", (string?)body["mail_id"]);
        Assert.Equal("", (string?)body["intent"]);
        Assert.Equal("", (string?)body["area"]!["name"]);
        var products = body["products"]!.AsArray();
        Assert.Single(products);
        Assert.Equal("", (string?)products[0]!["product_number"]!["id"]);
        Assert.Equal("BP1", (string?)products[0]!["product_number"]!["value"]);
        Assert.Equal("", (string?)products[0]!["unit_price"]!["value"]);
        Assert.Equal("", (string?)products[0]!["delivery_date"]!["value"]);
        Assert.Equal("", (string?)body["header"]!["order_number"]!["value"]);
        Assert.Equal("", (string?)body["header"]!["account_id"]!["value"]);
    }

    private static string SamplePath(string fileName)
    {
        var dir = new DirectoryInfo(AppContext.BaseDirectory);
        while (dir is not null && !Directory.Exists(Path.Combine(dir.FullName, "samples")))
            dir = dir.Parent;
        return Path.Combine(dir!.FullName, "samples", fileName);
    }
}
