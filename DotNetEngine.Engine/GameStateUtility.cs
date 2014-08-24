using System.Text;

namespace DotNetEngine.Engine
{
	/// <summary>
	/// Helper methods that act on the GameState object and its properties
	/// </summary>
	internal static class GameStateUtility
	{	

		/// <summary>
		/// Returns a bitboard that contains all of the white pieces
		/// </summary>
		/// <param name="state">The current game state</param>
		/// <returns>a bitboard that contains all of the white pieces</returns>
		internal static ulong GetAllWhitePiecesBoard(GameState state)
		{
			return (state.WhiteBishops | state.WhiteKing | state.WhiteKnights | state.WhitePawns | state.WhiteQueen | state.WhiteRooks);
		}

		/// <summary>
		/// Returns a bitboard that contains all of the black pieces
		/// </summary>
		/// <param name="state">The current game state</param>
		/// <returns>a bitboard that contains all of the black pieces</returns>
		internal static ulong GetAllBlackPiecesBoard(GameState state)
		{
			return (state.BlackBishops | state.BlackKing | state.BlackKnights | state.BlackPawns | state.BlackQueen | state.BlackRooks);
		}

		/// <summary>
		/// A helper method that converts the current game state to a easy to read console output
		/// </summary>
		/// <param name="state">The Current Game state</param>
		/// <returns>A formatted chess board</returns>
		internal static string ConvertBitBoardsToConsoleOutput(GameState state)
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
		internal static string ConvertBoardArrayToConsoleOutput(GameState state)
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
        internal static string ConvertSingleBitBoardsToConsoleOutput(ulong bitboard)
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
	}
}
