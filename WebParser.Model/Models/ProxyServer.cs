using System;

namespace WebParser.Model.Models
{
    public class ProxyServer
    {
        public string Ip { get; set; }
        public string Port { get; set; }
        public bool Https { get; set; }
        public DateTime LastUsed { get; set; }

        public string Url {
            get
            {
                return $"{(Https ? "https" : "http")}://{Ip}:{Port}";
            }
        }
    }
}
