using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ZooBussinessLogic.ViewModel
{
    public class ServiceClientViewModel
    {
        public int Id { get; set; }
        public int ServiceId { get; set; }
        public int ClientId { get; set; }
        public string TypeService { get; set; }
        public int Cost { get; set; }
    }
}
