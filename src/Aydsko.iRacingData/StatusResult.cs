// © 2023 Adrian Clark
// This file is licensed to you under the MIT license.

using Aydsko.iRacingData.Converters;

namespace Aydsko.iRacingData;

public class StatusResult
{
    [JsonPropertyName("rss_announcements")]
    public string RssAnnouncements { get; set; } = default!;

    [JsonPropertyName("timestamp"), JsonConverter(typeof(StatusTimeStampConverter))]
    public DateTimeOffset Timestamp { get; set; }

    [JsonPropertyName("tests")]
    public StatusResultTests Tests { get; set; } = default!;

    [JsonPropertyName("maint_messages")]
    public string[] MaintenanceMessages { get; set; } = Array.Empty<string>();
}

public class StatusResultTests
{
    [JsonPropertyName("Configuration Flags")]
    public ConfigurationFlags ConfigurationFlags { get; set; } = default!;

    [JsonPropertyName("Websites")]
    public StatusResultWebsites Websites { get; set; } = default!;

    [JsonPropertyName("Raceserver Network")]
    public RaceServerNetwork RaceServerNetwork { get; set; } = default!;

    [JsonPropertyName("Backend Services")]
    public BackendServices BackendServices { get; set; } = default!;
}

public class ConfigurationFlags
{
    [JsonPropertyName("iRacing UI in Maintenance Mode")]
    public ServiceStatusDetail iRacingUIinMaintenanceMode { get; set; } = default!;

    [JsonPropertyName("TestDrive Available")]
    public ServiceStatusDetail TestDriveAvailable { get; set; } = default!;

    [JsonPropertyName("Classic Member Site in Maintenance Mode")]
    public ServiceStatusDetail ClassicMemberSiteInMaintenanceMode { get; set; } = default!;
}

public class StatusResultWebsites
{
    [JsonPropertyName("Public Site (www.iracing.com)")]
    public ServiceStatusDetail PublicSiteWwwiRacingCom { get; set; } = default!;

    [JsonPropertyName("Forums")]
    public ServiceStatusDetail Forums { get; set; } = default!;

    [JsonPropertyName("Downloads")]
    public ServiceStatusDetail Downloads { get; set; } = default!;

    [JsonPropertyName("Legacy Forums")]
    public ServiceStatusDetail LegacyForums { get; set; } = default!;
}

public class RaceServerNetwork
{
    [JsonPropertyName("Raceserver Capacity - Boston")]
    public ServiceStatusDetail RaceServerCapacityBoston { get; set; } = default!;

    [JsonPropertyName("Raceserver Capacity - Frankfurt")]
    public ServiceStatusDetail RaceServerCapacityFrankfurt { get; set; } = default!;

    [JsonPropertyName("Raceserver Capacity - Sao Paulo")]
    public ServiceStatusDetail RaceServerCapacitySaoPaulo { get; set; } = default!;

    [JsonPropertyName("Raceserver Capacity - Sydney")]
    public ServiceStatusDetail RaceServerCapacitySydney { get; set; } = default!;

    [JsonPropertyName("Raceserver Capacity - Tokyo")]
    public ServiceStatusDetail RaceServerCapacityTokyo { get; set; } = default!;

    [JsonPropertyName("Raceserver Capacity - US West Coast")]
    public ServiceStatusDetail RaceServerCapacityUSWestCoast { get; set; } = default!;

    [JsonPropertyName("Raceserver Connectivity - Boston")]
    public ServiceStatusDetail RaceServerConnectivityBoston { get; set; } = default!;

    [JsonPropertyName("Raceserver Connectivity - Frankfurt")]
    public ServiceStatusDetail RaceServerConnectivityFrankfurt { get; set; } = default!;

    [JsonPropertyName("Raceserver Connectivity - Sao Paulo")]
    public ServiceStatusDetail RaceServerConnectivitySaoPaulo { get; set; } = default!;

    [JsonPropertyName("Raceserver Connectivity - Sydney")]
    public ServiceStatusDetail RaceServerConnectivitySydney { get; set; } = default!;

    [JsonPropertyName("Raceserver Connectivity - Tokyo")]
    public ServiceStatusDetail RaceServerConnectivityTokyo { get; set; } = default!;

    [JsonPropertyName("Raceserver Connectivity - US West Coast")]
    public ServiceStatusDetail RaceServerConnectivityUSWestCoast { get; set; } = default!;

    [JsonPropertyName("Session Scheduler Latency")]
    public ServiceStatusDetail SessionSchedulerLatency { get; set; } = default!;

    [JsonPropertyName("Connection Status Between AU/NZ and Boston")]
    public ServiceStatusDetail ConnectionStatusBetweenAUNZandBoston { get; set; } = default!;
}

public class BackendServices
{
    [JsonPropertyName("UI Backend Services")]
    public ServiceStatusDetail UIBackendServices { get; set; } = default!;

    [JsonPropertyName("Login and Authentication")]
    public ServiceStatusDetail LoginAndAuthentication { get; set; } = default!;

    [JsonPropertyName("Race Results Processing")]
    public ServiceStatusDetail RaceResultsProcessing { get; set; } = default!;

    [JsonPropertyName("Stats API")]
    public ServiceStatusDetail StatsAPI { get; set; } = default!;
}

public class ServiceStatusDetail
{
    [JsonPropertyName("description")]
    public string Description { get; set; } = default!;

    [JsonPropertyName("result"), JsonConverter(typeof(ServiceStatusHistoryItemArrayConverter))]
    public ServiceStatusHistoryItem[] Result { get; set; } = Array.Empty<ServiceStatusHistoryItem>();

    [JsonPropertyName("summary_label")]
    public string SummaryLabel { get; set; } = default!;

    [JsonPropertyName("summary_level")]
    public int SummaryLevel { get; set; }
}

public class ServiceStatusHistoryItem : IEquatable<ServiceStatusHistoryItem>
{
    public DateTimeOffset Instant { get; set; }
    public decimal Value { get; set; }

    public bool Equals(ServiceStatusHistoryItem? other)
    {
        if (other == null)
        {
            return false;
        }

        return (other.Instant == Instant && other.Value == Value);
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as ServiceStatusHistoryItem);
    }

    public override int GetHashCode()
    {
#if NET6_0_OR_GREATER
        return HashCode.Combine(Instant.GetHashCode(), Value.GetHashCode());
#else
        return Tuple.Create(Instant, Value).GetHashCode();
#endif
    }
}

[JsonSerializable(typeof(StatusResult)), JsonSourceGenerationOptions(WriteIndented = true)]
internal partial class StatusResultContext : JsonSerializerContext
{ }
