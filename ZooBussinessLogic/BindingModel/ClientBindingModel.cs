using System;
using System.Collections.Generic;
using System.Text;

namespace ZooBussinessLogic.BindingModel
{
    public class ClientBindingModel
    {
        public int? Id { get; set; }
        public string ClientFIO { get; set; }
        public string Gender { get; set; }
        public int Number { get; set; }
        public string Email { get; set; }
        public int CountTicket { get; set; }
        public int Score { get; set; }
        public DateTime? DateCreate { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public virtual List<TicketClientBindingModel> TicketClients { get; set; }
    }
}
