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
            ulong targetBitmap;
            var moves = new List<ulong>(64);
            var freeSquares = ~gameState.AllPieces;
            gameState.Moves.Add(ply, new List<uint>(64));

			if (gameState.WhiteToMove)
			{
                targetBitmap = gameState.WhitePieces;
                GenerateWhitePawnMoves(gameState, moveData, generationMode, freeSquares, ply);
                GenerateKnightMoves(gameState, generationMode, freeSquares, gameState.WhiteKnights, MoveUtility.WhiteKnight, moveData.KnightAttacks, gameState.BlackPieces, ply);
                GenerateRookMoves(gameState, moveData, generationMode, freeSquares, gameState.WhiteRooks, MoveUtility.WhiteRook, gameState.BlackPieces, ply);
                GenerateBishopMoves(gameState, moveData, generationMode, freeSquares, gameState.WhiteBishops, MoveUtility.WhiteBishop, gameState.BlackPieces, ply);
			}
		    else
            {
                targetBitmap = gameState.BlackPieces;
                GenerateBlackPawnMoves(gameState, moveData, generationMode, freeSquares, ply);
                GenerateKnightMoves(gameState, generationMode, freeSquares, gameState.BlackKnights, MoveUtility.BlackKnight, moveData.KnightAttacks, gameState.WhitePieces, ply);
                GenerateRookMoves(gameState, moveData, generationMode, freeSquares, gameState.BlackRooks, MoveUtility.BlackRook, gameState.WhitePieces, ply);
                GenerateBishopMoves(gameState, moveData, generationMode, freeSquares, gameState.BlackBishops, MoveUtility.BlackBishop, gameState.WhitePieces, ply);
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
