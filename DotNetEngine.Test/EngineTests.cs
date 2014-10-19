using System;
using Common.Logging;
using Moq;
using NUnit.Framework;

namespace DotNetEngine.Test
{
    public class EngineTests
    {
        [TestCase("c2c1q")]
        [TestCase("c2c1r")]
        [TestCase("c2c1n")]
        [TestCase("c2c1b")]
        public void TryMakeMove_Finds_Promotion_Move(string promotionMove)
        {
            var mockLogger = new Mock<ILog>();
            var engine = new Engine.Engine(mockLogger.Object);

            engine.SetBoard("8/8/8/8/8/K1k5/2p5/8 b - - 11 5");
            engine.TryMakeMove(promotionMove);

            VerifyNoErrorsOutput(mockLogger);
        }

        [TestCase("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/R3K2R w KQkq - 0 1", "e1g1")]
        [TestCase("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/R3K2R w KQkq - 0 1", "e1c1")]
        [TestCase("r3k2r/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR b KQkq - 0 1 ", "e8g8")]
        [TestCase("r3k2r/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR b KQkq - 0 1 ", "e8c8")]
        public void TryMakeMove_Finds_Castle_Move(string fen, string castleMove)
        {
            var mockLogger = new Mock<ILog>();
            var engine = new Engine.Engine(mockLogger.Object);

            engine.SetBoard(fen);
            engine.TryMakeMove(castleMove);

            VerifyNoErrorsOutput(mockLogger);
        }
        [TestCase("rnbqkbnr/1ppppppp/8/pP6/8/8/P1PPPPPP/RNBQKBNR w - a6 0 2", "b5a6")]
        [TestCase("rnbqkbnr/p1pppppp/8/8/Pp6/8/1PPPPPPP/RNBQKBNR b KQkq a3 0 1", "b4a3")]
        public void TryMakeMove_Finds_EnPassant_Move(string fen, string enpassantMove)
        {
            var mockLogger = new Mock<ILog>();
            var engine = new Engine.Engine(mockLogger.Object);

            engine.SetBoard(fen);
            engine.TryMakeMove(enpassantMove);

            VerifyNoErrorsOutput(mockLogger);
        }

         private void VerifyNoErrorsOutput(Mock<ILog> mockLogger)
        {
            mockLogger.Verify(x => x.Error(It.IsAny<object>()), Times.Exactly(0));
            mockLogger.Verify(x => x.Error(It.IsAny<object>(), It.IsAny<Exception>()), Times.Exactly(0));
            mockLogger.Verify(x => x.Error(It.IsAny<Action<FormatMessageHandler>>()), Times.Exactly(0));
            mockLogger.Verify(x => x.Error(It.IsAny<IFormatProvider>(), It.IsAny<Action<FormatMessageHandler>>()), Times.Exactly(0));
            mockLogger.Verify(x => x.Error(It.IsAny<IFormatProvider>(), It.IsAny<Action<FormatMessageHandler>>(), It.IsAny<Exception>()), Times.Exactly(0));
            mockLogger.Verify(x => x.Error(It.IsAny<object>()), Times.Exactly(0));
            mockLogger.Verify(x => x.ErrorFormat(It.IsAny<string>(), It.IsAny<object[]>()), Times.Exactly(0));
            mockLogger.Verify(x => x.ErrorFormat(It.IsAny<string>(), It.IsAny<Exception>(), It.IsAny<object[]>()), Times.Exactly(0));
            mockLogger.Verify(x => x.ErrorFormat(It.IsAny<IFormatProvider>(), It.IsAny<string>(), It.IsAny<object[]>()), Times.Exactly(0));
            mockLogger.Verify(x => x.ErrorFormat(It.IsAny<IFormatProvider>(), It.IsAny<string>(), It.IsAny<Exception>(), It.IsAny<object[]>()), Times.Exactly(0));
        }
    
     }
}
