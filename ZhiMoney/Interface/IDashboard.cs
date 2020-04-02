using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZhiMoney.Interface
{
    interface IDashboard
    {
        string MainTitle { get;  set; }
        string LeftTitle { get; set; }
        string RightTitle { get; set; }
    }
}
