using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetEngine.Engine
{
    public class BestMoveFoundEventArgs : EventArgs
    {
        public string BestMove { get; set; }
    }
}
