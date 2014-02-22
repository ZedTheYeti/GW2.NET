﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PointOfInterest.cs" company="GW2.Net Coding Team">
//   This product is licensed under the GNU General Public License version 2 (GPLv2) as defined on the following page: http://www.gnu.org/licenses/gpl-2.0.html
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Drawing;
using GW2DotNET.V1.Core.Converters;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace GW2DotNET.V1.Core.MapFloor.Models
{
    /// <summary>
    /// Represents a Point of Interest (POI) location.
    /// </summary>
    public class PointOfInterest : JsonObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PointOfInterest"/> class.
        /// </summary>
        public PointOfInterest()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PointOfInterest"/> class using the specified values.
        /// </summary>
        /// <param name="poiId">The Point of Interest's ID.</param>
        /// <param name="name">The Point of Interest's name.</param>
        /// <param name="type">The Point of Interest's type.</param>
        /// <param name="floor">The Point of Interest's floor.</param>
        /// <param name="coordinates">The Point of Interest's coordinates.</param>
        public PointOfInterest(int poiId, string name, PointOfInterestCategory type, int floor, PointF coordinates)
        {
            this.PoiId = poiId;
            this.Name = name;
            this.Type = type;
            this.Floor = floor;
            this.Coordinates = coordinates;
        }

        /// <summary>
        /// Gets or sets the Point of Interest's coordinates.
        /// </summary>
        [JsonProperty("coord", Order = 4)]
        [JsonConverter(typeof(PointFConverter))]
        public PointF Coordinates { get; set; }

        /// <summary>
        /// Gets or sets the Point of Interest's floor.
        /// </summary>
        [JsonProperty("floor", Order = 3)]
        public int Floor { get; set; }

        /// <summary>
        /// Gets or sets the Point of Interest's name.
        /// </summary>
        [JsonProperty("name", Order = 1)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the Point of Interest's ID.
        /// </summary>
        [JsonProperty("poi_id", Order = 0)]
        public int PoiId { get; set; }

        /// <summary>
        /// Gets or sets the Point of Interest's type.
        /// </summary>
        [JsonProperty("type", Order = 2)]
        [JsonConverter(typeof(StringEnumConverter))]
        public PointOfInterestCategory Type { get; set; }
    }
}