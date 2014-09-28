namespace DotNetEngine.Engine.Enums
{
    /// <summary>
    /// 0 - Cannot Castle 
    /// 1 - Can Castle 00
    /// 2 - Can Castle 000 
    /// 3 - Can Castle Both 00 and 000
    /// </summary>
    public enum CastleStatus
    {
        /// <summary>
        /// When this state is set, the side cannot castle anymore
        /// </summary>
        CannotCastle = 0,
        /// <summary>
        /// Only the O-O castle is available
        /// </summary>
        OOCastle = 1,
        /// <summary>
        /// Only the O-O-O castle is available
        /// </summary>
        OOOCastle = 2,
        /// <summary>
        /// Both the O-O-O and O-O castle is available
        /// </summary>
        BothCastle = 3
    }
}
