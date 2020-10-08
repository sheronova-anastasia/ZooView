using System;
using System.Collections.Generic;
using System.Text;

namespace ZooBussinessLogic.BindingModel
{
    public class ServiceClientBindingModel
    {
        public int Id { get; set; }
        public int ServiceId { get; set; }
        public int? ClientId { get; set; }
        public string TypeService { get; set; }
        public int Cost { get; set; }
    }
}
