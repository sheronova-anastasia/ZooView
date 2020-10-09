using ZooBussinessLogic.BindingModel;
using ZooBussinessLogic.Interfaces;
using ZooBussinessLogic.ViewModel;
using ZooDatabaseImplement.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZooDatabaseImplement.Implements
{
    public class ClientLogic : IClientLogic
    {
        public void CreateOrUpdate(ClientBindingModel model)
        {
            using (var context = new ZooDatabase())
            {
                Client element = context.Clients.FirstOrDefault(rec => rec.ClientFIO == model.ClientFIO && rec.Id == model.Id && rec.Job == model.Job && rec.Number == model.Number && rec.PassportData == model.PassportData && rec.Gender == model.Gender && rec.Score == model.Score && rec.DateCreate == model.DateCreate && rec.CountTicket == model.CountTicket && rec.Email == model.Email);
                if (element != null)
                {
                    throw new Exception("Уже есть такой клиент");
                }
                if (model.Id.HasValue)
                {
                    element = context.Clients.FirstOrDefault(rec => rec.Id == model.Id);
                    if (element == null)
                    {
                        throw new Exception("Элемент не найден");
                    }
                }
                else
                {
                    element = new Client();
                    context.Clients.Add(element);
                }
                element.PassportData = model.PassportData;
                element.ClientFIO = model.ClientFIO;
                element.Gender = model.Gender;
                element.Job = model.Job;
                element.Number = model.Number;
                element.Email = model.Email;
                element.CountTicket = model.CountTicket;
                element.Score = model.Score;
                element.DateCreate = DateTime.Now;
                context.SaveChanges();
                var groupTickets = model.TicketClients
                               .GroupBy(rec => rec.TicketId)
                               .Select(rec => new
                               {
                                   TicketId = rec.Key,
                               });

                foreach (var groupTicket in groupTickets)
                {
                    context.TicketClients.Add(new TicketClient
                    {
                        ClientId = element.Id,
                        TicketId = groupTicket.TicketId,
                    });
                    context.SaveChanges();
                }
            }
        }

        public void Delete(ClientBindingModel model)
        {
            using (var context = new ZooDatabase())
            {
                Client element = context.Clients.FirstOrDefault(rec => rec.Id == model.Id);
                if (element != null)
                {
                    context.Clients.Remove(element);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Элемент не найден");
                }
            }
        }
        public List<ClientViewModel> Read(ClientBindingModel model)
        {
            using (var context = new ZooDatabase())
            {
                return context.Clients
                .Where(rec => model == null || rec.Id == model.Id || model.DateFrom.HasValue && model.DateTo.HasValue && rec.DateCreate >= model.DateFrom && rec.DateCreate <= model.DateTo)
                .Select(rec => new ClientViewModel
                {
                    Id = rec.Id,
                    PassportData = rec.PassportData,
                    ClientFIO = rec.ClientFIO,
                    Gender = rec.Gender,
                    Job = rec.Job,
                    Number = rec.Number,
                    Email = rec.Email,
                    CountTicket = rec.CountTicket,
                    Score = rec.Score,
                    DateCreate = rec.DateCreate,
                    TicketClients = GetTicketClientViewModel(rec)
                })
                .ToList();
            }
        }
        public static List<TicketClientViewModel> GetTicketClientViewModel(Client client)
        {
            using (var context = new ZooDatabase())
            {
                var TicketClients = context.TicketClients
                    .Where(rec => rec.ClientId == client.Id)
                    .Include(rec => rec.Ticket)
                    .Select(rec => new TicketClientViewModel
                    {
                        Id = rec.Id,
                        ClientId = rec.ClientId,
                        TicketId = rec.TicketId,
                        Cost = rec.Cost
                    }).ToList();
                foreach (var tick in TicketClients)
                {
                    var tickData = context.Tickets.Where(rec => rec.Id == tick.TicketId).FirstOrDefault();
                    tick.TypeTicket = tickData.TypeTicket;
                    tick.Cost = tickData.Cost;
                }
                return TicketClients;
            }
        }
    }
}
