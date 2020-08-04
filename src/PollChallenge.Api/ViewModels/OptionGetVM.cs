using NJsonSchema.Annotations;
using System;
using System.Text.Json.Serialization;

namespace PollChallenge.Api.ViewModels
{
    /// <summary>
    /// Poll Option Registration Information
    /// </summary>
    public struct OptionGetVM
    {
        /// <summary>
        /// Poll Option Id in the Database
        /// </summary>
        [JsonSchemaExtensionData("example", 10)]
        [JsonPropertyName("option_id")]
        public int Id { get; set; }

        /// <summary>
        /// Poll Option Description
        /// </summary>
        [JsonSchemaExtensionData("example", "12 hours")]
        [JsonPropertyName("option_description")]
        public string Description { get; set; }

        public override bool Equals(object obj)
            => obj is OptionGetVM vM &&
                   Id == vM.Id &&
                   Description == vM.Description;
        public override int GetHashCode()
            => HashCode.Combine(Id, Description);
    }
}
