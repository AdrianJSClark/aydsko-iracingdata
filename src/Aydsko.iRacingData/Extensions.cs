// © 2023 Adrian Clark
// This file is licensed to you under the MIT license.

using System.Globalization;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

#if NET6_0_OR_GREATER
//using System.Reflection.Metadata;
#else
using System.Collections;
using System.Net;
#endif

namespace Aydsko.iRacingData;

internal static class Extensions
{
    /// <summary>Add the parameter with the property's <see cref="JsonPropertyNameAttribute.Name"/> as the key and it's value if that value is not null.</summary>
    /// <typeparam name="T">Type of the property.</typeparam>
    /// <param name="parameters">Collection of parameters to add to.</param>
    /// <param name="parameter">An expression which accesses the property.</param>
    /// <exception cref="ArgumentException">The expression couldn't be properly understood by the method.</exception>
    internal static void AddParameterIfNotNull<T>(this IDictionary<string, object?> parameters, Expression<Func<T>> parameter)
    {
#if NET6_0_OR_GREATER
        ArgumentNullException.ThrowIfNull(parameter);
#else
        if (parameter is null)
        {
            throw new ArgumentNullException(nameof(parameter));
        }
#endif

        var parameterMemberExp = (parameter.Body is UnaryExpression unaryExp)
                                     ? unaryExp.Operand as MemberExpression
                                     : parameter.Body as MemberExpression;

        if (parameterMemberExp is null || parameterMemberExp.Member is not PropertyInfo memberPropertyInfo)
        {
            throw new ArgumentException("Couldn't understand the expression.", nameof(parameter));
        }

        var parameterValue = parameter.Compile()();

        if (parameterValue is null)
        {
            return;
        }

        var propNameAttribute = parameterMemberExp.Member.GetCustomAttributes<JsonPropertyNameAttribute>().FirstOrDefault();
        var parameterName = propNameAttribute?.Name ?? parameterMemberExp.Member.Name;

        parameters.Add(new(parameterName, parameterValue));
    }

    internal static Uri ToUrlWithQuery(this string url, IEnumerable<KeyValuePair<string, object?>> parameters)
    {
        var builder = new UriBuilder(url);

        var queryBuilder = new StringBuilder();
        _ = queryBuilder.Append(builder.Query.TrimStart('?'));

        foreach (var parameter in parameters)
        {
            var values = GetParameterAsStringValues(parameter.Value);
            if (values is { Length: 0 })
            {
                continue; // Don't add anything if there aren't any values to include.
            }

            if (queryBuilder.Length > 0)
            {
                _ = queryBuilder.Append('&');
            }

            _ = queryBuilder.Append(Uri.EscapeDataString(parameter.Key));
            _ = queryBuilder.Append('=');

            if (values is { Length: 1 })
            {
                _ = queryBuilder.Append(Uri.EscapeDataString(values[0]));
            }
            else
            {
                for (var i = 0; i < values.Length; i++)
                {
                    if (i > 0)
                    {
                        _ = queryBuilder.Append(',');
                    }
                    _ = queryBuilder.Append(Uri.EscapeDataString(values[i]));
                }
            }
        }

        builder.Query = queryBuilder.ToString();

        return builder.Uri;
    }

    private static string[] GetParameterAsStringValues<T>(T parameterValue)
    {
#pragma warning disable CA1308 // Normalize strings to uppercase
        switch (parameterValue)
        {
            case string stringParam:
                return new[] { stringParam };

            case DateTime dateTimeParam:
                return new[] { dateTimeParam.ToString("yyyy-MM-dd\\THH:mm\\Z", CultureInfo.InvariantCulture) };

            case Array arrayParam:
                return GetNonNullValues(arrayParam).ToArray();

            case IEnumerable<string> enumerableOfString:
                return enumerableOfString.ToArray();

            case bool boolParam:
                return new[] { boolParam.ToString().ToLowerInvariant() };

            case Enum @enum:
                return new[] { @enum.ToString("D") };

            default:
                if (Convert.ToString(parameterValue, CultureInfo.InvariantCulture) is string parameterStringValue)
                {
                    return new[] { parameterStringValue };
                }
                else
                {
                    return Array.Empty<string>();
                }
        };
#pragma warning restore CA1308 // Normalize strings to uppercase

        static IEnumerable<string> GetNonNullValues(Array array)
        {
            for (var i = 0; i < array.Length; i++)
            {
                var value = array.GetValue(i);
                if (value is null)
                {
                    continue;
                }

                yield return Convert.ToString(value, CultureInfo.InvariantCulture)!;
            }
        }
    }

#if NET6_0_OR_GREATER == false
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Globalization", "CA1304:Specify CultureInfo", Justification = "<Pending>")]
    internal static CookieCollection GetAllCookies(this CookieContainer container)
    {
        var result = new CookieCollection();

        var table = (Hashtable)container.GetType().InvokeMember("m_domainTable",
                                                                BindingFlags.NonPublic | BindingFlags.GetField | BindingFlags.Instance,
                                                                null,
                                                                container,
                                                                null);

        foreach (string key in table.Keys)
        {
            var item = table[key];

            if (item.GetType().InvokeMember("m_list",
                                                    BindingFlags.NonPublic | BindingFlags.GetField | BindingFlags.Instance,
                                                    null,
                                                    item,
                                                    null) is not SortedList items)
            {
                break;
            }

            foreach (DictionaryEntry entry in items)
            {
                if (entry.Value is not CookieCollection cookieCollection)
                {
                    continue;
                }

                result.Add(cookieCollection);
            }
        }

        return result;
    }
#endif
}
