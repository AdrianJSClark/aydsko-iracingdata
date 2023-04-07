// © 2023 Adrian Clark
// This file is licensed to you under the MIT license.

namespace Aydsko.iRacingData.Common;

public interface IChunkInfoResultHeader<THeaderData> where THeaderData : IChunkInfoResultHeaderData
{
    THeaderData Data { get; }
}

public interface IChunkInfoResultHeaderData
{
    ChunkInfo ChunkInfo { get; }
}
