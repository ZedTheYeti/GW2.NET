﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MapsDetailsResult.cs" company="GW2.Net Coding Team">
//   This product is licensed under the GNU General Public License version 2 (GPLv2) as defined on the following page: http://www.gnu.org/licenses/gpl-2.0.html
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using Newtonsoft.Json;

namespace GW2DotNET.V1.Core.MapsInformation.Details
{
    /// <summary>
    /// Wraps a collection of maps and their details.
    /// </summary>
    public class MapsDetailsResult : JsonObject
    {
        /// <summary>
        /// Gets or sets the collection of maps and their details.
        /// </summary>
        [JsonProperty("maps", Order = 0)]
        public MapsDetails Maps { get; set; }
    }
}