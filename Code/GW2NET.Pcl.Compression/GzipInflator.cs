﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GzipInflator.cs" company="GW2.NET Coding Team">
//   This product is licensed under the GNU General Public License version 2 (GPLv2) as defined on the following page: http://www.gnu.org/licenses/gpl-2.0.html
// </copyright>
// <summary>
//   Represents the GZIP inflator.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace GW2NET.Compression
{
    using System.IO;
    using System.IO.Compression;

    using GW2NET.Common;

    /// <summary>Represents the GZIP inflator.</summary>
    public class GzipInflator : IConverter<Stream, Stream>
    {
        /// <summary>Inflates the given <see cref="Stream"/>.</summary>
        /// <param name="value">The compressed stream.</param>
        /// <returns>The <see cref="Stream"/>.</returns>
        public Stream Convert(Stream value)
        {
            return new GZipStream(value, CompressionMode.Decompress);
        }
    }
}