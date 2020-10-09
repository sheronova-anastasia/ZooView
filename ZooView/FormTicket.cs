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
    public partial class FormTicket : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        private readonly IClientLogic clientLogic;
        public int Id { set { id = value; } }
        private int? id;
        private List<TicketClientViewModel> TicketClients;
        public FormTicket(IClientLogic clientLogic)
        {
            InitializeComponent();
            this.clientLogic = clientLogic;
        }
        private void FormTicket_Load(object sender, EventArgs e)
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
                        TicketClients = view.TicketClients;
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
                if (TicketClients != null)
                {
                    dataGridView.Rows.Clear();
                    foreach (var bf in TicketClients)
                    {
                        dataGridView.Rows.Add(new object[] { bf.TypeTicket, bf.Cost });
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
