namespace Aydsko.iRacingData.Member;

public class MemberAward
{
    [JsonPropertyName("member_award_id")]
    public int MemberAwardId { get; set; }

    [JsonPropertyName("cust_id")]
    public int CustId { get; set; }

    [JsonPropertyName("award_id")]
    public int AwardId { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; }

    [JsonPropertyName("pdf_url")]
    public string PdfUrl { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("group_name")]
    public string GroupName { get; set; }

    [JsonPropertyName("icon_url_small")]
    public string IconUrlSmall { get; set; }

    [JsonPropertyName("icon_url_large")]
    public string IconUrlLarge { get; set; }

    [JsonPropertyName("icon_url_unawarded")]
    public string IconUrlUnawarded { get; set; }

    [JsonPropertyName("icon_background_color")]
    public string IconBackgroundColor { get; set; }

    [JsonPropertyName("certificate_file_name")]
    public string CertificateFileName { get; set; }

    [JsonPropertyName("weight")]
    public int Weight { get; set; }

    [JsonPropertyName("award_count")]
    public int AwardCount { get; set; }

    [JsonPropertyName("award_order")]
    public int AwardOrder { get; set; }

    [JsonPropertyName("achievement")]
    public bool Achievement { get; set; }

    [JsonPropertyName("award_date")]
    public string AwardDate { get; set; }

    [JsonPropertyName("display_date")]
    public string DisplayDate { get; set; }

    [JsonPropertyName("subsession_id")]
    public int SubsessionId { get; set; }

    [JsonPropertyName("awarded_description")]
    public string AwardedDescription { get; set; }

    [JsonPropertyName("viewed")]
    public bool Viewed { get; set; }

    [JsonPropertyName("threshold")]
    public int Threshold { get; set; }

    [JsonPropertyName("progress")]
    public int Progress { get; set; }

    [JsonPropertyName("progress_label")]
    public string ProgressLabel { get; set; }

    [JsonPropertyName("progress_text")]
    public string ProgressText { get; set; }

    [JsonPropertyName("progress_text_label")]
    public string ProgressTextLabel { get; set; }
}

[JsonSerializable(typeof(MemberAward[])), JsonSourceGenerationOptions(WriteIndented = true)]
internal partial class MemberAwardArrayContext : JsonSerializerContext
{ }
