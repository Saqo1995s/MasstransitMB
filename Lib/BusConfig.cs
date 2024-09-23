namespace Lib
{
    public class BusConfig
    {
        public MessageBrokerType MessageBrokerType { get; set; }
        public string Host { get; set; }
        public string Port { get; set; }
        public string ConnectionString { get; set; }
        public string VirtualHost { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool UseSSL { get; set; }
    }
}