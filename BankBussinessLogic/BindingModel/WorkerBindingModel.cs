using System;
using System.Collections.Generic;
using System.Text;

namespace ZooBussinessLogic.BindingModel
{
    public class WorkerBindingModel
    {
        public int? Id { get; set; }
        public string WorkerFIO { get; set; }
        public int Salary { get; set; }
        public string Email { get; set; }
    }
}
