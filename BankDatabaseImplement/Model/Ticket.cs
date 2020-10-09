using ZooBussinessLogic.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text;

namespace ZooDatabaseImplement.Model
{
    public class Ticket
    {
        public int Id { get; set; }
        public int WorkerId { get; set; }
        [Required]
        public string TypeTicket { get; set; }
        public Status Status { get; set; }
        public int Cost { get; set; }
        public virtual Worker Worker { get; set; }
        public virtual List<TicketClient> TicketClients { get; set; }
    }
}
