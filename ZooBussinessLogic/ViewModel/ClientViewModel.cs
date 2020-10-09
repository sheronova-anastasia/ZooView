using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ZooBussinessLogic.ViewModel
{
    public class ClientViewModel
    {
        public int Id { get; set; }
        [DisplayName("ФИО")]
        public string ClientFIO { get; set; }
        [DisplayName("Пол")]
        public string Gender { get; set; }
        [DisplayName("Номер телефона")]
        public int Number { get; set; }
        [DisplayName("Email")]
        public string Email { get; set; }
        [DisplayName("Количество билетов")]
        public int CountTicket { get; set; }
        [DisplayName("Счет")]
        public int Score { get; set; }
        [DisplayName("Дата обращения")]
        public DateTime DateCreate { get; set; }
        public virtual List<TicketClientViewModel> TicketClients { get; set; }
    }
}
