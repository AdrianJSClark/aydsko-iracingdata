// © 2022 Adrian Clark
// This file is licensed to you under the MIT license.

using System.Text.Json.Serialization;

namespace Aydsko.iRacingData.Leagues;

public class Owner
{
    [JsonPropertyName("cust_id")]
    public int CustId { get; set; }
    [JsonPropertyName("display_name")]
    public string DisplayName { get; set; } = null!;
    [JsonPropertyName("helmet")]
    public Helmet Helmet { get; set; } = null!;
    [JsonPropertyName("car_number")]
    public string? CarNumber { get; set; }
    [JsonPropertyName("nick_name")]
    public string? NickName { get; set; }
}
