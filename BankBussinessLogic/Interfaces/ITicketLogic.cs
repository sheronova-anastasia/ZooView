﻿using ZooBussinessLogic.BindingModel;
using ZooBussinessLogic.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace ZooBussinessLogic.Interfaces
{
    public interface ITicketLogic
    {
        List<TicketViewModel> Read(TicketBindingModel model);
    }
}
