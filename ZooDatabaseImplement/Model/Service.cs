using ZooBussinessLogic.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text;

namespace ZooDatabaseImplement.Model
{
    public class Service
    {
        public int Id { get; set; }
        public int WorkerId { get; set; }
        [Required]
        public string TypeService { get; set; }
        public Status Status { get; set; }
        public int Cost { get; set; }
        public virtual Worker Worker { get; set; }
        public virtual List<ServiceClient> ServiseClients { get; set; }
    }
}
