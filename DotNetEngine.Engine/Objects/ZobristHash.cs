using System;
using System.Security.Cryptography;

namespace DotNetEngine.Engine.Objects
{
    /// <summary>
    /// Used to Create the ZobristHash Values
    /// </summary>
    internal class ZobristHash
    {
        /// <summary>
        /// Used to hash pieces, the first dimension is the board square and the second dimension is the piece type
        /// </summary>
        internal ulong[][] PieceArray { get; private set; }
        /// <summary>
        /// A hash used to indicate the enpassant square
        /// </summary>
        internal ulong[] EnPassantSquares { get; private set; }
        /// <summary>
        /// A hash used to indicate white to move.
        /// </summary>
        internal ulong WhiteToMove { get; private set; }
        /// <summary>
        /// A hash used to indicate white can castle O-O
        /// </summary>
        internal ulong WhiteCanCastleOO { get; private set; }
        /// <summary>
        /// A hash used to indicate white can castle O-O-O
        /// </summary>
        internal ulong WhiteCanCastleOOO { get; private set; }
        /// <summary>
        /// A hash used to indicate black can castle O-O
        /// </summary>
        internal ulong BlackCanCastleOO { get; private set; }
        /// <summary>
        /// A hash used to indicate black can castle O-O-O
        /// </summary>
        internal ulong BlackCanCastleOOO { get; private set; }

        /// <summary>
        /// Constructor Generates the required hashes
        /// </summary>
        internal ZobristHash()
        {
            PieceArray = new ulong[64][];
            EnPassantSquares = new ulong[64];

            for (var i = 0; i < 64; i++)
            {
                EnPassantSquares[i] = NextInt64();
                PieceArray[i] = new ulong[16];

                for (var j = 0; j < 16; j++)
                {
                    PieceArray[i][j] = NextInt64();
                }
            }

            WhiteToMove = NextInt64();
            WhiteCanCastleOO = NextInt64();
            WhiteCanCastleOOO = NextInt64();
            BlackCanCastleOO = NextInt64();
            BlackCanCastleOOO = NextInt64();
        }

        private static UInt64 NextInt64()
        {
            var bytes = new byte[sizeof(Int64)];
            var rng = new RNGCryptoServiceProvider();
            rng.GetBytes(bytes);
            return BitConverter.ToUInt64(bytes, 0);
        }


    }
}
