﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchMate.Class
{
    public class Matched
    {
        public Place? Place { get; set; }

        public List<List<People>>? MatchedPerson { get; set; }
    }
}
