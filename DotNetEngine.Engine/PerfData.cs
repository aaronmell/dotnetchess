﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetEngine.Engine
{
    internal class PerftData
    {
        internal long TotalCaptures {get; set;}
        internal long TotalEnpassants { get; set; }
        internal long TotalPromotions { get; set; }
        internal long TotalOOCastles {get; set;}
        internal long TotalOOOCastles { get; set; }
        internal long TotalChecks { get; set; }
    }
}
