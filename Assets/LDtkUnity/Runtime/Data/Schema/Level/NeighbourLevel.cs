﻿using Newtonsoft.Json;

namespace LDtkUnity
{
    /// <summary>
    /// Nearby level info
    /// </summary>
    public partial class NeighbourLevel
    {
        /// <summary>
        /// A single lowercase character tipping on the level location (`n`orth, `s`outh, `w`est,
        /// `e`ast).
        /// </summary>
        [JsonProperty("dir")]
        public string Dir { get; set; }

        /// <summary>
        /// Neighbour Instance Identifier
        /// </summary>
        [JsonProperty("levelIid")]
        public string LevelIid { get; set; }

        /// <summary>
        /// **WARNING**: this deprecated value is no longer exported since version 1.2.0  Replaced
        /// by: `levelIid`
        /// </summary>
        [JsonProperty("levelUid")]
        public long? LevelUid { get; set; }
    }
}