using DotNetEngine.Engine;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetEngine.Test
{
    /// <summary>
    /// These are the Perft tests that validate the move path enumeration. Caution: They take awhile to run all of them.
    /// </summary>
    public class PerftTests
    {
        private MoveData _moveData = new MoveData();

        private long numberOfCaptures = 0;
        private long numberOfEnpassants = 0;
        private long numberOfPromotions = 0;
        private long numberOfOOCastles = 0;
        private long numberOfOOOCastles = 0;
        private long numberOfChecks = 0;
        private long checkmates = 0;

        private string DivideOutput = "Move Nodes" + Environment.NewLine;

        [TestCase("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1", 0, 1)]
        [TestCase("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1", 1, 20)]
        [TestCase("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1", 2, 400)]
        [TestCase("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1", 3, 8902)]
        [TestCase("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1", 4, 197281)]
        [TestCase("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1", 5, 4865609)]
        //[TestCase("rnbqkbnr/ppppp2p/8/5PpQ/8/8/PPPP1PPP/RNB1KBNR b KQkq - 0 3", 1, 1)]
        public void RunPerft(string fen, int depth, long moveCount)
        {
            var gameState = GameStateUtility.LoadGameStateFromFen(fen);

            var count = RunPerftRecursively(gameState, 1, depth);
            Assert.That(count, Is.EqualTo(moveCount));
        }

        [TestCase("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1", 5)]
        //[TestCase("rnbqkbnr/pppppppp/8/8/8/2N5/PPPPPPPP/R1BQKBNR b KQkq - 1 1", 4)]
        //[TestCase("rnbqkbnr/p1pppppp/8/1p6/8/N7/PPPPPPPP/R1BQKBNR w KQkq b6 0 2", 4)] //B7B5 Divide
        //[TestCase("rnbqkbnr/p1pppppp/8/1p6/2N5/8/PPPPPPPP/R1BQKBNR b KQkq - 1 2", 5)] //A3C4 Divide
        //[TestCase("rnbqkbnr/p1pppppp/8/8/2p5/8/PPPPPPPP/R1BQKBNR w KQkq - 0 3", 5)]
        public void RunDivide(string fen, int depth)
        {
            var gameState = GameStateUtility.LoadGameStateFromFen(fen);

            var result = CalculateDivide(gameState, 1, depth);            
        }

        private ulong RunPerftRecursively(GameState gameState, int ply, int depth)
        {
            if (depth == 0)
            {
                return 1;
            }

            ulong count = 0;

            gameState.GenerateMoves(MoveGenerationMode.All, ply, _moveData);

            foreach (var move in gameState.Moves[ply])
            {
                gameState.MakeMove(move);

                if (!gameState.IsOppositeSideKingAttacked(_moveData))
                {
                    count += RunPerftRecursively(gameState, ply + 1, depth - 1);
                    
                    if (depth == 1)
                    {
                        if (move.IsPieceCaptured())
                            numberOfCaptures++;
                        if (move.IsEnPassant())
                            numberOfEnpassants++;
                        if (move.IsPromotion())
                            numberOfPromotions++;
                        if (move.IsCastleOO())
                            numberOfOOCastles++;
                        if (move.IsCastleOOO())
                            numberOfOOOCastles++;
                        if (gameState.IsCurrentSideKingAttacked(_moveData))
                            numberOfChecks++;
                    }
                   
                }                 

                gameState.UnMakeMove(move);
            }
            return count;
        }


        private string CalculateDivide(GameState gameState, int ply, int depth)
        {
            var sb = new StringBuilder(DivideOutput);
            ulong count = 0;

            gameState.GenerateMoves(MoveGenerationMode.All, ply, _moveData);

            foreach (var move in gameState.Moves[ply])
            {
                gameState.MakeMove(move);

                if (!gameState.IsOppositeSideKingAttacked(_moveData))
                {
                    ulong moveCount = RunPerftRecursively(gameState, ply + 1, depth - 1);
                    sb.AppendFormat("{0}{1} {2}{3}", MoveUtility.RankAndFile[move.GetFromMove()], MoveUtility.RankAndFile[move.GetToMove()], moveCount, Environment.NewLine);
                    count += moveCount;                   
                }
                  

                gameState.UnMakeMove(move);
            }
            sb.AppendFormat("Total Nodes: {0}", count);
            return sb.ToString();
            
        }
    }
}
