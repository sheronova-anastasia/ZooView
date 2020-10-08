using ZooBussinessLogic.BindingModel;
using ZooBussinessLogic.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace ZooBussinessLogic.Interfaces
{
    public interface IServiceLogic
    {
        List<ServiceViewModel> Read(ServiceBindingModel model);
    }
}
