﻿using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using Common.Logging;
using DotNetEngine.Engine.Enums;
using DotNetEngine.Engine.Objects;

namespace DotNetEngine.Engine.Helpers
{
    /// <summary>
    /// Helper methods that act on the GameState object and its properties
    /// </summary>
    internal static class GameStateUtility
    {
        #region Private Properties

        private static readonly string _divideOutput = "Move Nodes" + Environment.NewLine;

        private static readonly char[] _files =
        {
            'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h'
        };

        private static readonly ILog _logger = LogManager.GetCurrentClassLogger();

        #endregion

        #region Internal Methods

        /// <summary>
        /// A helper method that converts the current game gameState to a easy to read console output
        /// </summary>
        /// <param name="state">The Current Game gameState</param>
        /// <returns>A formatted chess board</returns>
        internal static string ConvertBitBoardsToConsoleOutput(this GameState state)
        {
            var output = new StringBuilder(" - - - - - - - - ");
            output.AppendLine();

            for (var i = 7; i >= 0; i--)
            {
                for (var j = 0; j < 8; j++)
                {
                    output.Append("|");

                    var position = MoveUtility.BitStates[(i*8) + j];

                    if ((state.BlackBishops & position) == position)
                        output.Append("b");
                    else if ((state.BlackKing & position) == position)
                        output.Append("k");
                    else if ((state.BlackKnights & position) == position)
                        output.Append("n");
                    else if ((state.BlackPawns & position) == position)
                        output.Append("p");
                    else if ((state.BlackQueens & position) == position)
                        output.Append("q");
                    else if ((state.BlackRooks & position) == position)
                        output.Append("r");

                    else if ((state.WhiteBishops & position) == position)
                        output.Append("B");
                    else if ((state.WhiteKing & position) == position)
                        output.Append("K");
                    else if ((state.WhiteKnights & position) == position)
                        output.Append("N");
                    else if ((state.WhitePawns & position) == position)
                        output.Append("P");
                    else if ((state.WhiteQueens & position) == position)
                        output.Append("Q");
                    else if ((state.WhiteRooks & position) == position)
                        output.Append("R");
                    else
                        output.Append(" ");
                }

                output.AppendLine("|");
                output.AppendLine(" - - - - - - - - ");
            }

            return output.ToString();
        }

        /// <summary>
        /// A helper method that converts the board array to console output
        /// </summary>
        /// <param name="state">The Current Game gameState</param>
        /// <returns>A formatted chess board</returns>
        internal static string ConvertBoardArrayToConsoleOutput(this GameState state)
        {
            var output = new StringBuilder(" - - - - - - - - ");
            output.AppendLine();

            for (var i = 7; i >= 0; i--)
            {
                for (var j = 0; j < 8; j++)
                {
                    output.Append("|");

                    var position = state.BoardArray[(i*8) + j];

                    switch (position)
                    {
                        case MoveUtility.WhitePawn:
                        {
                            output.Append("P");
                            break;
                        }
                        case MoveUtility.WhiteKnight:
                        {
                            output.Append("N");
                            break;
                        }
                        case MoveUtility.WhiteKing:
                        {
                            output.Append("K");
                            break;
                        }
                        case MoveUtility.WhiteBishop:
                        {
                            output.Append("B");
                            break;
                        }
                        case MoveUtility.WhiteRook:
                        {
                            output.Append("R");
                            break;
                        }
                        case MoveUtility.WhiteQueen:
                        {
                            output.Append("Q");
                            break;
                        }

                        case MoveUtility.BlackPawn:
                        {
                            output.Append("p");
                            break;
                        }
                        case MoveUtility.BlackKnight:
                        {
                            output.Append("n");
                            break;
                        }
                        case MoveUtility.BlackKing:
                        {
                            output.Append("k");
                            break;
                        }
                        case MoveUtility.BlackBishop:
                        {
                            output.Append("b");
                            break;
                        }
                        case MoveUtility.BlackRook:
                        {
                            output.Append("r");
                            break;
                        }
                        case MoveUtility.BlackQueen:
                        {
                            output.Append("q");
                            break;
                        }
                        default:
                        {
                            output.Append(" ");
                            break;
                        }

                    }
                }

                output.AppendLine("|");
                output.AppendLine(" - - - - - - - - ");
            }
            return output.ToString();
        }

        /// <summary>
        /// A helper method that converts a single bitboard to a easy to read console output
        /// </summary>
        /// <param name="bitboard"></param>
        /// <returns>A formatted chess board</returns>
        internal static string ConvertSingleBitBoardsToConsoleOutput(this ulong bitboard)
        {
            var output = new StringBuilder(" - - - - - - - - ");
            output.AppendLine();

            for (var i = 7; i >= 0; i--)
            {
                for (var j = 0; j < 8; j++)
                {
                    output.Append("|");

                    var position = MoveUtility.BitStates[(i*8) + j];

                    output.Append((position & bitboard) == position ? "X" : " ");
                }

                output.AppendLine("|");
                output.AppendLine(" - - - - - - - - ");
            }

            return output.ToString();
        }

        /// <summary>
        /// A helper methods that converts all moves from a given ply to console output
        /// </summary>
        /// <param name="gameState">The current gamestate</param>
        /// <param name="ply">The ply to be converted</param>
        /// <returns>A string that contains all of the moves from a given ply</returns>
        internal static string ConvertAllMovesFromPlyToConsoleOutput(this GameState gameState, int ply)
        {
            var sb = new StringBuilder();
            foreach (var move in gameState.Moves[ply])
            {
                sb.AppendLine(
                    string.Format("FromSquare {0} ToSquare {1} MovingPiece {2} CapturedPiece {3} PromotedPiece {4}",
                        move.GetFromMove(), move.GetToMove(), move.GetMovingPiece(), move.GetCapturedPiece(),
                        move.GetPromotedPiece()));
            }
            return sb.ToString();
        }

        /// <summary>
        /// Runs perft (Performance Test) against a gamestate. This is used to verify that all moves are being generated correctly.
        /// </summary>
        /// <param name="gameState">The gamestate being tested</param>
        /// <param name="moveData">The moveData to use</param>
        /// <param name="perftData">The perft data. This is used if collection additional information about perft</param>
        /// <param name="ply">The ply to start the search on</param>
        /// <param name="depth">The depth of perft to run</param>
        /// <returns>A count of the total number of nodes at depth 0</returns>
        internal static ulong RunPerftRecursively(this GameState gameState, MoveData moveData, PerftData perftData,
            int ply, int depth)
        {
            if (depth == 0)
            {
                return 1;
            }

            ulong count = 0;

            gameState.GenerateMoves(MoveGenerationMode.All, ply, moveData);

            foreach (var move in gameState.Moves[ply])
            {

#if DEBUG
                var boardArray = new uint[64];

                for (var i = 0; i < boardArray.Length - 1; i++)
                {
                    boardArray[i] = gameState.BoardArray[i];
                }

                if (_logger.IsTraceEnabled)
                {
                    _logger.TraceFormat(
                        "MoveHash {0} GameState Move From {1} To {2} MovingPiece {3} CapturedPiece {4} PromotedPeice {5}",
                        move.GetHashCode(), move.GetFromMove().ToRankAndFile(), move.GetToMove().ToRankAndFile(),
                        move.GetMovingPiece(), move.GetCapturedPiece(), move.GetPromotedPiece());

                    _logger.TraceFormat("GameState All Bitboards Before Move {0} {1}", Environment.NewLine,
                        gameState.ConvertBitBoardsToConsoleOutput());
                    _logger.TraceFormat("GameState BoardArray Before Move {0} {1}", Environment.NewLine,
                        gameState.ConvertBoardArrayToConsoleOutput());
                }

#endif
                gameState.MakeMove(move);

#if DEBUG
                if (_logger.IsTraceEnabled)
                {
                    _logger.TraceFormat("MoveHash {0} GameState All Bitboards After Move {1} {2}", move.GetHashCode(),
                        Environment.NewLine,
                        gameState.ConvertBitBoardsToConsoleOutput());
                    _logger.TraceFormat("MoveHash {0} GameState BoardArray After Move {1} {2}", move.GetHashCode(),
                        Environment.NewLine,
                        gameState.ConvertBoardArrayToConsoleOutput());
                }
#endif

                if (!gameState.IsOppositeSideKingAttacked(moveData))
                {
                    count += RunPerftRecursively(gameState, moveData, perftData, ply + 1, depth - 1);
#if DEBUG
                    if (depth == 1)
                    {
                        if (move.IsPieceCaptured())
                            perftData.TotalCaptures++;
                        if (move.IsEnPassant())
                            perftData.TotalEnpassants++;
                        if (move.IsPromotion())
                            perftData.TotalPromotions++;
                        if (move.IsCastleOO())
                            perftData.TotalOOCastles++;
                        if (move.IsCastleOOO())
                            perftData.TotalOOOCastles++;
                        if (gameState.IsCurrentSideKingAttacked(moveData))
                            perftData.TotalChecks++;
                    }
#endif
                }

                gameState.UnMakeMove(move);

#if DEBUG
                if (_logger.IsTraceEnabled)
                {
                    _logger.TraceFormat("MoveHash {0} GameState All Bitboards After UnMakeMove {1} {2}",
                        move.GetHashCode(), Environment.NewLine,
                        gameState.ConvertBitBoardsToConsoleOutput());
                    _logger.TraceFormat("MoveHash {0} GameState BoardArray After UnMakeMove {1} {2}", move.GetHashCode(),
                        Environment.NewLine,
                        gameState.ConvertBoardArrayToConsoleOutput());
                }

                for (var i = 0; i < boardArray.Length - 1; i++)
                {
                    Debug.Assert(boardArray[i] == gameState.BoardArray[i]);
                }
#endif
            }
            return count;
        }

        /// <summary>
        /// Runs similar to perft against a gamestate. Returns the total number of nodes that each move from the starting position makes. This is used when troublshooting move generation
        /// </summary>
        /// <param name="gameState">The gamestate being tested</param>
        /// <param name="moveData">The moveData to use</param>
        /// <param name="perftData">The perft data. This is used if collection additional information about perft</param>
        /// <param name="ply">The ply to start the search on</param>
        /// <param name="depth">The depth of perft to run</param>
        internal static void CalculateDivide(this GameState gameState, MoveData moveData, PerftData perftData, int ply,
            int depth)
        {
            var sb = new StringBuilder(_divideOutput);
            ulong count = 0;

            gameState.GenerateMoves(MoveGenerationMode.All, ply, moveData);

            foreach (var move in gameState.Moves[ply])
            {
                gameState.MakeMove(move);

                if (!gameState.IsOppositeSideKingAttacked(moveData))
                {
                    ulong moveCount = RunPerftRecursively(gameState, moveData, perftData, ply + 1, depth - 1);
                    sb.AppendFormat("{0}{1} {2}{3}", MoveUtility.RankAndFile[move.GetFromMove()],
                        MoveUtility.RankAndFile[move.GetToMove()], moveCount, Environment.NewLine);
                    count += moveCount;
                }
                gameState.UnMakeMove(move);
            }
            sb.AppendFormat("Total Nodes: {0}", count);
            _logger.InfoFormat(sb.ToString());

        }

        /// <summary>
        /// Take a FEN string and converts it into a GameState object
        /// </summary>
        /// <param name="fen">The fen string to be loaded</param>
        internal static GameState LoadGameStateFromFen(string fen)
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

            var gameState = new GameState
            {
                FiftyMoveRuleCount = splitFen.Length > 4 ? int.Parse(splitFen[4]) : 0,
                TotalMoveCount = splitFen.Length > 5 ? int.Parse(splitFen[5]) : 0,
                WhiteToMove = splitFen[1].ToLower() == "w",
                CurrentWhiteCastleStatus = whiteCastleStatus,
                CurrentBlackCastleStatus = blackCastleStatus,
                EnpassantTargetSquare = GetEnPassantSquare(splitFen[3])
            };

            var rankNumber = 7;
            foreach (var rank in ranks)
            {
                var file = 0;
                foreach (var c in rank)
                {
                    //Rank is empty
                    if (c == '8')
                        break;

                    var boardArrayPosition = (rankNumber*8 + file);
                    var currentPosition = MoveUtility.BitStates[boardArrayPosition];

                    switch (c)
                    {
                        case 'p':
                        {
                            gameState.BoardArray[boardArrayPosition] = MoveUtility.BlackPawn;
                            gameState.BlackPawns += currentPosition;
                            gameState.BlackPieces += currentPosition;
                            gameState.AllPieces += currentPosition;
                            break;
                        }
                        case 'k':
                        {
                            gameState.BoardArray[boardArrayPosition] = MoveUtility.BlackKing;
                            gameState.BlackKing += currentPosition;
                            gameState.BlackPieces += currentPosition;
                            gameState.AllPieces += currentPosition;
                            break;
                        }
                        case 'q':
                        {
                            gameState.BoardArray[boardArrayPosition] = MoveUtility.BlackQueen;
                            gameState.BlackQueens += currentPosition;
                            gameState.BlackPieces += currentPosition;
                            gameState.AllPieces += currentPosition;
                            break;
                        }
                        case 'r':
                        {
                            gameState.BoardArray[boardArrayPosition] = MoveUtility.BlackRook;
                            gameState.BlackRooks += currentPosition;
                            gameState.BlackPieces += currentPosition;
                            gameState.AllPieces += currentPosition;
                            break;
                        }
                        case 'n':
                        {
                            gameState.BoardArray[boardArrayPosition] = MoveUtility.BlackKnight;
                            gameState.BlackKnights += currentPosition;
                            gameState.BlackPieces += currentPosition;
                            gameState.AllPieces += currentPosition;
                            break;
                        }
                        case 'b':
                        {
                            gameState.BoardArray[boardArrayPosition] = MoveUtility.BlackBishop;
                            gameState.BlackBishops += currentPosition;
                            gameState.BlackPieces += currentPosition;
                            gameState.AllPieces += currentPosition;
                            break;
                        }

                        case 'P':
                        {
                            gameState.BoardArray[boardArrayPosition] = MoveUtility.WhitePawn;
                            gameState.WhitePawns += currentPosition;
                            gameState.WhitePieces += currentPosition;
                            gameState.AllPieces += currentPosition;
                            break;
                        }
                        case 'K':
                        {
                            gameState.BoardArray[boardArrayPosition] = MoveUtility.WhiteKing;
                            gameState.WhiteKing += currentPosition;
                            gameState.WhitePieces += currentPosition;
                            gameState.AllPieces += currentPosition;
                            break;
                        }
                        case 'Q':
                        {
                            gameState.BoardArray[boardArrayPosition] = MoveUtility.WhiteQueen;
                            gameState.WhiteQueens += currentPosition;
                            gameState.WhitePieces += currentPosition;
                            gameState.AllPieces += currentPosition;
                            break;
                        }
                        case 'R':
                        {
                            gameState.BoardArray[boardArrayPosition] = MoveUtility.WhiteRook;
                            gameState.WhiteRooks += currentPosition;
                            gameState.WhitePieces += currentPosition;
                            gameState.AllPieces += currentPosition;

                            break;
                        }
                        case 'N':
                        {
                            gameState.BoardArray[boardArrayPosition] = MoveUtility.WhiteKnight;
                            gameState.WhiteKnights += currentPosition;
                            gameState.WhitePieces += currentPosition;
                            gameState.AllPieces += currentPosition;
                            break;
                        }
                        case 'B':
                        {
                            gameState.BoardArray[boardArrayPosition] = MoveUtility.WhiteBishop;
                            gameState.WhiteBishops += currentPosition;
                            gameState.WhitePieces += currentPosition;
                            gameState.AllPieces += currentPosition;
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
            return gameState;
        }

        #endregion

        #region Private Methods

        private static uint GetEnPassantSquare(string enpassant)
        {
            if (enpassant == "-")
                return 0;

            var file = char.Parse(enpassant.Substring(0, 1));
            var rank = int.Parse(enpassant.Substring(1, 1)) - 1;

            var fileNumber = Array.IndexOf(_files, file);

            return (uint) ((rank*8) + fileNumber);
        }

        #endregion
    }
}