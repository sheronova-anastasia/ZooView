using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ZooDatabaseImplement.Model
{
    public class TicketClient
    {
        public int Id { get; set; }
        public int TicketId { get; set; }
        public int ClientId { get; set; }
        public int Cost { get; set; }
        public virtual Ticket Ticket { get; set; }
        public virtual Client Client { get; set; }
    }
}
