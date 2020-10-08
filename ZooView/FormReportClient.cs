using ZooBussinessLogic.BindingModel;
using ZooBussinessLogic.BusinessLogics;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Reporting.WinForms;
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
    public partial class FormReportClient : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        private readonly ReportLogic logic;
        public FormReportClient(ReportLogic logic)
        {
            InitializeComponent();
        }
        public void LoadData()
        {
            if (dateTimePickerFrom.Value.Date >= dateTimePickerTo.Value.Date)
            {
                MessageBox.Show("Дата начала должна быть меньше даты окончания", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                var dataSource = logic.GetClients(new ReportBindingModel { DateFrom = dateTimePickerFrom.Value.Date, DateTo = dateTimePickerTo.Value.Date });
                ReportDataSource source = new ReportDataSource("DataSetClient", dataSource);
                reportViewer.LocalReport.DataSources.Add(source);
                reportViewer.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
            }
            this.reportViewer.RefreshReport();
        }

        private void buttonSend_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormSendReportClient>();
            form.ShowDialog();
        }

        private void buttonCreate_Click(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}
