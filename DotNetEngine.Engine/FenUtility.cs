using System;
using System.Globalization;
using System.IO;
using System.Linq;

namespace DotNetEngine.Engine
{
	/// <summary>
	/// Utility class helpers allow loading a FEN (Forsyth-Edwards Notation) <see cref="http://kirill-kryukov.com/chess/doc/fen.html"/>
	/// </summary>
	internal static class FenUtility
	{
		private static readonly char[] Files = new []
		{
			'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h'
		};

		/// <summary>
		/// Take a FEN string and converts it into a GameState object
		/// </summary>
		/// <param name="fen"></param>
		/// <returns></returns>
		internal static GameState LoadStateFromFen(string fen)
		{
			fen = fen.Trim(' ');

			var splitFen = fen.Split(' ');

			if (splitFen.Count() != 6)
				throw new InvalidDataException(string.Format("The number of fields in the FEN string were incorrect. Expected 6, Received {0}", splitFen.Count()));

			var ranks = splitFen[0].Split('/');

			if (ranks.Count() != 8)
				throw new InvalidDataException(string.Format("The number of rows in the FEN string were incorrect. Expected 8, Received {0}", splitFen.Count()));

			var castleStatus = splitFen[2];
			var whiteCastleStatus = 0;
			var blackCastleStatus = 0;
			
			if (castleStatus.Contains("Q"))
				whiteCastleStatus += 1;
			if (castleStatus.Contains("K"))
				whiteCastleStatus += 2;

			if (castleStatus.Contains("q"))
				blackCastleStatus += 1;
			if (castleStatus.Contains("k"))
				blackCastleStatus += 2;

			var gameState = new GameState
			{
				FiftyMoveRuleCount = int.Parse(splitFen[4]),
				TotalMoveCount = int.Parse((splitFen[5])),
				WhiteToMove = splitFen[1].ToLower() == "w",
				CurrentWhiteCastleStatus = whiteCastleStatus,
				CurrentBlackCastleStatus = blackCastleStatus,
				EnpassantTargetSquare = GetEnPassantSquare(splitFen[3])
			};

			var rankNumber = 7;
			foreach (var rank in ranks)
			{
				var file = 0;
				foreach (var c in rank)
				{
					//Rank is empty
					if (c == '8')
						break;

					var boardArrayPosition = (rankNumber * 8 + file);
					var currentPosition = MoveUtility.BitStates[boardArrayPosition];

					switch (c)
					{
						case 'p':
						{
							gameState.BoardArray[boardArrayPosition] = MoveUtility.BlackPawn;
							gameState.BlackPawns += currentPosition;
							break;
						}
						case 'k':
						{
							gameState.BoardArray[boardArrayPosition] = MoveUtility.BlackKing;
							gameState.BlackKing += currentPosition;
							break;
						}
						case 'q':
						{
							gameState.BoardArray[boardArrayPosition] = MoveUtility.BlackQueen;
							gameState.BlackQueen += currentPosition;
							break;
						}
						case 'r':
						{
							gameState.BoardArray[boardArrayPosition] = MoveUtility.BlackRook;
							gameState.BlackRooks += currentPosition;
							break;
						}
						case 'n':
						{
							gameState.BoardArray[boardArrayPosition] = MoveUtility.BlackKnight;
							gameState.BlackKnights += currentPosition;
							break;
						}
						case 'b':
						{
							gameState.BoardArray[boardArrayPosition] = MoveUtility.BlackBishop;
							gameState.BlackBishops += currentPosition;
							break;
						}
						
						case 'P':
						{
							gameState.BoardArray[boardArrayPosition] = MoveUtility.WhitePawn;
							gameState.WhitePawns += currentPosition;
							break;
						}
						case 'K':
						{
							gameState.BoardArray[boardArrayPosition] = MoveUtility.WhiteKing;
							gameState.WhiteKing += currentPosition;
							break;
						}
						case 'Q':
						{
							gameState.BoardArray[boardArrayPosition] = MoveUtility.WhiteQueen;
							gameState.WhiteQueen += currentPosition;
							break;
						}
						case 'R':
						{
							gameState.BoardArray[boardArrayPosition] = MoveUtility.WhiteRook;
							gameState.WhiteRooks += currentPosition;
							break;
						}
						case 'N':
						{
							gameState.BoardArray[boardArrayPosition] = MoveUtility.WhiteKnight;
							gameState.WhiteKnights += currentPosition;
							break;
						}
						case 'B':
						{
							gameState.BoardArray[boardArrayPosition] = MoveUtility.BlackBishop;
							gameState.WhiteBishops += currentPosition;
							break;
						}
						default:
						{
							file += int.Parse(c.ToString(CultureInfo.InvariantCulture)) - 1;
							break;
						}
					}
					file++;
				}
				rankNumber--;
			}
			return gameState;
		}

		private static int GetEnPassantSquare(string enpassant)
		{
			if (enpassant == "-")
				return 0;

			var file =  char.Parse(enpassant.Substring(0, 1));
			var rank = int.Parse(enpassant.Substring(1, 1)) - 1;

			var fileNumber = Array.IndexOf(Files, file);

			return (rank * 8) + fileNumber;
		}
	}
}
