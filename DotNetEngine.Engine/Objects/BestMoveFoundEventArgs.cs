using System;

namespace DotNetEngine.Engine.Objects
{
    /// <summary>
    /// An event that is raised by the engine when the best move has been found
    /// </summary>
    public class BestMoveFoundEventArgs : EventArgs
    {
        /// <summary>
        /// The move the engine has selected as its best move
        /// </summary>
        public string BestMove { get; set; }
    }
}
