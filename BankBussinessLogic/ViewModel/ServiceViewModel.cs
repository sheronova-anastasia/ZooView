using ZooBussinessLogic.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text;

namespace ZooBussinessLogic.ViewModel
{
    [DataContract]
    public class ServiceViewModel
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int WorkerId { get; set; }
        [DataMember]
        [DisplayName("ФИО сотрудника")]
        public string WorkerFIO { get; set; }
        [DataMember]
        [DisplayName("Вид услуги")]
        public string TypeService { get; set; }
        [DataMember]
        [DisplayName("Статус")]
        public Status Status { get; set; }
        [DataMember]
        [DisplayName("Стоимость услуги")]
        public int Cost { get; set; }
        public List<ServiceClientViewModel> ServiceClients { get; set; }
    }
}
