using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetEngine.Engine.Objects
{
    /// <summary>
    /// An Entry in the Transpostion Table
    /// </summary>
    internal class TranspositionEntry
    {
        /// <summary>
        /// The best move
        /// </summary>
        internal uint Move { get; set; }
        /// <summary>
        /// The best score
        /// </summary>
        internal int Score { get; set; }
        /// <summary>
        /// The depth this entry was added at
        /// </summary>
        internal int Depth { get; set; }
        /// <summary>
        /// The type of result 0 = Exact, 1 = LowerBound, 2 = UpperBound
        /// </summary>
        internal int Flag { get; set; }
    }
}
