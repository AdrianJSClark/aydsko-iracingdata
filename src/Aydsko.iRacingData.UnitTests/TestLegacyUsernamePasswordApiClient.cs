using System.Net;
using System.Net.Http;
using Microsoft.Extensions.Logging;

namespace Aydsko.iRacingData.UnitTests;

/// <summary>Wraps the real <see cref="LegacyUsernamePasswordApiClient"/> to give test access to internal members.</summary>
internal sealed class TestLegacyUsernamePasswordApiClient(HttpClient httpClient,
                                                   iRacingDataClientOptions options,
                                                   CookieContainer cookieContainer,
                                                   ILogger<LegacyUsernamePasswordApiClient> logger)
    : LegacyUsernamePasswordApiClient(httpClient, options, cookieContainer, logger)
{
    public bool IsLoggedIn => isLoggedIn;
}
