using ZooBussinessLogic.BindingModel;
using ZooBussinessLogic.Enums;
using ZooBussinessLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unity;

namespace ZooView
{
    public partial class Grafic : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        private readonly IServiceLogic logicS;
        public Grafic(IServiceLogic logicS)
        {
            InitializeComponent();
            this.logicS = logicS;
        }
        int countDone = 0;
        int countPerformed = 0;
        int k = 0;
        private void button1_Click(object sender, EventArgs e)
        {

            var servi = logicS.Read(null);
            foreach (var serv in servi)
            {
                if (serv.Status == Status.Готово && serv.TypeService == "Вклад")
                    countDone++;
                if (serv.Status == Status.Выполняется && serv.TypeService == "Вклад")
                    countPerformed++;
            }
            if (k == 0)
            {
                chart.Series[0].Points.Add(countPerformed);
                chart.Series[0].Points[0].LegendText = "Выполняется";
                chart.Series[0].Points[0].AxisLabel = "Выполняется";
                chart.Series[0].Points[0].Label = countPerformed.ToString();
                chart.Series[0].Points[0].Color = Color.FromArgb(241, 226, 110);

                chart.Series[0].Points.Add(countDone);
                chart.Series[0].Points[1].LegendText = "Готово";
                chart.Series[0].Points[1].AxisLabel = "Готово";
                chart.Series[0].Points[1].Label = countDone.ToString();
                chart.Series[0].Points[1].Color = Color.FromArgb(163, 255, 123);
            }
            if (k == 1)
            {
                chart.Series[0].Points.Clear();
                k = -1;
                countPerformed = 0;
                countDone = 0;
            }
            k++;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            var servi = logicS.Read(null);
            foreach (var serv in servi)
            {
                if (serv.Status == Status.Готово && serv.TypeService == "Кредит")
                    countDone++;
                if (serv.Status == Status.Выполняется && serv.TypeService == "Кредит")
                    countPerformed++;
            }
            if (k == 0)
            {
                chart.Series[0].Points.Add(countPerformed);
                chart.Series[0].Points[0].LegendText = "Выполняется";
                chart.Series[0].Points[0].AxisLabel = "Выполняется";
                chart.Series[0].Points[0].Label = countPerformed.ToString();
                chart.Series[0].Points[0].Color = Color.FromArgb(43, 187, 223);

                chart.Series[0].Points.Add(countDone);
                chart.Series[0].Points[1].LegendText = "Готово";
                chart.Series[0].Points[1].AxisLabel = "Готово";
                chart.Series[0].Points[1].Label = countDone.ToString();
                chart.Series[0].Points[1].Color = Color.Pink;
            }
            if (k == 1)
            {
                chart.Series[0].Points.Clear();
                k = -1;
                countPerformed = 0;
                countDone = 0;
            }
            k++;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            var servi = logicS.Read(null);
            foreach (var serv in servi)
            {
                if (serv.Status == Status.Готово && serv.TypeService == "Рассрочка")
                    countDone++;
                if (serv.Status == Status.Выполняется && serv.TypeService == "Рассрочка")
                    countPerformed++;
            }
            if (k == 0)
            {
                chart.Series[0].Points.Add(countPerformed);
                chart.Series[0].Points[0].LegendText = "Выполняется";
                chart.Series[0].Points[0].AxisLabel = "Выполняется";
                chart.Series[0].Points[0].Label = countPerformed.ToString();
                chart.Series[0].Points[0].Color = Color.FromArgb(219, 55, 55);

                chart.Series[0].Points.Add(countDone);
                chart.Series[0].Points[1].LegendText = "Готово";
                chart.Series[0].Points[1].AxisLabel = "Готово";
                chart.Series[0].Points[1].Label = countDone.ToString();
                chart.Series[0].Points[1].Color = Color.FromArgb(152, 29, 131);
            }
            if (k == 1)
            {
                chart.Series[0].Points.Clear();
                k = -1;
                countPerformed = 0;
                countDone = 0;
            }
            k++;

        }
    }
}
