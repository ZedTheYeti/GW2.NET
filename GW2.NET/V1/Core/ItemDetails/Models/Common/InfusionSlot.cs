﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InfusionSlot.cs" company="GW2.Net Coding Team">
//   This product is licensed under the GNU General Public License version 2 (GPLv2) as defined on the following page: http://www.gnu.org/licenses/gpl-2.0.html
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using Newtonsoft.Json;

namespace GW2DotNET.V1.Core.ItemDetails.Models.Common
{
    /// <summary>
    /// Represents one of an item's infusion slots.
    /// </summary>
    public class InfusionSlot : JsonObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InfusionSlot"/> class.
        /// </summary>
        public InfusionSlot()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InfusionSlot"/> class using the specified values.
        /// </summary>
        /// <param name="flags">The infusion slot's type(s)</param>
        public InfusionSlot(InfusionSlotTypes flags)
            : this(flags, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InfusionSlot"/> class using the specified values.
        /// </summary>
        /// <param name="flags">The infusion slot's type(s)</param>
        /// <param name="item">The infusion slot's item. Reserved for future use.</param>
        public InfusionSlot(InfusionSlotTypes flags, string item)
        {
            this.Flags = flags;
            this.Item = item;
        }

        /// <summary>
        /// Gets or sets the infusion slot's type(s).
        /// </summary>
        [JsonProperty("flags", Order = 0)]
        public InfusionSlotTypes Flags { get; set; }

        /// <summary>
        /// Gets or sets the infusion slot's item. Reserved for future use.
        /// </summary>
        [JsonProperty("item", Order = 1, NullValueHandling = NullValueHandling.Ignore)]
        public string Item { get; set; }
    }
}