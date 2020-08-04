using NJsonSchema.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace PollChallenge.Api.ViewModels
{
    /// <summary>
    /// Poll Stats Registration Information
    /// </summary>
    public struct StatGetVM
    {
        /// <summary>
        /// Poll Views Quantity
        /// </summary>
        [JsonSchemaExtensionData("example", 125)]
        [JsonPropertyName("views")]
        public long ViewsQty { get; set; }

        [JsonPropertyName("votes")]
        public ICollection<VoteGetVM> Options { get; set; }

        public override bool Equals(object obj)
            => obj is StatGetVM vM &&
                   ViewsQty == vM.ViewsQty &&
                   Enumerable.SequenceEqual(Options, vM.Options);
        public override int GetHashCode()
            => HashCode.Combine(ViewsQty, Options);
    }
}
