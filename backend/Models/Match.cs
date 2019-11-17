using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace backend.Models
{
    public class Match
    {
        public int? id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public int? fk_TournamentId { get; set; }
    }
}