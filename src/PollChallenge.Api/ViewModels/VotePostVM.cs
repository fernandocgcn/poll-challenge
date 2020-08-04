using NJsonSchema.Annotations;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PollChallenge.Api.ViewModels
{
    /// <summary>
    /// Vote Registration Information
    /// </summary>
    public struct VotePostVM
    {
        /// <summary>
        /// Poll Option Id in the Database
        /// </summary>
        [JsonSchemaExtensionData("example", 10)]
        [JsonPropertyName("option_id")]
        [Required]
        public int Id { get; set; }
    }
}
