using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZhiMoney.DataBase
{
    public interface IInputData : IComparable
    {
        int Id { get; set; }
        float Summa { get; set; }
        string Name { get; set; }
        DateTime Date { get; set; }
    }
}
