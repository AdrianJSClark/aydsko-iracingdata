// © 2022 Adrian Clark
// This file is licensed to you under the MIT license.

namespace Aydsko.iRacingData;

public static class LoggingExtensions
{
    private const int EventIdLoginSuccessful = 1;
    private const int EventIdRateLimitsUpdated = 2;
    private const int EventIdErrorResponseUnknownTrace = 3;
    private const int EventIdErrorResponseTrace = 4;

    private static readonly Action<ILogger, string, Exception?> loginSuccessful = LoggerMessage.Define<string>(LogLevel.Information,
                                                                                                               new EventId(EventIdLoginSuccessful, nameof(LoginSuccessful)),
                                                                                                               "Authenticated successfully as {UserEmail}");
    private static readonly Action<ILogger, int?, int?, DateTimeOffset?, Exception?> rateLimitsUpdated = LoggerMessage.Define<int?, int?, DateTimeOffset?>(LogLevel.Debug,
                                                                                                                                                           new EventId(EventIdRateLimitsUpdated, nameof(RateLimitsUpdated)),
                                                                                                                                                           "Currently have {RateLimitRemaining} calls left from {RateLimitTotal} resetting at {RateLimitResetInstant}");
    private static readonly Action<ILogger, Exception?> errorResponseUnknownTrace = LoggerMessage.Define(LogLevel.Trace,
                                                                                                         new EventId(EventIdErrorResponseUnknownTrace, nameof(ErrorResponseUnknown)),
                                                                                                         "Error response was unknown so throwing a standard HTTP error");
    private static readonly Action<ILogger, string?, Exception?> errorResponse = LoggerMessage.Define<string?>(LogLevel.Trace,
                                                                                                               new EventId(EventIdErrorResponseTrace, nameof(ErrorResponse)),
                                                                                                               "Error response from iRacing API: {ErrorDescription}");

    public static void LoginSuccessful(this ILogger logger, string userEmail)
    {
        loginSuccessful(logger, userEmail, null);
    }

    public static void RateLimitsUpdated(this ILogger logger, int? rateLimitRemaining, int? rateLimit, DateTimeOffset? rateLimitResetInstant)
    {
        rateLimitsUpdated(logger, rateLimitRemaining, rateLimit, rateLimitResetInstant, null);
    }

    public static void ErrorResponseUnknown(this ILogger logger)
    {
        errorResponseUnknownTrace(logger, null);
    }

    public static void ErrorResponse(this ILogger logger, string? errorDescription, Exception exception)
    {
        errorResponse(logger, errorDescription, exception);
    }
}
