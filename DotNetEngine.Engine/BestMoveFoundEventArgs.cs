using System;

namespace DotNetEngine.Engine
{
    public class BestMoveFoundEventArgs : EventArgs
    {
        public string BestMove { get; set; }
    }
}
