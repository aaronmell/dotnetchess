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
        private byte[] byteBitStates = new byte[8]
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
        public ulong[][] DiagonalA1H8Attacks { get; private set; }
        public ulong[][] DiagonalA8H1Attacks { get; private set; }


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
            GenerateFileAttacks();

            GenerateDiagonalA1H8Attacks();
            GenerateDiagonalA8H1Attacks();
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
            DiagonalA1H8Attacks = new ulong[64][];
            DiagonalA8H1Attacks = new ulong[64][];
            SlidingAttacks = new byte[8][];

            for (int i = 0; i < 64; i++)
            {
                RankAttacks[i] = new ulong[64];
                FileAttacks[i] = new ulong[64];
                DiagonalA1H8Attacks[i] = new ulong[64];
                DiagonalA8H1Attacks[i] = new ulong[64];
            }  
          
            for (int i = 0; i < 8; i++)
            {
                SlidingAttacks[i] = new byte[64];
            }
        }

        private void GenerateDiagonalA8H1Attacks()
        {
            for (var square = 0; square < 64; square++)
            {
                for (var attackState = 0; attackState < 64; attackState++)
                {
                    for (var attackbit = 0; attackbit < 8; attackbit++)
                    {
                            
                        var slidingattackindex = 8 - GameStateUtility.Ranks[square] < GameStateUtility.Files[square] - 1 ? 8 - GameStateUtility.Ranks[square] : GameStateUtility.Files[square] - 1;
                        if ((SlidingAttacks[slidingattackindex][attackState] & byteBitStates[attackbit]) == byteBitStates[attackbit])
                        {
                            var diagonalLength = GameStateUtility.Files[square] + GameStateUtility.Ranks[square];
                            
                            var file = 0;
                            var rank = 0;

                            if (diagonalLength < 10)
                            {
                                file = attackbit + 1;
                                rank = diagonalLength - file;
                            }
                            else
                            {
                                rank = 8 - attackbit;
                                file = diagonalLength - rank;
                            }

                            if ((file > 0) && (file < 9) && (rank > 0) && (rank < 9))
                            {
                                DiagonalA8H1Attacks[square][attackState] |=  GameStateUtility.BitStates[GameStateUtility.BoardIndex[rank][file]];
                            }
                        }
                    }
                }
            }
        }

        private void GenerateDiagonalA1H8Attacks()
        {
            for (var square = 0; square < 64; square++)
            {
                for (var attackState = 0; attackState < 64; attackState++)
                {
                    for (var attackbit = 0; attackbit < 8; attackbit++)
                    {

                        var slidingattackindex = (GameStateUtility.Ranks[square] - 1) < (GameStateUtility.Files[square] - 1) ? (GameStateUtility.Ranks[square] - 1) : (GameStateUtility.Files[square] - 1);
                        if ((SlidingAttacks[slidingattackindex][attackState] & byteBitStates[attackbit]) == byteBitStates[attackbit])
                        {
                            var diagonalLength = GameStateUtility.Files[square] - GameStateUtility.Ranks[square];

                            var file = 0;
                            var rank = 0;

                            if (diagonalLength < 0)
                            {
                                file = attackbit + 1;
                                rank = file - diagonalLength;
                            }
                            else
                            {
                                rank = attackbit + 1;
                                file = diagonalLength + rank;
                            }

                            if ((file > 0) && (file < 9) && (rank > 0) && (rank < 9))
                            {
                                DiagonalA1H8Attacks[square][attackState] |= GameStateUtility.BitStates[GameStateUtility.BoardIndex[rank][file]];
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Generates the file attacks for rooks and queens.
        /// The Sliding attack is transformed from top to bottom to left to right.
        /// 
        ///                   H8
        ///     . . . . . A . .         LSB         MSB
        ///     . . . . . B . .     =>  A B C D E F G H
        ///     . . . . . C . .
        ///     . . . . . D . .
        ///     . . . . . E . .
        ///     . . . . . F . .
        ///     . . . . . G . .
        ///     . . . . . H . .
        ///     A1
        /// </summary>
        private void GenerateFileAttacks()
        {
            for (var square = 0; square < 64; square++)
            {
                for (var attackState = 0; attackState < 64; attackState++)
                {
                    for (var attackbit = 0; attackbit < 8; attackbit++)
                    {
                        if ((SlidingAttacks[8 - GameStateUtility.Ranks[square]][attackState] & byteBitStates[attackbit]) == byteBitStates[attackbit])
                        {
                            var file = GameStateUtility.Files[square];
                            var rank = 8 - attackbit;
                            FileAttacks[square][attackState] |= GameStateUtility.BitStates[GameStateUtility.BoardIndex[file][rank]];
                        }
                    }
                }
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
            for (int position = 0; position <= 7; position++)
            {
                for (uint state = 0; state < 64; state++)
                {
                    var stateMask = state << 1;
                    var attackMask = 0;

                    if (position < 7)
                    {
                        attackMask |= byteBitStates[position + 1];
                    }

                    var slide = position + 2;
                    while (slide <= 7)
                    {
                        if ((~stateMask & byteBitStates[slide - 1]) == byteBitStates[slide - 1])
                        {
                            attackMask |= byteBitStates[slide];
                        }
                        else
                        {
                            break;
                        }
                        slide++;
                    }
                    if (position > 0)
                    {
                        attackMask |= byteBitStates[position - 1];
                    }
                    slide = position - 2;
                    while (slide >= 0)
                    {
                        if ((~stateMask & byteBitStates[slide + 1]) == byteBitStates[slide + 1])
                        {
                            attackMask |= byteBitStates[slide];
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
            for (var square = 0; square < 64; square++)
            {
                for (var attackState = 0; attackState < 64; attackState++)
                {
                    RankAttacks[square][attackState] = ((ulong)SlidingAttacks[GameStateUtility.Files[square] - 1][attackState] << (GameStateUtility.ShiftedRank[square] - 1));
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
