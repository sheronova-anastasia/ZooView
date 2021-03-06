﻿using ZooBussinessLogic.BindingModel;
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
        public readonly ITicketLogic logic;
        public readonly IWorkerLogic logicW;
        public readonly ReportLogic reportLogic;
        public FormSend(ITicketLogic logic, ReportLogic reportLogic, IWorkerLogic logicW)
        {
            InitializeComponent();
            this.logic = logic;
            this.reportLogic = reportLogic;
            this.logicW = logicW;
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
                var ticket = logic.Read(new TicketBindingModel { WorkerId = id });
                if (checkBoxDoc.Checked)
                {

                    string fileName = "C:\\Users\\Настя\\Desktop\\data\\" + "Отчет по проданным билетам.docx";
                    reportLogic.SaveTicketsToWordFile(fileName, id, textBoxMail.Text);

                }
                if (checkBoxXls.Checked)
                {
                    string fileName = "C:\\Users\\Настя\\Desktop\\data\\" + "Worker.xlsx";
                    reportLogic.SaveTicketsToExcelFile(fileName, id, textBoxMail.Text);

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
            var ticke = logicW.Read(null);
            foreach (var tick in ticke)
            {
                if (tick.Id == id)
                {
                    textBoxMail.Text = tick.Email;
                }
            }
        }
    }
}
