namespace Aydsko.iRacingData.Cars;

public class CarRule
{
    [JsonPropertyName("rule_category")]
    public string RuleCategory { get; set; } = default!;
    
    [JsonPropertyName("text")]
    public string Text { get; set; } = default!;
}
