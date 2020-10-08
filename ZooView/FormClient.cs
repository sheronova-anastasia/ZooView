using ZooBussinessLogic.BindingModel;
using ZooBussinessLogic.Interfaces;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unity;

namespace ZooView
{
    public partial class FormClient : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        public int Id { set { id = value; } }
        private readonly IClientLogic logic;
        private readonly IServiceLogic serviceLogic;
        private int? id;
        public FormClient(IClientLogic logic, IServiceLogic serviceLogic)
        {
            InitializeComponent();
            this.logic = logic;
            this.serviceLogic = serviceLogic;
        }
        private void Client_Load(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    var view = logic.Read(new ClientBindingModel { Id = id })?[0];
                    if (view != null)
                    {
                        textBoxFIO.Text = view.ClientFIO;
                        textBoxGender.Text = view.Gender;
                        textBoxJob.Text = view.Job;
                        textBoxPassportData.Text = view.PassportData.ToString();
                        textBoxNumber.Text = view.Number.ToString();
                        textBoxCount.Text = view.CountService.ToString();
                        textBoxEmail.Text = view.Email;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                   MessageBoxIcon.Error);
                }
            }
        }
        public int CalculateSum(List<ServiceClientBindingModel> clientServices)
        {
            int sum = 0;
            foreach (var serv in clientServices)
            {
                var serData = serviceLogic.Read(new ServiceBindingModel { Id = serv.ServiceId }).FirstOrDefault();
                if (serData != null)
                {
                    sum += serData.Cost;
                }
            }
            return sum;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxFIO.Text))
            {
                MessageBox.Show("Заполните ФИО", "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBoxGender.Text))
            {
                MessageBox.Show("Заполните пол", "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBoxJob.Text))
            {
                MessageBox.Show("Заполните работу", "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBoxPassportData.Text))
            {
                MessageBox.Show("Заполните паспортные данные", "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBoxNumber.Text))
            {
                MessageBox.Show("Заполните номер телефона", "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBoxCount.Text))
            {
                MessageBox.Show("Заполните количество услуг", "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBoxCount.Text))
            {
                MessageBox.Show("Заполните Email", "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
                return;
            }

            Random rnd = new Random();
            var list = new List<ServiceClientBindingModel>();
            int count = Convert.ToInt32(textBoxCount.Text);
            var lis = new List<int>();
            for (int j = 0; j < 10; j++)
            {
                lis.Add(j);
            }
            for (int i = 0; i < count; i++)
            {
                if (!lis.Any())
                {
                    MessageBox.Show("Недостаточно услуг", "Ошибка", MessageBoxButtons.OK,
           MessageBoxIcon.Error);
                    break;
                }
                int temp = lis[rnd.Next(0, lis.Count)];
                list.Add(new ServiceClientBindingModel { ClientId = id, ServiceId = temp });
                lis.Remove(temp);

            }
            try
            {
                logic.CreateOrUpdate(new ClientBindingModel
                {
                    Id = id,
                    ClientFIO = textBoxFIO.Text,
                    Gender = textBoxGender.Text,
                    Job = textBoxJob.Text,
                    PassportData = Convert.ToInt32(textBoxPassportData.Text),
                    Number = Convert.ToInt32(textBoxNumber.Text),
                    Email = textBoxEmail.Text,
                    CountService = Convert.ToInt32(textBoxCount.Text),
                    Score = CalculateSum(list),
                    ServiceClients = list
                });
                MessageBox.Show("Сохранение прошло успешно", "Сообщение",
               MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
