using ZooBussinessLogic.BusinessLogics;
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
    public partial class FormReportService : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        private readonly ReportLogic logic;
        public FormReportService(ReportLogic logic)
        {
            InitializeComponent();
        }
        private void FormReportService_Load(object sender, EventArgs e)
        {
            try
            {
                var service = logic.GetServices();
                if (service != null)
                {
                    dataGridView.Rows.Clear();
                    int sum = 0;
                    foreach (var serv in service)
                    {
                        dataGridView.Rows.Add(new object[] { serv.WorkerFIO, serv.TypeService, serv.Status, });
                        sum++;
                    }
                    dataGridView.Rows.Add(new object[] { "", "Итого", sum });

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void buttonSend_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormSend>();
            form.ShowDialog();
        }
    }
}
