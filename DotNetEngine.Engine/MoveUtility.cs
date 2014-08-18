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
	public static class MoveUtility
	{
		public const uint Empty = 0;        // 0000
		public const uint WhitePawn = 1;      // 0001
		public const uint WhiteKing = 2;      // 0010
		public const uint WhiteKnight = 3;     // 0011
		public const uint WhiteBishop = 5;    // 0101
		public const uint WhiteRook = 6;      // 0110
		public const uint WhiteQueen = 7;     // 0111
		public const uint BlackPawn = 9;      // 1001
		public const uint BlackKing = 10;     // 1010
		public const uint BlackKnight = 11;    // 1011
		public const uint BlackBishop = 13;    // 1101
		public const uint BlackRook = 14;     // 1110
		public const uint BlackQueen = 15;     // 1111

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
		public static uint CreateMove(uint fromSquare, uint toSquare,  uint movingPiece, uint capturedPiece, uint promotionPiece)
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
		public static bool IsWhiteMove(uint move)
		{
			return (~move & 0x00008000) == 0x00008000;
		}

		/// <summary>
		/// Determines if a move is an enpassant
		/// </summary>
		/// <param name="move">The move being checked</param>
		/// <returns>A bool value indicating if a move is an enpassant</returns>
		public static bool IsEnPassant(uint move)
		{
			return (move & 0x00700000) == 0x00100000;
		}

		/// <summary>
		/// Determines if during a move, a pawn moved
		/// </summary>
		/// <param name="move">The move being checked</param>
		/// <returns>A bool value indicating if a pawn moved</returns>
		public static bool IsPawnMoved(uint move)
		{
			return (move & 0x00007000) == 0x00001000;
		}

		/// <summary>
		/// Determines if during a move, a rook moved
		/// </summary>
		/// <param name="move">The move being checked</param>
		/// <returns>A bool value indicating if a rook moved</returns>
		public static bool IsRookMoved(uint move)
		{
			return (move & 0x00007000) == 0x00006000;
		}

		/// <summary>
		/// Determines if during a move, a king moved
		/// </summary>
		/// <param name="move">The move being checked</param>
		/// <returns>A bool value indicating if a king moved</returns>
		public static bool IsKingMoved(uint move)
		{
			return (move & 0x00007000) == 0x00002000;
		}

		/// <summary>
		/// Determines if during a move, a pawn moved two squares
		/// </summary>
		/// <param name="move">The move being checked</param>
		/// <returns>A bool value indicating if a pawn moved two squares</returns>
		public static bool IsPawnDoubleMoved(uint move)
		{
			 return ((( move & 0x00007000) == 0x00001000) && (((( move & 0x00000038) == 0x00000008) && ((( move & 0x00000e00) == 0x00000600))) || 
													  ((( move & 0x00000038) == 0x00000030) && ((( move & 0x00000e00) == 0x00000800)))));
		}

		/// <summary>
		/// Determines if during a move, if any piece was captured
		/// </summary>
		/// <param name="move">The move being checked</param>
		/// <returns>A bool value indicating if any piece was captured</returns>
		public static bool IsPieceCaptured(uint move)
		{
			return (move & 0x000f0000) != 0x00000000;
		}

		/// <summary>
		/// Determines if during a move, if a king was captured
		/// </summary>
		/// <param name="move">The move being checked</param>
		/// <returns>A bool value indicating if a king was captured</returns>
		public static bool IsKingCaptured(uint move)
		{
			return (move & 0x00070000) == 0x00020000;
		}
		
		/// <summary>
		/// Determines if during a move, if a rook was captured
		/// </summary>
		/// <param name="move">The move being checked</param>
		/// <returns>A bool value indicating if a rook was captured</returns>
		public static bool IsRookCaptured(uint move)
		{
			return (move & 0x00070000) == 0x00060000;
		}
		
		/// <summary>
		/// Determines if during a move, either castle occurred
		/// </summary>
		/// <param name="move">The move being checked</param>
		/// <returns>A bool value indicating if either castle occurred</returns>
		public static bool IsCastle(uint move)
		{
			return (move & 0x00700000) == 0x00200000;
		}

		/// <summary>
		/// Determines if during a move, a O-O castle occurred
		/// </summary>
		/// <param name="move">The move being checked</param>
		/// <returns>A bool value indicating if a O-O castle occurred</returns>
// ReSharper disable InconsistentNaming
		public static bool IsCastleOO(uint move)
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
		public static bool IsCastleOOO(uint move)
// ReSharper restore InconsistentNaming
		{
			return (move & 0x007001c0) == 0x00200080;
		}
		
		/// <summary>
		/// Determines if during a move, a promotion of a piece occurred
		/// </summary>
		/// <param name="move">The move being checked</param>
		/// <returns>A bool value indicating if a promotion occurred</returns>
		public static bool IsPromotion(uint move)
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