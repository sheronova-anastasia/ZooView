using ZooBussinessLogic.BindingModel;
using ZooBussinessLogic.Enums;
using ZooBussinessLogic.HelperInfo;
using ZooBussinessLogic.Interfaces;
using ZooBussinessLogic.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace ZooBussinessLogic.BusinessLogics
{
    public class ReportLogic
    {
        private readonly IServiceLogic serviceLogic;
        private readonly IClientLogic clientLogic;
        public ReportLogic(IServiceLogic serviceLogic, IClientLogic clientLogic)
        {
            this.serviceLogic = serviceLogic;
            this.clientLogic = clientLogic;
        }
        public List<ServiceViewModel> GetServices()
        {
            var services = serviceLogic.Read(null);
            var list = new List<ServiceViewModel>();
            foreach (var serv in services)
            {
                if (serv.Status == Status.Готово)
                {
                    var record = new ServiceViewModel
                    {
                        WorkerFIO = serv.WorkerFIO,
                        TypeService = serv.TypeService,
                        Status = serv.Status
                    };
                    list.Add(record);
                }
            }
            return list;
        }
        public List<ServiceViewModel> GetServices(int id)
        {
            var services = serviceLogic.Read(null);
            var list = new List<ServiceViewModel>();
            foreach (var serv in services)
            {
                if (serv.WorkerId == id)
                {
                    if (serv.Status == Status.Готово)
                    {
                        var record = new ServiceViewModel
                        {
                            WorkerFIO = serv.WorkerFIO,
                            TypeService = serv.TypeService,
                            Status = serv.Status
                        };

                        list.Add(record);
                    }
                }
            }
            return list;
        }
        public List<ClientViewModel> GetClients(ReportBindingModel model)
        {
            var clients = clientLogic.Read(new ClientBindingModel
            {
                DateFrom = model.DateFrom,
                DateTo = model.DateTo
            });
            var list = new List<ClientViewModel>();
            foreach (var client in clients)
            {
                var record = new ClientViewModel
                {
                    ClientFIO = client.ClientFIO,
                    Score = client.Score
                };
                list.Add(record);
            }
            return list;
        }
        public List<ClientViewModel> GetClients(int id)
        {
            var clients = clientLogic.Read(null);
            var list = new List<ClientViewModel>();
            foreach (var client in clients)
            {
                if (client.Id == id)
                {
                    var record = new ClientViewModel
                    {
                        ClientFIO = client.ClientFIO,
                        Score = client.Score
                    };
                    list.Add(record);
                }
            }
            return list;
        }
        public void SaveServicesToExcelFile(string fileName, int id, string email)
        {
            string title = "Выполненные услуги";
            SaveToExcel.CreateDoc(new ExcelInfo
            {
                FileName = fileName,
                Title = title,
                Services = GetServices(id),
            });
            SendMail(email, fileName, title);
        }
        public void SaveServicesToWordFile(string fileName, int id, string email)
        {
            string title = "Выполненные услуги";
            SaveToWord.CreateDoc(new WordInfo
            {
                FileName = fileName,
                Title = title,
                Services = GetServices(id),
            });
            SendMail(email, fileName, title);
        }
        public void SaveClientsToPdfFile(string fileName, int id, string email)
        {
            string title = "Счет клиента";
            SaveToPdf.CreateDoc(new PdfInfo
            {
                FileName = fileName,
                Title = title,
                Clients = GetClients(id),
            });
            SendMail(email, fileName, title);
        }
        public void SendMail(string email, string fileName, string subject)
        {
            MailAddress from = new MailAddress("anastasiaaa4278@gmail.com", "Зоопарк «Юрский период»");
            MailAddress to = new MailAddress(email);
            MailMessage m = new MailMessage(from, to);
            m.Subject = subject;
            m.Attachments.Add(new Attachment(fileName));
            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.Credentials = new NetworkCredential("anastasiaaa4278@gmail.com", "iravol73");
            smtp.EnableSsl = true;
            smtp.Send(m);
        }
    }
}
