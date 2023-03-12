// © 2023 Adrian Clark
// This file is licensed to you under the MIT license.

using System.Globalization;
using System.Linq.Expressions;
using System.Reflection;

#if NET6_0_OR_GREATER
using System.Reflection.Metadata;
#else
using System.Collections;
using System.Net;
#endif

namespace Aydsko.iRacingData;

static internal class Extensions
{
    static internal void AddParameterIfNotNull<T>(this IDictionary<string, string> parameters, string parameterName, T parameterValue)
    {
#if (NET6_0_OR_GREATER)
        ArgumentNullException.ThrowIfNull(parameterName);
#else
        if (parameterName is null)
        {
            throw new ArgumentNullException(nameof(parameterName));
        }
#endif  
        if (string.IsNullOrWhiteSpace(parameterName))
        {
            throw new ArgumentException("Parameter name cannot be whitespace.", nameof(parameterName));
        }

        var parameterStringValue = GetParameterAsStringValue(parameterValue);
        parameters.Add(parameterName, parameterStringValue ?? string.Empty);
    }

    /// <summary>Add the parameter with the property's <see cref="JsonPropertyNameAttribute.Name"/> as the key and it's value if that value is not null.</summary>
    /// <typeparam name="T">Type of the property.</typeparam>
    /// <param name="parameters">Collection of parameters to add to.</param>
    /// <param name="parameter">An expression which accesses the property.</param>
    /// <exception cref="ArgumentException">The expression couldn't be properly understood by the method.</exception>
    static internal void AddParameterIfNotNull<T>(this IDictionary<string, string> parameters, Expression<Func<T>> parameter)
    {
#if (NET6_0_OR_GREATER)
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

        var parameterStringValue = GetParameterAsStringValue(parameterValue);

        parameters.Add(new(parameterName, parameterStringValue ?? string.Empty));
    }

    private static string? GetParameterAsStringValue<T>(T parameterValue)
    {
#pragma warning disable CA1308 // Normalize strings to uppercase
        return parameterValue switch
        {
            string stringParam => stringParam,
            DateTime dateTimeParam => dateTimeParam.ToString("yyyy-MM-dd\\THH:mm\\Z", CultureInfo.InvariantCulture),
            Array arrayParam => string.Join(",", GetNonNullValues(arrayParam)),
            IEnumerable<string> enumerableOfString => string.Join(",", enumerableOfString),
            bool boolParam => boolParam.ToString().ToLowerInvariant(),
            _ => Convert.ToString(parameterValue, CultureInfo.InvariantCulture)
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

#if (NET6_0_OR_GREATER == false)
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Globalization", "CA1304:Specify CultureInfo", Justification = "<Pending>")]
    static internal CookieCollection GetAllCookies(this CookieContainer container)
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
            var items = item.GetType().InvokeMember("m_list",
                                                    BindingFlags.NonPublic | BindingFlags.GetField | BindingFlags.Instance,
                                                    null,
                                                    item,
                                                    null) as SortedList;

            if (items is null)
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
