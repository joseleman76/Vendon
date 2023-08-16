using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Vendon.Core.Repository.Models
{
    public class Answer
    {
        public string? answer { get; set; }

        [JsonIgnore]
        public int? correctResponse { get; set; }

        public List<string>? responses { get; set; } 

    }
}
