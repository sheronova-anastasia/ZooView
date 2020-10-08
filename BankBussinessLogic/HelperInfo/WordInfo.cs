using ZooBussinessLogic.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace ZooBussinessLogic.HelperInfo
{
    public class WordInfo
    {
        public string FileName { get; set; }
        public string Title { get; set; }
        public List<ServiceViewModel> Services { get; set; }
    }
}
