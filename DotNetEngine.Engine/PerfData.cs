using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetEngine.Engine
{
    public class PerftData
    {
        public long TotalCaptures {get; set;}
        public long TotalEnpassants { get; set; }
        public long TotalPromotions { get; set; }
        public long TotalOOCastles {get; set;}
        public long TotalOOOCastles { get; set; }
        public long TotalChecks { get; set; }
    }
}
