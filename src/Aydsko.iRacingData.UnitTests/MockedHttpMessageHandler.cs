// © 2023 Adrian Clark
// This file is licensed to you under the MIT license.

using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Text.Json;

namespace Aydsko.iRacingData.UnitTests;

public class MockedHttpMessageHandler : HttpMessageHandler
{
    private readonly Assembly ResourceAssembly = typeof(MockedHttpMessageHandler).Assembly;
    private readonly CookieContainer cookieContainer;

    public Queue<MockedHttpRequest> RequestContent { get; } = new();
    public Queue<HttpResponseMessage> Responses { get; } = new();

    public MockedHttpMessageHandler(CookieContainer cookieContainer)
    {
        this.cookieContainer = cookieContainer;
    }

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
#if NET6_0_OR_GREATER
        ArgumentNullException.ThrowIfNull(request);
#else
        if (request is null)
        {
            throw new ArgumentNullException(nameof(request));
        }
#endif

        cancellationToken.ThrowIfCancellationRequested();

        RequestContent.Enqueue(new MockedHttpRequest(request));

#if NET6_0_OR_GREATER
        if (Responses.TryDequeue(out var response))
        {
            if (response.Headers.TryGetValues("Set-Cookie", out var cookieValues))
            {
                cookieContainer.SetCookies(request.RequestUri!, string.Join(",", cookieValues));
            }
            return Task.FromResult(response);
        }
#else
        try
        {
            var response = Responses.Dequeue();
            if (response.Headers.TryGetValues("Set-Cookie", out var cookieValues))
            {
                cookieContainer.SetCookies(request.RequestUri!, string.Join(",", cookieValues));
            }
            return Task.FromResult(response);
        }
        catch (InvalidOperationException)
        {
            // There was no response. Fall through to the not found below.
        }
#endif

        return Task.FromResult(new HttpResponseMessage(HttpStatusCode.NotFound));
    }

    public async Task QueueResponsesAsync(string testName, bool prefixLoginResponse = true)
    {
        var manifestResourceNames = (prefixLoginResponse
                                        ? new[] { "Aydsko.iRacingData.UnitTests.Responses.SucessfulLogin.json" }
                                        : Array.Empty<string>()
                                    ).Concat(ResourceAssembly.GetManifestResourceNames()
                                                             .Where(mrn => mrn.StartsWith($"Aydsko.iRacingData.UnitTests.Responses.{testName}", StringComparison.InvariantCultureIgnoreCase)));

        foreach (var manifestName in manifestResourceNames)
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

#pragma warning disable CA2000 // Dispose objects before losing scope - These responses are intentionally created to be returned later. This is OK in a test helper.
            var responseMessage = new HttpResponseMessage(statusCode)
            {
                Content = new StringContent(responseDictionary["content"].ToString(), Encoding.UTF8, "text/json")
            };
#pragma warning restore CA2000 // Dispose objects before losing scope

            foreach (var header in responseDictionary["headers"].EnumerateObject()
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
                responseMessage.Headers.Add(header.Key, header.Value);
            }

            Responses.Enqueue(responseMessage);
        }
    }
}
