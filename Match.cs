using System;
using System.Collections.Generic;
using System.Text;

namespace ScrapeTest
{
    public class Match
    {
        public string Event_id { get; set; }

        public string Hjemmehold { get; set; }

        public string Udehold { get; set; }

        public string Odds_1 { get; set; }

        public string Odds_x { get; set; }

        public string Odds_2 { get; set; }

        public string Kickoff { get; set; }
        
        public string Marked_tekst { get; set; }

        public string Hjemmescore { get; set; }

        public string Udescore { get; set; }

        public Result Result { get; set; }
    }
}
