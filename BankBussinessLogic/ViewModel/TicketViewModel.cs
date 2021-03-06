﻿using ZooBussinessLogic.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text;

namespace ZooBussinessLogic.ViewModel
{
    [DataContract]
    public class TicketViewModel
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int WorkerId { get; set; }
        [DataMember]
        [DisplayName("ФИО сотрудника")]
        public string WorkerFIO { get; set; }
        [DataMember]
        [DisplayName("Тип билета")]
        public string TypeTicket { get; set; }
        [DataMember]
        [DisplayName("Статус")]
        public Status Status { get; set; }
        [DataMember]
        [DisplayName("Стоимость билета")]
        public int Cost { get; set; }
        public List<TicketClientViewModel> TicketClients { get; set; }
    }
}
