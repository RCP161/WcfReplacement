namespace Prototype
{
    internal class ServerConfig : IServerConfig
    {
        public string IpAdress { get; internal set; }
        public int PortNumber { get; internal set; }

        public override string ToString()
        {
            return $"{IpAdress}:{PortNumber}";
        }
    }
}
