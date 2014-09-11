using System.Collections.Generic;

namespace DotNetEngine.Engine
{
	/// <summary>
	/// An Object that contains the current game state.
	/// </summary>
	internal class GameState
	{
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

        /// <summary>
        /// A stack containing all of the game state records, needed to unmake a move.
        /// </summary>
        internal Stack<GameStateRecord> PreviousGameStateRecords { get; set; }

        internal GameState()
		{
	        BoardArray = new uint[64];

            Moves = new Dictionary<int, List<uint>>();
            PreviousGameStateRecords = new Stack<GameStateRecord>();
		}

        //Updates the GameState to reflect a move being made
        internal void MakeMove(uint move)
        {
            var fromMove = move.GetFromMove();
            var toMove = move.GetToMove();
            var movingPiece = move.GetMovingPiece();
            var capturedPiece = move.GetCapturedPiece();

            PreviousGameStateRecords.Push(this.CreateGameStateRecord(move));

            ulong fromBitboard = MoveUtility.BitStates[fromMove];
            ulong fromAndToBitboard = fromBitboard | MoveUtility.BitStates[toMove];

            BoardArray[fromMove] = MoveUtility.Empty;
            EnpassantTargetSquare = 0;
            FiftyMoveRuleCount++;           

            switch (movingPiece)
            {
                case MoveUtility.WhitePawn:
                    {
                        WhitePawns ^= fromAndToBitboard;
                        WhitePieces ^= fromAndToBitboard;
                        
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
                                CapturePiece(toMove, capturedPiece, fromBitboard);
                                
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
                case MoveUtility.WhiteKing:
                    {
                        WhiteKing ^= fromAndToBitboard;
                        WhitePieces ^= fromAndToBitboard;
                       
                        BoardArray[toMove] = MoveUtility.WhiteKing;
                      
                        CurrentWhiteCastleStatus = (int)CastleStatus.CannotCastle;

                        if (capturedPiece > 0)
                        {
                            CapturePiece(toMove, capturedPiece, fromBitboard);                           
                        }
                        else
                        {
                            AllPieces ^= fromAndToBitboard;
                        }

                        if (move.IsCastle())
                        {
                            if (move.IsCastleOO())
                            {
                                WhiteRooks ^= MoveUtility.BitStates[5] | MoveUtility.BitStates[7];
                                WhitePieces ^= MoveUtility.BitStates[5] | MoveUtility.BitStates[7];
                                AllPieces ^= MoveUtility.BitStates[5] | MoveUtility.BitStates[7];
                                BoardArray[7] = MoveUtility.Empty;
                                BoardArray[5] = MoveUtility.WhiteRook;
                            }
                            else
                            {
                                WhiteRooks ^= MoveUtility.BitStates[0] | MoveUtility.BitStates[3];
                                WhitePieces ^= MoveUtility.BitStates[0] | MoveUtility.BitStates[3];
                                AllPieces ^= MoveUtility.BitStates[0] | MoveUtility.BitStates[3];
                                BoardArray[0] = MoveUtility.Empty;
                                BoardArray[3] = MoveUtility.WhiteRook;
                            }
                        }
                        break;
                    }
                case MoveUtility.WhiteKnight:
                    {
                        WhiteKnights ^= fromAndToBitboard;
                        WhitePieces ^= fromAndToBitboard;

                        BoardArray[toMove] = MoveUtility.WhiteKnight;

                        if (capturedPiece > 0)
                        {
                            CapturePiece(toMove, capturedPiece, fromBitboard);
                        }
                        else
                        {
                            AllPieces ^= fromAndToBitboard;
                        }
                        break;
                    }
                case MoveUtility.WhiteBishop:
                    {
                        WhiteBishops ^= fromAndToBitboard;
                        WhitePieces ^= fromAndToBitboard;

                        BoardArray[toMove] = MoveUtility.WhiteBishop;

                        if (capturedPiece > 0)
                        {
                            CapturePiece(toMove, capturedPiece, fromBitboard);
                        }
                        else
                        {
                            AllPieces ^= fromAndToBitboard;
                        }
                        break;
                    }
                case MoveUtility.WhiteRook:
                    {
                        WhiteRooks ^= fromAndToBitboard;
                        WhitePieces ^= fromAndToBitboard;

                        BoardArray[toMove] = MoveUtility.WhiteRook;

                        if (fromMove == 0)
                        {                           
                            CurrentWhiteCastleStatus &= ~(int)CastleStatus.OOOCastle;
                        }
                        if (fromMove == 7)
                        {
                            CurrentWhiteCastleStatus &= ~(int)CastleStatus.OOCastle;
                        }

                        if (capturedPiece > 0)
                        {
                            CapturePiece(toMove, capturedPiece, fromBitboard);
                        }
                        else
                        {
                            AllPieces ^= fromAndToBitboard;
                        }
                        break;
                    }
                case MoveUtility.WhiteQueen:
                    {
                        WhiteQueens ^= fromAndToBitboard;
                        WhitePieces ^= fromAndToBitboard;

                        BoardArray[toMove] = MoveUtility.WhiteQueen;

                        if (capturedPiece > 0)
                        {
                            CapturePiece(toMove, capturedPiece, fromBitboard);
                        }
                        else
                        {
                            AllPieces ^= fromAndToBitboard;
                        }
                        break;
                    }
                case MoveUtility.BlackPawn:
                    {
                        BlackPawns ^= fromAndToBitboard;
                        BlackPieces ^= fromAndToBitboard;
                       
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
                                CapturePiece(toMove, capturedPiece, fromBitboard);                               
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
                case MoveUtility.BlackKing:
                    {
                        BlackKing ^= fromAndToBitboard;
                        BlackPieces ^= fromAndToBitboard;

                        BoardArray[toMove] = MoveUtility.BlackKing;

                        CurrentBlackCastleStatus = (int)CastleStatus.CannotCastle;

                        if (capturedPiece > 0)
                        {
                            CapturePiece(toMove, capturedPiece, fromBitboard);
                        }
                        else
                        {
                            AllPieces ^= fromAndToBitboard;
                        }

                        if (move.IsCastle())
                        {
                            if (move.IsCastleOO())
                            {
                                BlackRooks ^= MoveUtility.BitStates[61] | MoveUtility.BitStates[63];
                                BlackPieces ^= MoveUtility.BitStates[61] | MoveUtility.BitStates[63];
                                AllPieces ^= MoveUtility.BitStates[61] | MoveUtility.BitStates[63];
                                BoardArray[63] = MoveUtility.Empty;
                                BoardArray[61] = MoveUtility.BlackRook;
                            }
                            else
                            {
                                BlackRooks ^= MoveUtility.BitStates[56] | MoveUtility.BitStates[59];
                                BlackPieces ^= MoveUtility.BitStates[56] | MoveUtility.BitStates[59];
                                AllPieces ^= MoveUtility.BitStates[56] | MoveUtility.BitStates[59];
                                BoardArray[56] = MoveUtility.Empty;
                                BoardArray[59] = MoveUtility.BlackRook;
                            }
                        }
                        break;
                    }
                case MoveUtility.BlackKnight:
                    {
                        BlackKnights ^= fromAndToBitboard;
                        BlackPieces ^= fromAndToBitboard;

                        BoardArray[toMove] = MoveUtility.BlackKnight;

                        if (capturedPiece > 0)
                        {
                            CapturePiece(toMove, capturedPiece, fromBitboard);
                        }
                        else
                        {
                            AllPieces ^= fromAndToBitboard;
                        }
                        break;
                    }
                case MoveUtility.BlackBishop:
                    {
                        BlackBishops ^= fromAndToBitboard;
                        BlackPieces ^= fromAndToBitboard;

                        BoardArray[toMove] = MoveUtility.BlackBishop;

                        if (capturedPiece > 0)
                        {
                            CapturePiece(toMove, capturedPiece, fromBitboard);
                        }
                        else
                        {
                            AllPieces ^= fromAndToBitboard;
                        }
                        break;
                    }
                case MoveUtility.BlackRook:
                    {
                        BlackRooks ^= fromAndToBitboard;
                        BlackPieces ^= fromAndToBitboard;

                        BoardArray[toMove] = MoveUtility.BlackRook;

                        if (fromMove == 56)
                        {
                            CurrentWhiteCastleStatus &= ~(int)CastleStatus.OOOCastle;
                        }
                        if (fromMove == 63)
                        {
                            CurrentWhiteCastleStatus &= ~(int)CastleStatus.OOCastle;
                        }

                        if (capturedPiece > 0)
                        {
                            CapturePiece(toMove, capturedPiece, fromBitboard);
                        }
                        else
                        {
                            AllPieces ^= fromAndToBitboard;
                        }
                        break;
                    }
                case MoveUtility.BlackQueen:
                    {
                        BlackQueens ^= fromAndToBitboard;
                        BlackPieces ^= fromAndToBitboard;

                        BoardArray[toMove] = MoveUtility.BlackQueen;

                        if (capturedPiece > 0)
                        {
                            CapturePiece(toMove, capturedPiece, fromBitboard);
                        }
                        else
                        {
                            AllPieces ^= fromAndToBitboard;
                        }
                        break;
                    }
            }
            WhiteToMove = !WhiteToMove;
        }

        internal void UnMakeMove(uint move)
        {
            var fromMove = move.GetFromMove();
            var toMove = move.GetToMove();
            var movingPiece = move.GetMovingPiece();
            var capturedPiece = move.GetCapturedPiece();

            ulong fromBitboard = MoveUtility.BitStates[fromMove];
            ulong fromAndToBitboard = fromBitboard | MoveUtility.BitStates[toMove];
            
            BoardArray[toMove] = MoveUtility.Empty;

            switch (movingPiece)
            {
                case MoveUtility.WhitePawn:
                    {
                        WhitePawns ^= fromAndToBitboard;
                        WhitePieces ^= fromAndToBitboard;

                        BoardArray[fromMove] = MoveUtility.WhitePawn;
                        
                        if (capturedPiece > 0)
                        {
                            if (move.IsEnPassant())
                            {
                                BlackPawns ^= MoveUtility.BitStates[toMove - 8];
                                BlackPieces ^= MoveUtility.BitStates[toMove - 8];
                                AllPieces ^= fromAndToBitboard | MoveUtility.BitStates[toMove - 8];
                                BoardArray[toMove - 8] = MoveUtility.BlackPawn;
                            }
                            else
                            {
                                UnCapturePiece(toMove, capturedPiece, fromBitboard);
                            }
                        }
                        else
                        {
                            AllPieces ^= fromAndToBitboard;
                        }

                        if (move.IsPromotion())
                        {
                            UnPromotePiece(move.GetPromotedPiece(), toMove);                           
                        }
                        break;
                    }
                case MoveUtility.WhiteKing:
                    {
                        WhiteKing ^= fromAndToBitboard;
                        WhitePieces ^= fromAndToBitboard;

                        BoardArray[fromMove] = MoveUtility.WhiteKing;

                        CurrentWhiteCastleStatus = (int)CastleStatus.CannotCastle;

                        if (capturedPiece > 0)
                        {
                            UnCapturePiece(toMove, capturedPiece, fromBitboard);
                        }
                        else
                        {
                            AllPieces ^= fromAndToBitboard;
                        }

                        if (move.IsCastle())
                        {
                            if (move.IsCastleOO())
                            {
                                WhiteRooks ^= MoveUtility.BitStates[5] | MoveUtility.BitStates[7];
                                WhitePieces ^= MoveUtility.BitStates[5] | MoveUtility.BitStates[7];
                                AllPieces ^= MoveUtility.BitStates[5] | MoveUtility.BitStates[7];
                                BoardArray[5] = MoveUtility.Empty;
                                BoardArray[7] = MoveUtility.WhiteRook;
                            }
                            else
                            {
                                WhiteRooks ^= MoveUtility.BitStates[0] | MoveUtility.BitStates[3];
                                WhitePieces ^= MoveUtility.BitStates[0] | MoveUtility.BitStates[3];
                                AllPieces ^= MoveUtility.BitStates[0] | MoveUtility.BitStates[3];
                                BoardArray[3] = MoveUtility.Empty;
                                BoardArray[0] = MoveUtility.WhiteRook;
                            }
                        }
                        break;
                    }
                case MoveUtility.WhiteKnight:
                    {
                        WhiteKnights ^= fromAndToBitboard;
                        WhitePieces ^= fromAndToBitboard;

                        BoardArray[fromMove] = MoveUtility.WhiteKnight;

                        if (capturedPiece > 0)
                        {
                            UnCapturePiece(toMove, capturedPiece, fromBitboard);
                        }
                        else
                        {
                            AllPieces ^= fromAndToBitboard;
                        }
                        break;
                    }
                case MoveUtility.WhiteBishop:
                    {
                        WhiteBishops ^= fromAndToBitboard;
                        WhitePieces ^= fromAndToBitboard;

                        BoardArray[fromMove] = MoveUtility.WhiteBishop;

                        if (capturedPiece > 0)
                        {
                            UnCapturePiece(toMove, capturedPiece, fromBitboard);
                        }
                        else
                        {
                            AllPieces ^= fromAndToBitboard;
                        }
                        break;
                    }
                case MoveUtility.WhiteRook:
                    {
                        WhiteRooks ^= fromAndToBitboard;
                        WhitePieces ^= fromAndToBitboard;

                        BoardArray[fromMove] = MoveUtility.WhiteRook;

                        if (capturedPiece > 0)
                        {
                            UnCapturePiece(toMove, capturedPiece, fromBitboard);
                        }
                        else
                        {
                            AllPieces ^= fromAndToBitboard;
                        }
                        break;
                    }
                case MoveUtility.WhiteQueen:
                    {
                        WhiteQueens ^= fromAndToBitboard;
                        WhitePieces ^= fromAndToBitboard;

                        BoardArray[fromMove] = MoveUtility.WhiteQueen;

                        if (capturedPiece > 0)
                        {
                            CapturePiece(toMove, capturedPiece, fromBitboard);
                        }
                        else
                        {
                            AllPieces ^= fromAndToBitboard;
                        }
                        break;
                    }
                case MoveUtility.BlackPawn:
                    {
                        BlackPawns ^= fromAndToBitboard;
                        BlackPieces ^= fromAndToBitboard;

                        BoardArray[fromMove] = MoveUtility.BlackPawn;

                        if (capturedPiece > 0)
                        {
                            if (move.IsEnPassant())
                            {
                                WhitePawns ^= MoveUtility.BitStates[toMove + 8];
                                WhitePieces ^= MoveUtility.BitStates[toMove + 8];
                                AllPieces ^= fromAndToBitboard | MoveUtility.BitStates[toMove + 8];
                                BoardArray[toMove + 8] = MoveUtility.WhitePawn;
                            }
                            else
                            {
                                UnCapturePiece(toMove, capturedPiece, fromBitboard);
                            }
                        }
                        else
                        {
                            AllPieces ^= fromAndToBitboard;
                        }

                        if (move.IsPromotion())
                        {
                            UnPromotePiece(move.GetPromotedPiece(), toMove);
                        }
                        break;
                    }
                case MoveUtility.BlackKing:
                    {
                        BlackKing ^= fromAndToBitboard;
                        BlackPieces ^= fromAndToBitboard;

                        BoardArray[fromMove] = MoveUtility.BlackKing;

                        CurrentBlackCastleStatus = (int)CastleStatus.CannotCastle;

                        if (capturedPiece > 0)
                        {
                            CapturePiece(toMove, capturedPiece, fromBitboard);
                        }
                        else
                        {
                            AllPieces ^= fromAndToBitboard;
                        }

                        if (move.IsCastle())
                        {
                            if (move.IsCastleOO())
                            {
                                BlackRooks ^= MoveUtility.BitStates[61] | MoveUtility.BitStates[63];
                                BlackPieces ^= MoveUtility.BitStates[61] | MoveUtility.BitStates[63];
                                AllPieces ^= MoveUtility.BitStates[61] | MoveUtility.BitStates[63];
                                BoardArray[61] = MoveUtility.Empty;
                                BoardArray[63] = MoveUtility.BlackRook;
                            }
                            else
                            {
                                BlackRooks ^= MoveUtility.BitStates[56] | MoveUtility.BitStates[59];
                                BlackPieces ^= MoveUtility.BitStates[56] | MoveUtility.BitStates[59];
                                AllPieces ^= MoveUtility.BitStates[56] | MoveUtility.BitStates[59];
                                BoardArray[59] = MoveUtility.Empty;
                                BoardArray[56] = MoveUtility.BlackRook;
                            }
                        }
                        break;
                    }
                case MoveUtility.BlackKnight:
                    {
                        BlackKnights ^= fromAndToBitboard;
                        BlackPieces ^= fromAndToBitboard;

                        BoardArray[fromMove] = MoveUtility.BlackKnight;

                        if (capturedPiece > 0)
                        {
                            UnCapturePiece(toMove, capturedPiece, fromBitboard);
                        }
                        else
                        {
                            AllPieces ^= fromAndToBitboard;
                        }
                        break;
                    }
                case MoveUtility.BlackBishop:
                    {
                        BlackBishops ^= fromAndToBitboard;
                        BlackPieces ^= fromAndToBitboard;

                        BoardArray[fromMove] = MoveUtility.BlackBishop;

                        if (capturedPiece > 0)
                        {
                            UnCapturePiece(toMove, capturedPiece, fromBitboard);
                        }
                        else
                        {
                            AllPieces ^= fromAndToBitboard;
                        }
                        break;
                    }
                case MoveUtility.BlackRook:
                    {
                        BlackRooks ^= fromAndToBitboard;
                        BlackPieces ^= fromAndToBitboard;

                        BoardArray[fromMove] = MoveUtility.BlackRook;

                        if (fromMove == 56)
                        {
                            CurrentWhiteCastleStatus &= ~(int)CastleStatus.OOOCastle;
                        }
                        if (fromMove == 63)
                        {
                            CurrentWhiteCastleStatus &= ~(int)CastleStatus.OOCastle;
                        }

                        if (capturedPiece > 0)
                        {
                            UnCapturePiece(toMove, capturedPiece, fromBitboard);
                        }
                        else
                        {
                            AllPieces ^= fromAndToBitboard;
                        }
                        break;
                    }
                case MoveUtility.BlackQueen:
                    {
                        BlackQueens ^= fromAndToBitboard;
                        BlackPieces ^= fromAndToBitboard;

                        BoardArray[fromMove] = MoveUtility.BlackQueen;

                        if (capturedPiece > 0)
                        {
                            CapturePiece(toMove, capturedPiece, fromBitboard);
                        }
                        else
                        {
                            AllPieces ^= fromAndToBitboard;
                        }
                        break;
                    }
            }
            this.UpdateGameStateWithGameStateRecord(PreviousGameStateRecords.Pop());
            WhiteToMove = !WhiteToMove;
        }

        private void UnPromotePiece(uint promotedPiece, uint toMove)
        {
            var toBitBoard = MoveUtility.BitStates[toMove];

            if (!WhiteToMove)
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

        private void UnCapturePiece(uint toMove, uint capturedPiece, ulong fromBitboard)
        {
            var toBitBoard = MoveUtility.BitStates[toMove];
            AllPieces ^= fromBitboard;

            switch (capturedPiece)
            {
                case MoveUtility.WhitePawn:
                    {
                        WhitePawns ^= toBitBoard;
                        WhitePieces ^= toBitBoard;
                        BoardArray[toMove] = MoveUtility.WhitePawn;
                        break;
                    }
                case MoveUtility.WhiteQueen:
                    {
                        WhiteQueens ^= toBitBoard;
                        WhitePieces ^= toBitBoard;
                        BoardArray[toMove] = MoveUtility.WhiteQueen;

                        break;
                    }
                case MoveUtility.WhiteKnight:
                    {
                        WhiteKnights ^= toBitBoard;
                        WhitePieces ^= toBitBoard;
                        BoardArray[toMove] = MoveUtility.WhiteKnight;

                        break;
                    }
                case MoveUtility.WhiteKing:
                    {
                        WhiteKing ^= toBitBoard;
                        WhitePieces ^= toBitBoard;
                        BoardArray[toMove] = MoveUtility.WhiteKing;
                        break;
                    }
                case MoveUtility.WhiteRook:
                    {
                        WhiteRooks ^= toBitBoard;
                        WhitePieces ^= toBitBoard;

                        if (toMove == 0)
                        {
                            CurrentWhiteCastleStatus &= ~(int)CastleStatus.OOOCastle;
                        }
                        if (toMove == 7)
                        {
                            CurrentWhiteCastleStatus &= ~(int)CastleStatus.OOCastle;
                        }
                        BoardArray[toMove] = MoveUtility.WhiteRook;

                        break;
                    }
                case MoveUtility.WhiteBishop:
                    {
                        WhiteBishops ^= toBitBoard;
                        WhitePieces ^= toBitBoard;
                        BoardArray[toMove] = MoveUtility.WhiteBishop;
                        break;
                    }
                case MoveUtility.BlackPawn:
                    {
                        BlackPawns ^= toBitBoard;
                        BlackPieces ^= toBitBoard;
                        BoardArray[toMove] = MoveUtility.BlackPawn;
                        break;
                    }
                case MoveUtility.BlackQueen:
                    {
                        BlackQueens ^= toBitBoard;
                        BlackPieces ^= toBitBoard;
                        BoardArray[toMove] = MoveUtility.BlackQueen;

                        break;
                    }
                case MoveUtility.BlackKnight:
                    {
                        BlackKnights ^= toBitBoard;
                        BlackPieces ^= toBitBoard;
                        BoardArray[toMove] = MoveUtility.BlackKnight;
                        break;
                    }
                case MoveUtility.BlackKing:
                    {
                        BlackKing ^= toBitBoard;
                        BlackPieces ^= toBitBoard;
                        BoardArray[toMove] = MoveUtility.BlackKing;
                        break;
                    }
                case MoveUtility.BlackRook:
                    {
                        BlackRooks ^= toBitBoard;
                        BlackPieces ^= toBitBoard;

                        if (toMove == 56)
                        {
                            CurrentWhiteCastleStatus &= ~(int)CastleStatus.OOOCastle;
                        }
                        if (toMove == 63)
                        {
                            CurrentWhiteCastleStatus &= ~(int)CastleStatus.OOCastle;
                        }
                        BoardArray[toMove] = MoveUtility.BlackRook;
                        break;
                    }
                case MoveUtility.BlackBishop:
                    {
                        BlackBishops ^= toBitBoard;
                        BlackPieces ^= toBitBoard;
                        BoardArray[toMove] = MoveUtility.BlackBishop;
                        break;
                    }
            }
        }

        private void CapturePiece(uint toMove, uint capturedPiece, ulong fromBitBoard)
        {
            var toBitBoard = MoveUtility.BitStates[toMove];          

            FiftyMoveRuleCount = 0;
            AllPieces ^= fromBitBoard;

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
            case MoveUtility.WhiteRook:
                {
                    WhiteRooks ^= toBitBoard;
                    WhitePieces ^= toBitBoard;

                    if (toMove == 0)
                    {
                        CurrentWhiteCastleStatus &= ~(int)CastleStatus.OOOCastle;
                    }
                    if (toMove == 7)
                    {
                        CurrentWhiteCastleStatus &= ~(int)CastleStatus.OOCastle;
                    }
                    break;
                }
            case MoveUtility.WhiteBishop:
                {
                    WhiteBishops ^= toBitBoard;
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
            case MoveUtility.BlackRook:
                {
                    BlackRooks ^= toBitBoard;
                    BlackPieces ^= toBitBoard;

                    if (toMove == 56)
                    {
                        CurrentWhiteCastleStatus &= ~(int)CastleStatus.OOOCastle;
                    }
                    if (toMove == 63)
                    {
                        CurrentWhiteCastleStatus &= ~(int)CastleStatus.OOCastle;
                    }

                    break;
                }
            case MoveUtility.BlackBishop:
                {
                    BlackBishops ^= toBitBoard;
                    BlackPieces ^= toBitBoard;
                    break;
                } 
            }           
        }       
    }
}
