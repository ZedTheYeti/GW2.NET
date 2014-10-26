﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MapFloorService.cs" company="GW2.NET Coding Team">
//   This product is licensed under the GNU General Public License version 2 (GPLv2) as defined on the following page: http://www.gnu.org/licenses/gpl-2.0.html
// </copyright>
// <summary>
//   Provides the default implementation of the map floor service.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace GW2NET.V1.Floors
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using System.Globalization;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using GW2NET.Common;
    using GW2NET.Entities.Maps;
    using GW2NET.V1.Floors.Json;

    /// <summary>Provides the default implementation of the map floor service.</summary>
    public class MapFloorService : IMapFloorService
    {
        /// <summary>Infrastructure. Holds a reference to the service client.</summary>
        private readonly IServiceClient serviceClient;

        /// <summary>Initializes a new instance of the <see cref="MapFloorService"/> class.</summary>
        /// <param name="serviceClient">The service client.</param>
        public MapFloorService(IServiceClient serviceClient)
        {
            Contract.Requires(serviceClient != null);
            this.serviceClient = serviceClient;
        }

        /// <summary>Gets a map floor and its localized details.</summary>
        /// <param name="continent">The continent identifier.</param>
        /// <param name="floor">The floor identifier.</param>
        /// <returns>A map floor and its localized details.</returns>
        /// <remarks>See <a href="http://wiki.guildwars2.com/wiki/API:1/map_floor">wiki</a> for more information.</remarks>
        public Floor GetMapFloor(int continent, int floor)
        {
            var culture = new CultureInfo("en");
            return this.GetMapFloor(continent, floor, culture);
        }

        /// <summary>Gets a map floor and its localized details.</summary>
        /// <param name="continent">The continent identifier.</param>
        /// <param name="floor">The floor identifier.</param>
        /// <param name="language">The language.</param>
        /// <returns>A map floor and its localized details.</returns>
        /// <remarks>See <a href="http://wiki.guildwars2.com/wiki/API:1/map_floor">wiki</a> for more information.</remarks>
        public Floor GetMapFloor(int continent, int floor, CultureInfo language)
        {
            if (language == null)
            {
                throw new ArgumentNullException(paramName: "language", message: "Precondition failed: language != null");
            }

            Contract.EndContractBlock();

            var request = new MapFloorRequest { ContinentId = continent, Floor = floor, Culture = language };
            var response = this.serviceClient.Send<FloorDataContract>(request);
            if (response.Content == null)
            {
                return null;
            }

            var value = ConvertFloorContract(response.Content);
            value.ContinentId = continent;
            value.FloorId = floor;
            value.Locale = response.Culture ?? language;
            return value;
        }

        /// <summary>Gets a map floor and its localized details.</summary>
        /// <param name="continent">The continent identifier.</param>
        /// <param name="floor">The floor identifier.</param>
        /// <returns>A map floor and its localized details.</returns>
        /// <remarks>See <a href="http://wiki.guildwars2.com/wiki/API:1/map_floor">wiki</a> for more information.</remarks>
        public Task<Floor> GetMapFloorAsync(int continent, int floor)
        {
            var culture = new CultureInfo("en");
            return this.GetMapFloorAsync(continent, floor, culture, CancellationToken.None);
        }

        /// <summary>Gets a map floor and its localized details.</summary>
        /// <param name="continent">The continent identifier.</param>
        /// <param name="floor">The floor identifier.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that provides cancellation support.</param>
        /// <returns>A map floor and its localized details.</returns>
        /// <remarks>See <a href="http://wiki.guildwars2.com/wiki/API:1/map_floor">wiki</a> for more information.</remarks>
        public Task<Floor> GetMapFloorAsync(int continent, int floor, CancellationToken cancellationToken)
        {
            var culture = new CultureInfo("en");
            return this.GetMapFloorAsync(continent, floor, culture, cancellationToken);
        }

        /// <summary>Gets a map floor and its localized details.</summary>
        /// <param name="continent">The continent identifier.</param>
        /// <param name="floor">The floor identifier.</param>
        /// <param name="language">The language.</param>
        /// <returns>A map floor and its localized details.</returns>
        /// <remarks>See <a href="http://wiki.guildwars2.com/wiki/API:1/map_floor">wiki</a> for more information.</remarks>
        public Task<Floor> GetMapFloorAsync(int continent, int floor, CultureInfo language)
        {
            return this.GetMapFloorAsync(continent, floor, language, CancellationToken.None);
        }

        /// <summary>Gets a map floor and its localized details.</summary>
        /// <param name="continent">The continent identifier.</param>
        /// <param name="floor">The floor identifier.</param>
        /// <param name="language">The language.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> that provides cancellation support.</param>
        /// <returns>A map floor and its localized details.</returns>
        /// <remarks>See <a href="http://wiki.guildwars2.com/wiki/API:1/map_floor">wiki</a> for more information.</remarks>
        public Task<Floor> GetMapFloorAsync(int continent, int floor, CultureInfo language, CancellationToken cancellationToken)
        {
            if (language == null)
            {
                throw new ArgumentNullException(paramName: "language", message: "Precondition failed: language != null");
            }

            Contract.EndContractBlock();

            var request = new MapFloorRequest { ContinentId = continent, Floor = floor, Culture = language };
            return this.serviceClient.SendAsync<FloorDataContract>(request, cancellationToken).ContinueWith(
                task =>
                    {
                        var response = task.Result;
                        if (response.Content == null)
                        {
                            return null;
                        }

                        var value = ConvertFloorContract(response.Content);
                        value.ContinentId = continent;
                        value.FloorId = floor;
                        value.Locale = response.Culture ?? language;
                        return value;
                    }, 
                cancellationToken);
        }

        /// <summary>Infrastructure. Converts contracts to entities.</summary>
        /// <param name="content">The content.</param>
        /// <returns>An entity.</returns>
        private static Floor ConvertFloorContract(FloorDataContract content)
        {
            Contract.Requires(content != null);

            // Create a new floor object
            var value = new Floor();

            // Set the texture dimensions
            if (content.TextureDimensions != null && content.TextureDimensions.Length == 2)
            {
                value.TextureDimensions = ConvertSize2D(content.TextureDimensions);
            }

            // Set the clamped view dimensions
            if (content.ClampedView != null && content.ClampedView.Length == 2 && content.ClampedView[0] != null && content.ClampedView[0].Length == 2
                && content.ClampedView[1] != null && content.ClampedView[1].Length == 2)
            {
                value.ClampedView = ConvertRectangleContract(content.ClampedView);
            }

            // Set the regions
            if (content.Regions != null)
            {
                value.Regions = ConvertRegionDataContractCollection(content.Regions);
            }

            // Return the floor object
            return value;
        }

        /// <summary>Infrastructure. Converts contracts to entities.</summary>
        /// <param name="content">The content.</param>
        /// <returns>An entity.</returns>
        private static PointOfInterest ConvertPointOfInterestContract(PointOfInterestDataContract content)
        {
            Contract.Requires(content != null);
            var value = (PointOfInterest)Activator.CreateInstance(GetPointOfInterestType(content));
            value.PointOfInterestId = content.PointOfInterestId;
            value.Name = content.Name;
            value.Floor = content.Floor;
            if (content.Coordinates != null && content.Coordinates.Length == 2)
            {
                value.Coordinates = ConvertVector2D(content.Coordinates);
            }

            return value;
        }

        /// <summary>Infrastructure. Converts contracts to entities.</summary>
        /// <param name="content">The content.</param>
        /// <returns>A collection of entities.</returns>
        private static ICollection<PointOfInterest> ConvertPointOfInterestContractCollection(ICollection<PointOfInterestDataContract> content)
        {
            Contract.Requires(content != null);
            var values = new List<PointOfInterest>(content.Count);
            values.AddRange(content.Select(ConvertPointOfInterestContract));
            return values;
        }

        /// <summary>Infrastructure. Converts contracts to entities.</summary>
        /// <param name="content">The content.</param>
        /// <returns>An entity.</returns>
        private static Rectangle ConvertRectangleContract(double[][] content)
        {
            Contract.Requires(content != null && content.Length == 2);
            Contract.Requires(content[0] != null && content[0].Length == 2);
            Contract.Requires(content[1] != null && content[1].Length == 2);
            var nw = ConvertVector2D(content[0]);
            var se = ConvertVector2D(content[1]);
            return new Rectangle(nw, se);
        }

        /// <summary>Infrastructure. Converts contracts to entities.</summary>
        /// <param name="content">The content.</param>
        /// <returns>An entity.</returns>
        private static Region ConvertRegionDataContract(KeyValuePair<string, RegionDataContract> content)
        {
            Contract.Requires(content.Key != null);
            Contract.Requires(content.Value != null);
            Contract.Ensures(Contract.Result<Region>() != null);

            // Create a new region object
            var value = new Region();

            // Set the region identifier
            value.RegionId = int.Parse(content.Key);

            // Set the name of the region
            if (content.Value.Name != null)
            {
                value.Name = content.Value.Name;
            }

            // Set the position of the region label
            if (content.Value.LabelCoordinates != null && content.Value.LabelCoordinates.Length == 2)
            {
                value.LabelCoordinates = ConvertVector2D(content.Value.LabelCoordinates);
            }

            // Set the maps
            if (content.Value.Maps != null)
            {
                value.Maps = ConvertSubregionDataContractCollection(content.Value.Maps);
            }

            // Return the region object
            return value;
        }

        /// <summary>Infrastructure. Converts contracts to entities.</summary>
        /// <param name="content">The content.</param>
        /// <returns>A collection of entities.</returns>
        private static IDictionary<int, Region> ConvertRegionDataContractCollection(IDictionary<string, RegionDataContract> content)
        {
            Contract.Requires(content != null);
            var values = new Dictionary<int, Region>(content.Count);
            foreach (var value in content.Select(ConvertRegionDataContract))
            {
                Contract.Assume(value != null);
                values.Add(value.RegionId, value);
            }

            return values;
        }

        /// <summary>Infrastructure. Converts contracts to entities.</summary>
        /// <param name="content">The content.</param>
        /// <returns>An entity.</returns>
        private static RenownTask ConvertRenownTaskDataContract(RenownTaskDataContract content)
        {
            Contract.Requires(content != null);
            Contract.Requires(content.Coordinates != null);
            Contract.Requires(content.Coordinates.Length == 2);
            return new RenownTask
                {
                    TaskId = content.TaskId, 
                    Objective = content.Objective, 
                    Level = content.Level, 
                    Coordinates = ConvertVector2D(content.Coordinates)
                };
        }

        /// <summary>Infrastructure. Converts contracts to entities.</summary>
        /// <param name="content">The content.</param>
        /// <returns>A collection of entities.</returns>
        private static ICollection<RenownTask> ConvertRenownTaskDataContractCollection(ICollection<RenownTaskDataContract> content)
        {
            Contract.Requires(content != null);
            var values = new List<RenownTask>(content.Count);
            values.AddRange(content.Select(ConvertRenownTaskDataContract));
            return values;
        }

        /// <summary>Infrastructure. Converts contracts to entities.</summary>
        /// <param name="content">The content.</param>
        /// <returns>An entity.</returns>
        private static Sector ConvertSectorDataContract(SectorDataContract content)
        {
            Contract.Requires(content != null);
            Contract.Requires(content.Coordinates != null);
            Contract.Requires(content.Coordinates.Length == 2);
            return new Sector { SectorId = content.SectorId, Name = content.Name, Level = content.Level, Coordinates = ConvertVector2D(content.Coordinates) };
        }

        /// <summary>Infrastructure. Converts contracts to entities.</summary>
        /// <param name="content">The content.</param>
        /// <returns>A collection of entities.</returns>
        private static ICollection<Sector> ConvertSectorDataContractCollection(ICollection<SectorDataContract> content)
        {
            Contract.Requires(content != null);
            var values = new List<Sector>(content.Count);
            values.AddRange(content.Select(ConvertSectorDataContract));
            return values;
        }

        /// <summary>Infrastructure. Converts contracts to entities.</summary>
        /// <param name="content">The content.</param>
        /// <returns>An entity.</returns>
        private static Size2D ConvertSize2D(double[] content)
        {
            Contract.Requires(content != null);
            Contract.Requires(content.Length == 2);
            return new Size2D(content[0], content[1]);
        }

        /// <summary>Infrastructure. Converts contracts to entities.</summary>
        /// <param name="content">The content.</param>
        /// <returns>An entity.</returns>
        private static SkillChallenge ConvertSkillChallengeDataContract(SkillChallengeDataContract content)
        {
            Contract.Requires(content != null);
            Contract.Requires(content.Coordinates != null);
            Contract.Requires(content.Coordinates.Length == 2);
            return new SkillChallenge { Coordinates = ConvertVector2D(content.Coordinates) };
        }

        /// <summary>Infrastructure. Converts contracts to entities.</summary>
        /// <param name="content">The content.</param>
        /// <returns>A collection of entities.</returns>
        private static ICollection<SkillChallenge> ConvertSkillChallengeDataContractCollection(ICollection<SkillChallengeDataContract> content)
        {
            Contract.Requires(content != null);
            var values = new List<SkillChallenge>(content.Count);
            values.AddRange(content.Select(ConvertSkillChallengeDataContract));
            return values;
        }

        /// <summary>Infrastructure. Converts contracts to entities.</summary>
        /// <param name="content">The content.</param>
        /// <returns>An entity.</returns>
        private static Subregion ConvertSubregionDataContract(KeyValuePair<string, SubregionDataContract> content)
        {
            Contract.Requires(content.Key != null);
            Contract.Requires(content.Value != null);
            Contract.Ensures(Contract.Result<Subregion>() != null);

            // Create a new map object
            var value = new Subregion();

            // Set the map identifier
            value.MapId = int.Parse(content.Key);

            // Set the name of the map
            if (content.Value.Name != null)
            {
                value.Name = content.Value.Name;
            }

            // Set the minimum level
            value.MinimumLevel = content.Value.MinimumLevel;

            // Set the maximum level
            value.MaximumLevel = content.Value.MaximumLevel;

            // Set the default floor
            value.DefaultFloor = content.Value.DefaultFloor;

            // Set the map dimensions
            if (content.Value.MapRectangle != null && content.Value.MapRectangle.Length == 2 && content.Value.MapRectangle[0] != null
                && content.Value.MapRectangle[0].Length == 2 && content.Value.MapRectangle[1] != null && content.Value.MapRectangle[1].Length == 2)
            {
                value.MapRectangle = ConvertRectangleContract(content.Value.MapRectangle);
            }

            // Set the continent dimensions
            if (content.Value.ContinentRectangle != null && content.Value.ContinentRectangle.Length == 2 && content.Value.ContinentRectangle[0] != null
                && content.Value.ContinentRectangle[0].Length == 2 && content.Value.ContinentRectangle[1] != null
                && content.Value.ContinentRectangle[1].Length == 2)
            {
                value.ContinentRectangle = ConvertRectangleContract(content.Value.ContinentRectangle);
            }

            // Set the points of interest
            if (content.Value.PointsOfInterest != null)
            {
                value.PointsOfInterest = ConvertPointOfInterestContractCollection(content.Value.PointsOfInterest);
            }

            // Set the renown tasks
            if (content.Value.Tasks != null)
            {
                value.Tasks = ConvertRenownTaskDataContractCollection(content.Value.Tasks);
            }

            // Set the skill challenges
            if (content.Value.SkillChallenges != null)
            {
                value.SkillChallenges = ConvertSkillChallengeDataContractCollection(content.Value.SkillChallenges);
            }

            // Set the sectors
            if (content.Value.Sectors != null)
            {
                value.Sectors = ConvertSectorDataContractCollection(content.Value.Sectors);
            }

            // Return the map object
            return value;
        }

        /// <summary>Infrastructure. Converts contracts to entities.</summary>
        /// <param name="content">The content.</param>
        /// <returns>A collection of entities.</returns>
        private static IDictionary<int, Subregion> ConvertSubregionDataContractCollection(IDictionary<string, SubregionDataContract> content)
        {
            Contract.Requires(content != null);
            var values = new Dictionary<int, Subregion>(content.Count);
            foreach (var value in content.Select(ConvertSubregionDataContract))
            {
                Contract.Assume(value != null);
                values.Add(value.MapId, value);
            }

            return values;
        }

        /// <summary>Infrastructure. Converts contracts to entities.</summary>
        /// <param name="content">The content.</param>
        /// <returns>An entity.</returns>
        private static Vector2D ConvertVector2D(double[] content)
        {
            Contract.Requires(content != null);
            Contract.Requires(content.Length == 2);
            return new Vector2D(content[0], content[1]);
        }

        /// <summary>Infrastructure. Maps type discriminators to .NET types.</summary>
        /// <param name="content">The content.</param>
        /// <returns>The corresponding <see cref="System.Type"/>.</returns>
        private static Type GetPointOfInterestType(PointOfInterestDataContract content)
        {
            Contract.Requires(content != null);
            switch (content.Type)
            {
                case "unlock":
                    return typeof(Dungeon);
                case "landmark":
                    return typeof(Landmark);
                case "vista":
                    return typeof(Vista);
                case "waypoint":
                    return typeof(Waypoint);
                default:
                    return typeof(UnknownPointOfInterest);
            }
        }

        /// <summary>The invariant method for this class.</summary>
        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.serviceClient != null);
        }
    }
}