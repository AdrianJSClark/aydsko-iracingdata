// © 2023-2025 Adrian Clark
// This file is licensed to you under the MIT license.

using Aydsko.iRacingData.Converters;

namespace Aydsko.iRacingData.Member;

public class MemberAward
{
    [JsonPropertyName("member_award_id")]
    public int MemberAwardId { get; set; }

    [JsonPropertyName("cust_id")]
    public int CustomerId { get; set; }

    [JsonPropertyName("award_id")]
    public int AwardId { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; } = default!;

    [JsonPropertyName("pdf_url")]
    public string PdfUrl { get; set; } = default!;

    [JsonPropertyName("name")]
    public string Name { get; set; } = default!;

    [JsonPropertyName("group_name")]
    public string GroupName { get; set; } = default!;

    [JsonPropertyName("icon_url_small")]
    public string IconUrlSmall { get; set; } = default!;

    [JsonPropertyName("icon_url_large")]
    public string IconUrlLarge { get; set; } = default!;

    [JsonPropertyName("icon_url_unawarded")]
    public string IconUrlUnawarded { get; set; } = default!;

    [JsonPropertyName("icon_background_color")]
    public string IconBackgroundColor { get; set; } = default!;

    [JsonPropertyName("certificate_file_name")]
    public string CertificateFileName { get; set; } = default!;

    [JsonPropertyName("weight")]
    public int Weight { get; set; }

    [JsonPropertyName("award_count")]
    public int AwardCount { get; set; }

    [JsonPropertyName("award_order")]
    public int AwardOrder { get; set; }

    [JsonPropertyName("achievement")]
    public bool Achievement { get; set; }

#if NET6_0_OR_GREATER
    [JsonPropertyName("award_date"), JsonConverter(typeof(DateOnlyConverter))]
    public DateOnly AwardDate { get; set; } = default!;
#else
    [JsonPropertyName("award_date"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime AwardDate { get; set; } = default!;
#endif

#if NET6_0_OR_GREATER
    [JsonPropertyName("display_date"), JsonConverter(typeof(DateOnlyConverter))]
    public DateOnly DisplayDate { get; set; } = default!;
#else
    [JsonPropertyName("display_date"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime DisplayDate { get; set; } = default!;
#endif

    [JsonPropertyName("subsession_id")]
    public int SubsessionId { get; set; }

    [JsonPropertyName("awarded_description")]
    public string AwardedDescription { get; set; } = default!;

    [JsonPropertyName("viewed")]
    public bool Viewed { get; set; }

    [JsonPropertyName("threshold")]
    public int Threshold { get; set; }

    [JsonPropertyName("progress")]
    public int Progress { get; set; }

    [JsonPropertyName("progress_label")]
    public string ProgressLabel { get; set; } = default!;

    [JsonPropertyName("progress_text")]
    public string ProgressText { get; set; } = default!;

    [JsonPropertyName("progress_text_label")]
    public string ProgressTextLabel { get; set; } = default!;
}

[JsonSerializable(typeof(MemberAward[])), JsonSourceGenerationOptions(WriteIndented = true)]
internal partial class MemberAwardArrayContext : JsonSerializerContext
{ }
