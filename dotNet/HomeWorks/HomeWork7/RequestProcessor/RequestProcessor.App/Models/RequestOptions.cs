using System;
using System.Text.Json.Serialization;

namespace RequestProcessor.App.Models
{
    class RequestOptions : IRequestOptions, IResponseOptions
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("address")]
        public string Address { get; set; }

        [JsonPropertyName("method")]
        public string RequestMethodAsString { get; set; }

        [JsonPropertyName("contentType")]
        public string ContentType { get; set; }

        [JsonPropertyName("body")]
        public string Body { get; set; }

        [JsonPropertyName("path")]
        public string Path { get; set; }
        public RequestMethod Method { get; set; }

        public bool IsValid { get 
            {
                return Path != null && RequestMethodAsString != null && Address != null;
            }  
        }
    }
}
