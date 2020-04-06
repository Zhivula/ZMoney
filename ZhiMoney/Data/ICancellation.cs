using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZhiMoney.DataBase;

namespace ZhiMoney.Data
{
    interface ICancellation
    {
        IInputData GetLastElement();
        void DeleteLastElement();
    }
}
