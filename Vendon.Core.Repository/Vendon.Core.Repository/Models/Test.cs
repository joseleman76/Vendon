using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vendon.Core.Repository.Models
{
    public  class Test
    {
        public string? name { get; set; }
        public int? testId { get; set; }
        public List<Answer>? answers { get; set; }   

    }
}
