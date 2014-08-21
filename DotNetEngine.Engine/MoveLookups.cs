using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetEngine.Engine
{
     
    /// <summary>
    /// Contains bitboards of all of the Pre-Computed Valid locations for each piece type on the board based on its starting location.
    /// </summary>
    public class MoveLookups
    {
        public ulong[] KnightAttacks { get; private set; }
        public ulong[] KingAttacks { get; private set; }

        public ulong[] WhitePawnAttacks { get; private set; }
        public ulong[] WhitePawnMoves { get; private set; }
        public ulong[] WhitePawnDoubleMoves { get; private set; }

        public ulong[] BlackPawnAttacks { get; private set; }
        public ulong[] BlackPawnMoves { get; private set; }
        public ulong[] BlackPawnDoubleMoves { get; private set; }

        public ulong[][] RankAttacks { get; private set; }
        public ulong[][] FileAttacks { get; private set; }
        public ulong[][] DiagonalAttacks { get; private set; }


        //This should be private, but I wanted to write tests against it to ensure that it works correctly.
        public byte[][] SlidingAttacks {get; private set;}

        public MoveLookups()
        {
            InitializeArrays();

            GenerateKnightAttacks();
            GenerateKingAttacks();
            GenerateWhitePawnAttacks();
            GenerateBlackPawnAttacks();
            GenerateWhitePawnMoves();
            GenerateBlackPawnMoves();

            GenerateSlidingAttacks();

            GenerateRankAttacks();
        }

        private void InitializeArrays()
        {
            KnightAttacks = new ulong[64];
            KingAttacks = new ulong[64];
            WhitePawnAttacks = new ulong[64];
            WhitePawnMoves = new ulong[64];
            WhitePawnDoubleMoves = new ulong[64];
            BlackPawnAttacks = new ulong[64];
            BlackPawnDoubleMoves = new ulong[64];
            BlackPawnMoves = new ulong[64];

            RankAttacks = new ulong[64][];
            FileAttacks = new ulong[64][];
            DiagonalAttacks = new ulong[64][];
            SlidingAttacks = new byte[8][];

            for (int i = 0; i < 64; i++)
            {
                RankAttacks[i] = new ulong[64];
                FileAttacks[i] = new ulong[64];
                DiagonalAttacks[i] = new ulong[64];
            }  
          
            for (int i = 0; i < 8; i++)
            {
                SlidingAttacks[i] = new byte[64];
            }
        }

        /// <summary>
        /// Generates a jagged array of sliding attacks.
        /// Sliding attacks have two Components
        /// The index, which is the position of the attacker on the file rank or diagonal 1-8.
        /// And the occupancy state on the file rank or diagonal.
        /// 
        /// So for [0][0] the rank would looks like this .......X 
        /// There is no occupany on any of the other position,
        /// so we can move to any bit on the rank, so the value would be 254 = 11111110
        /// 
        /// For [0][63] the rank would look like this oooooooX
        /// There is only a single place we can attack so the value would be 2 = 00000010
        /// 
        /// This is used to generate all of the sliding piece attack bitboards
        /// </summary>
        private void GenerateSlidingAttacks()
        {
            var bitStates = new byte[8]
            {
                (byte)GameStateUtility.BitStates[0],
                (byte)GameStateUtility.BitStates[1],
                (byte)GameStateUtility.BitStates[2],
                (byte)GameStateUtility.BitStates[3],
                (byte)GameStateUtility.BitStates[4],
                (byte)GameStateUtility.BitStates[5],
                (byte)GameStateUtility.BitStates[6],
                (byte)GameStateUtility.BitStates[7],
            };

           
            for (int position = 0; position <= 7; position++)
            {
                for (uint state = 0; state < 64; state++)
                {
                    var stateMask = state << 1;
                    var attackMask = 0;

                    if (position < 7)
                    {
                        attackMask |= bitStates[position + 1];
                    }

                    var slide = position + 2;
                    while (slide <= 7)
                    {
                        if ((~stateMask & bitStates[slide - 1]) == bitStates[slide - 1])
                        {
                            attackMask |= bitStates[slide];
                        }
                        else
                        {
                            break;
                        }
                        slide++;
                    }
                    if (position > 0)
                    {
                        attackMask |= bitStates[position - 1];
                    }
                    slide = position - 2;
                    while (slide >= 0)
                    {
                        if ((~stateMask & bitStates[slide + 1]) == bitStates[slide + 1])
                        {
                            attackMask |= bitStates[slide];
                        }
                        else
                        {
                            break;
                        }

                           
                        slide--;
                    }
                    SlidingAttacks[position][state] = (byte)attackMask;
                }
            }
        }

        //The sliding attacks array doesn't require a transformation, just a shift for each row on the board. 
        private void GenerateRankAttacks()
        {
            for (var i = 0; i < 64; i++)
            {
                for (var j = 0; j < 64; j++)
                {
                    RankAttacks[i][j] = ((ulong)SlidingAttacks[GameStateUtility.Files[i] - 1][j] << (GameStateUtility.ShiftedRank[i] - 1));
                }
            }
        }

        private void GenerateBlackPawnMoves()
        {
            for (int i = 0; i < 64; i++)
            {
                var file = GameStateUtility.Files[i];
                var rank = GameStateUtility.Ranks[i];

                var targetRank = rank - 1;
                var targetFile = file;

                //SingleMove
                if (ValidLocation(targetFile, targetRank))
                    BlackPawnMoves[i] |= GameStateUtility.BitStates[GameStateUtility.BoardIndex[targetRank][targetFile]];

                //Double Move
                if (rank == 7)
                {
                    targetRank = rank - 2;

                    if (ValidLocation(targetFile, targetRank))
                        BlackPawnDoubleMoves[i] |= GameStateUtility.BitStates[GameStateUtility.BoardIndex[targetRank][targetFile]];
                }
            }
        }

        private void GenerateWhitePawnMoves()
        {
            for (int i = 0; i < 64; i++)
            {
                var file = GameStateUtility.Files[i];
                var rank = GameStateUtility.Ranks[i];

                var targetRank = rank + 1;
                var targetFile = file;

                //Single Move
                if (ValidLocation(targetFile, targetRank))
                    WhitePawnMoves[i] |= GameStateUtility.BitStates[GameStateUtility.BoardIndex[targetRank][targetFile]];

                //Double Move
                if (rank == 2)
                {
                    targetRank = rank + 2;
                    
                    if (ValidLocation(targetFile, targetRank))
                        WhitePawnDoubleMoves[i] |= GameStateUtility.BitStates[GameStateUtility.BoardIndex[targetRank][targetFile]];
                }
            }
        }

        private void GenerateBlackPawnAttacks()
        {

            for (int i = 0; i < 64; i++)
            {
                var file = GameStateUtility.Files[i];
                var rank = GameStateUtility.Ranks[i];

                //Attack Left
                var targetFile = file - 1;
                var targetRank = rank - 1;

                if (ValidLocation(targetFile, targetRank))
                    BlackPawnAttacks[i] |= GameStateUtility.BitStates[GameStateUtility.BoardIndex[targetRank][targetFile]];

                //Attack Right
                targetFile = file + 1;

                if (ValidLocation(targetFile, targetRank))
                    BlackPawnAttacks[i] |= GameStateUtility.BitStates[GameStateUtility.BoardIndex[targetRank][targetFile]];
            }
        }             

        private void GenerateWhitePawnAttacks()
        {
            for (int i = 0; i < 64; i++)
            {
                var file = GameStateUtility.Files[i];
                var rank = GameStateUtility.Ranks[i];

                //Attack Left
                var targetFile = file - 1;
                var targetRank = rank + 1;

                if (ValidLocation(targetFile, targetRank))
                    WhitePawnAttacks[i] |= GameStateUtility.BitStates[GameStateUtility.BoardIndex[targetRank][targetFile]];

                //Attack Right
                targetFile = file + 1;

                if (ValidLocation(targetFile, targetRank))
                    WhitePawnAttacks[i] |= GameStateUtility.BitStates[GameStateUtility.BoardIndex[targetRank][targetFile]];
            }
        }  

        private void GenerateKingAttacks()
        {
            for (int i = 0; i < 64; i++)
            {
                var file = GameStateUtility.Files[i];
                var rank = GameStateUtility.Ranks[i];

                //Down
                var targetRank = rank - 1;
                var targetFile = file;

                if (ValidLocation(targetFile, targetRank))
                    KingAttacks[i] |= GameStateUtility.BitStates[GameStateUtility.BoardIndex[targetRank][targetFile]];

                //Up
                targetRank = rank + 1;
                targetFile = file;

                if (ValidLocation(targetFile, targetRank))
                    KingAttacks[i] |= GameStateUtility.BitStates[GameStateUtility.BoardIndex[targetRank][targetFile]];

                //Left
                targetRank = rank;
                targetFile = file - 1;

                if (ValidLocation(targetFile, targetRank))
                    KingAttacks[i] |= GameStateUtility.BitStates[GameStateUtility.BoardIndex[targetRank][targetFile]];

                //Right
                targetRank = rank;
                targetFile = file + 1;

                if (ValidLocation(targetFile, targetRank))
                    KingAttacks[i] |= GameStateUtility.BitStates[GameStateUtility.BoardIndex[targetRank][targetFile]];

                //Down Left
                targetRank = rank - 1;
                targetFile = file - 1;

                if (ValidLocation(targetFile, targetRank))
                    KingAttacks[i] |= GameStateUtility.BitStates[GameStateUtility.BoardIndex[targetRank][targetFile]];

                //Down Right
                targetRank = rank - 1;
                targetFile = file + 1;

                if (ValidLocation(targetFile, targetRank))
                    KingAttacks[i] |= GameStateUtility.BitStates[GameStateUtility.BoardIndex[targetRank][targetFile]];

                //Up Left
                targetRank = rank + 1;
                targetFile = file - 1;

                if (ValidLocation(targetFile, targetRank))
                    KingAttacks[i] |= GameStateUtility.BitStates[GameStateUtility.BoardIndex[targetRank][targetFile]];

                //Up Right
                targetRank = rank + 1;
                targetFile = file + 1;

                if (ValidLocation(targetFile, targetRank))
                    KingAttacks[i] |= GameStateUtility.BitStates[GameStateUtility.BoardIndex[targetRank][targetFile]];
            }
        }

        private void GenerateKnightAttacks()
        {
            for (int i = 0; i < 64; i++)
            {
                var file = GameStateUtility.Files[i];
                var rank = GameStateUtility.Ranks[i];

                //Down 2 Right 1
                var targetRank = rank - 2;
                var targetFile = file + 1;

                if (ValidLocation(targetFile, targetRank))
                    KnightAttacks[i] |= GameStateUtility.BitStates[GameStateUtility.BoardIndex[targetRank][targetFile]];

                //Down 2 Left 1
                targetFile = file - 1;

                if (ValidLocation(targetFile, targetRank))
                    KnightAttacks[i] |= GameStateUtility.BitStates[GameStateUtility.BoardIndex[targetRank][targetFile]];

                //Down 1 Right 2
                targetRank = rank - 1;
                targetFile = file + 2;

                if (ValidLocation(targetFile, targetRank))
                    KnightAttacks[i] |= GameStateUtility.BitStates[GameStateUtility.BoardIndex[targetRank][targetFile]];

                //Down 1 Left 2
                targetFile = file - 2;

                if (ValidLocation(targetFile, targetRank))
                    KnightAttacks[i] |= GameStateUtility.BitStates[GameStateUtility.BoardIndex[targetRank][targetFile]];

                //Up 2 Right 1
                targetRank = rank + 2;
                targetFile = file + 1;

                if (ValidLocation(targetFile, targetRank))
                    KnightAttacks[i] |= GameStateUtility.BitStates[GameStateUtility.BoardIndex[targetRank][targetFile]];

                //Up 2 Left 1
                targetFile = file - 1;

                if (ValidLocation(targetFile, targetRank))
                    KnightAttacks[i] |= GameStateUtility.BitStates[GameStateUtility.BoardIndex[targetRank][targetFile]];

                //Up 1 Right 2
                targetRank = rank + 1;
                targetFile = file + 2;

                if (ValidLocation(targetFile, targetRank))
                    KnightAttacks[i] |= GameStateUtility.BitStates[GameStateUtility.BoardIndex[targetRank][targetFile]];

                //Up 1 Left 2
                targetFile = file - 2;

                if (ValidLocation(targetFile, targetRank))
                    KnightAttacks[i] |= GameStateUtility.BitStates[GameStateUtility.BoardIndex[targetRank][targetFile]];
               
            }
        }

        private bool ValidLocation(int file, int rank)
        {
            return file >= 1 && file <= 8 && rank >= 1 && rank <= 8;
        }
    }
}
