using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json.Nodes;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Acme.Integration.Tests;

public class TransformEndpointTests(WebApplicationFactory<Program> factory)
    : IClassFixture<WebApplicationFactory<Program>>
{
    [Fact]
    public async Task Transform_ReturnsPayloadInSameStructure()
    {
        var client = factory.CreateClient();
        var json = await File.ReadAllTextAsync(SamplePath("order-denmark.json"));
        var content = new StringContent(json, Encoding.UTF8);
        content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

        var response = await client.PostAsync("/transform", content);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var expected = JsonNode.Parse(json);
        var actual = JsonNode.Parse(await response.Content.ReadAsStringAsync());
        Assert.True(JsonNode.DeepEquals(expected, actual));
    }

    private static string SamplePath(string fileName)
    {
        var dir = new DirectoryInfo(AppContext.BaseDirectory);
        while (dir is not null && !Directory.Exists(Path.Combine(dir.FullName, "samples")))
            dir = dir.Parent;
        return Path.Combine(dir!.FullName, "samples", fileName);
    }
}
