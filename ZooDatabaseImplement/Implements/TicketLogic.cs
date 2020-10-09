using ZooBussinessLogic.BindingModel;
using ZooBussinessLogic.Enums;
using ZooBussinessLogic.Interfaces;
using ZooBussinessLogic.ViewModel;
using ZooDatabaseImplement.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace ZooDatabaseImplement.Implements
{
    public class TicketLogic : ITicketLogic
    {
        private readonly string TicketFileName = "C://Users//Настя//source//repos//ZooView//data//Ticket.xml";
        public List<Ticket> Tickets { get; set; }
        public TicketLogic()
        {
            Tickets = LoadTickets();
        }
        private List<Ticket> LoadTickets()
        {
            var list = new List<Ticket>();
            if (File.Exists(TicketFileName))
            {
                XDocument xDocument = XDocument.Load(TicketFileName);
                var xElements = xDocument.Root.Elements("Ticket").ToList();
                foreach (var elem in xElements)
                {
                    list.Add(new Ticket
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        WorkerId = Convert.ToInt32(elem.Element("WorkerId").Value),
                        TypeTicket = elem.Element("TypeTicket").Value,
                        Status = (Status)Enum.Parse(typeof(Status), elem.Element("Status").Value),
                        Cost = Convert.ToInt32(elem.Element("Cost").Value),
                    });
                }
            }
            return list;
        }

        public void SaveToDatabase()
        {
            var tickets = LoadTickets();
            using (var context = new ZooDatabase())
            {
                foreach (var ticket in tickets)
                {
                    Ticket element = context.Tickets.FirstOrDefault(rec => rec.Id == ticket.Id);
                    if (element != null)
                    {
                        break;
                    }
                    else
                    {
                        element = new Ticket();
                        context.Tickets.Add(element);
                    }
                    element.WorkerId = ticket.WorkerId;
                    element.TypeTicket = ticket.TypeTicket;
                    element.Status = ticket.Status;
                    element.Cost = ticket.Cost;
                    context.SaveChanges();
                }
            }
        }
        public List<TicketViewModel> Read(TicketBindingModel model)
        {
            using (var context = new ZooDatabase())
            {
                return context.Tickets
               .Where(rec => model == null || rec.Id == model.Id)
                .Select(rec => new TicketViewModel
                {
                    Id = rec.Id,
                    WorkerId = rec.WorkerId,
                    TypeTicket = rec.TypeTicket,
                    Status = rec.Status,
                    Cost = rec.Cost,
                    WorkerFIO = rec.Worker.WorkerFIO
                })
                .ToList();
            }
        }
    }
}
