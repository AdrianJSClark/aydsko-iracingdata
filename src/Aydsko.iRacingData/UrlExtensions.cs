// © 2023 Adrian Clark
// This file is licensed to you under the MIT license.

using System.Text;

namespace Aydsko.iRacingData;

internal static class UrlExtensions
{
    public static Uri ToUrlWithQuery(this string url, IEnumerable<KeyValuePair<string, string>> parameters)
    {
        var builder = new UriBuilder(url);

        var queryBuilder = new StringBuilder();
        queryBuilder.Append(builder.Query.TrimStart('?'));

        foreach (var parameter in parameters)
        {
            if (queryBuilder.Length > 0)
            {
                _ = queryBuilder.Append('&');
            }

            _ = queryBuilder.Append(Uri.EscapeDataString(parameter.Key));
            _ = queryBuilder.Append('=');
            _ = queryBuilder.Append(Uri.EscapeDataString(parameter.Value));
        }

        builder.Query = queryBuilder.ToString();

        return builder.Uri;
    }
}
