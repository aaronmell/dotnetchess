using System;
using System.Linq;
using System.Threading;
using Common.Logging;
using DotNetEngine.Engine;
using Moq;
using NUnit.Framework;

namespace DotNetEngine.Test
{
    public class RunnerTests
    {
        [Test]
        public void SetBoard_Logs_Error_When_Invalid_FEN()
        {
            var mockLogger = new Mock<ILog>();

            var runner = new Runner(mockLogger.Object);

            runner.ProcessCommand("setboard", new [] { "a" });

            mockLogger.Verify(x => x.Error(It.IsAny<string>()), Times.Exactly(1));
        }

        [Test]
        public void SetBoard_Logs_No_Error_When_Valid_FEN()
        {
            var mockLogger = new Mock<ILog>();

            var runner = new Runner(mockLogger.Object);

            runner.ProcessCommand("setboard", new[] { "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1" });

            mockLogger.Verify(x => x.Error(It.IsAny<string>()), Times.Never);
        }

        [Test]
        public void Perft_Logs_Error_When_Invalid_Depth()
        {
            var mockLogger = new Mock<ILog>();

            var runner = new Runner(mockLogger.Object);

            runner.ProcessCommand("perft", new[] { "a" });

            mockLogger.Verify(x => x.Error(It.IsAny<string>()), Times.Exactly(1));
        }

        [Test]
        public void Perft_Logs_No_Error_When_Valid_Depth()
        {
            var mockLogger = new Mock<ILog>();

            var runner = new Runner(mockLogger.Object);

            runner.ProcessCommand("perft", new[] { "1" });

            mockLogger.Verify(x => x.Error(It.IsAny<string>()), Times.Never);
        }

        [Test]
        public void Divide_Logs_Error_When_Invalid_Depth()
        {
            var mockLogger = new Mock<ILog>();

            var runner = new Runner(mockLogger.Object);

            runner.ProcessCommand("divide", new[] { "a" });

            mockLogger.Verify(x => x.Error(It.IsAny<string>()), Times.Exactly(1));
        }

        [Test]
        public void Divide_Logs_No_Error_When_Valid_Depth()
        {
            var mockLogger = new Mock<ILog>();

            var runner = new Runner(mockLogger.Object);

            runner.ProcessCommand("divide", new[] { "1" });

            mockLogger.Verify(x => x.Error(It.IsAny<string>()), Times.Never);
        }

        [Test]
        public void UCI_Returns_Correct_Parameters()
        {
            var mockLogger = new Mock<ILog>();

            var runner = new Runner(mockLogger.Object);

            runner.ProcessCommand("uci", null);

            mockLogger.Verify(x => x.Info("uciok"), Times.Exactly(1));
        }

        [Test]
        public void IsReady_Returns_Correct_Parameters()
        {
            var mockLogger = new Mock<ILog>();

            var runner = new Runner(mockLogger.Object);

            runner.ProcessCommand("isready", null);

            mockLogger.Verify(x => x.Info("readyok"), Times.Exactly(1));
        }

        [Test]
        public void No_Errors_When_Position_Command_Given_With_Fen_And_Single_Move()
        {
            var mockLogger = new Mock<ILog>();

            string lastMessage = string.Empty;
            mockLogger.Setup(x => x.InfoFormat(It.Is<string>(y => y.Contains("bestmove")), It.IsAny<object[]>())).Callback<string, object[]>((message, objects) => lastMessage = message);
            
            var runner = new Runner(mockLogger.Object);

            runner.ProcessCommand("ucinewgame", null);
            runner.ProcessCommand("position", new [] {"fen", "8/8/8/8/8/8/1Kpk4/8", "w", "-", "-" ,"0" , "1" ,"moves", "b2a1" });
            runner.ProcessCommand("go", new[] { "wtime", "200000", "btime", "200000", "winc", "0","binc","0","movestogo","40" });

            WaitOnMove(20, 1000, ref lastMessage);

            VerifyNoErrorsOutput(mockLogger);
        }

        [Test]
        public void No_Errors_When_Position_Command_Given_With_Fen_And_Two_Moves()
        {
            var mockLogger = new Mock<ILog>();

            string lastMessage = string.Empty;
            mockLogger.Setup(x => x.InfoFormat(It.Is<string>(y => y.Contains("bestmove")), It.IsAny<object[]>())).Callback<string, object[]>((message, objects) => lastMessage = message);

            var runner = new Runner(mockLogger.Object);

            runner.ProcessCommand("ucinewgame", null);
            runner.ProcessCommand("position", new[] { "fen", "8/8/8/8/8/8/1Kpk4/8", "w", "-", "-", "0", "1", "moves", "b2a1", "d2c3"});
            runner.ProcessCommand("go", new[] { "wtime", "200000", "btime", "200000", "winc", "0", "binc", "0", "movestogo", "40" });

            WaitOnMove(20, 1000, ref lastMessage);
           
            VerifyNoErrorsOutput(mockLogger);
        }

        [Test]
        public void No_Errors_When_Position_Command_Given_With_StartPosition_And_Single_Move()
        {
            var mockLogger = new Mock<ILog>();
            
            string lastMessage = string.Empty;
            mockLogger.Setup(x => x.InfoFormat(It.Is<string>(y => y.Contains("bestmove")), It.IsAny<object[]>())).Callback<string, object[]>((message, objects) => lastMessage = message);
            
            var runner = new Runner(mockLogger.Object);

            runner.ProcessCommand("ucinewgame", null);
            runner.ProcessCommand("position", new[] { "startpos", "moves", "e2e3" });
            runner.ProcessCommand("go", new[] { "wtime", "200000", "btime", "200000", "winc", "0", "binc", "0", "movestogo", "40" });

            WaitOnMove(20, 1000, ref lastMessage);

            VerifyNoErrorsOutput(mockLogger);
        }

        [Test]
        public void No_Errors_When_Position_Command_Given_With_StartPosition_And_Two_Moves()
        {
            var mockLogger = new Mock<ILog>();
            string lastMessage = string.Empty;
            
            mockLogger.Setup(x => x.InfoFormat(It.Is<string>(y => y.Contains("bestmove")), It.IsAny<object[]>())).Callback<string, object[]>((message, objects) => lastMessage = message);

            var runner = new Runner(mockLogger.Object);

            runner.ProcessCommand("ucinewgame", null);
            runner.ProcessCommand("position", new[] { "startpos", "moves", "e2e3", "g7g6" });
            runner.ProcessCommand("go", new[] { "wtime", "200000", "btime", "200000", "winc", "0", "binc", "0", "movestogo", "40" });

            WaitOnMove(20, 1000, ref lastMessage);

            VerifyNoErrorsOutput(mockLogger);
        }

        //Sanity Check. This ensures we didn't break something while working.
        [Test]
        public void Engine_Finds_Best_Moves_From_Position()
        {
            var mockLogger = new Mock<ILog>();
            string lastMessage = string.Empty;
            string bestMove = string.Empty;
            mockLogger.Setup(x => x.InfoFormat(It.Is<string>(y => y.Contains("bestmove")), It.IsAny<object[]>()))
                .Callback<string, object[]>(
                    (message, objects) =>
                    {
                        lastMessage = message;
                        bestMove = (string)objects.First();

                    });

            var runner = new Runner(mockLogger.Object);

            runner.ProcessCommand("ucinewgame", null);

            runner.ProcessCommand("position", new[] { "fen", "8/8/8/8/8/8/1Kpk4/8", "w", "-", "-", "8", "4", "moves", "b2a2" });
            runner.ProcessCommand("go", new[] { "wtime", "200000", "btime", "200000", "winc", "0", "binc", "0", "movestogo", "40" });

            WaitOnMove(20, 1000, ref lastMessage);

            VerifyNoErrorsOutput(mockLogger);
            Assert.That(bestMove, Is.EqualTo("d2c3"), "Failed on First Move");

            lastMessage = string.Empty;
            bestMove = string.Empty;

            runner.ProcessCommand("position", new[] { "fen", "8/8/8/8/8/8/1Kpk4/8", "w", "-", "-", "8", "4", "moves", "b2a2", "d2c3", "a2a3" });
            runner.ProcessCommand("go", new[] { "wtime", "200000", "btime", "200000", "winc", "0", "binc", "0", "movestogo", "40" });

           
            WaitOnMove(20, 1000, ref lastMessage);

            VerifyNoErrorsOutput(mockLogger);
            Assert.That(bestMove, Is.EqualTo("c2c1q"), "Failed On Second Move");

            lastMessage = string.Empty;
            bestMove = string.Empty;

            runner.ProcessCommand("position", new[] { "fen", "8/8/8/8/8/8/1Kpk4/8", "w", "-", "-", "8", "4", "moves", "b2a2", "d2c3", "a2a3", "c2c1q", "a3a4" });
            runner.ProcessCommand("go", new[] { "wtime", "200000", "btime", "200000", "winc", "0", "binc", "0", "movestogo", "40" });


            WaitOnMove(20, 1000, ref lastMessage);

            VerifyNoErrorsOutput(mockLogger);
            Assert.That(bestMove, Is.EqualTo("c1g5"), "Failed On Third Move");

            runner.ProcessCommand("position", new[] { "fen", "8/8/8/8/8/8/1Kpk4/8", "w", "-", "-", "8", "4", "moves", "b2a2", "d2c3", "a2a3", "c2c1q", "a3a4", "c1g5", "a4a3" });
            runner.ProcessCommand("go", new[] { "wtime", "200000", "btime", "200000", "winc", "0", "binc", "0", "movestogo", "40" });


            WaitOnMove(20, 1000, ref lastMessage);

            VerifyNoErrorsOutput(mockLogger);
            Assert.That(bestMove, Is.EqualTo("g5a5"), "Failed On Mate");
        }

        private void WaitOnMove(int cycles, int threadWaitTime, ref string lastMessage)
        {
            while (cycles > 0)
            {
                if (lastMessage.Contains("bestmove"))
                    break;

                Thread.Sleep(threadWaitTime);
                cycles--;
            }

            if (cycles == 0)
                Assert.Fail("Took more than 20 seconds to return bestmove");
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
