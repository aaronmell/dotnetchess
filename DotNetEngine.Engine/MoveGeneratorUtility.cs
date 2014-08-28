using System.Collections.Generic;

namespace DotNetEngine.Engine
{
	/// <summary>
	/// A utility class used to generate all of the psuedo legal moves a piece can make
	/// </summary>
	internal static class MoveGeneratorUtility
	{
		internal static void GenerateMoves(this GameState gameState, int ply, MoveData moveData)
		{
            ulong targetBitmap;
            var moves = new List<ulong>(64);
            var freeSquares = ~gameState.AllPieces;
            gameState.Moves.Add(ply, new List<uint>(64));

			if (gameState.WhiteToMove)
			{
                targetBitmap = gameState.WhitePieces;
                GenerateWhitePawnMoves(gameState, moveData, freeSquares, ply);               
			}
		    else
            {
                targetBitmap = gameState.BlackPieces;
                GenerateBlackPawnMoves(gameState, moveData, freeSquares, ply);
            }
		}

        private static void GenerateWhitePawnMoves(GameState gameState, MoveData moveData, ulong freeSquares, int ply)
        {   
            var pawnBoard = gameState.WhitePawns;
            var move = 0U.SetMovingPiece(MoveUtility.WhitePawn);

            while (pawnBoard > 0)
            {
                uint fromSquare = pawnBoard.GetFirstPieceFromBitBoard();
                move = move.SetFromMove(fromSquare);

                //Normal Moves
                var pawnMoves = moveData.WhitePawnMoves[fromSquare] & freeSquares;

                //Double Moves
                if (MoveUtility.Ranks[fromSquare] == 2  && pawnMoves > 0)
                {
                    pawnMoves |= moveData.WhitePawnDoubleMoves[fromSquare] & freeSquares;
                }

                //Captures
                pawnMoves |= moveData.WhitePawnAttacks[fromSquare] & gameState.BlackPieces;

                while (pawnMoves > 0)
                {
                    uint toSquare = pawnMoves.GetFirstPieceFromBitBoard();
                    move = move.SetToMove(toSquare);
                    move = move.SetCapturedPiece(gameState.BoardArray[toSquare]);
                   
                    if (MoveUtility.Ranks[toSquare] == 8)
                    {
                        move = move.SetPromotionPiece(MoveUtility.WhiteQueen);
                        gameState.Moves[ply].Add(move);

                        move = move.SetPromotionPiece(MoveUtility.WhiteRook);
                        gameState.Moves[ply].Add(move);

                        move = move.SetPromotionPiece(MoveUtility.WhiteBishop);
                        gameState.Moves[ply].Add(move);

                        move = move.SetPromotionPiece(MoveUtility.WhiteKnight);
                        gameState.Moves[ply].Add(move);

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
                pawnBoard ^= MoveUtility.BitStates[fromSquare];
                move = move.SetPromotionPiece(MoveUtility.Empty);
            }
        }

        private static void GenerateBlackPawnMoves(GameState gameState, MoveData moveData, ulong freeSquares, int ply)
        {
            var pawnBoard = gameState.BlackPawns;
            var move = 0U.SetMovingPiece(MoveUtility.BlackPawn);

            while (pawnBoard > 0)
            {
                uint fromSquare = pawnBoard.GetFirstPieceFromBitBoard();
                move = move.SetFromMove(fromSquare);

                //Normal Moves
                var pawnMoves = moveData.BlackPawnMoves[fromSquare] & freeSquares;

                //Double Moves
                if (MoveUtility.Ranks[fromSquare] == 7 && pawnMoves > 0)
                {
                    pawnMoves |= moveData.BlackPawnDoubleMoves[fromSquare] & freeSquares;
                }

                //Captures
                pawnMoves |= moveData.BlackPawnAttacks[fromSquare] & gameState.WhitePieces;

                while (pawnMoves > 0)
                {
                    uint toSquare = pawnMoves.GetFirstPieceFromBitBoard();
                    move = move.SetToMove(toSquare);
                    move = move.SetCapturedPiece(gameState.BoardArray[toSquare]);

                    if (MoveUtility.Ranks[toSquare] == 1)
                    {
                        move = move.SetPromotionPiece(MoveUtility.BlackQueen);
                        gameState.Moves[ply].Add(move);

                        move = move.SetPromotionPiece(MoveUtility.BlackRook);
                        gameState.Moves[ply].Add(move);

                        move = move.SetPromotionPiece(MoveUtility.BlackBishop);
                        gameState.Moves[ply].Add(move);

                        move = move.SetPromotionPiece(MoveUtility.BlackKnight);
                        gameState.Moves[ply].Add(move);

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
                pawnBoard ^= MoveUtility.BitStates[fromSquare];
                move = move.SetPromotionPiece(MoveUtility.Empty);
            }
        }
    }
}
