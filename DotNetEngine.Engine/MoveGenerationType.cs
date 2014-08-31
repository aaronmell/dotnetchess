using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetEngine.Engine
{
    public enum MoveGenerationMode
    {
        /// <summary>
        /// Generates all pseudo-legal moves that are captures and queen promotions
        /// </summary>
        CaptureMovesOnly,
        /// <summary>
        /// Generates all pseudo-legal moves that are non-captures and underpromotions
        /// </summary>
        QuietMovesOnly,
        /// <summary>
        /// Generates all pseudo-legal captures and non-captures. 
        /// </summary>
        All
    }
}
