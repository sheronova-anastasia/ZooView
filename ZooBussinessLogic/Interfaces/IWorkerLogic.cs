using ZooBussinessLogic.BindingModel;
using ZooBussinessLogic.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace ZooBussinessLogic.Interfaces
{
    public interface IWorkerLogic
    {
        List<WorkerViewModel> Read(WorkerBindingModel model);
        void CreateOrUpdate(WorkerBindingModel model);
    }
}
