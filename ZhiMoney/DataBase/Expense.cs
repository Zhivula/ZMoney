using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZhiMoney.DataBase
{
    class Expense : IInputData
    {
        public int Id { get; set; }
        public float Summa { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public int CompareTo(object obj)
        {
            if (obj == null)
            {
                return 1;
            }
            else
            {
                if (obj is IInputData)
                {
                    IInputData income = obj as IInputData;
                    return Id.CompareTo(income.Id);
                }
                else return 1;
            }
        }
    }
}
