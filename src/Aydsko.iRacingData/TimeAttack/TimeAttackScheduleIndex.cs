// © 2023 Adrian Clark
// This file is licensed to you under the MIT license.

namespace Aydsko.iRacingData.TimeAttack;

public class TimeAttackScheduleIndex
{
    [JsonPropertyName("ta_schedule_filename")]
    public string ScheduleFilename { get; set; } = default!;
}

[JsonSerializable(typeof(TimeAttackScheduleIndex)), JsonSourceGenerationOptions(WriteIndented = true)]
internal partial class TimeAttackScheduleIndexContext : JsonSerializerContext
{ }
