// © 2023 Adrian Clark
// This file is licensed to you under the MIT license.

namespace Aydsko.iRacingData.UnitTests;

public class MockedHttpRequest
{
    public KeyValuePair<string, IEnumerable<string>>[] Headers { get; private set; }
    public Stream ContentStream { get; private set; }

    public MockedHttpRequest(HttpRequestMessage request)
    {
        Headers = request.Headers.ToArray();
        if (request.Content != null)
        {
            var contentStream = new MemoryStream();
            request.Content.CopyToAsync(contentStream).GetAwaiter().GetResult();
            contentStream.Position = 0;
            ContentStream = contentStream;
        }
        else
        {
            ContentStream = Stream.Null;
        }
    }
}
