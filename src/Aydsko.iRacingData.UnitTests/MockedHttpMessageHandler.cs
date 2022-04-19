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
    private readonly CookieContainer cookieContainer;

    public Queue<HttpRequestMessage> Requests { get; } = new();
    public Queue<HttpResponseMessage> Responses { get; } = new();

    public MockedHttpMessageHandler(CookieContainer cookieContainer)
    {
        this.cookieContainer = cookieContainer;
    }

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        if (request is null)
        {
            throw new ArgumentNullException(nameof(request));
        }

        cancellationToken.ThrowIfCancellationRequested();

        Requests.Enqueue(request);

        if (Responses.TryDequeue(out var response))
        {
            if (response.Headers.TryGetValues("Set-Cookie", out var cookieValues))
            {
                cookieContainer.SetCookies(request.RequestUri!, string.Join(",", cookieValues));
            }
            return Task.FromResult(response);
        }

        return Task.FromResult(new HttpResponseMessage(HttpStatusCode.NotFound));
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

            var responseMessage = new HttpResponseMessage(statusCode)
            {
                Content = new StringContent(responseDictionary["content"].ToString(), Encoding.UTF8, "text/json")
            };

            foreach (var (headerName, values) in responseDictionary["headers"].EnumerateObject()
                                                                       .ToDictionary(prop => prop.Name,
                                                                                     prop =>
                                                                                     {
                                                                                         if (prop.Value.ValueKind == JsonValueKind.Array)
                                                                                         {
                                                                                             return prop.Value.EnumerateArray().Select(e => e.ToString()).ToArray();
                                                                                         }
                                                                                         else
                                                                                         {
                                                                                             return new[] { prop.Value.GetString() };
                                                                                         }
                                                                                     }).ToArray())
            {
                responseMessage.Headers.Add(headerName, values);
            }

            Responses.Enqueue(responseMessage);
        }
    }
}
