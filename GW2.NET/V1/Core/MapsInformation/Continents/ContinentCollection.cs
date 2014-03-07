﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ContinentCollection.cs" company="GW2.Net Coding Team">
//   This product is licensed under the GNU General Public License version 2 (GPLv2) as defined on the following page: http://www.gnu.org/licenses/gpl-2.0.html
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;

namespace GW2DotNET.V1.Core.MapsInformation.Continents
{
    /// <summary>
    /// Represents a collection of continents.
    /// </summary>
    public class ContinentCollection : JsonDictionary<int, Continent>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ContinentCollection"/> class.
        /// </summary>
        public ContinentCollection()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ContinentCollection"/> class.
        /// </summary>
        /// <param name="capacity">The initial number of elements that the new dictionary can contain.</param>
        public ContinentCollection(int capacity)
            : base(capacity)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ContinentCollection"/> class.
        /// </summary>
        /// <param name="dictionary">The dictionary whose values are copied to the new dictionary.</param>
        public ContinentCollection(IDictionary<int, Continent> dictionary)
            : base(dictionary)
        {
        }
    }
}