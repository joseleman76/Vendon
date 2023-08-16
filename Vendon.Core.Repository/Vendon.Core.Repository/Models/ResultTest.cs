using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vendon.Core.Repository.Models
{
    public class ResultTest
    {
        public string? id { get; set; }
        public string? name { get; set; }
        public int? testId { get; set; }
        public int? answersCount { get; set; }
        public List<int>? reponsesOk { get; set; }
    }
}
