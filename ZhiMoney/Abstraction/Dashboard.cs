using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZhiMoney.Abstraction
{
    public abstract class Dashboard
    {
        public string mainTitle;
        public string leftTitle;
        public string rightTitle;

        public string[] Names;
        public float[] Values;
    }
}
