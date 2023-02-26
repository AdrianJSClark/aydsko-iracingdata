// © 2023 Adrian Clark
// This file is licensed to you under the MIT license.

using System.Collections;
using System.Globalization;
using System.Linq.Expressions;
using System.Net;
using System.Reflection;

namespace Aydsko.iRacingData;

static internal class Extensions
{
    /// <summary>Add the parameter with the property's <see cref="JsonPropertyNameAttribute.Name"/> as the key and it's value if that value is not null.</summary>
    /// <typeparam name="T">Type of the property.</typeparam>
    /// <param name="parameters">Collection of parameters to add to.</param>
    /// <param name="param">An expression which accesses the property.</param>
    /// <exception cref="ArgumentException">The expresssion couldn't be properly understood by the method.</exception>
    static internal void AddParameterIfNotNull<T>(this IDictionary<string, string> parameters, Expression<Func<T>> param)
    {
#if (NET6_0_OR_GREATER)
        ArgumentNullException.ThrowIfNull(param);
#else
        if (param is null)
        {
            throw new ArgumentNullException(nameof(param));
        }
#endif

        var paramMemberExp = (param.Body is UnaryExpression unaryExp)
                                 ? unaryExp.Operand as MemberExpression
                                 : param.Body as MemberExpression;

        if (paramMemberExp is null || paramMemberExp.Member is not PropertyInfo memberPropertyInfo)
        {
            throw new ArgumentException("Couldn't understand the expresssion.", nameof(param));
        }

        var parameterValue = param.Compile()();

        if (parameterValue is null)
        {
            return;
        }

#pragma warning disable CA1308 // Normalize strings to uppercase
        var parameterStringValue = parameterValue switch
        {
            string stringParam => stringParam,
            DateTime dateTimeParam => dateTimeParam.ToString("yyyy-MM-dd\\THH:mm\\Z", CultureInfo.InvariantCulture),
            Array arrayParam => string.Join(",", GetNonNullValues(arrayParam)),
            IEnumerable<string> enumerableOfString => string.Join(",", enumerableOfString),
            bool boolParam => boolParam.ToString().ToLowerInvariant(),
            _ => Convert.ToString(parameterValue, CultureInfo.InvariantCulture)
        };
#pragma warning restore CA1308 // Normalize strings to uppercase

        var propNameAttr = paramMemberExp.Member.GetCustomAttributes<JsonPropertyNameAttribute>().FirstOrDefault();
        var parameterName = propNameAttr?.Name ?? paramMemberExp.Member.Name;

        parameters.Add(new(parameterName, parameterStringValue ?? string.Empty));

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
