namespace Aydsko.iRacingData.Stats;

/// <summary>Represents a comma separated value (CSV) file containing statistics about a category of drivers.</summary>
/// <seealso cref="IDataClient.GetDriverStatisticsByCategoryCsvAsync(int, CancellationToken)"/>
/// <seealso cref="Constants.Category"/>
public class DriverStatisticsCsvFile
{
    /// <summary>The Category Id value used to retrieve these statistics.</summary>
    public int CategoryId { get; set; }

    /// <summary>The name of the file.</summary>
    public string FileName { get; set; } = default!;

    /// <summary>Content of the CSV file.</summary>
    public byte[] ContentBytes { get; set; } = default!;
}
