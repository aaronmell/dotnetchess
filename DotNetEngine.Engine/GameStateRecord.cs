namespace DotNetEngine.Engine
{
    /// <summary>
    /// A Record of GameState Information. This struct contains enough information to unmake a move
    /// </summary>
    internal struct GameStateRecord
    {   
        /// <summary>
        /// The move associated with this gameState
        /// </summary>
        internal uint Move { get; set; }

        /// <summary>
        /// The current wite castle status of this game state
        /// </summary>
        internal int CurrentWhiteCastleStatus { get; set; }

        /// <summary>
        /// The current black castle status of this game state
        /// </summary>
        internal int CurrentBlackCastleStatus { get; set; }

        /// <summary>
        /// The number of half moves currently made with no capture, pawn or king pieces moving
        /// </summary>
        internal int FiftyMoveRuleCount { get; set; }

        /// <summary>
        /// The target square of a pawn that can be captured via an en-passant
        /// </summary>
        internal uint EnpassantTargetSquare { get; set; }
    }
}
