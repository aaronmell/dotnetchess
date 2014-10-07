using System;
using System.Diagnostics;
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
        /// <param name="zobristHash">The zobristHash data to use</param>
        /// <param name="perftData">The perft data. This is used if collection additional information about perft</param>
        /// <param name="ply">The ply to start the search on</param>
        /// <param name="depth">The depth of perft to run</param>
        /// <returns>A count of the total number of nodes at depth 0</returns>
        internal static ulong RunPerftRecursively(this GameState gameState, MoveData moveData, ZobristHash zobristHash, PerftData perftData,
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
                var previousHash = gameState.HashKey;
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
                gameState.MakeMove(move, zobristHash);

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
                    count += RunPerftRecursively(gameState, moveData, zobristHash, perftData, ply + 1, depth - 1);
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

                Debug.Assert(previousHash == gameState.HashKey);
#endif
            }
            return count;
        }

        /// <summary>
        /// Runs similar to perft against a gamestate. Returns the total number of nodes that each move from the starting position makes. This is used when troublshooting move generation
        /// </summary>
        /// <param name="gameState">The gamestate being tested</param>
        /// <param name="moveData">The moveData to use</param>
        /// <param name="zobristHash">The zobrist hash data to use</param>
        /// <param name="perftData">The perft data. This is used if collection additional information about perft</param>
        /// <param name="ply">The ply to start the search on</param>
        /// <param name="depth">The depth of perft to run</param>
        internal static void CalculateDivide(this GameState gameState, MoveData moveData, ZobristHash zobristHash, PerftData perftData, int ply,
            int depth)
        {
            var sb = new StringBuilder(_divideOutput);
            ulong count = 0;

            gameState.GenerateMoves(MoveGenerationMode.All, ply, moveData);

            foreach (var move in gameState.Moves[ply])
            {
                gameState.MakeMove(move, zobristHash);

                if (!gameState.IsOppositeSideKingAttacked(moveData))
                {
                    ulong moveCount = RunPerftRecursively(gameState, moveData, zobristHash, perftData, ply + 1, depth - 1);
                    sb.AppendFormat("{0}{1} {2}{3}", MoveUtility.RankAndFile[move.GetFromMove()],
                        MoveUtility.RankAndFile[move.GetToMove()], moveCount, Environment.NewLine);
                    count += moveCount;
                }
                gameState.UnMakeMove(move);
            }
            sb.AppendFormat("Total Nodes: {0}", count);
            _logger.InfoFormat(sb.ToString());

        }
        #endregion
    }
}
