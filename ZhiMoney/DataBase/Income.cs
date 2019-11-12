using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZhiMoney.DataBase
{
    class Income : IInputData
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
                    IInputData expense = obj as IInputData;
                    return Id.CompareTo(expense.Id);
                }
                else return 1;
            }  
        }
    }
}
