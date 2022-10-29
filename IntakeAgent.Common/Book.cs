using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace IntakeAgent.Common
{
    public class Book : IJsonOnDeserialized
    {
        [JsonPropertyName("@id")]
        public string ID { get; set; }

        [JsonPropertyName("author")]
        public string Author { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("genre")]
        public string Genre { get; set; }

        [JsonPropertyName("price")]
        public float Price { get; set; }

        [JsonPropertyName("publish_date")]
        public DateTime PublishDate { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        const string  ERROR_FORMAT = "Book \"{0}\" ID '{1}' failed to read property '{2}' from file";
        public void OnDeserialized()
        {
            if (ID == default) { throw new ModelParseException(string.Format(ERROR_FORMAT, Title, ID, "@id")); }
            if (Author == default) { throw new ModelParseException(string.Format(ERROR_FORMAT, Title, ID, "author")); }
            if (Title == default) { throw new ModelParseException(string.Format(ERROR_FORMAT, Title, ID, "title")); }
            if (Genre == default) { throw new ModelParseException(string.Format(ERROR_FORMAT, Title, ID, "genre")); }
            if (Price == default) { throw new ModelParseException(string.Format(ERROR_FORMAT, Title, ID, "price")); }
            if (PublishDate == default) { throw new ModelParseException(string.Format(ERROR_FORMAT, Title, ID, "publish_date")); }
            if (Description == default) { throw new ModelParseException(string.Format(ERROR_FORMAT, Title, ID, "description")); }
        }
    }
}
