using System.Data;
using DotNetEngine.Engine.Objects;

namespace DotNetEngine.Engine.Helpers
{
    /// <summary>
    /// Contains all of the functions required to evaluate a given board.
    /// </summary>
    internal static class EvaluationHelper
    {
        private const int PawnWeight = 100;
        private const int KnightWeight = 350;
        private const int BishopWeight = 350;
        private const int RookWeight = 525;
        private const int QueenWeight = 1000;
        private const int KingWeight = 10000;

        internal static int Evaluate(this GameState gameState)
        {
              
            //Evaluate Material
            return EvaluateMaterial(gameState);
            //Evaluate Mobility
         }

        private static int EvaluateMaterial(GameState gameState)
        {
            var whitePawnsScore = GetBitCount(gameState.WhitePawns)*PawnWeight;
            var whiteKnightsScore = GetBitCount(gameState.WhiteKnights)*KnightWeight;
            var whiteBishopsScore = GetBitCount(gameState.WhiteBishops)*BishopWeight;
            var whiteRooksScore = GetBitCount(gameState.WhiteRooks)*RookWeight;
            var whiteQueensScore = GetBitCount(gameState.WhiteQueens)*QueenWeight;
            var whiteKingScore = GetBitCount(gameState.WhiteKing)*KingWeight;

            var blackPawnsScore = GetBitCount(gameState.BlackPawns) * PawnWeight;
            var blackKnightsScore = GetBitCount(gameState.BlackKnights) * KnightWeight;
            var blackBishopsScore = GetBitCount(gameState.BlackBishops) * BishopWeight;
            var blackRooksScore = GetBitCount(gameState.BlackRooks) * RookWeight;
            var blackQueensScore = GetBitCount(gameState.BlackQueens) * QueenWeight;
            var blackKingScore = GetBitCount(gameState.BlackKing) * KingWeight;

            return (whitePawnsScore + whiteKnightsScore + whiteBishopsScore + whiteRooksScore + whiteQueensScore +
                    whiteKingScore) +
                   (blackPawnsScore - blackKnightsScore - blackBishopsScore - blackRooksScore - blackQueensScore -
                    blackKingScore);
        }



        private static int GetBitCount(ulong i)
        {
            i = i - ((i >> 1) & 0x5555555555555555UL);
            i = (i & 0x3333333333333333UL) + ((i >> 2) & 0x3333333333333333UL);
            return (int)(unchecked(((i + (i >> 4)) & 0xF0F0F0F0F0F0F0FUL) * 0x101010101010101UL) >> 56);
        }
    }
}
