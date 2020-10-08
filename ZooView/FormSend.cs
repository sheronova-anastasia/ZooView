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
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ZooView
{
    public partial class FormSend : Form
    {
        public readonly IServiceLogic logic;
        public readonly IWorkerLogic logicW;
        public readonly ReportLogic reportLogic;
        public FormSend(IServiceLogic logic, ReportLogic reportLogic, IWorkerLogic logicW)
        {
            InitializeComponent();
        }
        private void buttonSend_Click(object sender, EventArgs e)
        {
            if (comboBoxFIO.SelectedValue == null)
            {
                MessageBox.Show("Выберите ФИО сотрудника", "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBoxMail.Text))
            {
                MessageBox.Show("Заполните Email", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!checkBoxDoc.Checked && !checkBoxXls.Checked)
            {
                MessageBox.Show("Выберите формат документа", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                int id = Convert.ToInt32(comboBoxFIO.SelectedValue);
                var service = logic.Read(new ServiceBindingModel { WorkerId = id });
                if (checkBoxDoc.Checked)
                {

                    string fileName = "C:\\Users\\marin.LAPTOP-0TUFHPTU\\Рабочий стол\\универ\\data\\" + "Отчет по выплненным услугам.docx";
                    reportLogic.SaveServicesToWordFile(fileName, id, textBoxMail.Text);

                }
                if (checkBoxXls.Checked)
                {
                    string fileName = "C:\\Users\\marin.LAPTOP-0TUFHPTU\\Рабочий стол\\универ\\data\\" + "Worker.xlsx";
                    reportLogic.SaveServicesToExcelFile(fileName, id, textBoxMail.Text);

                }

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

        private void FormSend_Load(object sender, EventArgs e)
        {
            try
            {
                var list = logicW.Read(null);
                comboBoxFIO.DataSource = list;
                comboBoxFIO.DisplayMember = "WorkerFIO";
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
            var servi = logicW.Read(null);
            foreach (var serv in servi)
            {
                if (serv.Id == id)
                {
                    textBoxMail.Text = serv.Email;
                }
            }
        }
    }
}
