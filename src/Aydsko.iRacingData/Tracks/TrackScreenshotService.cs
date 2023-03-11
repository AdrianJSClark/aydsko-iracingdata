using System.Globalization;

namespace Aydsko.iRacingData.Tracks;

/// <summary>Contains logic to build links for the screenshots available of tracks.</summary>
public class TrackScreenshotService
{
    private readonly IDataClient dataClient;

    /// <summary>Create a new instance of the screenshot URL creation service.</summary>
    /// <param name="dataClient">A configured iRacing Data API client implementation.</param>
    public TrackScreenshotService(IDataClient dataClient)
    {
        this.dataClient = dataClient;
    }

    /// <summary>Create a list of links to JPEG images which show the given track.</summary>
    /// <param name="track">Information about the track.</param>
    /// <param name="trackAssets">Information about the track's assets.</param>
    /// <returns>A series of <see cref="Uri"/> objects containing links that will resolve to JPEG images of the track.</returns>
    /// <exception cref="ArgumentNullException">Either parameter is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentException">
    /// <list type="bullet">
    /// <item>The <see cref="Track.TrackId"/> and <see cref="TrackAssets.TrackId"/> properties do not contain the same value.</item>
    /// <item>The <see cref="TrackAssets.TrackMap"/> value is null or whitespace.</item>
    /// </list>
    /// </exception>
    public static IEnumerable<Uri> GetScreenshotLinks(Track track, TrackAssets trackAssets)
    {
        if (track is null)
        {
            throw new ArgumentNullException(nameof(track));
        }

        if (trackAssets is null)
        {
            throw new ArgumentNullException(nameof(trackAssets));
        }

        if (track.TrackId != trackAssets.TrackId)
        {
            throw new ArgumentException("Track and TrackAssets should be from the same track to build a screenshot URL.");
        }

        if (string.IsNullOrWhiteSpace(trackAssets.TrackMap))
        {
            throw new ArgumentException("TrackMap property of TrackAssets object cannot be null or white space.", nameof(trackAssets));
        }

        var trackMapBaseUrl = new Uri(trackAssets.TrackMap);
        var trackScreenshotBaseUrl = new Uri(trackMapBaseUrl, $"/public/track-maps-screenshots/{track.PackageId}_screenshots/");

        if (trackAssets.NumberOfSvgImages <= 0)
        {
            yield break;
        }

        for (var i = 1; i <= trackAssets.NumberOfSvgImages; i++)
        {
            yield return new Uri(trackScreenshotBaseUrl, $"{i:00}.jpg");
        }
    }

    /// <summary>Create a list of links to JPEG images which show the given track.</summary>
    /// <param name="trackId">The unique identifier of the track for which the screenshot links should be generated.</param>
    /// <param name="cancellationToken">A token to allow the operation to be cancelled.</param>
    /// <returns>A <see cref="Task"/> that resolves to a series of <see cref="Uri"/> objects containing links to JPEG images of the track.</returns>
    /// <exception cref="ArgumentOutOfRangeException">The <paramref name="trackId"/> value given could not be resolved to a valid track.</exception>
    public async Task<IEnumerable<Uri>> GetScreenshotLinksAsync(int trackId, CancellationToken cancellationToken = default)
    {
        var track = await GetTrackByIdAsync(trackId, cancellationToken).ConfigureAwait(false);
        var trackAssets = await GetTrackAssetsByIdAsync(trackId, cancellationToken).ConfigureAwait(false);

        return GetScreenshotLinks(track, trackAssets);
    }

    /// <summary>Create a list of links to JPEG images which show the given track.</summary>
    /// <param name="track">The track information for which the screenshot links should be generated.</param>
    /// <param name="cancellationToken">A token to allow the operation to be cancelled.</param>
    /// <returns>A <see cref="Task"/> that resolves to a series of <see cref="Uri"/> objects containing links to JPEG images of the track.</returns>
    /// <exception cref="ArgumentNullException">The <paramref name="track"/> was passed as <see langword="null"/>.</exception>
    public async Task<IEnumerable<Uri>> GetScreenshotLinksAsync(Track track, CancellationToken cancellationToken = default)
    {
        if (track is null)
        {
            throw new ArgumentNullException(nameof(track));
        }

        var trackAssets = await GetTrackAssetsByIdAsync(track.TrackId, cancellationToken).ConfigureAwait(false);

        return GetScreenshotLinks(track, trackAssets);
    }

    /// <summary>Create a list of links to JPEG images which show the given track.</summary>
    /// <param name="trackAssets">The track asset information for which the screenshot links should be generated.</param>
    /// <param name="cancellationToken">A token to allow the operation to be cancelled.</param>
    /// <returns>A <see cref="Task"/> that resolves to a series of <see cref="Uri"/> objects containing links to JPEG images of the track.</returns>
    /// <exception cref="ArgumentNullException">The <paramref name="trackAssets"/> was passed as <see langword="null"/>.</exception>
    public async Task<IEnumerable<Uri>> GetScreenshotLinksAsync(TrackAssets trackAssets, CancellationToken cancellationToken = default)
    {
        if (trackAssets is null)
        {
            throw new ArgumentNullException(nameof(trackAssets));
        }

        var track = await GetTrackByIdAsync(trackAssets.TrackId, cancellationToken).ConfigureAwait(false);

        return GetScreenshotLinks(track, trackAssets);
    }

    private async Task<Track> GetTrackByIdAsync(int trackId, CancellationToken cancellationToken)
    {
        var tracksResponse = await dataClient.GetTracksAsync(cancellationToken)
                                             .ConfigureAwait(false);

        if (tracksResponse?.Data.FirstOrDefault(t => t.TrackId == trackId) is not Track track)
        {
            throw new ArgumentOutOfRangeException(nameof(trackId), "Track identifier supplied could not be located as a valid track.");
        }

        return track;
    }

    private async Task<TrackAssets> GetTrackAssetsByIdAsync(int trackId, CancellationToken cancellationToken)
    {
        var trackAssetsResponse = await dataClient.GetTrackAssetsAsync(cancellationToken)
                                                  .ConfigureAwait(false);

        var trackIdString = trackId.ToString(CultureInfo.InvariantCulture);

        if (trackAssetsResponse is null || !trackAssetsResponse.Data.TryGetValue(trackIdString, out var trackAssets))
        {
            throw new ArgumentOutOfRangeException(nameof(trackId), "Track identifier supplied could not be used to locate track assets.");
        }

        return trackAssets;
    }
}
