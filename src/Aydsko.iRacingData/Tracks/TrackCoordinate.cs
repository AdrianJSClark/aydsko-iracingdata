// © 2023 Adrian Clark
// This file is licensed to you under the MIT license.

using System.Globalization;

namespace Aydsko.iRacingData.Tracks;

/// <summary>Represents the latitude and longitude of a track.</summary>
public struct TrackCoordinate : IEquatable<TrackCoordinate>
{
    /// <value>Latitude value.</value>
    public double Latitude { get; private set; }
    /// <value>Longitude value.</value>
    public double Longitude { get; private set; }

    public TrackCoordinate(double latitude, double longitude)
    {
        Latitude = latitude;
        Longitude = longitude;
    }

    /// <summary>Attempt to parse the given string into a <see cref="TrackCoordinate"/> object.</summary>
    /// <param name="input">The value to parse.</param>
    /// <param name="result">Contains the coordinates value if the parse was successful, or <see langword="null"/> otherwise.</param>
    /// <returns><see langword="true"/> if the parse was successful, or <see langword="false"/>.</returns>
    public static bool TryParse(string? input, out TrackCoordinate? result)
    {
        if (input is null
            || (input.Split(new[] { ',' }, 2, StringSplitOptions.RemoveEmptyEntries) is not string[] values)
            || (!double.TryParse(values[0], NumberStyles.Number, CultureInfo.InvariantCulture, out var lat))
            || (!double.TryParse(values[1], NumberStyles.Number, CultureInfo.InvariantCulture, out var lon)))
        {
            result = default;
            return false;
        }

        result = new(lat, lon);
        return true;
    }

    public override bool Equals(object? obj)
    {
        return (obj is TrackCoordinate coordinate) && Equals(coordinate);
    }

    public override int GetHashCode()
    {
        return Latitude.GetHashCode() ^ Longitude.GetHashCode();
    }

    public static bool operator ==(TrackCoordinate left, TrackCoordinate right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(TrackCoordinate left, TrackCoordinate right)
    {
        return !(left == right);
    }

    public bool Equals(TrackCoordinate other)
    {
        return (Latitude == other.Latitude) && (Longitude == other.Longitude);
    }
}
