﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ZooBussinessLogic.BindingModel
{
    public class TicketClientBindingModel
    {
        public int Id { get; set; }
        public int TicketId { get; set; }
        public int? ClientId { get; set; }
        public string TypeTicket { get; set; }
        public int Cost { get; set; }
    }
}
