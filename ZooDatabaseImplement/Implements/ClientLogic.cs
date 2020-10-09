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
                Client element = context.Clients.FirstOrDefault(rec => rec.ClientFIO == model.ClientFIO && rec.Id == model.Id && rec.Job == model.Job && rec.Number == model.Number && rec.PassportData == model.PassportData && rec.Gender == model.Gender && rec.Score == model.Score && rec.DateCreate == model.DateCreate && rec.CountService == model.CountService && rec.Email == model.Email);
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
                element.CountService = model.CountService;
                element.Score = model.Score;
                element.DateCreate = DateTime.Now;
                context.SaveChanges();
                var groupServices = model.ServiceClients
                               .GroupBy(rec => rec.ServiceId)
                               .Select(rec => new
                               {
                                   ServiceId = rec.Key,
                               });

                foreach (var groupService in groupServices)
                {
                    context.ServiceClients.Add(new ServiceClient
                    {
                        ClientId = element.Id,
                        ServiceId = groupService.ServiceId,
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
                    CountService = rec.CountService,
                    Score = rec.Score,
                    DateCreate = rec.DateCreate,
                    ServiceClients = GetServiceClientViewModel(rec)
                })
                .ToList();
            }
        }
        public static List<TicketClientViewModel> GetServiceClientViewModel(Client client)
        {
            using (var context = new ZooDatabase())
            {
                var ServiceClients = context.ServiceClients
                    .Where(rec => rec.ClientId == client.Id)
                    .Include(rec => rec.Service)
                    .Select(rec => new TicketClientViewModel
                    {
                        Id = rec.Id,
                        ClientId = rec.ClientId,
                        ServiceId = rec.ServiceId,
                        Cost = rec.Cost
                    }).ToList();
                foreach (var serv in ServiceClients)
                {
                    var servData = context.Services.Where(rec => rec.Id == serv.ServiceId).FirstOrDefault();
                    serv.TypeService = servData.TypeService;
                    serv.Cost = servData.Cost;
                }
                return ServiceClients;
            }
        }
    }
}
