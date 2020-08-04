using NJsonSchema.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace PollChallenge.Api.ViewModels
{
    /// <summary>
    /// Poll Registration Information
    /// </summary>
    public struct PollGetVM
    {
        /// <summary>
        /// Poll Id in the Database
        /// </summary>
        [JsonSchemaExtensionData("example", 10)]
        [JsonPropertyName("poll_id")]
        public int Id { get; set; }

        /// <summary>
        /// Poll Description
        /// </summary>
        [JsonSchemaExtensionData("example", "How many hours a day do you use social media?")]
        [JsonPropertyName("poll_description")]
        public string Description { get; set; }

        [JsonPropertyName("options")]
        public ICollection<OptionGetVM> Options { get; set; }

        public override bool Equals(object obj)
            => obj is PollGetVM vM &&
                   Id == vM.Id &&
                   Description == vM.Description &&
                   Enumerable.SequenceEqual(Options, vM.Options);
        public override int GetHashCode()
            => HashCode.Combine(Id, Description, Options);
    }
}
