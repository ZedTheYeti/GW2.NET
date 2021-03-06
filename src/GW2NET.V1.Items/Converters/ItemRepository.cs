﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ItemRepository.cs" company="GW2.NET Coding Team">
//   This product is licensed under the GNU General Public License version 2 (GPLv2). See the License in the project root folder or the following page: http://www.gnu.org/licenses/gpl-2.0.html
// </copyright>
// <summary>
//   Represents a repository that retrieves data from the /v1/items.json and /v1/item_details.json interfaces. See the remarks section for important limitations regarding this implementation.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GW2NET.V1.Items.Converters
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Threading;
    using System.Threading.Tasks;

    using GW2NET.Common;
    using GW2NET.Items;
    using GW2NET.V1.Items.Json;

    /// <summary>Represents a repository that retrieves data from the /v1/items.json and /v1/item_details.json interfaces. See the remarks section for important limitations regarding this implementation.</summary>
    /// <remarks>
    /// This implementation does not retrieve associated entities.
    /// <list type="bullet">
    ///     <item>
    ///         <description><see cref="Item"/>: <see cref="Item.BuildId"/> is always <c>0</c>. Retrieve the build number from the build service.</description>
    ///     </item>
    ///     <item>
    ///         <description><see cref="ISkinnable"/>: <see cref="ISkinnable.DefaultSkin"/> is always <c>null</c>. Use the value of <see cref="ISkinnable.DefaultSkinId"/> to retrieve the skin (applies to <see cref="Armor"/>, <see cref="Backpack"/>, <see cref="GatheringTool"/> and <see cref="Weapon"/>).</description>
    ///     </item>
    ///     <item>
    ///         <description><see cref="IUpgradable"/>: <see cref="IUpgradable.SuffixItem"/> is always <c>null</c>. Use the value of <see cref="IUpgradable.SuffixItemId"/> to retrieve the suffix item (applies to <see cref="Armor"/>, <see cref="Backpack"/>, <see cref="Trinket"/> and <see cref="Weapon"/>).</description>
    ///     </item>
    ///     <item>
    ///         <description><see cref="IUpgradable"/>: <see cref="IUpgradable.SecondarySuffixItem"/> is always <c>null</c>. Use the value of <see cref="IUpgradable.SecondarySuffixItemId"/> to retrieve the secondary suffix item (applies to <see cref="Armor"/>, <see cref="Backpack"/>, <see cref="Trinket"/> and <see cref="Weapon"/>).</description>
    ///     </item>
    ///     <item>
    ///         <description><see cref="InfusionSlot"/>: <see cref="InfusionSlot.Item"/> is always <c>null</c>. Use the value of <see cref="InfusionSlot.ItemId"/> to retrieve the infusion item.</description>
    ///     </item>
    ///     <item>
    ///         <description><see cref="DyeUnlocker"/>: <see cref="DyeUnlocker.Color"/> is always <c>null</c>. Use the value of <see cref="DyeUnlocker.ColorId"/> to retrieve the color.</description>
    ///     </item>
    ///     <item>
    ///         <description><see cref="CraftingRecipeUnlocker"/>: <see cref="CraftingRecipeUnlocker.Recipe"/> is always <c>null</c>. Use the value of <see cref="CraftingRecipeUnlocker.RecipeId"/> to retrieve the recipe.</description>
    ///     </item>
    /// </list>
    /// </remarks>
    public class ItemRepository : IItemRepository
    {
        private readonly IConverter<ItemDTO, Item> itemConverter;

        private readonly IConverter<ItemCollectionDTO, ICollection<int>> itemCollectionConverter;

        private readonly IServiceClient serviceClient;

        /// <summary>Initializes a new instance of the <see cref="ItemRepository"/> class.</summary>
        /// <param name="serviceClient"></param>
        /// <param name="itemCollectionConverter">The converter for <see cref="T:ICollection{int}"/>.</param>
        /// <param name="itemConverter">The converter for <see cref="Item"/>.</param>
        public ItemRepository(IServiceClient serviceClient, IConverter<ItemCollectionDTO, ICollection<int>> itemCollectionConverter, IConverter<ItemDTO, Item> itemConverter)
        {
            if (serviceClient == null)
            {
                throw new ArgumentNullException("serviceClient");
            }

            if (itemCollectionConverter == null)
            {
                throw new ArgumentNullException("itemCollectionConverter");
            }

            if (itemConverter == null)
            {
                throw new ArgumentNullException("itemConverter");
            }

            this.serviceClient = serviceClient;
            this.itemCollectionConverter = itemCollectionConverter;
            this.itemConverter = itemConverter;
        }

        /// <inheritdoc />
        CultureInfo ILocalizable.Culture { get; set; }

        /// <inheritdoc />
        ICollection<int> IDiscoverable<int>.Discover()
        {
            var request = new ItemDiscoveryRequest();
            var response = this.serviceClient.Send<ItemCollectionDTO>(request);
            if (response.Content == null)
            {
                return new List<int>(0);
            }

            return this.itemCollectionConverter.Convert(response.Content, response);
        }

        /// <inheritdoc />
        Task<ICollection<int>> IDiscoverable<int>.DiscoverAsync()
        {
            IItemRepository self = this;
            return self.DiscoverAsync(CancellationToken.None);
        }

        /// <inheritdoc />
        async Task<ICollection<int>> IDiscoverable<int>.DiscoverAsync(CancellationToken cancellationToken)
        {
            var request = new ItemDiscoveryRequest();
            var response = await this.serviceClient.SendAsync<ItemCollectionDTO>(request, cancellationToken).ConfigureAwait(false);
            if (response.Content == null)
            {
                return new List<int>(0);
            }

            return this.itemCollectionConverter.Convert(response.Content, response);
        }

        /// <inheritdoc />
        Item IRepository<int, Item>.Find(int identifier)
        {
            IItemRepository self = this;
            var request = new ItemDetailsRequest
            {
                ItemId = identifier,
                Culture = self.Culture
            };
            var response = this.serviceClient.Send<ItemDTO>(request);
            if (response.Content == null)
            {
                return null;
            }

            var item = this.itemConverter.Convert(response.Content, null);
            item.Culture = request.Culture;
            return item;
        }

        /// <inheritdoc />
        IDictionaryRange<int, Item> IRepository<int, Item>.FindAll()
        {
            throw new NotSupportedException();
        }

        /// <inheritdoc />
        IDictionaryRange<int, Item> IRepository<int, Item>.FindAll(ICollection<int> identifiers)
        {
            throw new NotSupportedException();
        }

        /// <inheritdoc />
        Task<IDictionaryRange<int, Item>> IRepository<int, Item>.FindAllAsync()
        {
            throw new NotSupportedException();
        }

        /// <inheritdoc />
        Task<IDictionaryRange<int, Item>> IRepository<int, Item>.FindAllAsync(CancellationToken cancellationToken)
        {
            throw new NotSupportedException();
        }

        /// <inheritdoc />
        Task<IDictionaryRange<int, Item>> IRepository<int, Item>.FindAllAsync(ICollection<int> identifiers)
        {
            throw new NotSupportedException();
        }

        /// <inheritdoc />
        Task<IDictionaryRange<int, Item>> IRepository<int, Item>.FindAllAsync(ICollection<int> identifiers, CancellationToken cancellationToken)
        {
            throw new NotSupportedException();
        }

        /// <inheritdoc />
        Task<Item> IRepository<int, Item>.FindAsync(int identifier)
        {
            IItemRepository self = this;
            return self.FindAsync(identifier, CancellationToken.None);
        }

        /// <inheritdoc />
        async Task<Item> IRepository<int, Item>.FindAsync(int identifier, CancellationToken cancellationToken)
        {
            IItemRepository self = this;
            var request = new ItemDetailsRequest
            {
                ItemId = identifier,
                Culture = self.Culture
            };
            var response = await this.serviceClient.SendAsync<ItemDTO>(request, cancellationToken).ConfigureAwait(false);
            if (response.Content == null)
            {
                return null;
            }

            var item = this.itemConverter.Convert(response.Content, response);
            if (item == null)
            {
                return null;
            }

            item.Culture = request.Culture;
            return item;
        }

        /// <inheritdoc />
        ICollectionPage<Item> IPaginator<Item>.FindPage(int pageIndex)
        {
            throw new NotSupportedException();
        }

        /// <inheritdoc />
        ICollectionPage<Item> IPaginator<Item>.FindPage(int pageIndex, int pageSize)
        {
            throw new NotSupportedException();
        }

        /// <inheritdoc />
        Task<ICollectionPage<Item>> IPaginator<Item>.FindPageAsync(int pageIndex)
        {
            throw new NotSupportedException();
        }

        /// <inheritdoc />
        Task<ICollectionPage<Item>> IPaginator<Item>.FindPageAsync(int pageIndex, CancellationToken cancellationToken)
        {
            throw new NotSupportedException();
        }

        /// <inheritdoc />
        Task<ICollectionPage<Item>> IPaginator<Item>.FindPageAsync(int pageIndex, int pageSize)
        {
            throw new NotSupportedException();
        }

        /// <inheritdoc />
        Task<ICollectionPage<Item>> IPaginator<Item>.FindPageAsync(int pageIndex, int pageSize, CancellationToken cancellationToken)
        {
            throw new NotSupportedException();
        }
    }
}