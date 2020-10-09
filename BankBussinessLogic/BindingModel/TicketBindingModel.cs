using ZooBussinessLogic.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ZooBussinessLogic.BindingModel
{
    public class TicketBindingModel
    {
        public int? Id { get; set; }
        public string TypeService { get; set; }
        public int WorkerId { get; set; }
        public Status Status { get; set; }
        public int Cost { get; set; }
        public List<TicketClientBindingModel> TicketClients { get; set; }
    }
}
