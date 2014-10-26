﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConverterForMapBonus.cs" company="GW2.NET Coding Team">
//   This product is licensed under the GNU General Public License version 2 (GPLv2) as defined on the following page: http://www.gnu.org/licenses/gpl-2.0.html
// </copyright>
// <summary>
//   Converts objects of type <see cref="MapBonusDataContract" /> to objects of type <see cref="MapBonus" />.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace GW2NET.V1.WorldVersusWorld.Matches.Json.Converters
{
    using System;
    using System.Diagnostics.Contracts;

    using GW2NET.Common;
    using GW2NET.Entities.WorldVersusWorld;

    /// <summary>Converts objects of type <see cref="MapBonusDataContract"/> to objects of type <see cref="MapBonus"/>.</summary>
    internal sealed class ConverterForMapBonus : IConverter<MapBonusDataContract, MapBonus>
    {
        /// <summary>Infrastructure. Holds a reference to a type converter.</summary>
        private readonly IConverter<string, TeamColor> converterForTeamColor;

        /// <summary>Initializes a new instance of the <see cref="ConverterForMapBonus"/> class.</summary>
        public ConverterForMapBonus()
            : this(new ConverterForTeamColor())
        {
        }

        /// <summary>Initializes a new instance of the <see cref="ConverterForMapBonus"/> class.</summary>
        /// <param name="converterForTeamColor">The converter for <see cref="TeamColor"/>.</param>
        public ConverterForMapBonus(IConverter<string, TeamColor> converterForTeamColor)
        {
            this.converterForTeamColor = converterForTeamColor;
        }

        /// <summary>Converts the given object of type <see cref="MapBonusDataContract"/> to an object of type <see cref="MapBonus"/>.</summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The converted value.</returns>
        public MapBonus Convert(MapBonusDataContract value)
        {
            Contract.Requires(value != null);
            MapBonus mapBonus;
            if (string.Equals(value.Type, "bloodlust", StringComparison.OrdinalIgnoreCase))
            {
                mapBonus = new Bloodlust();
            }
            else
            {
                mapBonus = new UnknownMapBonus();
            }

            if (value.Owner != null)
            {
                mapBonus.Owner = this.converterForTeamColor.Convert(value.Owner);
            }

            return mapBonus;
        }
    }
}