using System.Collections.Generic;

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
		internal ulong WhiteQueen { get; set; }
		internal ulong WhiteKing { get; set; }

		//black bitboards
		internal ulong BlackPawns { get; set; }
		internal ulong BlackKnights { get; set; }
		internal ulong BlackBishops { get; set; }
		internal ulong BlackRooks { get; set; }
		internal ulong BlackQueen { get; set; }
		internal ulong BlackKing { get; set; }

		/// <summary>
		/// A list of all of the moves played so far.
		/// </summary>
		internal List<uint> Moves { get; set; }

		internal bool WhiteToMove { get; set; }

		/// <summary>
		/// An array of all the bitboards.
		/// </summary>
		internal ulong[] AllPieceBoards
		{
			get { return _allPieceBoards; }
		}

		/// <summary>
		/// 0 - Cannot Castle 
		/// 1 - Can Castle 00
		/// 2 - Can Castle 000 
		/// 3 - Can Castle Both 00 and 000
		/// </summary>
		internal int CurrentWhiteCastleStatus { get; set; }
		internal int CurrentBlackCastleStatus { get; set; }
		
		internal int EnpassantTargetSquare { get; set; }

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
				BlackQueen,
				BlackRooks,
				WhiteBishops,
				WhiteKing,
				WhiteKnights,
				WhitePawns,
				WhiteQueen,
				WhiteRooks
			};

			BoardArray = new uint[64];

			Moves = new List<uint>();
		}
	}
}
