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
    public class ServiceLogic : IServiceLogic
    {
        private readonly string ServiceFileName = "C://Users//marin.LAPTOP-0TUFHPTU//source//repos//Kursach_Bank//BankView//data//Service.xml";
        public List<Service> Services { get; set; }
        public ServiceLogic()
        {
            Services = LoadServices();
        }
        private List<Service> LoadServices()
        {
            var list = new List<Service>();
            if (File.Exists(ServiceFileName))
            {
                XDocument xDocument = XDocument.Load(ServiceFileName);
                var xElements = xDocument.Root.Elements("Service").ToList();
                foreach (var elem in xElements)
                {
                    list.Add(new Service
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        WorkerId = Convert.ToInt32(elem.Element("WorkerId").Value),
                        TypeService = elem.Element("TypeService").Value,
                        Status = (Status)Enum.Parse(typeof(Status), elem.Element("Status").Value),
                        Cost = Convert.ToInt32(elem.Element("Cost").Value),
                    });
                }
            }
            return list;
        }

        public void SaveToDatabase()
        {
            var services = LoadServices();
            using (var context = new ZooDatabase())
            {
                foreach (var service in services)
                {
                    Service element = context.Services.FirstOrDefault(rec => rec.Id == service.Id);
                    if (element != null)
                    {
                        break;
                    }
                    else
                    {
                        element = new Service();
                        context.Services.Add(element);
                    }
                    element.WorkerId = service.WorkerId;
                    element.TypeService = service.TypeService;
                    element.Status = service.Status;
                    element.Cost = service.Cost;
                    context.SaveChanges();
                }
            }
        }
        public List<ServiceViewModel> Read(ServiceBindingModel model)
        {
            using (var context = new ZooDatabase())
            {
                return context.Services
               .Where(rec => model == null || rec.Id == model.Id)
                .Select(rec => new ServiceViewModel
                {
                    Id = rec.Id,
                    WorkerId = rec.WorkerId,
                    TypeService = rec.TypeService,
                    Status = rec.Status,
                    Cost = rec.Cost,
                    WorkerFIO = rec.Worker.WorkerFIO
                })
                .ToList();
            }
        }
    }
}
