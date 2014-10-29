using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using DotNetEngine.Engine.Enums;
using DotNetEngine.Engine.Helpers;
using System.Linq;

namespace DotNetEngine.Engine.Objects
{
    /// <summary>
    /// An Object that contains the current game state.
    /// </summary>
    internal class GameState
    {
        private static readonly char[] _files =
        {
            'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h'
        };

        #region Internal Properties

        /// <summary>
        /// A bitboard that contains all of the white pawns
        /// </summary>
        internal ulong WhitePawns { get; set; }

        /// <summary>
        /// A bitboard that contains all of the white knights
        /// </summary>
        internal ulong WhiteKnights { get; set; }

        /// <summary>
        /// A bitboard that contains all of the white bishops
        /// </summary>
        internal ulong WhiteBishops { get; set; }

        /// <summary>
        /// A bitboard that contains all of the white rooks
        /// </summary>
        internal ulong WhiteRooks { get; set; }

        /// <summary>
        /// A bitboard that contains all of the white queens
        /// </summary>
        internal ulong WhiteQueens { get; set; }

        /// <summary>
        /// A bitboard that contains the white king
        /// </summary>
        internal ulong WhiteKing { get; set; }

        /// <summary>
        /// A bitboard that contains all of the white pieces
        /// </summary>
        internal ulong WhitePieces { get; set; }

        /// <summary>
        /// A bitboard that contains all of the black pawns
        /// </summary>
        internal ulong BlackPawns { get; set; }

        /// <summary>
        /// A bitboard that contains all of the black knights
        /// </summary>
        internal ulong BlackKnights { get; set; }

        /// <summary>
        /// A bitboard that contains all of the black bishops
        /// </summary>
        internal ulong BlackBishops { get; set; }

        /// <summary>
        /// A bitboard that contains all of the black rooks
        /// </summary>
        internal ulong BlackRooks { get; set; }

        /// <summary>
        /// A bitboard that contains all of the black queens
        /// </summary>
        internal ulong BlackQueens { get; set; }

        /// <summary>
        /// A bitboard that contains the black king
        /// </summary>
        internal ulong BlackKing { get; set; }

        /// <summary>
        /// A bitboard that contains all of the black pieces
        /// </summary>
        internal ulong BlackPieces { get; set; }

        /// <summary>
        /// A bitboard that contains all of the pieces
        /// </summary>
        internal ulong AllPieces { get; set; }

        /// <summary>
        /// A dictionary of all of the current moves relating to this gamestate. The key to this dictionary is the current ply
        /// </summary>
        internal Dictionary<int, List<uint>> Moves { get; set; }

        /// <summary>
        /// Determines which side is moving
        /// </summary>
        internal bool WhiteToMove { get; set; }

        /// <summary>
        /// The castle status of white <see cref="CastleStatus"/>
        /// </summary>
        internal int CurrentWhiteCastleStatus { get; set; }

        /// <summary>
        /// The castle status of black <see cref="CastleStatus"/>
        /// </summary>
        internal int CurrentBlackCastleStatus { get; set; }

        /// <summary>
        /// The target square of the enpassant. This is where the pawn will end up if it makes an enpasant capture
        /// </summary>
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
        /// A Zobrist HashKey That uniquely identifies a position
        /// </summary>
        internal ulong HashKey { get; set; }

        /// <summary>
        /// A stack containing all of the game state records, needed to unmake a move.
        /// </summary>
        internal GameStateRecord[] PreviousGameStateRecords { get; set; }
        #endregion

        #region Constructor

        internal GameState(ZobristHash zobristHash)
            : this("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1", zobristHash)
        {}

        internal GameState(string fen, ZobristHash zobristHash)
        {
            BoardArray = new uint[64];

            Moves = new Dictionary<int, List<uint>>();
            PreviousGameStateRecords = new GameStateRecord[1024];

            LoadGameStateFromFen(fen, zobristHash);
        }

        #endregion

        #region Internal Methods

        /// <summary>
        /// Updates the gamestate to reflect a move being made
        /// </summary>
        /// <param name="move">The move to make</param>
        /// <param name="zobristHash">The zobrist hash data to use</param>
        internal void MakeMove(uint move, ZobristHash zobristHash)
        {
            TotalMoveCount++;

            var fromMove = move.GetFromMove();
            var toMove = move.GetToMove();
            var movingPiece = move.GetMovingPiece();
            var capturedPiece = move.GetCapturedPiece();

            PreviousGameStateRecords[TotalMoveCount] = CreateGameStateRecord(move);

            ulong fromBitboard = MoveUtility.BitStates[fromMove];
            ulong fromAndToBitboard = fromBitboard | MoveUtility.BitStates[toMove];

            BoardArray[fromMove] = MoveUtility.EmptyPiece;

            if (EnpassantTargetSquare > 0)
                HashKey ^= zobristHash.EnPassantSquares[EnpassantTargetSquare];

            EnpassantTargetSquare = 0;
            FiftyMoveRuleCount++;
            
            HashKey ^= (zobristHash.PieceArray[fromMove][movingPiece] ^ zobristHash.PieceArray[toMove][movingPiece]);

            switch (movingPiece)
            {
                case MoveUtility.WhitePawn:
                {
                    FiftyMoveRuleCount = 0;
                    WhitePawns ^= fromAndToBitboard;
                    WhitePieces ^= fromAndToBitboard;

                    BoardArray[toMove] = MoveUtility.WhitePawn;

                    if (MoveUtility.Ranks[fromMove] == 2 && MoveUtility.Ranks[toMove] == 4)
                    {
                        EnpassantTargetSquare = fromMove + 8;
                        HashKey ^= zobristHash.EnPassantSquares[fromMove + 8];
                    }
                    
                    if (capturedPiece > 0)
                    {
                        if (move.IsEnPassant())
                        {
                            BlackPawns ^= MoveUtility.BitStates[toMove - 8];
                            BlackPieces ^= MoveUtility.BitStates[toMove - 8];
                            AllPieces ^= fromAndToBitboard | MoveUtility.BitStates[toMove - 8];
                            BoardArray[toMove - 8] = MoveUtility.EmptyPiece;
                            HashKey ^= zobristHash.PieceArray[toMove - 8][MoveUtility.BlackPawn];
                        }
                        else
                        {
                           CapturePiece(zobristHash, toMove, capturedPiece, fromBitboard);
                        }
                    }
                    else
                    {
                        AllPieces ^= fromAndToBitboard;
                    }

                    if (move.IsPromotion())
                    {
                        PromotePiece(zobristHash, move.GetPromotedPiece(), toMove);
                        BoardArray[toMove] = move.GetPromotedPiece();
                    }
                    break;
                }
                case MoveUtility.WhiteKing:
                {
                    WhiteKing ^= fromAndToBitboard;
                    WhitePieces ^= fromAndToBitboard;
                    BoardArray[toMove] = MoveUtility.WhiteKing;

                    if (CurrentWhiteCastleStatus == 1 || CurrentWhiteCastleStatus == 3) 
                        HashKey ^= zobristHash.WhiteCanCastleOO;

                    if (CurrentWhiteCastleStatus == 2 || CurrentWhiteCastleStatus == 3)
                        HashKey ^= zobristHash.WhiteCanCastleOOO;

                    CurrentWhiteCastleStatus = (int)CastleStatus.CannotCastle;

                    if (capturedPiece > 0)
                    {
                       CapturePiece(zobristHash, toMove, capturedPiece, fromBitboard);
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
                            BoardArray[7] = MoveUtility.EmptyPiece;
                            BoardArray[5] = MoveUtility.WhiteRook;

                            HashKey ^= (zobristHash.PieceArray[5][MoveUtility.WhiteRook] ^
                                        zobristHash.PieceArray[7][MoveUtility.WhiteRook]);
                        }
                        else
                        {
                            WhiteRooks ^= MoveUtility.BitStates[0] | MoveUtility.BitStates[3];
                            WhitePieces ^= MoveUtility.BitStates[0] | MoveUtility.BitStates[3];
                            AllPieces ^= MoveUtility.BitStates[0] | MoveUtility.BitStates[3];
                            BoardArray[0] = MoveUtility.EmptyPiece;
                            BoardArray[3] = MoveUtility.WhiteRook;

                            HashKey ^= (zobristHash.PieceArray[0][MoveUtility.WhiteRook] ^
                                       zobristHash.PieceArray[3][MoveUtility.WhiteRook]);
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
                       CapturePiece(zobristHash, toMove, capturedPiece, fromBitboard);
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
                       CapturePiece(zobristHash, toMove, capturedPiece, fromBitboard);
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
                        if ((CurrentWhiteCastleStatus == 2 || CurrentWhiteCastleStatus == 3))
                        {
                            CurrentWhiteCastleStatus &= ~(int) CastleStatus.OOOCastle;
                            HashKey ^= zobristHash.WhiteCanCastleOOO;
                        }
                    }
                    if (fromMove == 7)
                    {
                        if ((CurrentWhiteCastleStatus == 1 || CurrentWhiteCastleStatus == 3))
                        {
                            CurrentWhiteCastleStatus &= ~(int) CastleStatus.OOCastle;
                            HashKey ^= zobristHash.WhiteCanCastleOO;
                        }
                    }

                    if (capturedPiece > 0)
                    {
                       CapturePiece(zobristHash, toMove, capturedPiece, fromBitboard);
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
                       CapturePiece(zobristHash, toMove, capturedPiece, fromBitboard);
                    }
                    else
                    {
                        AllPieces ^= fromAndToBitboard;
                    }
                    break;
                }
                case MoveUtility.BlackPawn:
                {
                    FiftyMoveRuleCount = 0;
                    BlackPawns ^= fromAndToBitboard;
                    BlackPieces ^= fromAndToBitboard;

                    BoardArray[toMove] = MoveUtility.BlackPawn;

                    if (MoveUtility.Ranks[fromMove] == 7 && MoveUtility.Ranks[toMove] == 5)
                    {
                        EnpassantTargetSquare = fromMove - 8;
                        HashKey ^= zobristHash.EnPassantSquares[fromMove - 8];
                    }

                    if (capturedPiece > 0)
                    {
                        if (move.IsEnPassant())
                        {
                            WhitePawns ^= MoveUtility.BitStates[toMove + 8];
                            WhitePieces ^= MoveUtility.BitStates[toMove + 8];
                            AllPieces ^= fromAndToBitboard | MoveUtility.BitStates[toMove + 8];
                            BoardArray[toMove + 8] = MoveUtility.EmptyPiece;
                            HashKey ^= zobristHash.PieceArray[toMove + 8][MoveUtility.WhitePawn];

                        }
                        else
                        {
                           CapturePiece(zobristHash, toMove, capturedPiece, fromBitboard);
                        }
                    }
                    else
                    {
                        AllPieces ^= fromAndToBitboard;
                    }

                    if (move.IsPromotion())
                    {
                        PromotePiece(zobristHash, move.GetPromotedPiece(), toMove);
                        BoardArray[toMove] = move.GetPromotedPiece();
                    }
                    break;
                }
                case MoveUtility.BlackKing:
                {
                    BlackKing ^= fromAndToBitboard;
                    BlackPieces ^= fromAndToBitboard;
                    BoardArray[toMove] = MoveUtility.BlackKing;

                    if (CurrentBlackCastleStatus == 1 || CurrentBlackCastleStatus == 3)
                        HashKey ^= zobristHash.BlackCanCastleOO;

                    if (CurrentBlackCastleStatus == 2 || CurrentBlackCastleStatus == 3)
                        HashKey ^= zobristHash.BlackCanCastleOOO;

                    CurrentBlackCastleStatus = (int) CastleStatus.CannotCastle;

                    if (capturedPiece > 0)
                    {
                       CapturePiece(zobristHash, toMove, capturedPiece, fromBitboard);
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
                            BoardArray[63] = MoveUtility.EmptyPiece;
                            BoardArray[61] = MoveUtility.BlackRook;

                            HashKey ^= (zobristHash.PieceArray[61][MoveUtility.BlackRook] ^
                                zobristHash.PieceArray[63][MoveUtility.BlackRook]);

                        }
                        else
                        {
                            BlackRooks ^= MoveUtility.BitStates[56] | MoveUtility.BitStates[59];
                            BlackPieces ^= MoveUtility.BitStates[56] | MoveUtility.BitStates[59];
                            AllPieces ^= MoveUtility.BitStates[56] | MoveUtility.BitStates[59];
                            BoardArray[56] = MoveUtility.EmptyPiece;
                            BoardArray[59] = MoveUtility.BlackRook;

                            HashKey ^= (zobristHash.PieceArray[56][MoveUtility.BlackRook] ^
                               zobristHash.PieceArray[59][MoveUtility.BlackRook]);
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
                       CapturePiece(zobristHash, toMove, capturedPiece, fromBitboard);
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
                       CapturePiece(zobristHash, toMove, capturedPiece, fromBitboard);
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
                        if ((CurrentBlackCastleStatus == 2 || CurrentBlackCastleStatus == 3))
                        {
                            CurrentBlackCastleStatus &= ~(int)CastleStatus.OOOCastle;
                            HashKey ^= zobristHash.BlackCanCastleOOO;
                        }
                    }
                    if (fromMove == 63)
                    {
                        if ((CurrentBlackCastleStatus == 1 || CurrentBlackCastleStatus == 3))
                        {
                            CurrentBlackCastleStatus &= ~(int) CastleStatus.OOCastle;
                            HashKey ^= zobristHash.BlackCanCastleOO;
                        }
                    }

                    if (capturedPiece > 0)
                    {
                       CapturePiece(zobristHash, toMove, capturedPiece, fromBitboard);
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
                       CapturePiece(zobristHash, toMove, capturedPiece, fromBitboard);
                    }
                    else
                    {
                        AllPieces ^= fromAndToBitboard;
                    }
                    break;
                }
            }
            WhiteToMove = !WhiteToMove;
            HashKey ^= zobristHash.WhiteToMove;
        }

        /// <summary>
        /// Updates the gamestate to reflect a move being unmade
        /// </summary>
        /// <param name="move">The move to undo</param>
        internal void UnMakeMove(uint move)
        {
            var fromMove = move.GetFromMove();
            var toMove = move.GetToMove();
            var movingPiece = move.GetMovingPiece();
            var capturedPiece = move.GetCapturedPiece();

            ulong fromBitboard = MoveUtility.BitStates[fromMove];
            ulong fromAndToBitboard = fromBitboard | MoveUtility.BitStates[toMove];

            BoardArray[toMove] = MoveUtility.EmptyPiece;

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

                    CurrentWhiteCastleStatus = (int) CastleStatus.CannotCastle;

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
                            BoardArray[5] = MoveUtility.EmptyPiece;
                            BoardArray[7] = MoveUtility.WhiteRook;
                        }
                        else
                        {
                            WhiteRooks ^= MoveUtility.BitStates[0] | MoveUtility.BitStates[3];
                            WhitePieces ^= MoveUtility.BitStates[0] | MoveUtility.BitStates[3];
                            AllPieces ^= MoveUtility.BitStates[0] | MoveUtility.BitStates[3];
                            BoardArray[3] = MoveUtility.EmptyPiece;
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
                        UnCapturePiece(toMove, capturedPiece, fromBitboard);
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

                    CurrentBlackCastleStatus = (int) CastleStatus.CannotCastle;

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
                            BlackRooks ^= MoveUtility.BitStates[61] | MoveUtility.BitStates[63];
                            BlackPieces ^= MoveUtility.BitStates[61] | MoveUtility.BitStates[63];
                            AllPieces ^= MoveUtility.BitStates[61] | MoveUtility.BitStates[63];
                            BoardArray[61] = MoveUtility.EmptyPiece;
                            BoardArray[63] = MoveUtility.BlackRook;
                        }
                        else
                        {
                            BlackRooks ^= MoveUtility.BitStates[56] | MoveUtility.BitStates[59];
                            BlackPieces ^= MoveUtility.BitStates[56] | MoveUtility.BitStates[59];
                            AllPieces ^= MoveUtility.BitStates[56] | MoveUtility.BitStates[59];
                            BoardArray[59] = MoveUtility.EmptyPiece;
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
                        CurrentWhiteCastleStatus &= ~(int) CastleStatus.OOOCastle;
                    }
                    if (fromMove == 63)
                    {
                        CurrentWhiteCastleStatus &= ~(int) CastleStatus.OOCastle;
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
                        UnCapturePiece(toMove, capturedPiece, fromBitboard);
                    }
                    else
                    {
                        AllPieces ^= fromAndToBitboard;
                    }
                    break;
                }
            }
            UpdateGameStateWithGameStateRecord(PreviousGameStateRecords[TotalMoveCount]);
            WhiteToMove = !WhiteToMove;
            TotalMoveCount--;
        }

        /// <summary>
        /// Determines if the current sides king is under attack
        /// </summary>
        /// <returns>A bool value indicating if the current sides king is under attack</returns>
        internal bool IsCurrentSideKingAttacked(MoveData moveData)
        {
            return this.IsBitBoardAttacked(moveData, WhiteToMove ? WhiteKing : BlackKing, !WhiteToMove);
        }

        /// <summary>
        /// Determines if the opposite side king is under attack
        /// </summary>
        /// <returns>A bool value indicating if the current sides king is under attack</returns>
        internal bool IsOppositeSideKingAttacked(MoveData moveData)
        {
            return this.IsBitBoardAttacked(moveData, WhiteToMove ? BlackKing : WhiteKing, WhiteToMove);
        }

        /// <summary>
        /// Determines if the current board is in a three move repetition state
        /// </summary>
        /// <returns>A value indiciating if the current position results in a three move repetition draw</returns>
        internal bool IsThreeMoveRepetition()
        {
            var searchDistance = TotalMoveCount - FiftyMoveRuleCount;
            var repetitions = 1;

            for (var i = TotalMoveCount;
                 i >= searchDistance && i > 0;
                 i--)
            {
                if (HashKey == PreviousGameStateRecords[i].HashKey)
                {
                    repetitions++;
                }

            }
            return repetitions >= 3;
        }

        /// <summary>
        /// Counts the number of legal moves at he current position. This is useful before beginning a search if only a single move is possible from a given position.
        /// </summary>
        /// <param name="moveData">The movedata used to generate moves.</param>
        /// <param name="zobristHash">The zobrist hash data to use</param>
        /// <param name="move">The last legal move found</param>
        /// <returns>The count of legal moves</returns>
        internal int CountLegalMovesAtCurrentPlyAndReturnMove(MoveData moveData, ZobristHash zobristHash, out uint move)
        {
            move = 0U;
            var count = 0;

            foreach (var moveToTest in Moves[0])
            {
                MakeMove(moveToTest, zobristHash);

                if (!IsOppositeSideKingAttacked(moveData))
                {
                    move = moveToTest;
                    count++;
                }
                UnMakeMove(moveToTest);
            }
            return count;
        }
        #endregion

        #region Private Methods

        private void LoadGameStateFromFen(string fen, ZobristHash zobristHash)
        {
            fen = fen.Trim(' ');

            var splitFen = fen.Split(' ');

            if (splitFen.Count() < 4)
                throw new InvalidDataException(
                    string.Format("The number of fields in the FEN string were incorrect. Expected 4, Received {0}",
                        splitFen.Count()));

            var ranks = splitFen[0].Split('/');

            if (ranks.Count() != 8)
                throw new InvalidDataException(
                    string.Format("The number of rows in the FEN string were incorrect. Expected 8, Received {0}",
                        splitFen.Count()));

            var castleStatus = splitFen[2];
            var whiteCastleStatus = 0;
            var blackCastleStatus = 0;

            if (castleStatus.Contains("Q"))
                whiteCastleStatus += 2;
            if (castleStatus.Contains("K"))
                whiteCastleStatus += 1;

            if (castleStatus.Contains("q"))
                blackCastleStatus += 2;
            if (castleStatus.Contains("k"))
                blackCastleStatus += 1;
            
            FiftyMoveRuleCount = splitFen.Length > 4 ? int.Parse(splitFen[4]) : 0;
            TotalMoveCount = splitFen.Length > 5 ? int.Parse(splitFen[5]) : 0;
            WhiteToMove = splitFen[1].ToLower() == "w";
            CurrentWhiteCastleStatus = whiteCastleStatus;
            CurrentBlackCastleStatus = blackCastleStatus;
            EnpassantTargetSquare = GetEnPassantSquare(splitFen[3]);

            if (WhiteToMove)
                HashKey ^= zobristHash.WhiteToMove;

            if (CurrentWhiteCastleStatus == (int)CastleStatus.BothCastle || CurrentWhiteCastleStatus == (int)CastleStatus.OOCastle)
            {
                HashKey ^= zobristHash.WhiteCanCastleOO;
            }

            if (CurrentWhiteCastleStatus == (int)CastleStatus.BothCastle || CurrentWhiteCastleStatus == (int)CastleStatus.OOOCastle)
            {
                HashKey ^= zobristHash.WhiteCanCastleOOO;
            }

            if (CurrentBlackCastleStatus == (int)CastleStatus.BothCastle || CurrentBlackCastleStatus == (int)CastleStatus.OOCastle)
            {
                HashKey ^= zobristHash.BlackCanCastleOO;
            }

            if (CurrentBlackCastleStatus == (int)CastleStatus.BothCastle || CurrentBlackCastleStatus == (int)CastleStatus.OOOCastle)
            {
                HashKey ^= zobristHash.BlackCanCastleOOO;
            }

            if (EnpassantTargetSquare > 0)
            {
                HashKey ^= zobristHash.EnPassantSquares[EnpassantTargetSquare];
            }

            var rankNumber = 7;
            foreach (var rank in ranks)
            {
                var file = 0;
                foreach (var c in rank)
                {
                    //Rank is empty
                    if (c == '8')
                        break;

                    var boardArrayPosition = (rankNumber * 8 + file);
                    var currentPosition = MoveUtility.BitStates[boardArrayPosition];

                    switch (c)
                    {
                        case 'p':
                            {
                                BoardArray[boardArrayPosition] = MoveUtility.BlackPawn;
                                BlackPawns += currentPosition;
                                BlackPieces += currentPosition;
                                AllPieces += currentPosition;
                                HashKey ^= zobristHash.PieceArray[boardArrayPosition][MoveUtility.BlackPawn];
                                break;
                            }
                        case 'k':
                            {
                                BoardArray[boardArrayPosition] = MoveUtility.BlackKing;
                                BlackKing += currentPosition;
                                BlackPieces += currentPosition;
                                AllPieces += currentPosition;
                                HashKey ^= zobristHash.PieceArray[boardArrayPosition][MoveUtility.BlackKing];
                                break;
                            }
                        case 'q':
                            {
                                BoardArray[boardArrayPosition] = MoveUtility.BlackQueen;
                                BlackQueens += currentPosition;
                                BlackPieces += currentPosition;
                                AllPieces += currentPosition;
                                HashKey ^= zobristHash.PieceArray[boardArrayPosition][MoveUtility.BlackQueen];
                                break;
                            }
                        case 'r':
                            {
                                BoardArray[boardArrayPosition] = MoveUtility.BlackRook;
                                BlackRooks += currentPosition;
                                BlackPieces += currentPosition;
                                AllPieces += currentPosition;
                                HashKey ^= zobristHash.PieceArray[boardArrayPosition][MoveUtility.BlackRook];
                                break;
                            }
                        case 'n':
                            {
                                BoardArray[boardArrayPosition] = MoveUtility.BlackKnight;
                                BlackKnights += currentPosition;
                                BlackPieces += currentPosition;
                                AllPieces += currentPosition;
                                HashKey ^= zobristHash.PieceArray[boardArrayPosition][MoveUtility.BlackKnight];
                                break;
                            }
                        case 'b':
                            {
                                BoardArray[boardArrayPosition] = MoveUtility.BlackBishop;
                                BlackBishops += currentPosition;
                                BlackPieces += currentPosition;
                                AllPieces += currentPosition;
                                HashKey ^= zobristHash.PieceArray[boardArrayPosition][MoveUtility.BlackBishop];
                                break;
                            }

                        case 'P':
                            {
                                BoardArray[boardArrayPosition] = MoveUtility.WhitePawn;
                                WhitePawns += currentPosition;
                                WhitePieces += currentPosition;
                                AllPieces += currentPosition;
                                HashKey ^= zobristHash.PieceArray[boardArrayPosition][MoveUtility.WhitePawn];
                                break;
                            }
                        case 'K':
                            {
                                BoardArray[boardArrayPosition] = MoveUtility.WhiteKing;
                                WhiteKing += currentPosition;
                                WhitePieces += currentPosition;
                                AllPieces += currentPosition;
                                HashKey ^= zobristHash.PieceArray[boardArrayPosition][MoveUtility.WhiteKing];
                                break;
                            }
                        case 'Q':
                            {
                                BoardArray[boardArrayPosition] = MoveUtility.WhiteQueen;
                                WhiteQueens += currentPosition;
                                WhitePieces += currentPosition;
                                AllPieces += currentPosition;
                                HashKey ^= zobristHash.PieceArray[boardArrayPosition][MoveUtility.WhiteQueen];
                                break;
                            }
                        case 'R':
                            {
                                BoardArray[boardArrayPosition] = MoveUtility.WhiteRook;
                                WhiteRooks += currentPosition;
                                WhitePieces += currentPosition;
                                AllPieces += currentPosition;
                                HashKey ^= zobristHash.PieceArray[boardArrayPosition][MoveUtility.WhiteRook];

                                break;
                            }
                        case 'N':
                            {
                                BoardArray[boardArrayPosition] = MoveUtility.WhiteKnight;
                                WhiteKnights += currentPosition;
                                WhitePieces += currentPosition;
                                AllPieces += currentPosition;
                                HashKey ^= zobristHash.PieceArray[boardArrayPosition][MoveUtility.WhiteKnight];
                                break;
                            }
                        case 'B':
                            {
                                BoardArray[boardArrayPosition] = MoveUtility.WhiteBishop;
                                WhiteBishops += currentPosition;
                                WhitePieces += currentPosition;
                                AllPieces += currentPosition;
                                HashKey ^= zobristHash.PieceArray[boardArrayPosition][MoveUtility.WhiteBishop];
                                break;
                            }
                        default:
                            {
                                file += int.Parse(c.ToString(CultureInfo.InvariantCulture)) - 1;
                                break;
                            }
                    }
                    file++;
                }
                rankNumber--;
            }
        }

        private static uint GetEnPassantSquare(string enpassant)
        {
            if (enpassant == "-")
                return 0;

            var file = char.Parse(enpassant.Substring(0, 1));
            var rank = int.Parse(enpassant.Substring(1, 1)) - 1;

            var fileNumber = Array.IndexOf(_files, file);

            return (uint)((rank * 8) + fileNumber);
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

        private void PromotePiece(ZobristHash zobristHash, uint promotedPiece, uint toMove)
        {
            var toBitBoard = MoveUtility.BitStates[toMove];

            if (WhiteToMove)
            {
                WhitePawns ^= toBitBoard;
                HashKey ^= (zobristHash.PieceArray[toMove][MoveUtility.WhitePawn] ^
                            zobristHash.PieceArray[toMove][promotedPiece]);

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
                HashKey ^= (zobristHash.PieceArray[toMove][MoveUtility.BlackPawn] ^
                            zobristHash.PieceArray[toMove][promotedPiece]);

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
                        CurrentWhiteCastleStatus &= ~(int) CastleStatus.OOOCastle;
                    }
                    if (toMove == 7)
                    {
                        CurrentWhiteCastleStatus &= ~(int) CastleStatus.OOCastle;
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
                        CurrentWhiteCastleStatus &= ~(int) CastleStatus.OOOCastle;
                    }
                    if (toMove == 63)
                    {
                        CurrentWhiteCastleStatus &= ~(int) CastleStatus.OOCastle;
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

        private void CapturePiece(ZobristHash zobristHash, uint toMove, uint capturedPiece, ulong fromBitBoard)
        {
            var toBitBoard = MoveUtility.BitStates[toMove];
            HashKey ^= zobristHash.PieceArray[toMove][capturedPiece];

            FiftyMoveRuleCount = 0;
            AllPieces ^= fromBitBoard;

            switch (capturedPiece)
            {
                case MoveUtility.WhitePawn:
                {
                    WhitePawns ^= toBitBoard;
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
                        if ((CurrentWhiteCastleStatus == 2 || CurrentWhiteCastleStatus == 3))
                        {
                            CurrentWhiteCastleStatus &= ~(int) CastleStatus.OOOCastle;
                            HashKey ^= zobristHash.WhiteCanCastleOOO;
                        }
                    }
                    if (toMove == 7)
                    {
                        if ((CurrentWhiteCastleStatus == 1 || CurrentWhiteCastleStatus == 3))
                        {
                            CurrentWhiteCastleStatus &= ~(int) CastleStatus.OOCastle;
                            HashKey ^= zobristHash.WhiteCanCastleOO;
                        }
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
                    BlackPawns ^= toBitBoard;
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
                        if ((CurrentBlackCastleStatus == 2 || CurrentBlackCastleStatus == 3))
                        {
                            CurrentBlackCastleStatus &= ~(int)CastleStatus.OOOCastle;
                            HashKey ^= zobristHash.BlackCanCastleOOO;
                        }
                    }
                    if (toMove == 63)
                    {
                        if ((CurrentBlackCastleStatus == 1 || CurrentBlackCastleStatus == 3))
                        {
                            CurrentBlackCastleStatus &= ~(int)CastleStatus.OOCastle;
                            HashKey ^= zobristHash.BlackCanCastleOO;
                        }
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

        private GameStateRecord CreateGameStateRecord(uint move)
        {
            return new GameStateRecord
            {
                Move = move,
                CurrentWhiteCastleStatus = CurrentWhiteCastleStatus,
                CurrentBlackCastleStatus = CurrentBlackCastleStatus,
                EnpassantTargetSquare = EnpassantTargetSquare,
                FiftyMoveRuleCount = FiftyMoveRuleCount,
                HashKey = HashKey
            };
        }

        private void UpdateGameStateWithGameStateRecord(GameStateRecord gameStateRecord)
        {
            CurrentWhiteCastleStatus = gameStateRecord.CurrentWhiteCastleStatus;
            CurrentBlackCastleStatus = gameStateRecord.CurrentBlackCastleStatus;
            EnpassantTargetSquare = gameStateRecord.EnpassantTargetSquare;
            FiftyMoveRuleCount = gameStateRecord.FiftyMoveRuleCount;
            HashKey = gameStateRecord.HashKey;
        }

        #endregion
    }
}
