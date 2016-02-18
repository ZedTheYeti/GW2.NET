// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IObjectiveV2Repository.cs" company="GW2.NET Coding Team">
//   This product is licensed under the GNU General Public License version 2 (GPLv2). See the License in the project root folder or the following page: http://www.gnu.org/licenses/gpl-2.0.html
// </copyright>
// <summary>
//   Provides the interface for repositories that provide objective information.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace GW2NET.WorldVersusWorld
{
    using GW2NET.Common;

    public interface IObjectiveV2Repository : IRepository<int, ObjectiveV2>, ILocalizable
    {
    }
}
