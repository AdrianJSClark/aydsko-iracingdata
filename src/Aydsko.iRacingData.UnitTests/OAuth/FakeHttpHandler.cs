using System.Net;
using System.Text;

#if NETFRAMEWORK
using System.Net.Http;
#endif

namespace Aydsko.iRacingData.UnitTests.OAuth;

internal sealed class FakeHttpHandler
    : HttpMessageHandler
{
    private HttpClient? httpClient;

    public List<byte[]> RequestContentBytes { get; } = [];
    private readonly Queue<(Uri RequestUri, HttpResponseMessage Response, Func<HttpRequestMessage, Task<bool>>? RequestFilter)> expectedResponses = new();

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        if (expectedResponses.Count == 0)
        {
            throw new InvalidOperationException("Must configure at least one response before using.");
        }

        var (expectedUri, response, requestFilter) = expectedResponses.Dequeue();

        if (request.RequestUri != expectedUri
)
        {
#pragma warning disable CA2201 // Do not raise reserved exception types
            throw new Exception($"Expected request for \"{expectedUri}\" but received request for \"{request.RequestUri}\".");
#pragma warning restore CA2201 // Do not raise reserved exception types
        }

        if ((requestFilter != null && !await requestFilter(request).ConfigureAwait(false)))
        {
#pragma warning disable CA2201 // Do not raise reserved exception types
            throw new Exception($"Expected request to match the given filter but it did not.");
#pragma warning restore CA2201 // Do not raise reserved exception types
        }


#pragma warning disable CA2016 // Forward the 'CancellationToken' parameter to methods
        if (request.Content is HttpContent content)
        {
            RequestContentBytes.Add(await content.ReadAsByteArrayAsync().ConfigureAwait(false));
        }
#pragma warning restore CA2016 // Forward the 'CancellationToken' parameter to methods

        return response;
    }

    public void AddJsonResponse(Uri expectedRequestUri, HttpStatusCode statusCode, string content, Func<HttpRequestMessage, Task<bool>>? requestFilter = null)
    {
#pragma warning disable CA2000 // Dispose objects before losing scope
        var response = new HttpResponseMessage(statusCode)
        {
            Content = new StringContent(content, Encoding.UTF8, "text/json")
        };
        expectedResponses.Enqueue((expectedRequestUri, response, requestFilter));
#pragma warning restore CA2000 // Dispose objects before losing scope
    }

    public HttpClient GetClient()
    {
        httpClient ??= new HttpClient(this);
        return httpClient;
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            httpClient?.Dispose();
            httpClient = null;
        }
        base.Dispose(disposing);
    }
}
