﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ZooBussinessLogic.ViewModel
{
    public class TicketClientViewModel
    {
        public int Id { get; set; }
        public int TicketId { get; set; }
        public int ClientId { get; set; }
        public string TypeTicket { get; set; }
        public int Cost { get; set; }
    }
}
