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
using Unity;

namespace ZooView
{
    public partial class FormWorker : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        private readonly IWorkerLogic workerLogic;
        public FormWorker(IWorkerLogic workerLogic)
        {
            InitializeComponent();
            this.workerLogic = workerLogic;
        }
        private void FormWorker_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        private void LoadData()
        {
            try
            {
                var list = workerLogic.Read(null);
                if (list != null)
                {
                    dataGridView.DataSource = list;
                    dataGridView.Columns[0].Visible = false;
                    dataGridView.Columns[3].Visible = false;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
            }
        }

        private void buttonSalary_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormSalary>();
            form.ShowDialog();
            LoadData();
        }
    }
}
