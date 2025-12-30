// © Adrian Clark - Aydsko.iRacingData
// This file is licensed to you under the MIT license.

using Aydsko.iRacingData.Member;
using Aydsko.iRacingData.Results;

namespace Aydsko.iRacingData.Series;

public class RegisteredDriversList
{
    [JsonPropertyName("subscribed")]
    public bool IsSubscribed { get; set; }

    [JsonPropertyName("subsession_id")]
    public int SubsessionId { get; set; }

    [JsonPropertyName("entries")]
    public Entry[] Entries { get; set; } = [];

    [JsonPropertyName("success")]
    public bool Success { get; set; }
}

public class Entry
{
    /// <summary>Unique identifier for the member.</summary>
    [JsonPropertyName("cust_id")]
    public int CustomerId { get; set; }

    /// <summary>The display value used to represent the member on the service.</summary>
    [JsonPropertyName("display_name")]
    public string DisplayName { get; set; } = default!;

    [JsonPropertyName("car_id")]
    public int CarId { get; set; }

    [JsonPropertyName("car_name")]
    public string CarName { get; set; } = default!;

    [JsonPropertyName("car_class_id")]
    public int CarClassId { get; set; }

    [JsonPropertyName("car_class_name")]
    public string CarClassName { get; set; } = default!;

    [JsonPropertyName("helmet")]
    public Helmet Helmet { get; set; } = default!;

    [JsonPropertyName("crew_allowed")]
    public int CrewAllowed { get; set; }

    [JsonPropertyName("crew_password_protected")]
    public bool CrewPasswordProtected { get; set; }

    [JsonPropertyName("reg_status")]
    public string RegistrationStatus { get; set; } = default!;

    [JsonPropertyName("trusted_spotter")]
    public bool TrustedSpotter { get; set; }

    [JsonPropertyName("livery")]
    public Livery Livery { get; set; } = default!;

    [JsonPropertyName("license")]
    public LicenseInfo License { get; set; } = default!;

    [JsonPropertyName("session_id")]
    public int SessionId { get; set; }

    [JsonPropertyName("subsession_id")]
    public int SubsessionId { get; set; }

    [JsonPropertyName("event_type")]
    public int EventType { get; set; }

    [JsonPropertyName("event_type_name")]
    public string EventTypeName { get; set; } = default!;

    [JsonPropertyName("license_order")]
    public int LicenseOrder { get; set; }

    [JsonPropertyName("flair_id")]
    public int FlairId { get; set; }

    [JsonPropertyName("flair_name")]
    public string FlairName { get; set; } = default!;

    [JsonPropertyName("flair_shortname")]
    public string FlairShortname { get; set; } = default!;

    [JsonPropertyName("country_code")]
    public string CountryCode { get; set; } = default!;

    [JsonPropertyName("farm_display_name")]
    public string FarmDisplayName { get; set; } = default!;

    [JsonPropertyName("elig")]
    public Eligibility Eligibility { get; set; } = default!;
}

public class Eligibility
{
    [JsonPropertyName("session_full")]
    public bool SessionFull { get; set; }

    [JsonPropertyName("can_spot")]
    public bool CanSpot { get; set; }

    [JsonPropertyName("can_watch")]
    public bool CanWatch { get; set; }

    [JsonPropertyName("can_drive")]
    public bool CanDrive { get; set; }

    [JsonPropertyName("trusted_spotter")]
    public bool IsTrustedSpotter { get; set; }

    [JsonPropertyName("has_sess_password")]
    public bool HasSessionPassword { get; set; }

    [JsonPropertyName("has_crew_password")]
    public bool HasCrewPassword { get; set; }

    [JsonPropertyName("needs_purchase")]
    public bool NeedsPurchase { get; set; }

    [JsonPropertyName("purchase_skus")]
    public int[] PurchaseSkus { get; set; } = [];

    [JsonPropertyName("registered")]
    public bool Registered { get; set; }
}

[JsonSerializable(typeof(RegisteredDriversList)), JsonSourceGenerationOptions(WriteIndented = true)]
internal partial class RegisteredDriversListContext : JsonSerializerContext
{ }
