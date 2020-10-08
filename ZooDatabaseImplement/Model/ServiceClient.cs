using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ZooDatabaseImplement.Model
{
    public class ServiceClient
    {
        public int Id { get; set; }
        public int ServiceId { get; set; }
        public int ClientId { get; set; }
        public int Cost { get; set; }
        public virtual Service Service { get; set; }
        public virtual Client Client { get; set; }
    }
}
