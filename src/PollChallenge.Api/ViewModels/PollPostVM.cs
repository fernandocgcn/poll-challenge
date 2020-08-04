using NJsonSchema.Annotations;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;

namespace PollChallenge.Api.ViewModels
{
    /// <summary>
    /// Poll Registration Information
    /// </summary>
    public struct PollPostVM
    {
        /// <summary>
        /// Poll Description
        /// </summary>
        [JsonSchemaExtensionData("example", "How many hours a day do you use social media?")]
        [JsonPropertyName("poll_description")]
        [Required(AllowEmptyStrings = false)]
        public string Description { get; set; }

        /// <summary>
        /// Poll Option Description
        /// </summary>
        [JsonSchemaExtensionData("example", new string[] { "8 hours", "12 hours", "16 hours" })]
        [JsonPropertyName("options")]
        public ICollection<string> Options { get; set; }

        [JsonIgnore]
        [DisplayName(nameof(Options))]
        [Required]
        public string HasOptions 
            => Options?.Count > 0 && Options.All(s => !string.IsNullOrEmpty(s)) 
                ? nameof(Options) : null;
    }
}
