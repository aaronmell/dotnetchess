namespace DotNetEngine.Engine
{
     
    /// <summary>
    /// Contains all of the move related data.
    /// </summary>
    internal class MoveData
    {
        private static readonly byte[] ByteBitStates = new []
            {
                (byte)MoveUtility.BitStates[0],
                (byte)MoveUtility.BitStates[1],
                (byte)MoveUtility.BitStates[2],
                (byte)MoveUtility.BitStates[3],
                (byte)MoveUtility.BitStates[4],
                (byte)MoveUtility.BitStates[5],
                (byte)MoveUtility.BitStates[6],
                (byte)MoveUtility.BitStates[7]
            };
              
        private readonly ulong[] _rankMask = new ulong[64];
        private readonly ulong[] _fileMask = new ulong[64];
        
        private readonly ulong[] _diagonalA1H8Mask = new ulong[64];
        private readonly ulong[] _diagonalA8H1Mask = new ulong[64];

        internal ulong[] KnightAttacks { get; private set; }
        internal ulong[] KingAttacks { get; private set; }

        internal ulong[] WhitePawnAttacks { get; private set; }
        internal ulong[] WhitePawnMoves { get; private set; }
        internal ulong[] WhitePawnDoubleMoves { get; private set; }

        internal ulong[] BlackPawnAttacks { get; private set; }
        internal ulong[] BlackPawnMoves { get; private set; }
        internal ulong[] BlackPawnDoubleMoves { get; private set; }

        internal ulong[][] RankAttacks { get; private set; }
        internal ulong[][] FileAttacks { get; private set; }
        internal ulong[][] DiagonalA1H8Attacks { get; private set; }
        internal ulong[][] DiagonalA8H1Attacks { get; private set; }       

// ReSharper disable InconsistentNaming
        internal uint WhiteCastleOOMove { get; private set; }
        internal uint WhiteCastleOOOMove { get; private set; }
        internal uint BlackCastleOOMove { get; private set; }
        internal uint BlackCastleOOOMove { get; private set; }
// ReSharper restore InconsistentNaming

        //This should be private, but I wanted to write tests against it to ensure that it works correctly.
        internal byte[][] SlidingAttacks {get; private set;}      

        internal MoveData()
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

            GenerateMasks();
            GenerateCastlingMoves();

        }

        private void GenerateCastlingMoves()
        {
            var move = 0U.SetMovingPiece(MoveUtility.WhiteKing);
            move = move.SetPromotionPiece(MoveUtility.WhiteKing);
            move = move.SetFromMove(4U);
            move = move.SetToMove(6U);

            WhiteCastleOOMove = move;
            move = move.SetToMove(2U);
            WhiteCastleOOOMove = move;

            move = 0U.SetMovingPiece(MoveUtility.BlackKing);
            move = move.SetPromotionPiece(MoveUtility.BlackKing);
            move = move.SetFromMove(60U);
            move = move.SetToMove(62U);

            BlackCastleOOMove = move;
            move = move.SetToMove(58);
            BlackCastleOOOMove = move;
        } 

        internal ulong GetRookMoves(uint fromSquare, ulong occupiedSquares, ulong targetboard)
        {
            return GetRankMoves(fromSquare, occupiedSquares, targetboard) | GetFileMoves(fromSquare, occupiedSquares, targetboard);
        }

        internal ulong GetBishopMoves(uint fromSquare, ulong occupiedSquares, ulong targetboard)
        {
            return GetDiagonalA8H1Moves(fromSquare, occupiedSquares, targetboard) | GetDiagonalA1H8Moves(fromSquare, occupiedSquares, targetboard);
        }

        public ulong GetQueenMoves(uint fromSquare, ulong occupiedSquares, ulong targetboard)
        {
            return GetRookMoves(fromSquare, occupiedSquares, targetboard) | GetBishopMoves(fromSquare, occupiedSquares, targetboard);
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

        private void GenerateMasks()
        {
            for (var file = 1; file < 9; file++)
            {
                for (var rank = 1; rank < 9; rank++)
                {
                    for (var bit = 2; bit < 8; bit++)
                    {
                        _rankMask[MoveUtility.BoardIndex[rank][file]] |= MoveUtility.GetBitStatesByBoardIndex(rank, bit);
                        _fileMask[MoveUtility.BoardIndex[rank][file]] |= MoveUtility.GetBitStatesByBoardIndex(bit, file);
                    }

                    var diagonalA8H1 = file + rank; // from 2 to 16, longest diagonal = 9
                   
                    if (diagonalA8H1 < 10)
                    {
                        for (var square = 2; square < diagonalA8H1 - 1; square++)
                        {
                            _diagonalA8H1Mask[MoveUtility.BoardIndex[rank][file]] |= MoveUtility.GetBitStatesByBoardIndex(diagonalA8H1 - square, square);
                        }
                    }
                    else
                    {
                        for (var square = 2; square < 17 - diagonalA8H1; square++)
                        {
                            _diagonalA8H1Mask[MoveUtility.BoardIndex[rank][file]] |= MoveUtility.GetBitStatesByBoardIndex(9 - square, diagonalA8H1 + square - 9);
                        }
                    }
                   
                    var diagonalA1H8 = file - rank; // from -7 to +7, longest diagonal = 0

                    if (diagonalA1H8 > -1) // lower half, diagonals 0 to 7
                    {
                        for (var square = 2 ; square < 8 - diagonalA1H8 ; square ++)
                        {
                            _diagonalA1H8Mask[MoveUtility.BoardIndex[rank][file]] |= MoveUtility.GetBitStatesByBoardIndex(square, diagonalA1H8 + square);
                        }
                    }
                    else
                    {
                        for (var square = 2 ; square < 8 + diagonalA1H8 ; square ++)
                        {
                            _diagonalA1H8Mask[MoveUtility.BoardIndex[rank][file]] |= MoveUtility.GetBitStatesByBoardIndex(square - diagonalA1H8, square );
                        }
                    }                    
                }
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
                            
                        var slidingattackindex = 8 - MoveUtility.Ranks[square] < MoveUtility.Files[square] - 1 ? 8 - MoveUtility.Ranks[square] : MoveUtility.Files[square] - 1;
                        if ((SlidingAttacks[slidingattackindex][attackState] & ByteBitStates[attackbit]) == ByteBitStates[attackbit])
                        {
                            var diagonalLength = MoveUtility.Files[square] + MoveUtility.Ranks[square];
                            
                            int file;
                            int rank;

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
                                DiagonalA8H1Attacks[square][attackState] |=  MoveUtility.GetBitStatesByBoardIndex(rank, file);
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

                        var slidingattackindex = (MoveUtility.Ranks[square] - 1) < (MoveUtility.Files[square] - 1) ? (MoveUtility.Ranks[square] - 1) : (MoveUtility.Files[square] - 1);
                        if ((SlidingAttacks[slidingattackindex][attackState] & ByteBitStates[attackbit]) == ByteBitStates[attackbit])
                        {
                            var diagonalLength = MoveUtility.Files[square] - MoveUtility.Ranks[square];

                            int file;
                            int rank;

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
                                DiagonalA1H8Attacks[square][attackState] |= MoveUtility.GetBitStatesByBoardIndex(rank, file);
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
                        if ((SlidingAttacks[8 - MoveUtility.Ranks[square]][attackState] & ByteBitStates[attackbit]) == ByteBitStates[attackbit])
                        {
                            var file = MoveUtility.Files[square];
                            var rank = 8 - attackbit;
                            FileAttacks[square][attackState] |= MoveUtility.GetBitStatesByBoardIndex(rank, file);
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
                        attackMask |= ByteBitStates[position + 1];
                    }

                    var slide = position + 2;
                    while (slide <= 7)
                    {
                        if ((~stateMask & ByteBitStates[slide - 1]) == ByteBitStates[slide - 1])
                        {
                            attackMask |= ByteBitStates[slide];
                        }
                        else
                        {
                            break;
                        }
                        slide++;
                    }
                    if (position > 0)
                    {
                        attackMask |= ByteBitStates[position - 1];
                    }
                    slide = position - 2;
                    while (slide >= 0)
                    {
                        if ((~stateMask & ByteBitStates[slide + 1]) == ByteBitStates[slide + 1])
                        {
                            attackMask |= ByteBitStates[slide];
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
                    RankAttacks[square][attackState] = ((ulong)SlidingAttacks[MoveUtility.Files[square] - 1][attackState] << (MoveUtility.ShiftedRank[square] - 1));
                }
            }
        }

        private void GenerateBlackPawnMoves()
        {
            for (int i = 0; i < 64; i++)
            {
                var file = MoveUtility.Files[i];
                var rank = MoveUtility.Ranks[i];

                var targetRank = rank - 1;
                var targetFile = file;

                //SingleMove
                if (ValidLocation(targetFile, targetRank))
                    BlackPawnMoves[i] |= MoveUtility.GetBitStatesByBoardIndex(targetRank, targetFile);

                //Double Move
                if (rank == 7)
                {
                    targetRank = rank - 2;

                    if (ValidLocation(targetFile, targetRank))
                        BlackPawnDoubleMoves[i] |= MoveUtility.GetBitStatesByBoardIndex(targetRank, targetFile);
                }
            }
        }

        private void GenerateWhitePawnMoves()
        {
            for (int i = 0; i < 64; i++)
            {
                var file = MoveUtility.Files[i];
                var rank = MoveUtility.Ranks[i];

                var targetRank = rank + 1;
                var targetFile = file;

                //Single Move
                if (ValidLocation(targetFile, targetRank))
                    WhitePawnMoves[i] |= MoveUtility.GetBitStatesByBoardIndex(targetRank, targetFile);

                //Double Move
                if (rank == 2)
                {
                    targetRank = rank + 2;
                    
                    if (ValidLocation(targetFile, targetRank))
                        WhitePawnDoubleMoves[i] |= MoveUtility.GetBitStatesByBoardIndex(targetRank, targetFile);
                }
            }
        }

        private void GenerateBlackPawnAttacks()
        {

            for (int i = 0; i < 64; i++)
            {
                var file = MoveUtility.Files[i];
                var rank = MoveUtility.Ranks[i];

                //Attack Left
                var targetFile = file - 1;
                var targetRank = rank - 1;

                if (ValidLocation(targetFile, targetRank))
                    BlackPawnAttacks[i] |= MoveUtility.GetBitStatesByBoardIndex(targetRank, targetFile);

                //Attack Right
                targetFile = file + 1;

                if (ValidLocation(targetFile, targetRank))
                    BlackPawnAttacks[i] |= MoveUtility.GetBitStatesByBoardIndex(targetRank, targetFile);
            }
        }             

        private void GenerateWhitePawnAttacks()
        {
            for (int i = 0; i < 64; i++)
            {
                var file = MoveUtility.Files[i];
                var rank = MoveUtility.Ranks[i];

                //Attack Left
                var targetFile = file - 1;
                var targetRank = rank + 1;

                if (ValidLocation(targetFile, targetRank))
                    WhitePawnAttacks[i] |= MoveUtility.GetBitStatesByBoardIndex(targetRank, targetFile);

                //Attack Right
                targetFile = file + 1;

                if (ValidLocation(targetFile, targetRank))
                    WhitePawnAttacks[i] |= MoveUtility.GetBitStatesByBoardIndex(targetRank, targetFile);
            }
        }  

        private void GenerateKingAttacks()
        {
            for (int i = 0; i < 64; i++)
            {
                var file = MoveUtility.Files[i];
                var rank = MoveUtility.Ranks[i];

                //Down
                var targetRank = rank - 1;
                var targetFile = file;

                if (ValidLocation(targetFile, targetRank))
                    KingAttacks[i] |= MoveUtility.GetBitStatesByBoardIndex(targetRank, targetFile);

                //Up
                targetRank = rank + 1;
                targetFile = file;

                if (ValidLocation(targetFile, targetRank))
                    KingAttacks[i] |= MoveUtility.GetBitStatesByBoardIndex(targetRank, targetFile);

                //Left
                targetRank = rank;
                targetFile = file - 1;

                if (ValidLocation(targetFile, targetRank))
                    KingAttacks[i] |= MoveUtility.GetBitStatesByBoardIndex(targetRank, targetFile);

                //Right
                targetRank = rank;
                targetFile = file + 1;

                if (ValidLocation(targetFile, targetRank))
                    KingAttacks[i] |= MoveUtility.GetBitStatesByBoardIndex(targetRank, targetFile);

                //Down Left
                targetRank = rank - 1;
                targetFile = file - 1;

                if (ValidLocation(targetFile, targetRank))
                    KingAttacks[i] |= MoveUtility.GetBitStatesByBoardIndex(targetRank, targetFile);

                //Down Right
                targetRank = rank - 1;
                targetFile = file + 1;

                if (ValidLocation(targetFile, targetRank))
                    KingAttacks[i] |= MoveUtility.GetBitStatesByBoardIndex(targetRank, targetFile);

                //Up Left
                targetRank = rank + 1;
                targetFile = file - 1;

                if (ValidLocation(targetFile, targetRank))
                    KingAttacks[i] |= MoveUtility.GetBitStatesByBoardIndex(targetRank, targetFile);

                //Up Right
                targetRank = rank + 1;
                targetFile = file + 1;

                if (ValidLocation(targetFile, targetRank))
                    KingAttacks[i] |= MoveUtility.GetBitStatesByBoardIndex(targetRank, targetFile);
            }
        }

        private void GenerateKnightAttacks()
        {
            for (int i = 0; i < 64; i++)
            {
                var file = MoveUtility.Files[i];
                var rank = MoveUtility.Ranks[i];

                //Down 2 Right 1
                var targetRank = rank - 2;
                var targetFile = file + 1;

                if (ValidLocation(targetFile, targetRank))
                    KnightAttacks[i] |= MoveUtility.GetBitStatesByBoardIndex(targetRank, targetFile);

                //Down 2 Left 1
                targetFile = file - 1;

                if (ValidLocation(targetFile, targetRank))
                    KnightAttacks[i] |= MoveUtility.GetBitStatesByBoardIndex(targetRank, targetFile);

                //Down 1 Right 2
                targetRank = rank - 1;
                targetFile = file + 2;

                if (ValidLocation(targetFile, targetRank))
                    KnightAttacks[i] |= MoveUtility.GetBitStatesByBoardIndex(targetRank, targetFile);

                //Down 1 Left 2
                targetFile = file - 2;

                if (ValidLocation(targetFile, targetRank))
                    KnightAttacks[i] |= MoveUtility.GetBitStatesByBoardIndex(targetRank, targetFile);

                //Up 2 Right 1
                targetRank = rank + 2;
                targetFile = file + 1;

                if (ValidLocation(targetFile, targetRank))
                    KnightAttacks[i] |= MoveUtility.GetBitStatesByBoardIndex(targetRank, targetFile);

                //Up 2 Left 1
                targetFile = file - 1;

                if (ValidLocation(targetFile, targetRank))
                    KnightAttacks[i] |= MoveUtility.GetBitStatesByBoardIndex(targetRank, targetFile);

                //Up 1 Right 2
                targetRank = rank + 1;
                targetFile = file + 2;

                if (ValidLocation(targetFile, targetRank))
                    KnightAttacks[i] |= MoveUtility.GetBitStatesByBoardIndex(targetRank, targetFile);

                //Up 1 Left 2
                targetFile = file - 2;

                if (ValidLocation(targetFile, targetRank))
                    KnightAttacks[i] |= MoveUtility.GetBitStatesByBoardIndex(targetRank, targetFile);
               
            }
        }

        private bool ValidLocation(int file, int rank)
        {
            return file >= 1 && file <= 8 && rank >= 1 && rank <= 8;
        }
    
        private ulong GetRankMoves(uint fromSquare, ulong occupiedSquares, ulong targetboard)
        {
            return RankAttacks[fromSquare][(occupiedSquares & _rankMask[fromSquare]) >> MoveUtility.ShiftedRank[fromSquare]] & targetboard;
        }

        private ulong GetFileMoves(uint fromSquare, ulong occupiedSquares, ulong targetboard)
        {

            return FileAttacks[fromSquare][(occupiedSquares & _fileMask[fromSquare]) * MoveUtility.FileMagicMultiplication[fromSquare] >> 57] & targetboard;
        }

        private ulong GetDiagonalA8H1Moves(uint fromSquare, ulong occupiedSquares, ulong targetboard)
        {
            return DiagonalA8H1Attacks[fromSquare][(occupiedSquares & _diagonalA8H1Mask[fromSquare]) * MoveUtility.DiagonalA8H1MagicMultiplcation[fromSquare] >> 57] & targetboard;
        }

        private ulong GetDiagonalA1H8Moves(uint fromSquare, ulong occupiedSquares, ulong targetboard)
        {
            return DiagonalA1H8Attacks[fromSquare][(occupiedSquares & _diagonalA1H8Mask[fromSquare]) * MoveUtility.DiagonalA1H8MagicMultiplcation[fromSquare] >> 57] & targetboard;
        }
    }
}