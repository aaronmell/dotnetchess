using System.Collections.Generic;

namespace DotNetEngine.Engine
{
	/// <summary>
	/// A utility class used to generate all of the psuedo legal moves a piece can make
	/// </summary>
	internal static class MoveGeneratorUtility
	{  
		internal static void GenerateMoves(this GameState gameState, MoveGenerationMode generationMode, int ply, MoveData moveData)
		{
			var freeSquares = ~gameState.AllPieces;

            if (!gameState.Moves.ContainsKey(ply))
                gameState.Moves.Add(ply, new List<uint>(218));
            else
                gameState.Moves[ply] = new List<uint>(218);

			if (gameState.WhiteToMove)
			{
				GenerateWhitePawnMoves(gameState, moveData, generationMode, freeSquares, ply);
                GenerateKnightMoves(gameState, generationMode, freeSquares, gameState.WhiteKnights, MoveUtility.WhiteKnight, moveData.KnightAttacks, gameState.BlackPieces, ply);
                GenerateRookMoves(gameState, moveData, generationMode, freeSquares, gameState.WhiteRooks, MoveUtility.WhiteRook, gameState.BlackPieces, ply);
                GenerateBishopMoves(gameState, moveData, generationMode, freeSquares, gameState.WhiteBishops, MoveUtility.WhiteBishop, gameState.BlackPieces, ply);
                GenerateQueenMoves(gameState, moveData, generationMode, freeSquares, gameState.WhiteQueens, MoveUtility.WhiteQueen, gameState.BlackPieces, ply);
                GenerateKingMoves(gameState, generationMode, freeSquares, gameState.WhiteKing, 
                    MoveUtility.WhiteKing, moveData.KingAttacks, gameState.BlackPieces, gameState.CurrentWhiteCastleStatus, 
                    MoveUtility.WhiteCastleOOMask, MoveUtility.WhiteCastleOOOMask, moveData.WhiteCastleOOMove, moveData.WhiteCastleOOOMove, ply);
			}
		    else
            {
	            GenerateBlackPawnMoves(gameState, moveData, generationMode, freeSquares, ply);
                GenerateKnightMoves(gameState, generationMode, freeSquares, gameState.BlackKnights, MoveUtility.BlackKnight, moveData.KnightAttacks, gameState.WhitePieces, ply);
                GenerateRookMoves(gameState, moveData, generationMode, freeSquares, gameState.BlackRooks, MoveUtility.BlackRook, gameState.WhitePieces, ply);
                GenerateBishopMoves(gameState, moveData, generationMode, freeSquares, gameState.BlackBishops, MoveUtility.BlackBishop, gameState.WhitePieces, ply);
                GenerateQueenMoves(gameState, moveData, generationMode, freeSquares, gameState.BlackQueens, MoveUtility.BlackQueen, gameState.WhitePieces, ply);
                GenerateKingMoves(gameState, generationMode, freeSquares, gameState.BlackKing,
                  MoveUtility.BlackKing, moveData.KingAttacks, gameState.WhitePieces, gameState.CurrentBlackCastleStatus,
                  MoveUtility.BlackCastleOOMask, MoveUtility.BlackCastleOOOMask, moveData.BlackCastleOOMove, moveData.BlackCastleOOOMove, ply);
            }
		}

        /// <summary>
        /// This method is used to determine if a bitboard is attacked by a piece. 
        /// </summary>        
        /// <param name="checkWhiteAttack">IS true, we are checking white attacks.</param>
        /// <returns>Returns true as soon as an attack is found. Returns false if no attack was found.</returns>
        internal static bool IsBitBoardAttacked(this GameState gamestate, MoveData moveData, ulong targetBoard, bool checkWhiteAttacks)
        {
            if (checkWhiteAttacks)
            {
                while (targetBoard > 0)
                {
                    var targetPiece = targetBoard.GetFirstPieceFromBitBoard();

                    if (((gamestate.WhitePawns & moveData.BlackPawnAttacks[targetPiece]) > 0) ||
                        ((gamestate.WhiteKnights & moveData.KnightAttacks[targetPiece]) > 0) ||
                            ((gamestate.WhiteKing & moveData.KingAttacks[targetPiece]) > 0))
                    {
                        return true;
                    }                   

                    var slidingAttacks = gamestate.WhiteQueens | gamestate.WhiteRooks;

                    if (slidingAttacks > 0)
                    {
                        if (((moveData.RankAttacks[targetPiece][((gamestate.AllPieces & moveData.RankMask[targetPiece]) >> MoveUtility.ShiftedRank[targetPiece])] & slidingAttacks) > 0) ||
                            ((moveData.FileAttacks[targetPiece][((gamestate.AllPieces & moveData.FileMask[targetPiece]) * MoveUtility.FileMagicMultiplication[targetPiece]) >> 57] & slidingAttacks) > 0))
                        {
                            return true;
                        }
                    }

                    var diagonalAttacks = gamestate.WhiteQueens | gamestate.WhiteBishops;

                    if (diagonalAttacks > 0)
                    {
                        if (((moveData.DiagonalA1H8Attacks[targetPiece][((gamestate.AllPieces & moveData.DiagonalA1H8Mask[targetPiece]) * MoveUtility.DiagonalA1H8MagicMultiplication[targetPiece]) >> 57] & diagonalAttacks) > 0) ||
                            ((moveData.DiagonalA8H1Attacks[targetPiece][((gamestate.AllPieces & moveData.DiagonalA8H1Mask[targetPiece]) * MoveUtility.DiagonalA8H1MagicMultiplication[targetPiece]) >> 57] & diagonalAttacks) > 0))
                        {
                            return true;
                        }
                    }

                    targetBoard ^= MoveUtility.BitStates[targetPiece];
                }
            }
            else
            {
                while (targetBoard > 0)
                {
                    var targetPiece = targetBoard.GetFirstPieceFromBitBoard();

                    if (((gamestate.BlackPawns & moveData.WhitePawnAttacks[targetPiece]) > 0) ||
                        ((gamestate.BlackKnights & moveData.KnightAttacks[targetPiece]) > 0) ||
                            ((gamestate.BlackKing & moveData.KingAttacks[targetPiece]) > 0))
                    {
                        return true;
                    }

                    var slidingAttacks = gamestate.BlackQueens | gamestate.BlackRooks;

                    if (slidingAttacks > 0)
                    {
                        if (((moveData.RankAttacks[targetPiece][((gamestate.AllPieces & moveData.RankMask[targetPiece]) >> MoveUtility.ShiftedRank[targetPiece])] & slidingAttacks) > 0) ||
                            ((moveData.FileAttacks[targetPiece][((gamestate.AllPieces & moveData.FileMask[targetPiece]) * MoveUtility.FileMagicMultiplication[targetPiece]) >> 57] & slidingAttacks) > 0))
                        {
                            return true;
                        }
                    }

                    var diagonalAttacks = gamestate.BlackQueens | gamestate.BlackBishops;

                    if (diagonalAttacks > 0)
                    {
                        if (((moveData.DiagonalA1H8Attacks[targetPiece][((gamestate.AllPieces & moveData.DiagonalA1H8Mask[targetPiece]) * MoveUtility.DiagonalA1H8MagicMultiplication[targetPiece]) >> 57] & diagonalAttacks) > 0) ||
                            ((moveData.DiagonalA8H1Attacks[targetPiece][((gamestate.AllPieces & moveData.DiagonalA8H1Mask[targetPiece]) * MoveUtility.DiagonalA8H1MagicMultiplication[targetPiece]) >> 57] & diagonalAttacks) > 0))
                        {
                            return true;
                        }
                    }

                    targetBoard ^= MoveUtility.BitStates[targetPiece];
                }
            }

            return false;
        }

        private static void GenerateKingMoves(GameState gameState, MoveGenerationMode generationMode, ulong freeSquares, 
            ulong kingBoard, uint movingPiece, ulong[] attackSquares, ulong attackedBoard, int castleStatus,ulong castleMaskOO, 
            ulong castleMaskOOO, uint castleOOMove, uint castleOOOMove, int ply)
        {
            var move = 0U.SetMovingPiece(movingPiece);

            while (kingBoard > 0)
            {
                var fromSquare = kingBoard.GetFirstPieceFromBitBoard();
                move = move.SetFromMove(fromSquare);

                var kingMoves = 0UL;

                if (generationMode != MoveGenerationMode.CaptureMovesOnly)
                {
                    kingMoves = attackSquares[fromSquare] & freeSquares;

                    if (castleStatus.CanCastleOO())
                    {
                        if ((castleMaskOO & gameState.AllPieces) == 0)
                        {
                            gameState.Moves[ply].Add(castleOOMove);
                        }
                    }

                    if (castleStatus.CanCastleOOO())
                    {
                        if ((castleMaskOOO & gameState.AllPieces) == 0)
                        {
                            gameState.Moves[ply].Add(castleOOOMove);
                        }
                    }
                }

                if (generationMode == MoveGenerationMode.CaptureMovesOnly || generationMode == MoveGenerationMode.All)
                {
                    kingMoves |= attackSquares[fromSquare] & attackedBoard;
                }

                while (kingMoves > 0)
                {
                    uint toSquare = kingMoves.GetFirstPieceFromBitBoard();
                    move = move.SetToMove(toSquare);
                    move = move.SetCapturedPiece(gameState.BoardArray[toSquare]);
                    gameState.Moves[ply].Add(move);
                    kingMoves ^= MoveUtility.BitStates[toSquare];
                }
                kingBoard ^= MoveUtility.BitStates[fromSquare];
            }
        }
       
        private static void GenerateQueenMoves(GameState gameState, MoveData moveData, MoveGenerationMode generationMode, ulong freeSquares, ulong queenBoard, uint movingPiece, ulong attackedBoard, int ply)
        {
            var move = 0U.SetMovingPiece(movingPiece);

            while (queenBoard > 0)
            {
                uint fromSquare = queenBoard.GetFirstPieceFromBitBoard();
                move = move.SetFromMove(fromSquare);

                var queenMoves = 0UL;

                if (generationMode != MoveGenerationMode.CaptureMovesOnly)
                {
                    queenMoves = moveData.GetQueenMoves(fromSquare, gameState.AllPieces, freeSquares) & freeSquares;
                }

                if (generationMode == MoveGenerationMode.CaptureMovesOnly || generationMode == MoveGenerationMode.All)
                {
                    queenMoves |= moveData.GetQueenMoves(fromSquare, gameState.AllPieces, attackedBoard) & attackedBoard;
                }

                while (queenMoves > 0)
                {
                    uint toSquare = queenMoves.GetFirstPieceFromBitBoard();
                    move = move.SetToMove(toSquare);
                    move = move.SetCapturedPiece(gameState.BoardArray[toSquare]);
                    gameState.Moves[ply].Add(move);
                    queenMoves ^= MoveUtility.BitStates[toSquare];
                }
                queenBoard ^= MoveUtility.BitStates[fromSquare];
            }
        }

        private static void GenerateBishopMoves(GameState gameState, MoveData moveData, MoveGenerationMode generationMode, ulong freeSquares, ulong bishopBoard, uint movingPiece, ulong attackedBoard, int ply)
        {
            var move = 0U.SetMovingPiece(movingPiece);

            while (bishopBoard > 0)
            {
                uint fromSquare = bishopBoard.GetFirstPieceFromBitBoard();
                move = move.SetFromMove(fromSquare);

                var bishopMoves = 0UL;

                if (generationMode != MoveGenerationMode.CaptureMovesOnly)
                {
                    bishopMoves = moveData.GetBishopMoves(fromSquare, gameState.AllPieces, freeSquares) & freeSquares;
                }

                if (generationMode == MoveGenerationMode.CaptureMovesOnly || generationMode == MoveGenerationMode.All)
                {
                    bishopMoves |= moveData.GetBishopMoves(fromSquare, gameState.AllPieces, attackedBoard) & attackedBoard;
                }

                while (bishopMoves > 0)
                {
                    uint toSquare = bishopMoves.GetFirstPieceFromBitBoard();
                    move = move.SetToMove(toSquare);
                    move = move.SetCapturedPiece(gameState.BoardArray[toSquare]);
                    gameState.Moves[ply].Add(move);
                    bishopMoves ^= MoveUtility.BitStates[toSquare];
                }
                bishopBoard ^= MoveUtility.BitStates[fromSquare];
            }
        }

        private static void GenerateRookMoves(GameState gameState, MoveData moveData, MoveGenerationMode generationMode, ulong freeSquares, ulong rookBoard, uint movingPiece, ulong attackedBoard, int ply)
        {
            var move = 0U.SetMovingPiece(movingPiece);

            while (rookBoard > 0)
            {
                uint fromSquare = rookBoard.GetFirstPieceFromBitBoard();
                move = move.SetFromMove(fromSquare);

                var rookMoves = 0UL;

                if (generationMode != MoveGenerationMode.CaptureMovesOnly)
                {
                    rookMoves = moveData.GetRookMoves(fromSquare, gameState.AllPieces, freeSquares) & freeSquares;
                }

                if (generationMode == MoveGenerationMode.CaptureMovesOnly || generationMode == MoveGenerationMode.All)
                {
                    rookMoves |= moveData.GetRookMoves(fromSquare, gameState.AllPieces, attackedBoard) & attackedBoard;
                }

                while (rookMoves > 0)
                {
                    uint toSquare = rookMoves.GetFirstPieceFromBitBoard();
                    move = move.SetToMove(toSquare);
                    move = move.SetCapturedPiece(gameState.BoardArray[toSquare]);
                    gameState.Moves[ply].Add(move);
                    rookMoves ^= MoveUtility.BitStates[toSquare];
                }
                rookBoard ^= MoveUtility.BitStates[fromSquare];
            }       
        }

        private static void GenerateKnightMoves(GameState gameState, MoveGenerationMode generationMode, ulong freeSquares, ulong knightBoard, uint movingPiece, ulong[] attackSquares, ulong attackedBoard, int ply)
        {           
            var move = 0U.SetMovingPiece(movingPiece);

            while (knightBoard > 0)
            {
                uint fromSquare = knightBoard.GetFirstPieceFromBitBoard();
                move = move.SetFromMove(fromSquare);
               
                var knightMoves = 0UL;

                if (generationMode != MoveGenerationMode.CaptureMovesOnly)
                {
                    knightMoves = attackSquares[fromSquare] & freeSquares;
                }

                if (generationMode == MoveGenerationMode.CaptureMovesOnly || generationMode == MoveGenerationMode.All)
                {
                    knightMoves |= attackSquares[fromSquare] & attackedBoard;
                }

                while (knightMoves > 0)
                {
                    uint toSquare = knightMoves.GetFirstPieceFromBitBoard();
                    move = move.SetToMove(toSquare);
                    move = move.SetCapturedPiece(gameState.BoardArray[toSquare]);
                    gameState.Moves[ply].Add(move);
                    knightMoves ^= MoveUtility.BitStates[toSquare];
                }
                knightBoard ^= MoveUtility.BitStates[fromSquare];
            }           
        }

        private static void GenerateWhitePawnMoves(GameState gameState, MoveData moveData, MoveGenerationMode generationMode, ulong freeSquares, int ply)
        {   
            var pawnBoard = gameState.WhitePawns;
            var move = 0U.SetMovingPiece(MoveUtility.WhitePawn);

            while (pawnBoard > 0)
            {
                uint fromSquare = pawnBoard.GetFirstPieceFromBitBoard();
                move = move.SetFromMove(fromSquare);

                var pawnMoves = 0UL;
                
                //Single and double pawn pushes, no promotions
                if (generationMode != MoveGenerationMode.CaptureMovesOnly)
                {
                    //Normal Moves
                    pawnMoves |= moveData.WhitePawnMoves[fromSquare] & freeSquares;

                    //Double Moves
                    if (MoveUtility.Ranks[fromSquare] == 2 && pawnMoves > 0)
                    {
                        pawnMoves |= moveData.WhitePawnDoubleMoves[fromSquare] & freeSquares;
                    }
                }

                //Standard Captures.
                if (generationMode == MoveGenerationMode.CaptureMovesOnly || generationMode == MoveGenerationMode.All)
                {
                    //Captures
                    pawnMoves |= moveData.WhitePawnAttacks[fromSquare] & gameState.BlackPieces;

                    if (gameState.EnpassantTargetSquare > 0)
                    {
                        if ((moveData.WhitePawnAttacks[fromSquare] & MoveUtility.BitStates[gameState.EnpassantTargetSquare]) == MoveUtility.BitStates[gameState.EnpassantTargetSquare])
                        {
                            if ((gameState.BlackPawns & MoveUtility.BitStates[gameState.EnpassantTargetSquare - 8]) == MoveUtility.BitStates[gameState.EnpassantTargetSquare - 8])
                            {
                                move = move.SetPromotionPiece(MoveUtility.WhitePawn);
                                move = move.SetCapturedPiece(MoveUtility.BlackPawn);
                                move = move.SetToMove(gameState.EnpassantTargetSquare);
                                gameState.Moves[ply].Add(move);
                            }
                        }
                    }
                }   

                while (pawnMoves > 0)
                {
                    uint toSquare = pawnMoves.GetFirstPieceFromBitBoard();
                    move = move.SetToMove(toSquare);
                    move = move.SetCapturedPiece(gameState.BoardArray[toSquare]);
                   
                    if (MoveUtility.Ranks[toSquare] == 8)
                    {                        
                        //Knights and Queens are good enough for now for all generation types
                        move = move.SetPromotionPiece(MoveUtility.WhiteQueen);
                        gameState.Moves[ply].Add(move);

                        move = move.SetPromotionPiece(MoveUtility.WhiteKnight);
                        gameState.Moves[ply].Add(move);

                        //A queen is always preferable to a rook and bishop, so no need to generate these, unless we have a reason to generate all pseudo legal moves
                        if (generationMode == MoveGenerationMode.All)
                        {
                            move = move.SetPromotionPiece(MoveUtility.WhiteRook);
                            gameState.Moves[ply].Add(move);

                            move = move.SetPromotionPiece(MoveUtility.WhiteBishop);
                            gameState.Moves[ply].Add(move);
                        }  

                        //Reset it back to empty so it doesn't screw up the next piece!
                        move = move.SetPromotionPiece(MoveUtility.Empty);                        
                    }
                    else
                    {
                        gameState.Moves[ply].Add(move);
                    }

                    //Remove the bit we just processed from the board
                    pawnMoves ^= MoveUtility.BitStates[toSquare];
                } 
                pawnBoard ^= MoveUtility.BitStates[fromSquare];
                move = move.SetPromotionPiece(MoveUtility.Empty);
            }
        }

        private static void GenerateBlackPawnMoves(GameState gameState, MoveData moveData, MoveGenerationMode generationMode, ulong freeSquares, int ply)
        {
            var pawnBoard = gameState.BlackPawns;
            var move = 0U.SetMovingPiece(MoveUtility.BlackPawn);

            while (pawnBoard > 0)
            {
                uint fromSquare = pawnBoard.GetFirstPieceFromBitBoard();
                move = move.SetFromMove(fromSquare);

                var pawnMoves = 0UL;

                //Single and double pawn pushes, no promotions
                if (generationMode != MoveGenerationMode.CaptureMovesOnly)
                {
                    //Normal Moves
                    pawnMoves |= moveData.BlackPawnMoves[fromSquare] & freeSquares;

                    //Double Moves
                    if (MoveUtility.Ranks[fromSquare] == 7 && pawnMoves > 0)
                    {
                        pawnMoves |= moveData.BlackPawnDoubleMoves[fromSquare] & freeSquares;
                    }
                }

                //Standard Captures.
                if (generationMode == MoveGenerationMode.CaptureMovesOnly || generationMode == MoveGenerationMode.All)
                {
                    //Captures
                    pawnMoves |= moveData.BlackPawnAttacks[fromSquare] & gameState.WhitePieces;

                    if (gameState.EnpassantTargetSquare > 0)
                    {
                        if ((moveData.BlackPawnAttacks[fromSquare] & MoveUtility.BitStates[gameState.EnpassantTargetSquare]) == MoveUtility.BitStates[gameState.EnpassantTargetSquare])
                        {
                            if ((gameState.WhitePawns & MoveUtility.BitStates[gameState.EnpassantTargetSquare + 8]) == MoveUtility.BitStates[gameState.EnpassantTargetSquare + 8])
                            {
                                move = move.SetPromotionPiece(MoveUtility.BlackPawn);
                                move = move.SetCapturedPiece(MoveUtility.WhitePawn);
                                move = move.SetToMove(gameState.EnpassantTargetSquare);
                                gameState.Moves[ply].Add(move);
                            }
                        }
                    }
                }

                while (pawnMoves > 0)
                {
                    uint toSquare = pawnMoves.GetFirstPieceFromBitBoard();
                    move = move.SetToMove(toSquare);
                    move = move.SetCapturedPiece(gameState.BoardArray[toSquare]);

                    if (MoveUtility.Ranks[toSquare] == 1)
                    {
                        //Knights and Queens are good enough for now for all generation types
                        move = move.SetPromotionPiece(MoveUtility.BlackQueen);
                        gameState.Moves[ply].Add(move);

                        move = move.SetPromotionPiece(MoveUtility.BlackKnight);
                        gameState.Moves[ply].Add(move);

                        //A queen is always preferable to a rook and bishop, so no need to generate these, unless we have a reason to generate all pseudo legal moves
                        if (generationMode == MoveGenerationMode.All)
                        {
                            move = move.SetPromotionPiece(MoveUtility.BlackBishop);
                            gameState.Moves[ply].Add(move);

                            move = move.SetPromotionPiece(MoveUtility.BlackRook);
                            gameState.Moves[ply].Add(move);
                        }

                        //Reset it back to empty so it doesn't screw up the next piece!
                        move = move.SetPromotionPiece(MoveUtility.Empty);
                    }
                    else
                    {
                        gameState.Moves[ply].Add(move);
                    }

                    //Remove the bit we just processed from the board
                    pawnMoves ^= MoveUtility.BitStates[toSquare];
                }
                pawnBoard ^= MoveUtility.BitStates[fromSquare];
                move = move.SetPromotionPiece(MoveUtility.Empty);
            }
        }
    }
}
