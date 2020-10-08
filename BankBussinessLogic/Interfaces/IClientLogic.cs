using ZooBussinessLogic.BindingModel;
using ZooBussinessLogic.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace ZooBussinessLogic.Interfaces
{
    public interface IClientLogic
    {
        List<ClientViewModel> Read(ClientBindingModel model);
        void CreateOrUpdate(ClientBindingModel model);
        void Delete(ClientBindingModel model);
    }
}
