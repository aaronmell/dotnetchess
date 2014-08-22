using DotNetEngine.Engine;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetEngine.Test
{
    public class MoveLookupTests
    {
        private MoveLookups moveLookups;

        [SetUp]
        public void Setup()
        {
            moveLookups = new MoveLookups();
        }

        [Test]
        public void Generates_Knight_Board_Correctly_Lower_Left_Board()
        {
            var board = moveLookups.KnightAttacks[0];

            Assert.That(board & GameStateUtility.BitStates[10], Is.EqualTo(GameStateUtility.BitStates[10]));
            Assert.That(board & GameStateUtility.BitStates[17], Is.EqualTo(GameStateUtility.BitStates[17]));

            //check that nothing else is set
            for (var i = 0; i < 64; i++)
            {
                if (i != 10 && i != 17)
                    Assert.That(board & GameStateUtility.BitStates[i], Is.EqualTo(0));
            }
        }

         [Test]
        public void Generates_Knight_Board_Correctly_Lower_Right_Board()
        {
            var board = moveLookups.KnightAttacks[7];

            Assert.That(board & GameStateUtility.BitStates[13], Is.EqualTo(GameStateUtility.BitStates[13]));
            Assert.That(board & GameStateUtility.BitStates[22], Is.EqualTo(GameStateUtility.BitStates[22]));

            //check that nothing else is set
            for (var i = 0; i < 64; i++)
            {
                if (i != 13 && i != 22)
                    Assert.That(board & GameStateUtility.BitStates[i], Is.EqualTo(0));
            }
        }

         [Test]
        public void Generates_Knight_Board_Correctly_Upper_Left_Board()
        {
            var board = moveLookups.KnightAttacks[56];

            Assert.That(board & GameStateUtility.BitStates[50], Is.EqualTo(GameStateUtility.BitStates[50]));
            Assert.That(board & GameStateUtility.BitStates[41], Is.EqualTo(GameStateUtility.BitStates[41]));

            //check that nothing else is set
            for (var i = 0; i < 64; i++)
            {
                if (i != 50 && i != 41)
                    Assert.That(board & GameStateUtility.BitStates[i], Is.EqualTo(0));
            }
        }

         [Test]
        public void Generates_Knight_Board_Correctly_Upper_Right_Board()
        {
            var board = moveLookups.KnightAttacks[63];

            Assert.That(board & GameStateUtility.BitStates[53], Is.EqualTo(GameStateUtility.BitStates[53]));
            Assert.That(board & GameStateUtility.BitStates[46], Is.EqualTo(GameStateUtility.BitStates[46]));

            //check that nothing else is set
            for (var i = 0; i < 64; i++)
            {
                if (i != 53 && i != 46)
                    Assert.That(board & GameStateUtility.BitStates[i], Is.EqualTo(0));
            }
        }

         [Test]
        public void Generates_Knight_Board_Correctly_Middle_Board()
        {
            var board = moveLookups.KnightAttacks[27];

            Assert.That(board & GameStateUtility.BitStates[42], Is.EqualTo(GameStateUtility.BitStates[42]));
            Assert.That(board & GameStateUtility.BitStates[44], Is.EqualTo(GameStateUtility.BitStates[44]));

            Assert.That(board & GameStateUtility.BitStates[33], Is.EqualTo(GameStateUtility.BitStates[33]));
            Assert.That(board & GameStateUtility.BitStates[37], Is.EqualTo(GameStateUtility.BitStates[37]));

            Assert.That(board & GameStateUtility.BitStates[17], Is.EqualTo(GameStateUtility.BitStates[17]));
            Assert.That(board & GameStateUtility.BitStates[21], Is.EqualTo(GameStateUtility.BitStates[21]));

            Assert.That(board & GameStateUtility.BitStates[10], Is.EqualTo(GameStateUtility.BitStates[10]));
            Assert.That(board & GameStateUtility.BitStates[12], Is.EqualTo(GameStateUtility.BitStates[12]));

            //check that nothing else is set
            for (var i = 0; i < 64; i++)
            {
                if (i != 42 && i != 44 && i != 33 && i != 37 && i != 17 && i != 21 && i != 10 && i != 12)
                    Assert.That(board & GameStateUtility.BitStates[i], Is.EqualTo(0));
            }
        }

        [Test]
        public void Generates_King_Board_Correctly_Lower_Left()
        {
            var board = moveLookups.KingAttacks[0];

            Assert.That(board & GameStateUtility.BitStates[1], Is.EqualTo(GameStateUtility.BitStates[1]));
            Assert.That(board & GameStateUtility.BitStates[8], Is.EqualTo(GameStateUtility.BitStates[8]));
            Assert.That(board & GameStateUtility.BitStates[9], Is.EqualTo(GameStateUtility.BitStates[9]));

            //check that nothing else is set
            for (var i = 0; i < 64; i++)
            {
                if (i != 1 && i != 8 && i != 9)
                    Assert.That(board & GameStateUtility.BitStates[i], Is.EqualTo(0));
            }
        }

        [Test]
        public void Generates_King_Board_Correctly_Lower_Right()
        {
            var board = moveLookups.KingAttacks[7];

            Assert.That(board & GameStateUtility.BitStates[6], Is.EqualTo(GameStateUtility.BitStates[6]));
            Assert.That(board & GameStateUtility.BitStates[15], Is.EqualTo(GameStateUtility.BitStates[15]));
            Assert.That(board & GameStateUtility.BitStates[14], Is.EqualTo(GameStateUtility.BitStates[14]));

            //check that nothing else is set
            for (var i = 0; i < 64; i++)
            {
                if (i != 6 && i != 15 && i != 14)
                    Assert.That(board & GameStateUtility.BitStates[i], Is.EqualTo(0));
            }
        }

        [Test]
        public void Generates_King_Board_Correctly_Upper_Left()
        {
            var board = moveLookups.KingAttacks[56];

            Assert.That(board & GameStateUtility.BitStates[48], Is.EqualTo(GameStateUtility.BitStates[48]));
            Assert.That(board & GameStateUtility.BitStates[49], Is.EqualTo(GameStateUtility.BitStates[49]));
            Assert.That(board & GameStateUtility.BitStates[57], Is.EqualTo(GameStateUtility.BitStates[57]));

            //check that nothing else is set
            for (var i = 0; i < 64; i++)
            {
                if (i != 48 && i != 49 && i != 57)
                    Assert.That(board & GameStateUtility.BitStates[i], Is.EqualTo(0));
            }
        }

        [Test]
        public void Generates_King_Board_Correctly_Upper_Right()
        {
            var board = moveLookups.KingAttacks[63];

            Assert.That(board & GameStateUtility.BitStates[62], Is.EqualTo(GameStateUtility.BitStates[62]));
            Assert.That(board & GameStateUtility.BitStates[55], Is.EqualTo(GameStateUtility.BitStates[55]));
            Assert.That(board & GameStateUtility.BitStates[54], Is.EqualTo(GameStateUtility.BitStates[54]));

            //check that nothing else is set
            for (var i = 0; i < 64; i++)
            {
                if (i != 62 && i != 55 && i != 54)
                    Assert.That(board & GameStateUtility.BitStates[i], Is.EqualTo(0));
            }
        }

        [Test]
        public void Generates_King_Board_Correctly_Center()
        {
            var board = moveLookups.KingAttacks[27];
            
            Assert.That(board & GameStateUtility.BitStates[26], Is.EqualTo(GameStateUtility.BitStates[26]));
            Assert.That(board & GameStateUtility.BitStates[28], Is.EqualTo(GameStateUtility.BitStates[28]));

            Assert.That(board & GameStateUtility.BitStates[35], Is.EqualTo(GameStateUtility.BitStates[35]));
            Assert.That(board & GameStateUtility.BitStates[19], Is.EqualTo(GameStateUtility.BitStates[19]));

            Assert.That(board & GameStateUtility.BitStates[34], Is.EqualTo(GameStateUtility.BitStates[34]));
            Assert.That(board & GameStateUtility.BitStates[36], Is.EqualTo(GameStateUtility.BitStates[36]));

            Assert.That(board & GameStateUtility.BitStates[18], Is.EqualTo(GameStateUtility.BitStates[18]));
            Assert.That(board & GameStateUtility.BitStates[20], Is.EqualTo(GameStateUtility.BitStates[20]));

            //check that nothing else is set
            for (var i = 0; i < 64; i++)
            {
                if (i != 26 && i != 28 && i != 35 && i != 19 && i != 34 && i != 36 && i != 18 && i != 20)
                    Assert.That(board & GameStateUtility.BitStates[i], Is.EqualTo(0));
            }
        }
   
        [Test]
        public void Generates_White_Pawn_Attack_Board_Correctly()
        {
            var board = moveLookups.WhitePawnAttacks[1];

            Assert.That(board & GameStateUtility.BitStates[8], Is.EqualTo(GameStateUtility.BitStates[8]));
            Assert.That(board & GameStateUtility.BitStates[10], Is.EqualTo(GameStateUtility.BitStates[10]));

            //check that nothing else is set
            for (var i = 0; i < 64; i++)
            {
                if (i != 8 && i != 10)
                    Assert.That(board & GameStateUtility.BitStates[i], Is.EqualTo(0));
            }
        }

        [Test]
        public void Generates_Black_Pawn_Attack_Board_Correctly()
        {
            var board = moveLookups.BlackPawnAttacks[57];

            Assert.That(board & GameStateUtility.BitStates[48], Is.EqualTo(GameStateUtility.BitStates[48]));
            Assert.That(board & GameStateUtility.BitStates[50], Is.EqualTo(GameStateUtility.BitStates[50]));

            //check that nothing else is set
            for (var i = 0; i < 64; i++)
            {
                if (i != 48 && i != 50)
                    Assert.That(board & GameStateUtility.BitStates[i], Is.EqualTo(0));
            }
        }

        [TestCase(0, 8)]
        [TestCase(55, 63)]
        public void Generates_White_Pawn_Single_Move_Correctly(int boardIndex, int targetSquare)
        {
            var board = moveLookups.WhitePawnMoves[boardIndex];

            Assert.That(board & GameStateUtility.BitStates[targetSquare], Is.EqualTo(GameStateUtility.BitStates[targetSquare]));

            //check that nothing else is set
            for (var i = 0; i < 64; i++)
            {
                if (i != targetSquare)
                    Assert.That(board & GameStateUtility.BitStates[i], Is.EqualTo(0));
            }
        }

        [TestCase(8, 0)]
        [TestCase(63, 55)]
        public void Generates_Black_Pawn_Single_Move_Correctly(int boardIndex, int targetSquare)
        {
            var board = moveLookups.BlackPawnMoves[boardIndex];

            Assert.That(board & GameStateUtility.BitStates[targetSquare], Is.EqualTo(GameStateUtility.BitStates[targetSquare]));

            //check that nothing else is set
            for (var i = 0; i < 64; i++)
            {
                if (i != targetSquare)
                    Assert.That(board & GameStateUtility.BitStates[i], Is.EqualTo(0));
            }
        }

        [TestCase(8, 24)]
        [TestCase(15, 31)]
        public void Generates_White_Pawn_Double_Move_Correctly(int boardIndex, int targetSquare)
        {
            var board = moveLookups.WhitePawnDoubleMoves[boardIndex];

            Assert.That(board & GameStateUtility.BitStates[targetSquare], Is.EqualTo(GameStateUtility.BitStates[targetSquare]));

            //check that nothing else is set
            for (var i = 0; i < 64; i++)
            {
                if (i != targetSquare)
                    Assert.That(board & GameStateUtility.BitStates[i], Is.EqualTo(0));
            }
        }

        [TestCase(48, 32)]
        [TestCase(55, 39)]
        public void Generates_Black_Pawn_Double_Move_Correctly(int boardIndex, int targetSquare)
        {
            var board = moveLookups.BlackPawnDoubleMoves[boardIndex];

            Assert.That(board & GameStateUtility.BitStates[targetSquare], Is.EqualTo(GameStateUtility.BitStates[targetSquare]));

            //check that nothing else is set
            for (var i = 0; i < 64; i++)
            {
                if (i != targetSquare)
                    Assert.That(board & GameStateUtility.BitStates[i], Is.EqualTo(0));
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

        [TestCase(0, 0, 254ul)]
        [TestCase(7, 0, 127ul)]
        [TestCase(0, 63, 2ul)]
        [TestCase(63, 0, 9151314442816847872ul)]
        [TestCase(56, 0, 18302628885633695744ul)]
        [TestCase(63, 63, 4611686018427387904ul)]
        public void Generates_RankAttacks_Correctly(int position, int state, ulong result)
        {
            var attack = moveLookups.RankAttacks[position][state];

            Assert.That(attack, Is.EqualTo(result));
        }


        [TestCase(0, 0, 254ul)]
        [TestCase(7, 0, 18302628885633695744ul)]
        [TestCase(0, 63, 2ul)]
        [TestCase(63, 0, 9151314442816847872ul)]
        [TestCase(56, 0, 127ul)]
        [TestCase(63, 63, 4611686018427387904ul)]
        public void Generates_FileAttacks_Correctly(int position, int state, ulong result)
        {
            var attack = moveLookups.FileAttacks[position][state];

            Assert.That(attack, Is.EqualTo(result));
        }
    }
}