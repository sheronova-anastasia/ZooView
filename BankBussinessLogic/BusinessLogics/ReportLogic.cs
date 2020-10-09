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
        private readonly ITicketLogic ticketLogic;
        private readonly IClientLogic clientLogic;
        public ReportLogic(ITicketLogic ticketLogic, IClientLogic clientLogic)
        {
            this.ticketLogic = ticketLogic;
            this.clientLogic = clientLogic;
        }
        public List<TicketViewModel> GetTickets()
        {
            var tickets = ticketLogic.Read(null);
            var list = new List<TicketViewModel>();
            foreach (var tick in tickets)
            {
                if (tick.Status == Status.Продан)
                {
                    var record = new TicketViewModel
                    {
                        WorkerFIO = tick.WorkerFIO,
                        TypeTicket = tick.TypeTicket,
                        Status = tick.Status
                    };
                    list.Add(record);
                }
            }
            return list;
        }
        public List<TicketViewModel> GetTickets(int id)
        {
            var tickets = ticketLogic.Read(null);
            var list = new List<TicketViewModel>();
            foreach (var tick in tickets)
            {
                if (tick.WorkerId == id)
                {
                    if (tick.Status == Status.Продан)
                    {
                        var record = new TicketViewModel
                        {
                            WorkerFIO = tick.WorkerFIO,
                            TypeTicket = tick.TypeTicket,
                            Status = tick.Status
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
        public void SaveTicketsToExcelFile(string fileName, int id, string email)
        {
            string title = "Проданные билеты";
            SaveToExcel.CreateDoc(new ExcelInfo
            {
                FileName = fileName,
                Title = title,
                Tickets = GetTickets(id),
            });
            SendMail(email, fileName, title);
        }
        public void SaveTicketsToWordFile(string fileName, int id, string email)
        {
            string title = "Проданные билеты";
            SaveToWord.CreateDoc(new WordInfo
            {
                FileName = fileName,
                Title = title,
                Tickets = GetTickets(id),
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
            MailAddress from = new MailAddress("labwork15kafis@gmail.com", "Зоопарк «Юрский период»");
            MailAddress to = new MailAddress(email);
            MailMessage m = new MailMessage(from, to);
            m.Subject = subject;
            m.Attachments.Add(new Attachment(fileName));
            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.Credentials = new NetworkCredential("labwork15kafis@gmail.com", "passlab15");
            smtp.EnableSsl = true;
            smtp.Send(m);
        }
    }
}
