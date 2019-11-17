using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace backend.Models
{
    public class Team
    {
        public int? id { get; set; }
        public string Name { get; set; }
        public int? fk_MatchId { get; set; }
    }
}