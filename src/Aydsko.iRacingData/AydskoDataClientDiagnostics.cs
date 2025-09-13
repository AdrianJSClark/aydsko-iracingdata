using System.Diagnostics;
using System.Reflection;

namespace Aydsko.iRacingData;

internal static class AydskoDataClientDiagnostics
{
    internal static ActivitySource ActivitySource { get; } = new("Aydsko.iRacingData", typeof(DataClient).Assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion ?? "");
}
