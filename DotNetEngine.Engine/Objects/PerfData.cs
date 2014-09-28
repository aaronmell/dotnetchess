namespace DotNetEngine.Engine.Objects
{
    /// <summary>
    /// An object that contains Perft Related Test Data
    /// </summary>
    internal class PerftData
    {
        /// <summary>
        /// The total number of captures made
        /// </summary>
        internal long TotalCaptures {get; set;}
        /// <summary>
        /// The total number of enpassants
        /// </summary>
        internal long TotalEnpassants { get; set; }
        /// <summary>
        /// The total number of promotions
        /// </summary>
        internal long TotalPromotions { get; set; }
        /// <summary>
        /// The total number of OO Castles
        /// </summary>
        internal long TotalOOCastles {get; set;}
        /// <summary>
        /// The total number of OOO castles
        /// </summary>
        internal long TotalOOOCastles { get; set; }
        /// <summary>
        /// The total number of checks
        /// </summary>
        internal long TotalChecks { get; set; }
    }
}
