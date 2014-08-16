using DotNetEngine.Engine;
using NUnit.Framework;

namespace DotNetEngine.Test
{
	public class MoveUtilityTests
	{
		[TestCase(0u)]
		[TestCase(1u)]
		[TestCase(63u)]
		public void CreateMove_Sets_To_Square_Correctly(uint moveToSquare)
		{
			var move = MoveUtility.CreateMove(0, moveToSquare, MoveUtility.WhitePawn, 0, 0);

			var squareMovedTo = (move >>  6) & 0x0000003f;
			Assert.That(squareMovedTo == moveToSquare);
		}

		[TestCase(0u)]
		[TestCase(1u)]
		[TestCase(63u)]
		public void CreateMove_Sets_From_Square_Correctly(uint moveFromSquare)
		{
			var move = MoveUtility.CreateMove(moveFromSquare, 18, MoveUtility.WhitePawn, 0, 0);

			var squareMovedFrom = (move) & 0x0000003f;
			Assert.That(squareMovedFrom == moveFromSquare);
		}

		[TestCase(MoveUtility.WhitePawn)]
		[TestCase(MoveUtility.WhiteQueen)]
		[TestCase(MoveUtility.BlackPawn)]
		[TestCase(MoveUtility.BlackQueen)]
		public void CreateMove_Sets_Moved_Piece_Correctly(uint movedPiece)
		{
			var move = MoveUtility.CreateMove(0, 1, movedPiece, 0, 0);

			var movedPieceValue = (move >> 12) & 0x0000000f; 
			Assert.That(movedPiece == movedPieceValue);
		}

		[TestCase(MoveUtility.WhitePawn)]
		[TestCase(MoveUtility.WhiteQueen)]
		[TestCase(MoveUtility.BlackPawn)]
		[TestCase(MoveUtility.BlackQueen)]
		public void CreateMove_Sets_Captured_Piece_Correctly(uint capturePiece)
		{
			var move = MoveUtility.CreateMove(0, 1, 1, capturePiece, 0);

			var capturedPieceValue = (move >> 16) & 0x0000000f; 
			Assert.That(capturePiece == capturedPieceValue);
		}

		[TestCase(MoveUtility.WhitePawn)]
		[TestCase(MoveUtility.WhiteQueen)]
		[TestCase(MoveUtility.BlackPawn)]
		[TestCase(MoveUtility.BlackQueen)]
		public void CreateMove_Sets_Promoted_Piece_Correctly(uint promotedPiece)
		{
			var move = MoveUtility.CreateMove(0, 1, 1, 0, promotedPiece);

			var capturedPieceValue = (move >> 20) & 0x0000000f; 
			Assert.That(promotedPiece == capturedPieceValue);
		}

		[TestCase(MoveUtility.WhitePawn, true)]
		[TestCase(MoveUtility.WhiteQueen, true)]
		[TestCase(MoveUtility.BlackPawn, false)]
		[TestCase(MoveUtility.BlackQueen, false)]
		public void IsWhiteMove_Returns_Correct_Value(uint movedPiece, bool expectedResult)
		{
			var move = MoveUtility.CreateMove(0, 1, movedPiece, 0, 0);

			Assert.That(MoveUtility.IsWhiteMove(move), Is.EqualTo(expectedResult));
		}

		[TestCase(MoveUtility.WhitePawn, true)]
		[TestCase(MoveUtility.BlackPawn, true)]
		[TestCase(MoveUtility.BlackBishop, false)]
		[TestCase(MoveUtility.WhiteBishop, false)]
		public void IsPawnMoved_Returns_Correct_Value(uint movedPiece, bool expectedResult)
		{
			var move = MoveUtility.CreateMove(0, 1, movedPiece, 0, 0);

			Assert.That(MoveUtility.IsPawnMoved(move), Is.EqualTo(expectedResult));
		}

		[TestCase(MoveUtility.WhiteKing, true)]
		[TestCase(MoveUtility.BlackKing, true)]
		[TestCase(MoveUtility.BlackBishop, false)]
		[TestCase(MoveUtility.WhiteBishop, false)]
		public void IsKingMoved_Returns_Correct_Value(uint movedPiece, bool expectedResult)
		{
			var move = MoveUtility.CreateMove(0, 1, movedPiece, 0, 0);

			Assert.That(MoveUtility.IsKingMoved(move), Is.EqualTo(expectedResult));
		}

		[TestCase(MoveUtility.WhiteRook, true)]
		[TestCase(MoveUtility.BlackRook, true)]
		[TestCase(MoveUtility.BlackBishop, false)]
		[TestCase(MoveUtility.WhiteBishop, false)]
		public void IsRookMoved_Returns_Correct_Value(uint movedPiece, bool expectedResult)
		{
			var move = MoveUtility.CreateMove(0, 1, movedPiece, 0, 0);

			Assert.That(MoveUtility.IsRookMoved(move), Is.EqualTo(expectedResult));
		}

		[TestCase(MoveUtility.WhitePawn, 8u,24u, true)]
		[TestCase(MoveUtility.BlackPawn, 8u, 24u, true)]
		[TestCase(MoveUtility.BlackBishop, 8u, 24u, false)]
		[TestCase(MoveUtility.WhiteBishop, 8u, 24u, false)]
		[TestCase(MoveUtility.WhitePawn, 8u, 16u, false)]
		public void IsPawnDoubleMoved_Returns_Correct_Value(uint movedPiece, uint moveToSquare, uint moveFromSquare ,bool expectedResult)
		{
			var move = MoveUtility.CreateMove(moveToSquare, moveFromSquare, movedPiece, 0, 0);

			Assert.That(MoveUtility.IsPawnDoubleMoved(move), Is.EqualTo(expectedResult));
		}

		[TestCase(MoveUtility.WhitePawn, true)]
		[TestCase(MoveUtility.BlackPawn, true)]
		[TestCase(MoveUtility.BlackQueen, false)]
		public void IsEnPassant_Returns_Correct_Value(uint promotionPiece, bool expectedResult)
		{
			var move = MoveUtility.CreateMove(0, 1, MoveUtility.WhitePawn, 0, promotionPiece);

			Assert.That(MoveUtility.IsEnPassant(move), Is.EqualTo(expectedResult));
		}


		[TestCase(MoveUtility.WhitePawn, true)]
		[TestCase(MoveUtility.BlackPawn, true)]
		[TestCase(0u, false)]
		public void IsPieceCapture_Returns_Correct_Value(uint capturedPiece, bool expectedResult)
		{
			var move = MoveUtility.CreateMove(0, 1, MoveUtility.WhitePawn, capturedPiece, 0);

			Assert.That(MoveUtility.IsPieceCaptured(move), Is.EqualTo(expectedResult));
		}


		[TestCase(MoveUtility.WhiteKing, true)]
		[TestCase(MoveUtility.BlackKing, true)]
		[TestCase(MoveUtility.WhiteRook, false)]
		public void IsKingCaptured_Returns_Correct_Value(uint capturedPiece, bool expectedResult)
		{
			var move = MoveUtility.CreateMove(0, 1, MoveUtility.WhitePawn, capturedPiece, 0);

			Assert.That(MoveUtility.IsKingCaptured(move), Is.EqualTo(expectedResult));
		}

		[TestCase(MoveUtility.WhiteRook, true)]
		[TestCase(MoveUtility.BlackRook, true)]
		[TestCase(MoveUtility.BlackBishop, false)]
		public void IsRookCaptured_Returns_Correct_Value(uint capturedPiece, bool expectedResult)
		{
			var move = MoveUtility.CreateMove(0, 1, MoveUtility.WhitePawn, capturedPiece, 0);

			Assert.That(MoveUtility.IsRookCaptured(move), Is.EqualTo(expectedResult));
		}

		[TestCase(MoveUtility.BlackKing, true)]
		[TestCase(MoveUtility.WhiteKing, true)]
		[TestCase(MoveUtility.BlackRook, false)]
		public void IsCastle_Returns_Correct_Value(uint promotionPiece, bool expectedResult)
		{
			var move = MoveUtility.CreateMove(0, 1, MoveUtility.WhitePawn, 0, promotionPiece);

			Assert.That(MoveUtility.IsCastle(move), Is.EqualTo(expectedResult));
		}

		[TestCase(MoveUtility.BlackKing, 4u, 6u, true)]
		[TestCase(MoveUtility.WhiteKing, 4u, 6u, true)]
		[TestCase(MoveUtility.BlackRook, 4u, 6u, false)]
		[TestCase(MoveUtility.BlackKing, 4u, 5u, false)]
		public void IsCastleOO_Returns_Correct_Value(uint promotionPiece, uint fromSquare, uint toSquare, bool expectedResult)
		{
			var move = MoveUtility.CreateMove(fromSquare, toSquare, promotionPiece, 0, promotionPiece);

			Assert.That(MoveUtility.IsCastleOO(move), Is.EqualTo(expectedResult));
		}

		[TestCase(MoveUtility.BlackKing, 4u, 2u, true)]
		[TestCase(MoveUtility.WhiteKing, 4u, 2u, true)]
		[TestCase(MoveUtility.BlackRook, 4u, 2u, false)]
		[TestCase(MoveUtility.BlackKing, 4u, 3u, false)]
		public void IsCastleOOO_Returns_Correct_Value(uint promotionPiece, uint fromSquare, uint toSquare, bool expectedResult)
		{
			var move = MoveUtility.CreateMove(fromSquare, toSquare, promotionPiece, 0, promotionPiece);

			Assert.That(MoveUtility.IsCastleOOO(move), Is.EqualTo(expectedResult));
		}

		[TestCase(MoveUtility.BlackQueen, true)]
		[TestCase(MoveUtility.WhiteQueen, true)]
		[TestCase(0u, false)]
		public void IsPromotion_Returns_Correct_Value(uint promotionPiece, bool expectedResult)
		{
			var move = MoveUtility.CreateMove(0, 1, MoveUtility.WhitePawn, 0, promotionPiece);

			Assert.That(MoveUtility.IsPromotion(move), Is.EqualTo(expectedResult));
		}
	}
}
