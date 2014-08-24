namespace DotNetEngine.Engine
{
    	/// <summary>
	/// Helper functions used when dealing with a representation of a single move.
	/// A single move is contained inside a single uint.
	/// 
	/// [MoveFrom] [MoveTo] [MovingPiece] [CapturedPiece] [PromotionPiece]
	/// 000000 000000 0000 0000 0000
	/// 
	/// MoveFrom - 6 bits 0-63 
	/// MoveTo - 6 bits 0-63
	/// MovingPiece - 4 bits 0-7 white 9=15 black
	/// CapturePiece - 4 bits 0-7 white 9=15 black
	/// PromotionPiece - 4 bits 0-7 white 9=15 black
	/// </summary>
	internal static class MoveUtility
	{
		internal const uint Empty = 0;        // 0000
		internal const uint WhitePawn = 1;      // 0001
		internal const uint WhiteKing = 2;      // 0010
		internal const uint WhiteKnight = 3;     // 0011
		internal const uint WhiteBishop = 5;    // 0101
		internal const uint WhiteRook = 6;      // 0110
		internal const uint WhiteQueen = 7;     // 0111
		internal const uint BlackPawn = 9;      // 1001
		internal const uint BlackKing = 10;     // 1010
		internal const uint BlackKnight = 11;    // 1011
		internal const uint BlackBishop = 13;    // 1101
		internal const uint BlackRook = 14;     // 1110
		internal const uint BlackQueen = 15;     // 1111

        /// <summary>
        /// The representation of each bit in the ulong. 
        /// Bitboards use Little-Endian File-Rank Mapping a1 is position 0 and h8 is 63 
        /// </summary>
        internal static ulong[] BitStates = new[]
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
        internal static int[] Ranks = new[]
        {
            1, 1, 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2, 2, 2, 3, 3, 3, 3, 3, 3, 3, 3, 4, 4, 4, 4, 
            4, 4, 4, 4, 5, 5, 5, 5, 5, 5, 5, 5, 6, 6, 6, 6, 6, 6, 6, 6, 7, 7, 7, 7, 7, 7, 7, 7,
            8, 8, 8, 8, 8, 8, 8, 8
        };

        /// <summary>
        /// The file of each position on the board
        /// </summary>
        internal static int[] Files = new[]
        {
            1, 2, 3, 4, 5, 6, 7, 8, 1, 2, 3, 4, 5, 6, 7, 8, 1, 2, 3, 4, 5, 6, 7, 8, 1, 2, 3, 4, 5, 6, 7, 8, 1, 2, 3, 
            4, 5, 6, 7, 8, 1, 2, 3, 4, 5, 6, 7, 8, 1, 2, 3, 4, 5, 6, 7, 8, 1, 2, 3, 4, 5, 6, 7, 8
        };

        /// <summary>
        /// A Translation of rank and file to its index on the board
        /// </summary>
        internal static int[][] BoardIndex = new int[][]
        {
            new [] { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            new [] { 0, 0, 1, 2, 3, 4, 5, 6, 7, },
            new [] { 0, 8, 9, 10, 11, 12, 13, 14, 15 },
            new [] { 0, 16, 17, 18, 19, 20, 21, 22, 23 }, 
            new [] { 0, 24, 25, 26, 27, 28, 29, 30, 31 }, 
            new [] { 0, 32, 33, 34, 35, 36, 37, 38, 39 }, 
            new [] { 0, 40, 41, 42, 43, 44, 45, 46, 47 }, 
            new [] { 0, 48, 49, 50, 51, 52, 53, 54, 55 }, 
            new [] { 0, 56, 57, 58, 59, 60, 61, 62, 63 }
        };

        /// <summary>
        /// The give rank of a row if it was shifted up by a single rank
        /// </summary>
        internal static int[] ShiftedRank = new int[64] 
        {
            1, 1, 1, 1, 1, 1, 1, 1,
            9, 9, 9, 9, 9, 9, 9, 9,
            17, 17, 17, 17, 17, 17, 17, 17,
            25, 25, 25, 25, 25, 25, 25, 25,
            33, 33, 33, 33, 33, 33, 33, 33,
            41, 41, 41, 41, 41, 41, 41, 41,
            49, 49, 49, 49, 49, 49, 49, 49,
            57, 57, 57, 57, 57, 57, 57, 57
        };

        internal static ulong[] FileMagicMultiplication = new ulong[64] 
        {
            9241421688590303744, 4620710844295151872, 2310355422147575936, 1155177711073787968, 577588855536893984, 
            288794427768446992, 144397213884223496, 72198606942111748, 9241421688590303744, 4620710844295151872, 2310355422147575936, 
            1155177711073787968, 577588855536893984, 288794427768446992, 144397213884223496, 72198606942111748, 9241421688590303744, 
            4620710844295151872, 2310355422147575936, 1155177711073787968, 577588855536893984, 288794427768446992, 144397213884223496, 
            72198606942111748, 9241421688590303744, 4620710844295151872, 2310355422147575936, 1155177711073787968, 577588855536893984, 
            288794427768446992, 144397213884223496, 72198606942111748, 9241421688590303744, 4620710844295151872, 2310355422147575936, 
            1155177711073787968, 577588855536893984, 288794427768446992, 144397213884223496, 72198606942111748, 9241421688590303744, 
            4620710844295151872, 2310355422147575936, 1155177711073787968, 577588855536893984, 288794427768446992, 144397213884223496, 
            72198606942111748, 9241421688590303744, 4620710844295151872, 2310355422147575936, 1155177711073787968, 577588855536893984, 
            288794427768446992, 144397213884223496, 72198606942111748, 9241421688590303744, 4620710844295151872, 2310355422147575936, 
            1155177711073787968, 577588855536893984, 288794427768446992, 144397213884223496, 72198606942111748

        };
       
        internal static ulong[] DiagonalA1H8MagicMultiplcation = new ulong[64] 
        {
           72340172838076672, 36170086419038336, 18085043209519168, 9042521604759584, 4521260802379792, 2260630401189896, 0, 0, 72340172838076672,
           72340172838076672, 36170086419038336, 18085043209519168, 9042521604759584, 4521260802379792, 2260630401189896, 0, 72340172838076672, 
           72340172838076672, 72340172838076672, 36170086419038336, 18085043209519168, 9042521604759584, 4521260802379792, 2260630401189896, 
           72340172838076672, 72340172838076672, 72340172838076672, 72340172838076672, 36170086419038336, 18085043209519168, 9042521604759584, 
           4521260802379792, 72340172838076672, 72340172838076672, 72340172838076672, 72340172838076672, 72340172838076672, 36170086419038336, 
           18085043209519168, 9042521604759584, 72340172838076672, 72340172838076672, 72340172838076672, 72340172838076672, 72340172838076672, 
           72340172838076672, 36170086419038336, 18085043209519168, 0, 72340172838076672, 72340172838076672, 72340172838076672, 72340172838076672, 
           72340172838076672, 72340172838076672, 36170086419038336, 0, 0, 72340172838076672, 72340172838076672, 72340172838076672, 72340172838076672, 
           72340172838076672, 72340172838076672
        };

        internal static ulong[] DiagonalA8H1MagicMultiplcation = new ulong[64] 
        {
           0, 0, 72340172838076672, 72340172838076672, 72340172838076672, 72340172838076672, 72340172838076672, 72340172838076672, 0, 72340172838076672, 
           72340172838076672, 72340172838076672, 72340172838076672, 72340172838076672, 72340172838076672, 36170086419038336, 72340172838076672, 72340172838076672, 
           72340172838076672, 72340172838076672, 72340172838076672, 72340172838076672, 36170086419038336, 18085043209519168, 72340172838076672, 72340172838076672, 
           72340172838076672, 72340172838076672, 72340172838076672, 36170086419038336, 18085043209519168, 9042521604759584, 72340172838076672, 72340172838076672, 
           72340172838076672, 72340172838076672, 36170086419038336, 18085043209519168, 9042521604759584, 4521260802379792, 72340172838076672, 72340172838076672, 
           72340172838076672, 36170086419038336, 18085043209519168, 9042521604759584, 4521260802379792, 2260630401189896, 72340172838076672, 72340172838076672,
           36170086419038336, 18085043209519168, 9042521604759584, 4521260802379792, 2260630401189896, 0, 72340172838076672, 36170086419038336, 
           18085043209519168, 9042521604759584, 4521260802379792, 2260630401189896, 0, 0
        };

		/// <summary>
		/// Used to create a new move
		/// </summary>
		/// <param name="fromSquare">The Square the piece is moving from</param>
		/// <param name="toSquare">The square the piece is moving to</param>
		/// <param name="movingPiece">The peice that is moving</param>
		/// <param name="capturedPiece">The piece that was capture. If no piece was captured, this will be 0</param>
		/// <param name="promotionPiece">The piece that was promoted. If no piece was promoted this will be 0. 
		/// If an enpassant capture occurs this field will be set to a white or black pawn.
		/// If a castling occurs, this field will be set to a white or black king</param>
		/// <returns></returns>
		internal static uint CreateMove(uint fromSquare, uint toSquare,  uint movingPiece, uint capturedPiece, uint promotionPiece)
		{
			var move = 0u;

			move = SetFromMove(move, fromSquare);
			move = SetToMove(move, toSquare);
			move = SetMovingPiece(move, movingPiece);

			if (capturedPiece != 0)
				move = SetCapturedPiece(move, capturedPiece);

			if (promotionPiece != 0)
				move = SetPromotionPiece(move, promotionPiece);

			return move;

		}

		/// <summary>
		/// Determines if a move was made by white or black
		/// </summary>
		/// <param name="move">The move being checked</param>
		/// <returns>A bool value indicating if a move was made by white or black</returns>
		internal static bool IsWhiteMove(uint move)
		{
			return (~move & 0x00008000) == 0x00008000;
		}

		/// <summary>
		/// Determines if a move is an enpassant
		/// </summary>
		/// <param name="move">The move being checked</param>
		/// <returns>A bool value indicating if a move is an enpassant</returns>
		internal static bool IsEnPassant(uint move)
		{
			return (move & 0x00700000) == 0x00100000;
		}

		/// <summary>
		/// Determines if during a move, a pawn moved
		/// </summary>
		/// <param name="move">The move being checked</param>
		/// <returns>A bool value indicating if a pawn moved</returns>
		internal static bool IsPawnMoved(uint move)
		{
			return (move & 0x00007000) == 0x00001000;
		}

		/// <summary>
		/// Determines if during a move, a rook moved
		/// </summary>
		/// <param name="move">The move being checked</param>
		/// <returns>A bool value indicating if a rook moved</returns>
		internal static bool IsRookMoved(uint move)
		{
			return (move & 0x00007000) == 0x00006000;
		}

		/// <summary>
		/// Determines if during a move, a king moved
		/// </summary>
		/// <param name="move">The move being checked</param>
		/// <returns>A bool value indicating if a king moved</returns>
		internal static bool IsKingMoved(uint move)
		{
			return (move & 0x00007000) == 0x00002000;
		}

		/// <summary>
		/// Determines if during a move, a pawn moved two squares
		/// </summary>
		/// <param name="move">The move being checked</param>
		/// <returns>A bool value indicating if a pawn moved two squares</returns>
		internal static bool IsPawnDoubleMoved(uint move)
		{
			 return ((( move & 0x00007000) == 0x00001000) && (((( move & 0x00000038) == 0x00000008) && ((( move & 0x00000e00) == 0x00000600))) || 
													  ((( move & 0x00000038) == 0x00000030) && ((( move & 0x00000e00) == 0x00000800)))));
		}

		/// <summary>
		/// Determines if during a move, if any piece was captured
		/// </summary>
		/// <param name="move">The move being checked</param>
		/// <returns>A bool value indicating if any piece was captured</returns>
		internal static bool IsPieceCaptured(uint move)
		{
			return (move & 0x000f0000) != 0x00000000;
		}

		/// <summary>
		/// Determines if during a move, if a king was captured
		/// </summary>
		/// <param name="move">The move being checked</param>
		/// <returns>A bool value indicating if a king was captured</returns>
		internal static bool IsKingCaptured(uint move)
		{
			return (move & 0x00070000) == 0x00020000;
		}
		
		/// <summary>
		/// Determines if during a move, if a rook was captured
		/// </summary>
		/// <param name="move">The move being checked</param>
		/// <returns>A bool value indicating if a rook was captured</returns>
		internal static bool IsRookCaptured(uint move)
		{
			return (move & 0x00070000) == 0x00060000;
		}
		
		/// <summary>
		/// Determines if during a move, either castle occurred
		/// </summary>
		/// <param name="move">The move being checked</param>
		/// <returns>A bool value indicating if either castle occurred</returns>
		internal static bool IsCastle(uint move)
		{
			return (move & 0x00700000) == 0x00200000;
		}

		/// <summary>
		/// Determines if during a move, a O-O castle occurred
		/// </summary>
		/// <param name="move">The move being checked</param>
		/// <returns>A bool value indicating if a O-O castle occurred</returns>
// ReSharper disable InconsistentNaming
		internal static bool IsCastleOO(uint move)
// ReSharper restore InconsistentNaming
		{
			return (move & 0x007001c0) == 0x00200180;
		}

		/// <summary>
		/// Determines if during a move, a O-O-O castle occurred
		/// </summary>
		/// <param name="move">The move being checked</param>
		/// <returns>A bool value indicating if a O-O-O castle occurred</returns>
// ReSharper disable InconsistentNaming
		internal static bool IsCastleOOO(uint move)
// ReSharper restore InconsistentNaming
		{
			return (move & 0x007001c0) == 0x00200080;
		}
		
		/// <summary>
		/// Determines if during a move, a promotion of a piece occurred
		/// </summary>
		/// <param name="move">The move being checked</param>
		/// <returns>A bool value indicating if a promotion occurred</returns>
		internal static bool IsPromotion(uint move)
		{
			return (move & 0x00700000) > 0x00200000;
		}

		private static uint SetPromotionPiece(uint move, uint promotionPiece)
		{
			move &= 0xff0fffff;
			move |= (promotionPiece & 0x0000000f) << 20;
			return move;
		}

		private static uint SetCapturedPiece(uint move, uint capturedPiece)
		{
			move &= 0xfff0ffff;
			move |= (capturedPiece & 0x0000000f) << 16;
			return move;
		}

		private static uint SetFromMove(uint move, uint moveFrom)
		{
			move &= 0xffffffc0;
			move |= (moveFrom & 0x0000003f);
			return move;
		}

		private static uint SetToMove(uint move, uint moveTo)
		{
			move &= 0xfffff03f;
			move |= (moveTo & 0x0000003f) << 6;
			return move;
		}

		private static uint SetMovingPiece(uint move, uint movingpiece)
		{
			move &= 0xffff0fff;
			move |= (movingpiece & 0x0000000f) << 12;
			return move;
		}
	}
}