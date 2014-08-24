using DotNetEngine.Engine;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetEngine.Test
{
    public class MoveDataTests
    {
        private MoveData moveLookups;

        [SetUp]
        public void Setup()
        {
            moveLookups = new MoveData();
        }

        [Test]
        public void Generates_Knight_Board_Correctly_Lower_Left_Board()
        {
            var board = moveLookups.KnightAttacks[0];

            Assert.That(board & MoveUtility.BitStates[10], Is.EqualTo(MoveUtility.BitStates[10]));
            Assert.That(board & MoveUtility.BitStates[17], Is.EqualTo(MoveUtility.BitStates[17]));

            //check that nothing else is set
            for (var i = 0; i < 64; i++)
            {
                if (i != 10 && i != 17)
                    Assert.That(board & MoveUtility.BitStates[i], Is.EqualTo(0));
            }
        }

         [Test]
        public void Generates_Knight_Board_Correctly_Lower_Right_Board()
        {
            var board = moveLookups.KnightAttacks[7];

            Assert.That(board & MoveUtility.BitStates[13], Is.EqualTo(MoveUtility.BitStates[13]));
            Assert.That(board & MoveUtility.BitStates[22], Is.EqualTo(MoveUtility.BitStates[22]));

            //check that nothing else is set
            for (var i = 0; i < 64; i++)
            {
                if (i != 13 && i != 22)
                    Assert.That(board & MoveUtility.BitStates[i], Is.EqualTo(0));
            }
        }

         [Test]
        public void Generates_Knight_Board_Correctly_Upper_Left_Board()
        {
            var board = moveLookups.KnightAttacks[56];

            Assert.That(board & MoveUtility.BitStates[50], Is.EqualTo(MoveUtility.BitStates[50]));
            Assert.That(board & MoveUtility.BitStates[41], Is.EqualTo(MoveUtility.BitStates[41]));

            //check that nothing else is set
            for (var i = 0; i < 64; i++)
            {
                if (i != 50 && i != 41)
                    Assert.That(board & MoveUtility.BitStates[i], Is.EqualTo(0));
            }
        }

         [Test]
        public void Generates_Knight_Board_Correctly_Upper_Right_Board()
        {
            var board = moveLookups.KnightAttacks[63];

            Assert.That(board & MoveUtility.BitStates[53], Is.EqualTo(MoveUtility.BitStates[53]));
            Assert.That(board & MoveUtility.BitStates[46], Is.EqualTo(MoveUtility.BitStates[46]));

            //check that nothing else is set
            for (var i = 0; i < 64; i++)
            {
                if (i != 53 && i != 46)
                    Assert.That(board & MoveUtility.BitStates[i], Is.EqualTo(0));
            }
        }

         [Test]
        public void Generates_Knight_Board_Correctly_Middle_Board()
        {
            var board = moveLookups.KnightAttacks[27];

            Assert.That(board & MoveUtility.BitStates[42], Is.EqualTo(MoveUtility.BitStates[42]));
            Assert.That(board & MoveUtility.BitStates[44], Is.EqualTo(MoveUtility.BitStates[44]));

            Assert.That(board & MoveUtility.BitStates[33], Is.EqualTo(MoveUtility.BitStates[33]));
            Assert.That(board & MoveUtility.BitStates[37], Is.EqualTo(MoveUtility.BitStates[37]));

            Assert.That(board & MoveUtility.BitStates[17], Is.EqualTo(MoveUtility.BitStates[17]));
            Assert.That(board & MoveUtility.BitStates[21], Is.EqualTo(MoveUtility.BitStates[21]));

            Assert.That(board & MoveUtility.BitStates[10], Is.EqualTo(MoveUtility.BitStates[10]));
            Assert.That(board & MoveUtility.BitStates[12], Is.EqualTo(MoveUtility.BitStates[12]));

            //check that nothing else is set
            for (var i = 0; i < 64; i++)
            {
                if (i != 42 && i != 44 && i != 33 && i != 37 && i != 17 && i != 21 && i != 10 && i != 12)
                    Assert.That(board & MoveUtility.BitStates[i], Is.EqualTo(0));
            }
        }

        [Test]
        public void Generates_King_Board_Correctly_Lower_Left()
        {
            var board = moveLookups.KingAttacks[0];

            Assert.That(board & MoveUtility.BitStates[1], Is.EqualTo(MoveUtility.BitStates[1]));
            Assert.That(board & MoveUtility.BitStates[8], Is.EqualTo(MoveUtility.BitStates[8]));
            Assert.That(board & MoveUtility.BitStates[9], Is.EqualTo(MoveUtility.BitStates[9]));

            //check that nothing else is set
            for (var i = 0; i < 64; i++)
            {
                if (i != 1 && i != 8 && i != 9)
                    Assert.That(board & MoveUtility.BitStates[i], Is.EqualTo(0));
            }
        }

        [Test]
        public void Generates_King_Board_Correctly_Lower_Right()
        {
            var board = moveLookups.KingAttacks[7];

            Assert.That(board & MoveUtility.BitStates[6], Is.EqualTo(MoveUtility.BitStates[6]));
            Assert.That(board & MoveUtility.BitStates[15], Is.EqualTo(MoveUtility.BitStates[15]));
            Assert.That(board & MoveUtility.BitStates[14], Is.EqualTo(MoveUtility.BitStates[14]));

            //check that nothing else is set
            for (var i = 0; i < 64; i++)
            {
                if (i != 6 && i != 15 && i != 14)
                    Assert.That(board & MoveUtility.BitStates[i], Is.EqualTo(0));
            }
        }

        [Test]
        public void Generates_King_Board_Correctly_Upper_Left()
        {
            var board = moveLookups.KingAttacks[56];

            Assert.That(board & MoveUtility.BitStates[48], Is.EqualTo(MoveUtility.BitStates[48]));
            Assert.That(board & MoveUtility.BitStates[49], Is.EqualTo(MoveUtility.BitStates[49]));
            Assert.That(board & MoveUtility.BitStates[57], Is.EqualTo(MoveUtility.BitStates[57]));

            //check that nothing else is set
            for (var i = 0; i < 64; i++)
            {
                if (i != 48 && i != 49 && i != 57)
                    Assert.That(board & MoveUtility.BitStates[i], Is.EqualTo(0));
            }
        }

        [Test]
        public void Generates_King_Board_Correctly_Upper_Right()
        {
            var board = moveLookups.KingAttacks[63];

            Assert.That(board & MoveUtility.BitStates[62], Is.EqualTo(MoveUtility.BitStates[62]));
            Assert.That(board & MoveUtility.BitStates[55], Is.EqualTo(MoveUtility.BitStates[55]));
            Assert.That(board & MoveUtility.BitStates[54], Is.EqualTo(MoveUtility.BitStates[54]));

            //check that nothing else is set
            for (var i = 0; i < 64; i++)
            {
                if (i != 62 && i != 55 && i != 54)
                    Assert.That(board & MoveUtility.BitStates[i], Is.EqualTo(0));
            }
        }

        [Test]
        public void Generates_King_Board_Correctly_Center()
        {
            var board = moveLookups.KingAttacks[27];
            
            Assert.That(board & MoveUtility.BitStates[26], Is.EqualTo(MoveUtility.BitStates[26]));
            Assert.That(board & MoveUtility.BitStates[28], Is.EqualTo(MoveUtility.BitStates[28]));

            Assert.That(board & MoveUtility.BitStates[35], Is.EqualTo(MoveUtility.BitStates[35]));
            Assert.That(board & MoveUtility.BitStates[19], Is.EqualTo(MoveUtility.BitStates[19]));

            Assert.That(board & MoveUtility.BitStates[34], Is.EqualTo(MoveUtility.BitStates[34]));
            Assert.That(board & MoveUtility.BitStates[36], Is.EqualTo(MoveUtility.BitStates[36]));

            Assert.That(board & MoveUtility.BitStates[18], Is.EqualTo(MoveUtility.BitStates[18]));
            Assert.That(board & MoveUtility.BitStates[20], Is.EqualTo(MoveUtility.BitStates[20]));

            //check that nothing else is set
            for (var i = 0; i < 64; i++)
            {
                if (i != 26 && i != 28 && i != 35 && i != 19 && i != 34 && i != 36 && i != 18 && i != 20)
                    Assert.That(board & MoveUtility.BitStates[i], Is.EqualTo(0));
            }
        }
   
        [Test]
        public void Generates_White_Pawn_Attack_Board_Correctly()
        {
            var board = moveLookups.WhitePawnAttacks[1];

            Assert.That(board & MoveUtility.BitStates[8], Is.EqualTo(MoveUtility.BitStates[8]));
            Assert.That(board & MoveUtility.BitStates[10], Is.EqualTo(MoveUtility.BitStates[10]));

            //check that nothing else is set
            for (var i = 0; i < 64; i++)
            {
                if (i != 8 && i != 10)
                    Assert.That(board & MoveUtility.BitStates[i], Is.EqualTo(0));
            }
        }

        [Test]
        public void Generates_Black_Pawn_Attack_Board_Correctly()
        {
            var board = moveLookups.BlackPawnAttacks[57];

            Assert.That(board & MoveUtility.BitStates[48], Is.EqualTo(MoveUtility.BitStates[48]));
            Assert.That(board & MoveUtility.BitStates[50], Is.EqualTo(MoveUtility.BitStates[50]));

            //check that nothing else is set
            for (var i = 0; i < 64; i++)
            {
                if (i != 48 && i != 50)
                    Assert.That(board & MoveUtility.BitStates[i], Is.EqualTo(0));
            }
        }

        [TestCase(0, 8)]
        [TestCase(55, 63)]
        public void Generates_White_Pawn_Single_Move_Correctly(int boardIndex, int targetSquare)
        {
            var board = moveLookups.WhitePawnMoves[boardIndex];

            Assert.That(board & MoveUtility.BitStates[targetSquare], Is.EqualTo(MoveUtility.BitStates[targetSquare]));

            //check that nothing else is set
            for (var i = 0; i < 64; i++)
            {
                if (i != targetSquare)
                    Assert.That(board & MoveUtility.BitStates[i], Is.EqualTo(0));
            }
        }

        [TestCase(8, 0)]
        [TestCase(63, 55)]
        public void Generates_Black_Pawn_Single_Move_Correctly(int boardIndex, int targetSquare)
        {
            var board = moveLookups.BlackPawnMoves[boardIndex];

            Assert.That(board & MoveUtility.BitStates[targetSquare], Is.EqualTo(MoveUtility.BitStates[targetSquare]));

            //check that nothing else is set
            for (var i = 0; i < 64; i++)
            {
                if (i != targetSquare)
                    Assert.That(board & MoveUtility.BitStates[i], Is.EqualTo(0));
            }
        }

        [TestCase(8, 24)]
        [TestCase(15, 31)]
        public void Generates_White_Pawn_Double_Move_Correctly(int boardIndex, int targetSquare)
        {
            var board = moveLookups.WhitePawnDoubleMoves[boardIndex];

            Assert.That(board & MoveUtility.BitStates[targetSquare], Is.EqualTo(MoveUtility.BitStates[targetSquare]));

            //check that nothing else is set
            for (var i = 0; i < 64; i++)
            {
                if (i != targetSquare)
                    Assert.That(board & MoveUtility.BitStates[i], Is.EqualTo(0));
            }
        }

        [TestCase(48, 32)]
        [TestCase(55, 39)]
        public void Generates_Black_Pawn_Double_Move_Correctly(int boardIndex, int targetSquare)
        {
            var board = moveLookups.BlackPawnDoubleMoves[boardIndex];

            Assert.That(board & MoveUtility.BitStates[targetSquare], Is.EqualTo(MoveUtility.BitStates[targetSquare]));

            //check that nothing else is set
            for (var i = 0; i < 64; i++)
            {
                if (i != targetSquare)
                    Assert.That(board & MoveUtility.BitStates[i], Is.EqualTo(0));
            }
        }

        [TestCase(0,0,254)]
        [TestCase(7, 0, 127)]
        [TestCase(0,63,2)]
        [TestCase(7, 63, 64)]
        [TestCase(5, 63, 80)]
        [TestCase(5, 0, 223)]
        public void Generates_Sliding_Attacks_Correctly(int position, int state, int result)
        {
            var attack = moveLookups.SlidingAttacks[position][state];

            Assert.That(attack, Is.EqualTo(result));
        }

        [TestCase(0, 0, 254UL)]
        [TestCase(7, 0, 127UL)]
        [TestCase(0, 63, 2UL)]
        [TestCase(63, 0, 9151314442816847872UL)]
        [TestCase(56, 0, 18302628885633695744UL)]
        [TestCase(63, 63, 4611686018427387904UL)]
        public void Generates_RankAttacks_Correctly(int position, int state, ulong result)
        {
            var attack = moveLookups.RankAttacks[position][state];

            Assert.That(attack, Is.EqualTo(result));
        }

        [TestCase(0,0, 72340172838076672UL)]
        [TestCase(0, 63, 256UL)]
        [TestCase(56, 0, 282578800148737UL)]
        [TestCase(56, 63, 281474976710656UL)]
        public void Generates_FileAttacks_Correctly(int position, int state, ulong result)
        {
            var attack = moveLookups.FileAttacks[position][state];

            Assert.That(attack, Is.EqualTo(result));
        }

        [TestCase(6,0, 32768UL)]
        [TestCase(6, 63,32768UL)]
        [TestCase(48, 0, 144115188075855872UL)]
        [TestCase(48, 63, 144115188075855872UL)]
        [TestCase(56, 0, 0UL)]
        [TestCase(7, 0, 0UL)]
        [TestCase(0, 0, 9241421688590303744UL)]
        [TestCase(0, 63, 512UL)]
        [TestCase(63, 0, 18049651735527937UL)]
        [TestCase(63, 63, 18014398509481984UL)]
        public void Generates_A1H8_DiagonalAttacks_Correctly(int position, int state, ulong result)
        {
            var attack = moveLookups.DiagonalA1H8Attacks[position][state];

            Assert.That(attack, Is.EqualTo(result));
        }

        [TestCase(8, 0, 2UL)]
        [TestCase(8, 63, 2UL)]
        [TestCase(0, 0, 0UL)]
        [TestCase(63, 0, 0UL)]
        [TestCase(62, 0, 36028797018963968UL)]
        [TestCase(62, 63, 36028797018963968UL)]
        [TestCase(56, 0, 567382630219904UL)]
        [TestCase(56, 63, 562949953421312UL)]
        [TestCase(7, 0, 72624976668147712UL)]
        [TestCase(7, 63, 16384UL)]
        public void Generates_A8H1_DiagonalAttacks_Correctly(int position, int state, ulong result)
        {
            var attack = moveLookups.DiagonalA8H1Attacks[position][state];

            Assert.That(attack, Is.EqualTo(result));
        }

        [TestCase(0, 1ul, ulong.MaxValue, 72340172838076926UL)] 
        [TestCase(0, 1ul, ulong.MinValue, 0UL)]
        [TestCase(0, ulong.MaxValue, ulong.MaxValue, 258UL)]
        [TestCase(0, ulong.MaxValue, ulong.MinValue, 0UL)] 
        [TestCase(7, 128ul, ulong.MaxValue, 9259542123273814143UL)]
        [TestCase(7, 128ul, ulong.MinValue, 0UL)]
        [TestCase(56, 72057594037927936ul, ulong.MaxValue, 18302911464433844481UL)]
        [TestCase(56,72057594037927936ul, ulong.MinValue,0UL)]
        [TestCase(63, 9223372036854775808UL, ulong.MaxValue, 9187484529235886208UL)]
        [TestCase(63, 9223372036854775808UL, ulong.MinValue, 0UL)]
        [TestCase(27, 134217728UL, ulong.MaxValue, 578721386714368008UL)]
        [TestCase(27, 134217728UL, ulong.MinValue, 0UL)]
        public void Generates_RookMoves_Correctly(int fromSquare, ulong occupiedSquares, ulong targetboard, ulong result)
        {
            var move = moveLookups.GetRookMoves(fromSquare, occupiedSquares, targetboard);
            Assert.That(move, Is.EqualTo(result));
        }

        [TestCase(27, 134217728UL, ulong.MaxValue, 9241705379636978241UL)]
        [TestCase(27, 134217728UL, ulong.MinValue, 0UL)]
        [TestCase(27, ulong.MaxValue, ulong.MaxValue, 85900656640UL)]
        [TestCase(27, ulong.MaxValue, ulong.MinValue, 0UL)]
        [TestCase(28, 134217728UL, ulong.MaxValue, 108724279602332802UL)]
        [TestCase(28, 134217728UL, ulong.MinValue, 0UL)]
        [TestCase(28, ulong.MaxValue, ulong.MaxValue, 171801313280UL)]
        [TestCase(28, ulong.MaxValue, ulong.MinValue, 0UL)]
        public void Generates_BishopMoves_Correctly(int fromSquare, ulong occupiedSquares, ulong targetboard, ulong result)
        {
            var move = moveLookups.GetBishopMoves(fromSquare, occupiedSquares, targetboard);

            GameStateUtility.ConvertSingleBitBoardsToConsoleOutput(move);

            Assert.That(move, Is.EqualTo(result));


        }       
    }
}