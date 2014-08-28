using DotNetEngine.Engine;
using NUnit.Framework;
using System.Text;

namespace DotNetEngine.Test
{
	public class MoveUtilityTests
	{
        [Test]
        public void BoardIndex_Returns_Correct_Value()
        {
            var count = 0;
            for (var rank = 1; rank < 9; rank++)
            {
                for (var file = 1; file < 9; file++)
                {
                    Assert.That(MoveUtility.BoardIndex[rank][file], Is.EqualTo(count));
                    count++;
                }
            }
        }

        [Test]
        public void Ranks_Return_Correct_Value()
        {
            var count = 0;

            for (var i = 0; i < 64; i++)
            {
                if (i % 8 == 0)
                {
                    count++;
                }
                Assert.That(MoveUtility.Ranks[i], Is.EqualTo(count));
            }
        }

        [Test]
        public void Files_Return_Correct_Value()
        {
            var count = 0;

            for (var i = 0; i < 64; i++)
            {
                if (i % 8 == 0)
                {
                    count = 1;
                }
                Assert.That(MoveUtility.Files[i], Is.EqualTo(count));
                count++;
            }
        }

		[TestCase(0u)]
		[TestCase(1u)]
		[TestCase(63u)]
        public void SetToMove_Sets_To_Square_Correctly(uint moveToSquare)
		{
            var move = 0U.SetToMove(moveToSquare);    

			var squareMovedTo = (move >>  6) & 0x0000003f;
			Assert.That(squareMovedTo == moveToSquare);
		}

        [TestCase(0u)]
        [TestCase(1u)]
        [TestCase(63u)]
        public void SetFromMove_Sets_From_Square_Correctly(uint moveFromSquare)
        {
            var move = 0U.SetFromMove(moveFromSquare); 
               
            var squareMovedFrom = (move) & 0x0000003f;
            Assert.That(squareMovedFrom == moveFromSquare);
        }

        [TestCase(MoveUtility.WhitePawn)]
        [TestCase(MoveUtility.WhiteQueen)]
        [TestCase(MoveUtility.BlackPawn)]
        [TestCase(MoveUtility.BlackQueen)]
        public void SetMovingPiece_Sets_Moved_Piece_Correctly(uint movedPiece)
        {
            var move = 0U.SetMovingPiece(movedPiece);             

            var movedPieceValue = (move >> 12) & 0x0000000f;
            Assert.That(movedPiece == movedPieceValue);
        }

        [TestCase(MoveUtility.WhitePawn)]
        [TestCase(MoveUtility.WhiteQueen)]
        [TestCase(MoveUtility.BlackPawn)]
        [TestCase(MoveUtility.BlackQueen)]
        public void SetCapturedPiece_Sets_Captured_Piece_Correctly(uint capturePiece)
        {
            var move = 0U.SetCapturedPiece(capturePiece);

            var capturedPieceValue = (move >> 16) & 0x0000000f;
            Assert.That(capturePiece == capturedPieceValue);
        }

        [TestCase(MoveUtility.WhitePawn)]
        [TestCase(MoveUtility.WhiteQueen)]
        [TestCase(MoveUtility.BlackPawn)]
        [TestCase(MoveUtility.BlackQueen)]
        public void SetPromotionPiece_Sets_Promoted_Piece_Correctly(uint promotedPiece)
        {
            var move = 0U.SetPromotionPiece(promotedPiece);

            var capturedPieceValue = (move >> 20) & 0x0000000f;
            Assert.That(promotedPiece == capturedPieceValue);
        }

        [TestCase(MoveUtility.WhitePawn, true)]
        [TestCase(MoveUtility.WhiteQueen, true)]
        [TestCase(MoveUtility.BlackPawn, false)]
        [TestCase(MoveUtility.BlackQueen, false)]
        public void IsWhiteMove_Returns_Correct_Value(uint movedPiece, bool expectedResult)
        {
            var move = 0U.SetMovingPiece(movedPiece);

            Assert.That(MoveUtility.IsWhiteMove(move), Is.EqualTo(expectedResult));
        }

        [TestCase(MoveUtility.WhitePawn, true)]
        [TestCase(MoveUtility.BlackPawn, true)]
        [TestCase(MoveUtility.BlackBishop, false)]
        [TestCase(MoveUtility.WhiteBishop, false)]
        public void IsPawnMoved_Returns_Correct_Value(uint movedPiece, bool expectedResult)
        {
            var move = 0U.SetMovingPiece(movedPiece);

            Assert.That(MoveUtility.IsPawnMoved(move), Is.EqualTo(expectedResult));
        }

        [TestCase(MoveUtility.WhiteKing, true)]
        [TestCase(MoveUtility.BlackKing, true)]
        [TestCase(MoveUtility.BlackBishop, false)]
        [TestCase(MoveUtility.WhiteBishop, false)]
        public void IsKingMoved_Returns_Correct_Value(uint movedPiece, bool expectedResult)
        {
            var move = 0U.SetMovingPiece(movedPiece);

            Assert.That(MoveUtility.IsKingMoved(move), Is.EqualTo(expectedResult));
        }

        [TestCase(MoveUtility.WhiteRook, true)]
        [TestCase(MoveUtility.BlackRook, true)]
        [TestCase(MoveUtility.BlackBishop, false)]
        [TestCase(MoveUtility.WhiteBishop, false)]
        public void IsRookMoved_Returns_Correct_Value(uint movedPiece, bool expectedResult)
        {
            var move = 0U.SetMovingPiece(movedPiece);

            Assert.That(MoveUtility.IsRookMoved(move), Is.EqualTo(expectedResult));
        }

        [TestCase(MoveUtility.WhitePawn, 8u, 24u, true)]
        [TestCase(MoveUtility.BlackPawn, 8u, 24u, true)]
        [TestCase(MoveUtility.BlackBishop, 8u, 24u, false)]
        [TestCase(MoveUtility.WhiteBishop, 8u, 24u, false)]
        [TestCase(MoveUtility.WhitePawn, 8u, 16u, false)]
        public void IsPawnDoubleMoved_Returns_Correct_Value(uint movedPiece, uint fromSquare, uint toSquare, bool expectedResult)
        {
            var move = 0U.SetFromMove(fromSquare);
            move = move.SetToMove(toSquare);
            move = move.SetMovingPiece(movedPiece);

            Assert.That(MoveUtility.IsPawnDoubleMoved(move), Is.EqualTo(expectedResult));
        }

        [TestCase(MoveUtility.WhitePawn, true)]
        [TestCase(MoveUtility.BlackPawn, true)]
        [TestCase(MoveUtility.BlackQueen, false)]
        public void IsEnPassant_Returns_Correct_Value(uint promotionPiece, bool expectedResult)
        {
            var move = 0U.SetPromotionPiece(promotionPiece);
            
            Assert.That(MoveUtility.IsEnPassant(move), Is.EqualTo(expectedResult));
        }


        [TestCase(MoveUtility.WhitePawn, true)]
        [TestCase(MoveUtility.BlackPawn, true)]
        [TestCase(0u, false)]
        public void IsPieceCapture_Returns_Correct_Value(uint capturedPiece, bool expectedResult)
        {
            var move = 0U.SetCapturedPiece(capturedPiece);

            Assert.That(MoveUtility.IsPieceCaptured(move), Is.EqualTo(expectedResult));
        }


        [TestCase(MoveUtility.WhiteKing, true)]
        [TestCase(MoveUtility.BlackKing, true)]
        [TestCase(MoveUtility.WhiteRook, false)]
        public void IsKingCaptured_Returns_Correct_Value(uint capturedPiece, bool expectedResult)
        {
            var move = 0U.SetCapturedPiece(capturedPiece);

            Assert.That(MoveUtility.IsKingCaptured(move), Is.EqualTo(expectedResult));
        }

        [TestCase(MoveUtility.WhiteRook, true)]
        [TestCase(MoveUtility.BlackRook, true)]
        [TestCase(MoveUtility.BlackBishop, false)]
        public void IsRookCaptured_Returns_Correct_Value(uint capturedPiece, bool expectedResult)
        {
            var move = 0U.SetCapturedPiece(capturedPiece);

            Assert.That(MoveUtility.IsRookCaptured(move), Is.EqualTo(expectedResult));
        }

        [TestCase(MoveUtility.BlackKing, true)]
        [TestCase(MoveUtility.WhiteKing, true)]
        [TestCase(MoveUtility.BlackRook, false)]
        public void IsCastle_Returns_Correct_Value(uint promotionPiece, bool expectedResult)
        {
            var move = 0U.SetPromotionPiece(promotionPiece);

            Assert.That(MoveUtility.IsCastle(move), Is.EqualTo(expectedResult));
        }

        [TestCase(MoveUtility.BlackKing, 4u, 6u, true)]
        [TestCase(MoveUtility.WhiteKing, 4u, 6u, true)]
        [TestCase(MoveUtility.BlackRook, 4u, 6u, false)]
        [TestCase(MoveUtility.BlackKing, 4u, 5u, false)]
        public void IsCastleOO_Returns_Correct_Value(uint promotionPiece, uint fromSquare, uint toSquare, bool expectedResult)
        {
            var move = 0U.SetFromMove(fromSquare);
            move = move.SetToMove(toSquare);
            move = move.SetMovingPiece(promotionPiece);
            move = move.SetPromotionPiece(promotionPiece);
          
           

            Assert.That(MoveUtility.IsCastleOO(move), Is.EqualTo(expectedResult));
        }

        [TestCase(MoveUtility.BlackKing, 4u, 2u, true)]
        [TestCase(MoveUtility.WhiteKing, 4u, 2u, true)]
        [TestCase(MoveUtility.BlackRook, 4u, 2u, false)]
        [TestCase(MoveUtility.BlackKing, 4u, 3u, false)]
        public void IsCastleOOO_Returns_Correct_Value(uint promotionPiece, uint fromSquare, uint toSquare, bool expectedResult)
        {
            var move = 0U.SetFromMove(fromSquare);
            move = move.SetToMove(toSquare);
            move = move.SetMovingPiece(promotionPiece);
            move = move.SetPromotionPiece(promotionPiece);

            Assert.That(MoveUtility.IsCastleOOO(move), Is.EqualTo(expectedResult));
        }

        [TestCase(MoveUtility.BlackQueen, true)]
        [TestCase(MoveUtility.WhiteQueen, true)]
        [TestCase(0u, false)]
        public void IsPromotion_Returns_Correct_Value(uint promotionPiece, bool expectedResult)
        {
            var move = 0U.SetPromotionPiece(promotionPiece);

            Assert.That(MoveUtility.IsPromotion(move), Is.EqualTo(expectedResult));
        } 
       
        [TestCase(ulong.MaxValue, 0)]
        [TestCase(ulong.MaxValue - 1, 1)]
        [TestCase(9223372036854775808UL, 63)]
        public void GetFirstPieceFromBitBoard_Returns_CorrectValue(ulong board, int expectedResult)
        {
            var result = board.GetFirstPieceFromBitBoard();

            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [TestCase(0U)]
        [TestCase(63U)]
        public void GetFromMove_Returns_CorrectValue(uint expectedResult)
        {
            var move = 0U.SetFromMove(expectedResult);

            var result = move.GetFromMove();

            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [TestCase(0U)]
        [TestCase(63U)]
        public void GetToMove_Returns_CorrectValue(uint expectedResult)
        {
            var move = 0U.SetToMove(expectedResult);

            var result = move.GetToMove();

            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [TestCase(MoveUtility.WhitePawn)]
        [TestCase(MoveUtility.BlackQueen)]
        public void GetMovingPiece_Returns_CorrectValue(uint expectedResult)
        {
            var move = 0U.SetMovingPiece(expectedResult);

            var result = move.GetMovingPiece();

            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [TestCase(MoveUtility.WhitePawn)]
        [TestCase(MoveUtility.BlackQueen)]
        public void GetCapturedPiece_Returns_CorrectValue(uint expectedResult)
        {
            var move = 0U.SetCapturedPiece(expectedResult);

            var result = move.GetCapturedPiece();

            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [TestCase(MoveUtility.WhiteRook)]
        [TestCase(MoveUtility.BlackQueen)]
        public void GetPromotedPiece_Returns_CorrectValue(uint expectedResult)
        {
            var move = 0U.SetCapturedPiece(expectedResult);

            var result = move.GetCapturedPiece();

            Assert.That(result, Is.EqualTo(expectedResult));
        }
	}
}
