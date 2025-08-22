namespace Aydsko.iRacingData;

public interface IAuthenticatingHttpClient
{
    Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
                                        HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead,
                                        CancellationToken cancellationToken = default);
    Task<HttpResponseMessage> SendAuthenticatedRequestAsync(HttpRequestMessage request,
                                                            HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead,
                                                            CancellationToken cancellationToken = default);
    void ClearLoggedInState();
}
