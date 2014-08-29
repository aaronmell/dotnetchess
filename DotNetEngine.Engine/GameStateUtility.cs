using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace DotNetEngine.Engine
{
	/// <summary>
	/// Helper methods that act on the GameState object and its properties
	/// </summary>
	internal static class GameStateUtility
	{
        private static readonly char[] Files = new[]
		{
			'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h'
		};

        /// <summary>
		/// A helper method that converts the current game state to a easy to read console output
		/// </summary>
		/// <param name="state">The Current Game state</param>
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

					var position = MoveUtility.BitStates[(i * 8) + j];

					if ((state.BlackBishops & position) == position)
						output.Append("b");
					else if ((state.BlackKing & position) == position)
						output.Append("k");
					else if ((state.BlackKnights & position) == position)
						output.Append("n");
					else if ((state.BlackPawns & position) == position)
						output.Append("p");
					else if ((state.BlackQueen & position) == position)
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
					else if ((state.WhiteQueen & position) == position)
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
		/// <param name="state">The Current Game state</param>
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

					var position = state.BoardArray[(i * 8) + j];

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
        /// A helper method that converts the current game state to a easy to read console output
        /// </summary>
        /// <param name="state">The Current Game state</param>
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

                    var position = MoveUtility.BitStates[(i * 8) + j];

                    if ((position & bitboard) == position)
                        output.Append("X");
                    else
                        output.Append(" ");
                }

                output.AppendLine("|");
                output.AppendLine(" - - - - - - - - ");
            }

            return output.ToString();
        }
	
        internal static string ConvertAllMovesFromPlyToConsoleOutput(this GameState state, int ply)
        {
            var sb = new StringBuilder();
            foreach (var move in state.Moves[ply])
            {
                sb.AppendLine(string.Format("FromSquare {0} ToSquare {1} MovingPiece {1} CapturedPiece {2} PromotedPiece {3}", move.GetFromMove(), move.GetToMove(), move.GetMovingPiece(), move.GetCapturedPiece(), move.GetPromotedPiece()));
            }
            return sb.ToString();
        }
              

        /// <summary>
        /// Take a FEN string and converts it into a GameState object
        /// </summary>
        /// <param name="fen"></param>
        /// <returns></returns>
        internal static GameState LoadStateFromFen(string fen)
        {
            fen = fen.Trim(' ');

            var splitFen = fen.Split(' ');

            if (splitFen.Count() != 6)
                throw new InvalidDataException(string.Format("The number of fields in the FEN string were incorrect. Expected 6, Received {0}", splitFen.Count()));

            var ranks = splitFen[0].Split('/');

            if (ranks.Count() != 8)
                throw new InvalidDataException(string.Format("The number of rows in the FEN string were incorrect. Expected 8, Received {0}", splitFen.Count()));

            var castleStatus = splitFen[2];
            var whiteCastleStatus = 0;
            var blackCastleStatus = 0;

            if (castleStatus.Contains("Q"))
                whiteCastleStatus += 1;
            if (castleStatus.Contains("K"))
                whiteCastleStatus += 2;

            if (castleStatus.Contains("q"))
                blackCastleStatus += 1;
            if (castleStatus.Contains("k"))
                blackCastleStatus += 2;

            var gameState = new GameState
            {
                FiftyMoveRuleCount = int.Parse(splitFen[4]),
                TotalMoveCount = int.Parse((splitFen[5])),
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

                    var boardArrayPosition = (rankNumber * 8 + file);
                    var currentPosition = MoveUtility.BitStates[boardArrayPosition];

                    switch (c)
                    {
                        case 'p':
                            {
                                gameState.BoardArray[boardArrayPosition] = MoveUtility.BlackPawn;
                                gameState.BlackPawns += currentPosition;
                                break;
                            }
                        case 'k':
                            {
                                gameState.BoardArray[boardArrayPosition] = MoveUtility.BlackKing;
                                gameState.BlackKing += currentPosition;
                                break;
                            }
                        case 'q':
                            {
                                gameState.BoardArray[boardArrayPosition] = MoveUtility.BlackQueen;
                                gameState.BlackQueen += currentPosition;
                                break;
                            }
                        case 'r':
                            {
                                gameState.BoardArray[boardArrayPosition] = MoveUtility.BlackRook;
                                gameState.BlackRooks += currentPosition;
                                break;
                            }
                        case 'n':
                            {
                                gameState.BoardArray[boardArrayPosition] = MoveUtility.BlackKnight;
                                gameState.BlackKnights += currentPosition;
                                break;
                            }
                        case 'b':
                            {
                                gameState.BoardArray[boardArrayPosition] = MoveUtility.BlackBishop;
                                gameState.BlackBishops += currentPosition;
                                break;
                            }

                        case 'P':
                            {
                                gameState.BoardArray[boardArrayPosition] = MoveUtility.WhitePawn;
                                gameState.WhitePawns += currentPosition;
                                break;
                            }
                        case 'K':
                            {
                                gameState.BoardArray[boardArrayPosition] = MoveUtility.WhiteKing;
                                gameState.WhiteKing += currentPosition;
                                break;
                            }
                        case 'Q':
                            {
                                gameState.BoardArray[boardArrayPosition] = MoveUtility.WhiteQueen;
                                gameState.WhiteQueen += currentPosition;
                                break;
                            }
                        case 'R':
                            {
                                gameState.BoardArray[boardArrayPosition] = MoveUtility.WhiteRook;
                                gameState.WhiteRooks += currentPosition;
                                break;
                            }
                        case 'N':
                            {
                                gameState.BoardArray[boardArrayPosition] = MoveUtility.WhiteKnight;
                                gameState.WhiteKnights += currentPosition;
                                break;
                            }
                        case 'B':
                            {
                                gameState.BoardArray[boardArrayPosition] = MoveUtility.BlackBishop;
                                gameState.WhiteBishops += currentPosition;
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

        private static uint GetEnPassantSquare(string enpassant)
        {
            if (enpassant == "-")
                return 0;

            var file = char.Parse(enpassant.Substring(0, 1));
            var rank = int.Parse(enpassant.Substring(1, 1)) - 1;

            var fileNumber = Array.IndexOf(Files, file);

            return (uint)((rank * 8) + fileNumber);
        }
    }
}
