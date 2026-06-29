using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Dars_5ososy_API.Domain.Entities
{
    public class Governorate
    {
        public long Id { get; set; }
        public string ArabicName { get; set; }
        public string EnglishName { get; set; }

        [ForeignKey("Province")]
        public long ProvinceId { get; set; }

        [JsonIgnore]
        [ForeignKey(nameof(ProvinceId))]
        public Province Province { get; set; }
    }
}
