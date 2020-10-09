using ZooBussinessLogic.BindingModel;
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
    public partial class FormService : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        private readonly IClientLogic clientLogic;
        public int Id { set { id = value; } }
        private int? id;
        private List<TicketClientViewModel> ServiceClients;
        public FormService(IClientLogic clientLogic)
        {
            InitializeComponent();
            this.clientLogic = clientLogic;
        }
        private void FormService_Load(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    ClientViewModel view = clientLogic.Read(new ClientBindingModel
                    {
                        Id = id.Value
                    })?[0];
                    if (view != null)
                    {
                        ServiceClients = view.ServiceClients;
                        LoadData();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                   MessageBoxIcon.Error);
                }
            }
        }
        private void LoadData()
        {
            try
            {
                if (ServiceClients != null)
                {
                    dataGridView.Rows.Clear();
                    foreach (var bf in ServiceClients)
                    {
                        dataGridView.Rows.Add(new object[] { bf.TypeService, bf.Cost });
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
            }
        }
    }
}
