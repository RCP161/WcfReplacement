namespace Prototype.Publisher.Core.Enums
{
    internal enum Scenario
    {
        /// <summary>
        /// Ends the server
        /// </summary>
        Stop,
        /// <summary>
        /// Request, 1s, 64 kb
        /// </summary>
        PresentStandard,
        /// <summary>
        /// 0.5s, 5mb
        /// </summary>
        RequestPerformance,
        /// <summary>
        /// Xml/Byte/Proto, 1s, 5mb
        /// </summary>
        SerialisationPerformance
    }
}
