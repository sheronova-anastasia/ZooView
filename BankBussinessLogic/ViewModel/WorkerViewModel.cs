using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text;

namespace ZooBussinessLogic.ViewModel
{
    [DataContract]
    public class WorkerViewModel
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        [DisplayName("ФИО")]
        public string WorkerFIO { get; set; }
        [DataMember]
        [DisplayName("Зарплата")]
        public int Salary { get; set; }
        public string Email { get; set; }
    }
}
