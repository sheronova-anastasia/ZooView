using ZooBussinessLogic.BusinessLogics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unity;

namespace ZooView
{
    public partial class FormMain : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        private readonly BackUpAbstractLogic backUpAbstractLogic;
        public FormMain(BackUpAbstractLogic backUpAbstractLogic)
        {
            InitializeComponent();
        }
        private void клиентыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormClients>();
            form.ShowDialog();
        }
        private void расчтеССотрудникамиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormWorker>();
            form.ShowDialog();
        }

        private void отчетПоВыполненнымУслугамToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormReportService>();
            form.ShowDialog();
        }

        private void отчетПоКлиентамИИхСчетуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormReportClient>();
            form.ShowDialog();
        }

        private void создатьБекапToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (backUpAbstractLogic != null)
                {
                    var fbd = new FolderBrowserDialog();
                    if (fbd.ShowDialog() == DialogResult.OK)
                    {
                        backUpAbstractLogic.CreateArchive(fbd.SelectedPath);
                        MessageBox.Show("Бекап создан", "Сообщение",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
            }

        }

        private void графикиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<Grafic>();
            form.ShowDialog();
        }
    }
}
