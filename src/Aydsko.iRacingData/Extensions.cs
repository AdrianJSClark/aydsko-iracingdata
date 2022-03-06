// © 2022 Adrian Clark
// This file is licensed to you under the MIT license.

using System.Collections;
using System.Net;
using System.Reflection;

namespace Aydsko.iRacingData;

static internal class Extensions
{
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
