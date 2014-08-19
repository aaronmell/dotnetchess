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

        public MoveLookups()
        {
            InitializeArrays();

            GenerateKnightAttacks();
            GenerateKingAttacks();
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

            for (int i = 0; i < 64; i++)
            {
                RankAttacks[i] = new ulong[64];
                FileAttacks[i] = new ulong[64];
                DiagonalAttacks[i] = new ulong[64];
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
