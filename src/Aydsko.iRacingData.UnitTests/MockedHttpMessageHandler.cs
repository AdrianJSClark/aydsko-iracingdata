// © 2022 Adrian Clark
// This file is licensed to you under the MIT license.

using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Reflection;
using System.Text;
using System.Text.Json;

namespace Aydsko.iRacingData.UnitTests;

public class MockedHttpMessageHandler : HttpMessageHandler
{
    private readonly Assembly ResourceAssembly = typeof(MockedHttpMessageHandler).Assembly;

    public Queue<HttpRequestMessage> Requests { get; } = new();
    public Queue<HttpResponseMessage> Responses { get; } = new();

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        Requests.Enqueue(request);

        if (Responses.TryDequeue(out var response))
        {
            return Task.FromResult(response);
        }

        return Task.FromResult(new HttpResponseMessage(System.Net.HttpStatusCode.NotFound));
    }

    [SuppressMessage("Reliability", "CA2000:Dispose objects before losing scope", Justification = "Streams need to be available for use.")]
    public async Task QueueResponsesAsync(string testName)
    {
        foreach (var manifestName in ResourceAssembly.GetManifestResourceNames()
                                                     .Where(mrn => mrn.StartsWith($"Aydsko.iRacingData.UnitTests.Responses.{testName}", StringComparison.InvariantCultureIgnoreCase)))
        {
            var resourceStream = ResourceAssembly.GetManifestResourceStream(manifestName)
                ?? throw ResourceNotFoundException.ForManifestResourceName(manifestName);

            var responseDocument = await JsonSerializer.DeserializeAsync<JsonDocument>(resourceStream).ConfigureAwait(false);

            if (responseDocument is null)
            {
                continue;
            }

            var responseDictionary = responseDocument!.RootElement.EnumerateObject()
                                                                  .ToDictionary(prop => prop.Name, prop => prop.Value);

            if (!responseDictionary.ContainsKey("statuscode")
                || !Enum.TryParse<HttpStatusCode>(responseDictionary["statuscode"].ToString(), out var statusCode))
            {
                statusCode = HttpStatusCode.OK;
            }

            var responseMessage = new HttpResponseMessage(statusCode) {
                Content = new StringContent(responseDictionary["content"].ToString(), Encoding.UTF8, "text/json")
            };

            foreach (var (headerName, values) in responseDictionary["headers"].EnumerateObject()
                                                                       .ToDictionary(prop => prop.Name, prop => prop.Value.GetString()).ToArray())
            {
                responseMessage.Headers.Add(headerName, values);
            }

            Responses.Enqueue(responseMessage);
        }
    }
}
