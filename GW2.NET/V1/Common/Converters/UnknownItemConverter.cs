﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnknownItemConverter.cs" company="GW2.NET Coding Team">
//   This product is licensed under the GNU General Public License version 2 (GPLv2) as defined on the following page: http://www.gnu.org/licenses/gpl-2.0.html
// </copyright>
// <summary>
//   Converts an object to and/or from JSON.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace GW2DotNET.V1.Common.Converters
{
    using System;

    using GW2DotNET.V1.Items.Contracts;

    using Newtonsoft.Json;

    /// <summary>Converts an object to and/or from JSON.</summary>
    public sealed class UnknownItemConverter : JsonConverter
    {
        /// <summary>Gets a value indicating whether this <see cref="T:Newtonsoft.Json.JsonConverter"/> can read JSON.</summary>
        /// <value><c>true</c> if this <see cref="T:Newtonsoft.Json.JsonConverter"/> can read JSON; otherwise, <c>false</c>.</value>
        public override bool CanRead
        {
            get
            {
                return true;
            }
        }

        /// <summary>Gets a value indicating whether this <see cref="T:Newtonsoft.Json.JsonConverter"/> can write JSON.</summary>
        /// <value><c>true</c> if this <see cref="T:Newtonsoft.Json.JsonConverter"/> can write JSON; otherwise, <c>false</c>.</value>
        public override bool CanWrite
        {
            get
            {
                return true;
            }
        }

        /// <summary>Determines whether this instance can convert the specified object type.</summary>
        /// <param name="objectType">Type of the object.</param>
        /// <returns><c>true</c> if this instance can convert the specified object type; otherwise, <c>false</c>.</returns>
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(Item);
        }

        /// <summary>Reads the JSON representation of the object.</summary>
        /// <param name="reader">The <see cref="T:Newtonsoft.Json.JsonReader"/> to read from.</param>
        /// <param name="objectType">Type of the object.</param>
        /// <param name="existingValue">The existing value of object being read.</param>
        /// <param name="serializer">The calling serializer.</param>
        /// <returns>The object value.</returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var itemId = serializer.Deserialize<int?>(reader);
            if (!itemId.HasValue)
            {
                return null;
            }

            return new UnknownItem { ItemId = itemId.Value };
        }

        /// <summary>Writes the JSON representation of the object.</summary>
        /// <param name="writer">The <see cref="T:Newtonsoft.Json.JsonWriter"/> to write to.</param>
        /// <param name="value">The value.</param>
        /// <param name="serializer">The calling serializer.</param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, ((Item)value).ItemId);
        }
    }
}