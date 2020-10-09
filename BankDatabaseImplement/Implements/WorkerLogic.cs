using ZooBussinessLogic.BindingModel;
using ZooBussinessLogic.Interfaces;
using ZooBussinessLogic.ViewModel;
using ZooDatabaseImplement.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace ZooDatabaseImplement.Implements
{
    public class WorkerLogic : IWorkerLogic
    {
        private readonly string WorkerFileName = "C://Users//Настя//source//repos//ZooView//data//Worker.xml";
        public List<Worker> Workers { get; set; }
        public WorkerLogic()
        {
            Workers = LoadWorkers();
        }
        private List<Worker> LoadWorkers()
        {
            var list = new List<Worker>();
            if (File.Exists(WorkerFileName))
            {
                XDocument xDocument = XDocument.Load(WorkerFileName);
                var xElements = xDocument.Root.Elements("Worker").ToList();
                foreach (var elem in xElements)
                {
                    list.Add(new Worker
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        WorkerFIO = elem.Element("WorkerFIO").Value,
                        Email = elem.Element("Email").Value,
                    });
                }
            }
            return list;
        }
        public void SaveToDatabase()
        {
            var workers = LoadWorkers();
            using (var context = new ZooDatabase())
            {
                foreach (var worker in workers)
                {
                    Worker element = context.Workers.FirstOrDefault(rec => rec.Id == worker.Id);
                    if (element != null)
                    {
                        break;
                    }
                    else
                    {
                        element = new Worker();
                        context.Workers.Add(element);
                    }
                    element.WorkerFIO = worker.WorkerFIO;
                    element.Email = worker.Email;
                    element.Salary = worker.Salary;
                    context.SaveChanges();
                }
            }
        }
        public List<WorkerViewModel> Read(WorkerBindingModel model)
        {
            using (var context = new ZooDatabase())
            {
                return context.Workers
                .Where(rec => model == null || rec.Id == model.Id)
                .ToList()
                .Select(rec => new WorkerViewModel
                {
                    Id = rec.Id,
                    WorkerFIO = rec.WorkerFIO,
                    Email = rec.Email,
                    Salary = rec.Salary
                })
                .ToList();
            }
        }
        public void CreateOrUpdate(WorkerBindingModel model)
        {
            using (var context = new ZooDatabase())
            {
                Worker element = context.Workers.FirstOrDefault(rec => rec.Id == model.Id);
                if (model.Id.HasValue)
                {
                    element = context.Workers.FirstOrDefault(rec => rec.Id == model.Id);
                    if (element == null)
                    {
                        throw new Exception("Элемент не найден");
                    }
                }
                else
                {
                    element = new Worker();
                    context.Workers.Add(element);
                }
                element.WorkerFIO = model.WorkerFIO;
                element.Salary = model.Salary;
                context.SaveChanges();
            }
        }
    }
}
