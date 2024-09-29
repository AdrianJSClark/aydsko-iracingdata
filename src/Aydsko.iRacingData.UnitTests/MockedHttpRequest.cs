// © 2023 Adrian Clark
// This file is licensed to you under the MIT license.

namespace Aydsko.iRacingData.UnitTests;

internal sealed class MockedHttpRequest
{
#pragma warning disable CA1819 // Properties should not return arrays - For a test project helper this is fine.
    public KeyValuePair<string, IEnumerable<string>>[] Headers { get; private set; }
#pragma warning restore CA1819 // Properties should not return arrays

    public Stream ContentStream { get; private set; }

    public MockedHttpRequest(System.Net.Http.HttpRequestMessage request)
    {
#if NET6_0_OR_GREATER
        ArgumentNullException.ThrowIfNull(request);
#else
        if (request is null)
        {
            throw new ArgumentNullException(nameof(request));
        }
#endif

        Headers = request.Headers.ToArray();
        if (request.Content != null)
        {
            var contentStream = new MemoryStream();
#pragma warning disable VSTHRD002 // Avoid problematic synchronous waits - Low risk, used in testing only.
            request.Content.CopyToAsync(contentStream).GetAwaiter().GetResult();
#pragma warning restore VSTHRD002 // Avoid problematic synchronous waits
            contentStream.Position = 0;
            ContentStream = contentStream;
        }
        else
        {
            ContentStream = Stream.Null;
        }
    }
}
