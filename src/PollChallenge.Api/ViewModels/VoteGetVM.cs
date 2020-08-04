using NJsonSchema.Annotations;
using System;
using System.Text.Json.Serialization;

namespace PollChallenge.Api.ViewModels
{
    /// <summary>
    /// Votes Registration Information
    /// </summary>
    public struct VoteGetVM
    {
        /// <summary>
        /// Poll Option Id in the Database
        /// </summary>
        [JsonSchemaExtensionData("example", 10)]
        [JsonPropertyName("option_id")]
        public int Id { get; set; }

        /// <summary>
        /// Votes Quantity
        /// </summary>
        [JsonSchemaExtensionData("example", "35")]
        [JsonPropertyName("qty")]
        public long VotesQty { get; set; }

        public override bool Equals(object obj)
            => obj is VoteGetVM vM &&
                   Id == vM.Id &&
                   VotesQty == vM.VotesQty;
        public override int GetHashCode() 
            => HashCode.Combine(Id, VotesQty);
    }
}
