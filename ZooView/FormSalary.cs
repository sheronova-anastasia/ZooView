using ZooBussinessLogic.BindingModel;
using ZooBussinessLogic.Enums;
using ZooBussinessLogic.Interfaces;
using ZooBussinessLogic.ViewModel;
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
    public partial class FormSalary : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        private readonly IWorkerLogic logicW;
        private readonly IServiceLogic logicS;
        public int Id { set { id = value; } }
        private int? id;
        public FormSalary()
        {
            InitializeComponent();
            this.logicW = logicW;
            this.logicS = logicS;
        }
        private void FormSalary_Load(object sender, EventArgs e)
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
        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxSalary.Text))
            {
                MessageBox.Show("Заполните поле Зарплата", "Ошибка",
               MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBoxFIO.SelectedValue == null)
            {
                MessageBox.Show("Выберите ФИО сотрудника", "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
                return;
            }
            try
            {
                logicW.CreateOrUpdate(new WorkerBindingModel
                {
                    Id = Convert.ToInt32(comboBoxFIO.SelectedValue),
                    WorkerFIO = comboBoxFIO.Text,
                    Salary = Convert.ToInt32(textBoxSalary.Text)
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

        private void buttonCharge_Click(object sender, EventArgs e)
        {
            if (comboBoxFIO.SelectedValue == null)
            {
                MessageBox.Show("Выберите ФИО сотрудника", "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
                return;
            }
            if (comboBoxFIO.SelectedValue != null)
            {
                try
                {
                    int client = 0;
                    int id = Convert.ToInt32(comboBoxFIO.SelectedValue);
                    WorkerBindingModel model = new WorkerBindingModel();
                    var countDone = 0;
                    var servi = logicS.Read(null);
                    foreach (var serv in servi)
                    {
                        if (serv.WorkerId == id)
                        {
                            client++;
                            if (serv.Status == Status.Готово)
                                countDone++;
                        }
                    }
                    if (countDone == client)
                        model.Salary = 40000;
                    if ((client - countDone >= 1) && (client - countDone <= 3))
                        model.Salary = 30000;
                    if (client - countDone > 3)
                        model.Salary = 20000;
                    textBoxSalary.Text = model.Salary.ToString();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                   MessageBoxIcon.Error);
                }
            }
        }
    }
}
