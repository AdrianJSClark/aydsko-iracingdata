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

    public List<byte[]> Requests { get; } = [];
    public Stack<HttpResponseMessage> Responses { get; } = [];

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        if (Responses.Count == 0)
        {
            throw new InvalidOperationException("Must configure at least one response before using.");
        }

#pragma warning disable CA2016 // Forward the 'CancellationToken' parameter to methods
        if (request.Content is HttpContent content)
        {
            Requests.Add(await content.ReadAsByteArrayAsync().ConfigureAwait(false));
        }
#pragma warning restore CA2016 // Forward the 'CancellationToken' parameter to methods

        return Responses.Pop();
    }

    public void AddJsonResponse(HttpStatusCode statusCode, string content)
    {
#pragma warning disable CA2000 // Dispose objects before losing scope
        Responses.Push(new HttpResponseMessage(statusCode)
        {
            Content = new StringContent(content, Encoding.UTF8, "text/json")
        });
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
