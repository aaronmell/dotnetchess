﻿using System.Collections.Generic;

namespace DotNetEngine.Engine
{
	/// <summary>
	/// An Object that contains the current game state.
	/// </summary>
	internal class GameState
	{
        private readonly ulong[] _allPieceBoards;        
     
		//white bitboards
		internal ulong WhitePawns { get; set; }
		internal ulong WhiteKnights { get; set; }
		internal ulong WhiteBishops { get; set; }
		internal ulong WhiteRooks { get; set; }
		internal ulong WhiteQueens { get; set; }
		internal ulong WhiteKing { get; set; }

        internal ulong WhitePieces { get; set; }

		//black bitboards
		internal ulong BlackPawns { get; set; }
		internal ulong BlackKnights { get; set; }
		internal ulong BlackBishops { get; set; }
		internal ulong BlackRooks { get; set; }
		internal ulong BlackQueens { get; set; }
		internal ulong BlackKing { get; set; }

        internal ulong BlackPieces { get; set; }
        
        internal ulong AllPieces { get; set; }

        /// <summary>
        /// A List of all of the moves relating to this GameState.
        /// </summary>
        internal Dictionary<int,List<uint>> Moves { get; set; }

		internal bool WhiteToMove { get; set; }

		/// <summary>
		/// 0 - Cannot Castle 
		/// 1 - Can Castle 00
		/// 2 - Can Castle 000 
		/// 3 - Can Castle Both 00 and 000
		/// </summary>
		internal int CurrentWhiteCastleStatus { get; set; }
		internal int CurrentBlackCastleStatus { get; set; }
		
        internal uint EnpassantTargetSquare { get; set; }

		/// <summary>
		/// The number of half moves currently made
		/// </summary>
		internal int FiftyMoveRuleCount { get; set; }

		/// <summary>
		/// The total number of full moves currently made
		/// </summary>
		internal int TotalMoveCount { get; set; }

		/// <summary>
		/// An single dimension array of the board. Each Piece is represented by a unique bit
		/// This array is little endian encoded. A1 = 0 and H8 = 63
		/// </summary>
		internal uint[] BoardArray { get; private set; }
               
        internal GameState()
		{
			_allPieceBoards = new[]
			{
				BlackBishops,
				BlackKing,
				BlackKnights,
				BlackPawns,
				BlackQueens,
				BlackRooks,
				WhiteBishops,
				WhiteKing,
				WhiteKnights,
				WhitePawns,
				WhiteQueens,
				WhiteRooks
			};

			BoardArray = new uint[64];

            Moves = new Dictionary<int, List<uint>>();
		}

        //Updates the GameState to reflect a move being made
        internal void MakeMove(uint move)
        {
            var fromMove = move.GetFromMove();
            var toMove = move.GetToMove();
            var movingPiece = move.GetMovingPiece();
            var capturedPiece = move.GetCapturedPiece();

            ulong fromBitBoard = MoveUtility.BitStates[fromMove];
            ulong fromAndToBitboard = fromBitBoard | MoveUtility.BitStates[toMove];

            switch (movingPiece)
            {
                case MoveUtility.WhitePawn:
                    {
                        WhitePawns ^= fromAndToBitboard;
                        WhitePieces ^= fromAndToBitboard;
                        BoardArray[fromMove] = MoveUtility.Empty;
                        BoardArray[toMove] = MoveUtility.WhitePawn;

                        EnpassantTargetSquare = (MoveUtility.Ranks[fromMove] == 2 && MoveUtility.Ranks[toMove] == 4) ? fromMove + 8 : 0;
                        FiftyMoveRuleCount = 0;

                        if (capturedPiece > 0)
                        {
                            if (move.IsEnPassant())
                            {
                                BlackPawns ^= MoveUtility.BitStates[toMove - 8];
                                BlackPieces ^= MoveUtility.BitStates[toMove - 8];
                                AllPieces ^= fromAndToBitboard | MoveUtility.BitStates[toMove - 8];
                                BoardArray[toMove - 8] = MoveUtility.Empty;
                            }
                            else
                            {
                                CapturePiece(toMove, capturedPiece);
                                AllPieces ^= fromBitBoard;
                            }
                        }
                        else
                        {
                            AllPieces ^= fromAndToBitboard;
                        }

                        if (move.IsPromotion())
                        {
                            PromotePiece(move.GetPromotedPiece(), toMove);
                            BoardArray[toMove] = move.GetPromotedPiece();
                        }
                        break;
                    }
                case MoveUtility.BlackPawn:
                    {
                        BlackPawns ^= fromAndToBitboard;
                        BlackPieces ^= fromAndToBitboard;
                        BoardArray[fromMove] = MoveUtility.Empty;
                        BoardArray[toMove] = MoveUtility.BlackPawn;

                        EnpassantTargetSquare = (MoveUtility.Ranks[fromMove] == 7 && MoveUtility.Ranks[toMove] == 5) ? fromMove - 8 : 0;
                        FiftyMoveRuleCount = 0;

                        if (capturedPiece > 0)
                        {
                            if (move.IsEnPassant())
                            {
                                WhitePawns ^= MoveUtility.BitStates[toMove + 8];
                                WhitePieces ^= MoveUtility.BitStates[toMove + 8];
                                AllPieces ^= fromAndToBitboard | MoveUtility.BitStates[toMove + 8];
                                BoardArray[toMove + 8] = MoveUtility.Empty;
                            }
                            else
                            {
                                CapturePiece(toMove, capturedPiece);
                                AllPieces ^= fromBitBoard;
                            }
                        }
                        else
                        {
                            AllPieces ^= fromAndToBitboard;
                        }

                        if (move.IsPromotion())
                        {
                            PromotePiece(move.GetPromotedPiece(), toMove);
                            BoardArray[toMove] = move.GetPromotedPiece();
                        }
                        break;
                    }
            }

            WhiteToMove = !WhiteToMove;

        }

        private void PromotePiece(uint promotedPiece, uint toMove)
        {
            var toBitBoard = MoveUtility.BitStates[toMove];
            
            if (WhiteToMove)
            {
                WhitePawns ^= toBitBoard;

                switch (promotedPiece)
                {
                    case MoveUtility.WhiteQueen:
                    {
                        WhiteQueens ^= toBitBoard;
                        break;
                    }
                    case MoveUtility.WhiteKnight:
                    {
                        WhiteKnights ^= toBitBoard;
                        break;
                    }
                    case MoveUtility.WhiteRook:
                    {
                        WhiteRooks ^= toBitBoard;
                        break;
                    }
                    case MoveUtility.WhiteBishop:
                    {
                        WhiteBishops ^= toBitBoard;
                        break;
                    }
                }
            }
            else
            {
                BlackPawns ^= toBitBoard;

                switch (promotedPiece)
                {
                    case MoveUtility.BlackQueen:
                    {
                        BlackQueens ^= toBitBoard;
                        break;
                    }
                    case MoveUtility.BlackKnight:
                    {
                        BlackKnights ^= toBitBoard;
                        break;
                    }
                    case MoveUtility.BlackRook:
                    {
                        BlackRooks ^= toBitBoard;
                        break;
                    }
                    case MoveUtility.BlackBishop:
                    {
                        BlackBishops ^= toBitBoard;
                        break;
                    }
                }
            }
        }       

        private void CapturePiece(uint toMove, uint capturedPiece)
        {
            var toBitBoard = toMove;

            switch (capturedPiece)
            {
            case MoveUtility.WhitePawn:
                {
                    WhitePawns  ^=toBitBoard;
                    WhitePieces ^= toBitBoard;
                    break;
                }
            case MoveUtility.WhiteQueen:
                {
                    WhiteQueens ^= toBitBoard;
                    WhitePieces ^= toBitBoard;
                    break;
                }
            case MoveUtility.WhiteKnight:
                {
                    WhiteKnights ^= toBitBoard;
                    WhitePieces ^= toBitBoard;
                    break;
                }
            case MoveUtility.WhiteKing:
                {
                    WhiteKing ^= toBitBoard;
                    WhitePieces ^= toBitBoard;
                    break;
                }                   
            case MoveUtility.BlackPawn:
                {
                    BlackPawns  ^=toBitBoard;
                    BlackPieces ^= toBitBoard;
                    break;
                }
            case MoveUtility.BlackQueen:
                {
                    BlackQueens ^= toBitBoard;
                    BlackPieces ^= toBitBoard;
                    break;
                }
            case MoveUtility.BlackKnight:
                {
                    BlackKnights ^= toBitBoard;
                    BlackPieces ^= toBitBoard;
                    break;
                }
            case MoveUtility.BlackKing:
                {
                    BlackKing ^= toBitBoard;
                    BlackPieces ^= toBitBoard;
                    break;
                }
            }

            FiftyMoveRuleCount = 0;
        }

        
    }
}
