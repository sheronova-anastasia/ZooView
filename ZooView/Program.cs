using ZooBussinessLogic.BusinessLogics;
using ZooBussinessLogic.Interfaces;
using ZooBussinessLogic.ViewModel;
using ZooDatabaseImplement.Implements;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unity;
using Unity.Lifetime;

namespace ZooView
{
    static class Program
    {
        public static bool IsLogined;
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            TicketLogic logicS = new TicketLogic();
            WorkerLogic logic = new WorkerLogic();
            logic.SaveToDatabase();
            logicS.SaveToDatabase();
            var container = BuildUnityContainer();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var login = new FormAutorization();
            login.ShowDialog();
            if (IsLogined)
            {
                Application.Run(container.Resolve<FormMain>());
            }
        }
        private static IUnityContainer BuildUnityContainer()
        {
            var currentContainer = new UnityContainer();
            currentContainer.RegisterType<IWorkerLogic, WorkerLogic>(new
            HierarchicalLifetimeManager());
            currentContainer.RegisterType<IClientLogic, ClientLogic>(new
           HierarchicalLifetimeManager());
            currentContainer.RegisterType<ITicketLogic,TicketLogic>(new
           HierarchicalLifetimeManager());
            currentContainer.RegisterType<BackUpAbstractLogic, BackUpLogic>(new
           HierarchicalLifetimeManager());
            return currentContainer;
        }
    }
}
