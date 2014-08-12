using System.Collections.Generic;

namespace DotNetEngine.Engine
{
	/// <summary>
	/// An Object that contains the game state.
	/// </summary>
	public class GameState
	{
		private readonly ulong[] _allPieceBoards;

		//white bitboards
		public ulong WhitePawns { get; set; }
		public ulong WhiteKnights { get; set; }
		public ulong WhiteBishops { get; set; }
		public ulong WhiteRooks { get; set; }
		public ulong WhiteQueen { get; set; }
		public ulong WhiteKing { get; set; }

		//black bitboards
		public ulong BlackPawns { get; set; }
		public ulong BlackKnights { get; set; }
		public ulong BlackBishops { get; set; }
		public ulong BlackRooks { get; set; }
		public ulong BlackQueen { get; set; }
		public ulong BlackKing { get; set; }

		/// <summary>
		/// A list of all of the moves played so far.
		/// </summary>
		public List<uint> Moves { get; set; }

		public bool WhiteToMove { get; set; }

		/// <summary>
		/// An array of all the bitboards.
		/// </summary>
		public ulong[] AllPieceBoards
		{
			get { return _allPieceBoards; }
		}

		/// <summary>
		/// 0 - Cannot Castle 
		/// 1 - Can Castle 00
		/// 2 - Can Castle 000 
		/// 3 - Can Castle Both 00 and 000
		/// </summary>
		public int CurrentWhiteCastleStatus { get; set; }
		public int CurrentBlackCastleStatus { get; set; }
		
		public int EnpassantTargetSquare { get; set; }

		/// <summary>
		/// The number of half moves currently made
		/// </summary>
		public int FiftyMoveRuleCount { get; set; }

		/// <summary>
		/// The total number of full moves currently made
		/// </summary>
		public int TotalMoveCount { get; set; }

		/// <summary>
		/// An single dimension array of the board. Each Piece is represented by a unique bit
		/// This array is little endian encoded. A1 = 0 and H8 = 63
		/// </summary>
		public uint[] BoardArray { get; private set; }

		public GameState()
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
