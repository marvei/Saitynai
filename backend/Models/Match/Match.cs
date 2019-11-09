﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace backend.Models.Match
{
    public class Match
    {
        String name;

        public String Name
        {
            get { return name; }
            set { name = value; }
        }

        int date;

        public int Date
        {
            get { return date; }
            set { date = value; }
        }

        int matchId;

        public int MatchId
        {
            get { return matchId; }
            set { matchId = value; }
        }
    }
}