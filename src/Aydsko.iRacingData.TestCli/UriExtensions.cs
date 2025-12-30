using System.Text;

namespace Aydsko.iRacingData.TestCli;

internal static class UriExtensions
{
    extension(Uri url)
    {
        internal Uri ToUrlWithQuery(params IEnumerable<KeyValuePair<string, string>> parameters)
        {
            var builder = new UriBuilder(url);

            var queryBuilder = new StringBuilder();
            _ = queryBuilder.Append(builder.Query.TrimStart('?'));

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
}
