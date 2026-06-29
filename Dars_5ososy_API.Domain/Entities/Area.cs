using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Dars_5ososy_API.Domain.Entities
{
    public class Area
    {
        public long Id { get; set; }
        public string ArabicName { get; set; }
        public string EnglishName { get; set; }

        public long GovernorateId { get; set; }
        [JsonIgnore]
        [ForeignKey(nameof(GovernorateId))]
        public Governorate Governorate { get; set; }
    }
}
