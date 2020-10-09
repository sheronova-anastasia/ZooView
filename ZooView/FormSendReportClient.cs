using ZooBussinessLogic.BindingModel;
using ZooBussinessLogic.BusinessLogics;
using ZooBussinessLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ZooView
{
    public partial class FormSendReportClient : Form
    {
        public readonly IClientLogic logic;
        public readonly ReportLogic reportLogic;
        public FormSendReportClient(IClientLogic logic, ReportLogic reportLogic)
        {
            InitializeComponent();
            this.logic = logic;
            this.reportLogic = reportLogic;
        }
        private void FormSendReportClient_Load(object sender, EventArgs e)
        {
            try
            {
                var list = logic.Read(null);
                comboBoxFIO.DataSource = list;
                comboBoxFIO.DisplayMember = "ClientFIO";
                comboBoxFIO.ValueMember = "Id";
                comboBoxFIO.SelectedItem = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
            }
        }

        private void buttonEmail_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(comboBoxFIO.SelectedValue);
            var client = logic.Read(null);
            foreach (var cl in client)
            {
                if (cl.Id == id)
                {
                    textBoxMail.Text = cl.Email;
                }
            }
        }

        private void buttonSend_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxMail.Text))
            {
                MessageBox.Show("Заполните Email", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBoxFIO.SelectedValue == null)
            {
                MessageBox.Show("Выберите ФИО клиента", "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
                return;
            }
            try
            {
                int id = Convert.ToInt32(comboBoxFIO.SelectedValue);
                var client = logic.Read(new ClientBindingModel { Id = id });
                string fileName = "C:\\Users\\Настя\\Desktop\\data\\" + "Отчет по по клиентам и их счету.pdf";
                reportLogic.SaveClientsToPdfFile(fileName, id, textBoxMail.Text);
                MessageBox.Show("Отправлено", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
