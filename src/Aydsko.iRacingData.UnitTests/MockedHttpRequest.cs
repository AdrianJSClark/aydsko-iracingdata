// © 2023 Adrian Clark
// This file is licensed to you under the MIT license.

namespace Aydsko.iRacingData.UnitTests;

public class MockedHttpRequest
{
#pragma warning disable CA1819 // Properties should not return arrays - For a test project helper this is fine.
    public KeyValuePair<string, IEnumerable<string>>[] Headers { get; private set; }
#pragma warning restore CA1819 // Properties should not return arrays

    public Stream ContentStream { get; private set; }

    public MockedHttpRequest(HttpRequestMessage request)
    {
        if (request is null)
        {
            throw new ArgumentNullException(nameof(request));
        }

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
