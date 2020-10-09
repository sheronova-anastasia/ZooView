using ZooBussinessLogic.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace ZooBussinessLogic.HelperInfo
{
    class PdfInfo
    {
        public string FileName { get; set; }
        public string Title { get; set; }
        public List<ClientViewModel> Clients { get; set; }
    }
}
