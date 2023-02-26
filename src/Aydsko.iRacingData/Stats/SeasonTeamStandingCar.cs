// © 2023 Adrian Clark
// This file is licensed to you under the MIT license.

namespace Aydsko.iRacingData.Stats;

public class SeasonTeamStandingCar
{
    [JsonPropertyName("carclassid")]
    public int Carclassid { get; set; }

    [JsonPropertyName("carid")]
    public int Carid { get; set; }
}
