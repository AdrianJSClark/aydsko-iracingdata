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
    public Tests Tests { get; set; } = default!;

    [JsonPropertyName("maint_messages")]
    public string[] MaintenanceMessages { get; set; } = Array.Empty<string>();
}

public class Tests
{
    [JsonPropertyName("ConfigurationFlags")]
    public ConfigurationFlags ConfigurationFlags { get; set; } = default!;

    [JsonPropertyName("Websites")]
    public Websites Websites { get; set; } = default!;

    [JsonPropertyName("RaceserverNetwork")]
    public RaceserverNetwork RaceserverNetwork { get; set; } = default!;

    [JsonPropertyName("BackendServices")]
    public BackendServices BackendServices { get; set; } = default!;
}

public class ConfigurationFlags
{
    [JsonPropertyName("iRacingUIinMaintenanceMode")]
    public ServiceStatusDetail iRacingUIinMaintenanceMode { get; set; } = default!;

    [JsonPropertyName("TestDriveAvailable")]
    public ServiceStatusDetail TestDriveAvailable { get; set; } = default!;

    [JsonPropertyName("ClassicMemberSiteinMaintenanceMode")]
    public ServiceStatusDetail ClassicMemberSiteInMaintenanceMode { get; set; } = default!;
}

public class Websites
{
    [JsonPropertyName("PublicSitewwwiracingcom")]
    public ServiceStatusDetail PublicSiteWwwiRacingCom { get; set; } = default!;

    [JsonPropertyName("Forums")]
    public ServiceStatusDetail Forums { get; set; } = default!;

    [JsonPropertyName("Downloads")]
    public ServiceStatusDetail Downloads { get; set; } = default!;

    [JsonPropertyName("LegacyForums")]
    public ServiceStatusDetail LegacyForums { get; set; } = default!;
}

public class RaceserverNetwork
{
    [JsonPropertyName("RaceserverCapacityBoston")]
    public ServiceStatusDetail RaceServerCapacityBoston { get; set; } = default!;

    [JsonPropertyName("RaceserverCapacityFrankfurt")]
    public ServiceStatusDetail RaceServerCapacityFrankfurt { get; set; } = default!;

    [JsonPropertyName("RaceserverCapacitySaoPaulo")]
    public ServiceStatusDetail RaceServerCapacitySaoPaulo { get; set; } = default!;

    [JsonPropertyName("RaceserverCapacitySydney")]
    public ServiceStatusDetail RaceServerCapacitySydney { get; set; } = default!;

    [JsonPropertyName("RaceserverCapacityTokyo")]
    public ServiceStatusDetail RaceServerCapacityTokyo { get; set; } = default!;

    [JsonPropertyName("RaceserverCapacityUSWestCoast")]
    public ServiceStatusDetail RaceServerCapacityUSWestCoast { get; set; } = default!;

    [JsonPropertyName("RaceserverConnectivityBoston")]
    public ServiceStatusDetail RaceServerConnectivityBoston { get; set; } = default!;

    [JsonPropertyName("RaceserverConnectivityFrankfurt")]
    public ServiceStatusDetail RaceServerConnectivityFrankfurt { get; set; } = default!;

    [JsonPropertyName("RaceserverConnectivitySaoPaulo")]
    public ServiceStatusDetail RaceServerConnectivitySaoPaulo { get; set; } = default!;

    [JsonPropertyName("RaceserverConnectivitySydney")]
    public ServiceStatusDetail RaceServerConnectivitySydney { get; set; } = default!;

    [JsonPropertyName("RaceserverConnectivityTokyo")]
    public ServiceStatusDetail RaceServerConnectivityTokyo { get; set; } = default!;

    [JsonPropertyName("RaceserverConnectivityUSWestCoast")]
    public ServiceStatusDetail RaceServerConnectivityUSWestCoast { get; set; } = default!;

    [JsonPropertyName("SessionSchedulerLatency")]
    public ServiceStatusDetail SessionSchedulerLatency { get; set; } = default!;

    [JsonPropertyName("ConnectionStatusBetweenAUNZandBoston")]
    public ServiceStatusDetail ConnectionStatusBetweenAUNZandBoston { get; set; } = default!;
}

public class BackendServices
{
    [JsonPropertyName("UIBackendServices")]
    public ServiceStatusDetail UIBackendServices { get; set; } = default!;

    [JsonPropertyName("LoginandAuthentication")]
    public ServiceStatusDetail LoginandAuthentication { get; set; } = default!;

    [JsonPropertyName("RaceResultsProcessing")]
    public ServiceStatusDetail RaceResultsProcessing { get; set; } = default!;

    [JsonPropertyName("StatsAPI")]
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
        return Instant.GetHashCode() ^ Value.GetHashCode();
#endif
    }
}

[JsonSerializable(typeof(StatusResult)), JsonSourceGenerationOptions(WriteIndented = true)]
internal partial class StatusResultContext : JsonSerializerContext
{ }
