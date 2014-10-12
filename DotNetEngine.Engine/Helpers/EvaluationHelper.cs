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
        private const int DrawScore = 0;

        internal static int Evaluate(this GameState gameState)
        {
            var whitePawnCount = GetBitCount(gameState.WhitePawns);
            var whiteKnightCount = GetBitCount(gameState.WhiteKnights);
            var whiteBishopCount = GetBitCount(gameState.WhiteBishops);
            var whiteRooksCount = GetBitCount(gameState.WhiteRooks);
            var whiteQueenCount = GetBitCount(gameState.WhiteQueens);

            var whiteTotalPieces = whitePawnCount + whiteKnightCount + whiteRooksCount + whiteQueenCount;
            var whiteMinorPieces = whiteKnightCount + whiteBishopCount;

            var blackPawnsCount = GetBitCount(gameState.BlackPawns);
            var blackKnightsCount = GetBitCount(gameState.BlackKnights);
            var blackBishopsCount = GetBitCount(gameState.BlackBishops);
            var blackRooksCount = GetBitCount(gameState.BlackRooks);
            var blackQueensCount = GetBitCount(gameState.BlackQueens);

            var blackTotalPieces = blackPawnsCount + blackKnightsCount + blackBishopsCount + blackQueensCount;
            var blackMinorPieces = blackKnightsCount + blackBishopsCount;

            //Insufficent Material Checks
            if (whitePawnCount == 0 && blackPawnsCount == 0)
            {
                if (whiteTotalPieces < 3 && whiteRooksCount == 0 && whiteQueenCount == 0 && blackTotalPieces < 3 &&
                    blackRooksCount == 0 && blackQueensCount == 0)
                {
                    
                    if (whiteMinorPieces <= 1 && blackMinorPieces <= 1)
                        return DrawScore;

                    if ((whiteMinorPieces == 2 && whiteBishopCount < 2 && blackMinorPieces == 1) ||
                        (blackMinorPieces == 2 && blackBishopsCount < 2 && whiteMinorPieces == 1))
                        return DrawScore;
                }
            }

             var materialScore = ((whitePawnCount * PawnWeight) + (whiteKnightCount * KnightWeight) + (whiteBishopCount * BishopWeight) + (whiteRooksCount * RookWeight) + (whiteQueenCount * QueenWeight)) -
                ((blackPawnsCount * PawnWeight) - (blackKnightsCount * KnightWeight) - (blackBishopsCount * BishopWeight) - (blackRooksCount * RookWeight)- (blackQueensCount * QueenWeight));

            return materialScore;

        }


        private static int GetBitCount(ulong i)
        {
            i = i - ((i >> 1) & 0x5555555555555555UL);
            i = (i & 0x3333333333333333UL) + ((i >> 2) & 0x3333333333333333UL);
            return (int)(unchecked(((i + (i >> 4)) & 0xF0F0F0F0F0F0F0FUL) * 0x101010101010101UL) >> 56);
        }
    }
}
