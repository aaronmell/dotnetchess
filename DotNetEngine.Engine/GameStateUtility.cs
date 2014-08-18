using System.Text;

namespace DotNetEngine.Engine
{
	/// <summary>
	/// Helper methods that act on the GameState object and its properties
	/// </summary>
	public static class GameStateUtility
	{
		/// <summary>
		/// The representation of each bit in the ulong. 
		/// Bitboards use Little-Endian File-Rank Mapping a1 is position 0 and h8 is 63 
		/// </summary>
		public static ulong[] BitStates = new []
		{
			1UL, 2UL, 4UL, 8UL, 16UL, 32UL, 64UL, 128UL, 256UL, 512UL, 1024UL, 2048UL, 4096UL, 8192UL, 16384UL, 32768UL, 
			65536UL, 131072UL, 262144UL, 524288UL, 1048576UL, 2097152UL, 4194304UL, 8388608UL, 16777216UL, 33554432UL, 
			67108864UL, 134217728UL, 268435456UL, 536870912UL, 1073741824UL, 2147483648UL, 4294967296UL, 8589934592UL, 
			17179869184UL, 34359738368UL, 68719476736UL, 137438953472UL, 274877906944UL, 549755813888UL,
			1099511627776UL, 2199023255552UL, 4398046511104UL, 8796093022208UL, 17592186044416UL, 35184372088832UL,
			70368744177664UL, 140737488355328UL, 281474976710656UL, 562949953421312UL, 1125899906842624UL,
			2251799813685248UL, 4503599627370496UL, 9007199254740992UL, 18014398509481984UL, 36028797018963968UL,
			72057594037927936UL, 144115188075855872UL, 288230376151711744UL, 576460752303423488UL,
			1152921504606846976UL, 2305843009213693952UL, 4611686018427387904UL, 9223372036854775808UL
		};

        /// <summary>
        /// The rank of each position on the board
        /// </summary>
        public static int[] Ranks = new[]
        {
            1, 1, 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2, 2, 2, 3, 3, 3, 3, 3, 3, 3, 3, 4, 4, 4, 4, 
            4, 4, 4, 4, 5, 5, 5, 5, 5, 5, 5, 5, 6, 6, 6, 6, 6, 6, 6, 6, 7, 7, 7, 7, 7, 7, 7, 7,
            8, 8, 8, 8, 8, 8, 8, 8
        };

        /// <summary>
        /// The file of each position on the board
        /// </summary>
        public static int[] Files = new[]
        {
            1, 2, 3, 4, 5, 6, 7, 8, 1, 2, 3, 4, 5, 6, 7, 8, 1, 2, 3, 4, 5, 6, 7, 8, 1, 2, 3, 4, 5, 6, 7, 8, 1, 2, 3, 
            4, 5, 6, 7, 8, 1, 2, 3, 4, 5, 6, 7, 8, 1, 2, 3, 4, 5, 6, 7, 8, 1, 2, 3, 4, 5, 6, 7, 8
        };

        /// <summary>
        /// A Translation of rank and file to its index on the board
        /// </summary>
        public static int[][] BoardIndex = new int[][]
        {
            new [] { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            new [] { 0, 0, 1, 2, 3, 4, 5, 6, 7, },
            new [] { 0, 8, 9, 10, 11, 12, 13, 14, 15 },
            new [] { 0, 16, 17, 18, 19, 20, 21, 22, 23 }, 
            new [] { 0, 24, 25, 26, 27, 28, 29, 30, 31 }, 
            new [] { 0, 32, 33, 34, 35, 36, 37, 38, 39 }, 
            new [] { 0, 40, 41, 42, 43, 44, 45, 46, 47 }, 
            new [] { 0, 48, 49, 50, 51, 52, 53, 54, 55 }, 
            new [] { 0, 56, 57, 58, 59, 60, 61, 62, 63 },
            

        };

		/// <summary>
		/// Returns a bitboard that contains all of the white pieces
		/// </summary>
		/// <param name="state">The current game state</param>
		/// <returns>a bitboard that contains all of the white pieces</returns>
		public static ulong GetAllWhitePiecesBoard(GameState state)
		{
			return (state.WhiteBishops | state.WhiteKing | state.WhiteKnights | state.WhitePawns | state.WhiteQueen | state.WhiteRooks);
		}

		/// <summary>
		/// Returns a bitboard that contains all of the black pieces
		/// </summary>
		/// <param name="state">The current game state</param>
		/// <returns>a bitboard that contains all of the black pieces</returns>
		public static ulong GetAllBlackPiecesBoard(GameState state)
		{
			return (state.BlackBishops | state.BlackKing | state.BlackKnights | state.BlackPawns | state.BlackQueen | state.BlackRooks);
		}

		/// <summary>
		/// A helper method that converts the current game state to a easy to read console output
		/// </summary>
		/// <param name="state">The Current Game state</param>
		/// <returns>A formatted chess board</returns>
		public static string ConvertBitBoardToConsoleOutput(GameState state)
		{
			var output = new StringBuilder(" - - - - - - - - ");
			output.AppendLine();

			for (var i = 7; i >= 0; i--)
			{
				for (var j = 0; j < 8; j++)
				{
					output.Append("|");

					var position = BitStates[(i * 8) + j];

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
		public static string ConvertBoardArrayToConsoleOutput(GameState state)
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
	}
}
