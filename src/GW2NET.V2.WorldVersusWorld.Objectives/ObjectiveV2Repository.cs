// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ObjectiveV2Repository.cs" company="GW2.NET Coding Team">
//   This product is licensed under the GNU General Public License version 2 (GPLv2). See the License in the project root folder or the following page: http://www.gnu.org/licenses/gpl-2.0.html
// </copyright>
// <summary>
//   Represents a repository that retrieves data from the /v2/wvw/objectives interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GW2NET.V2.WorldVersusWorld.Objectives
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Globalization;
    using System.Threading;
    using GW2NET.Common;
    using GW2NET.WorldVersusWorld;

    public class ObjectiveV2Repository : IObjectiveV2Repository
    {
        /// <inheritdoc />
        CultureInfo ILocalizable.Culture { get; set; }

        ICollection<int> IDiscoverable<int>.Discover()
        {
            throw new NotImplementedException();
        }

        Task<ICollection<int>> IDiscoverable<int>.DiscoverAsync()
        {
            throw new NotImplementedException();
        }

        Task<ICollection<int>> IDiscoverable<int>.DiscoverAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        ObjectiveV2 IRepository<int, ObjectiveV2>.Find(int identifier)
        {
            throw new NotImplementedException();
        }

        IDictionaryRange<int, ObjectiveV2> IRepository<int, ObjectiveV2>.FindAll()
        {
            throw new NotImplementedException();
        }

        IDictionaryRange<int, ObjectiveV2> IRepository<int, ObjectiveV2>.FindAll(ICollection<int> identifiers)
        {
            throw new NotImplementedException();
        }

        Task<IDictionaryRange<int, ObjectiveV2>> IRepository<int, ObjectiveV2>.FindAllAsync()
        {
            throw new NotImplementedException();
        }

        Task<IDictionaryRange<int, ObjectiveV2>> IRepository<int, ObjectiveV2>.FindAllAsync(ICollection<int> identifiers)
        {
            throw new NotImplementedException();
        }

        Task<IDictionaryRange<int, ObjectiveV2>> IRepository<int, ObjectiveV2>.FindAllAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        Task<IDictionaryRange<int, ObjectiveV2>> IRepository<int, ObjectiveV2>.FindAllAsync(ICollection<int> identifiers, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        Task<ObjectiveV2> IRepository<int, ObjectiveV2>.FindAsync(int identifier)
        {
            throw new NotImplementedException();
        }

        Task<ObjectiveV2> IRepository<int, ObjectiveV2>.FindAsync(int identifier, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        ICollectionPage<ObjectiveV2> IPaginator<ObjectiveV2>.FindPage(int pageIndex)
        {
            throw new NotImplementedException();
        }

        ICollectionPage<ObjectiveV2> IPaginator<ObjectiveV2>.FindPage(int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }

        Task<ICollectionPage<ObjectiveV2>> IPaginator<ObjectiveV2>.FindPageAsync(int pageIndex)
        {
            throw new NotImplementedException();
        }

        Task<ICollectionPage<ObjectiveV2>> IPaginator<ObjectiveV2>.FindPageAsync(int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }

        Task<ICollectionPage<ObjectiveV2>> IPaginator<ObjectiveV2>.FindPageAsync(int pageIndex, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        Task<ICollectionPage<ObjectiveV2>> IPaginator<ObjectiveV2>.FindPageAsync(int pageIndex, int pageSize, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
