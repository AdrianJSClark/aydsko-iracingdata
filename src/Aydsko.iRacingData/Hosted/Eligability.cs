// © 2023 Adrian Clark
// This file is licensed to you under the MIT license.

namespace Aydsko.iRacingData.Hosted;

public class Eligability
{
    [JsonPropertyName("session_full")]
    public bool SessionFull { get; set; }

    [JsonPropertyName("can_spot")]
    public bool CanSpot { get; set; }

    [JsonPropertyName("can_watch")]
    public bool CanWatch { get; set; }

    [JsonPropertyName("can_drive")]
    public bool CanDrive { get; set; }

    [JsonPropertyName("has_sess_password")]
    public bool HasSessionPassword { get; set; }

    [JsonPropertyName("needs_purchase")]
    public bool NeedsPurchase { get; set; }

    [JsonPropertyName("own_car")]
    public bool OwnCar { get; set; }

    [JsonPropertyName("own_track")]
    public bool OwnTrack { get; set; }

    [JsonPropertyName("purchase_skus")]
    public int[] PurchaseSkus { get; set; } = Array.Empty<int>();

    [JsonPropertyName("registered")]
    public bool Registered { get; set; }
}
