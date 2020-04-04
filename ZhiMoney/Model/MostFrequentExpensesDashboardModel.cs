using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZhiMoney.Data;
using ZhiMoney.DataBase;

namespace ZhiMoney.Model
{
    class MostFrequentExpensesDashboardModel: Dashboard
    {
        public MostFrequentExpensesDashboardModel()
        {
            mainTitle = "Наиболее частые";
            leftTitle = "Наименование";
            rightTitle = "Количество";

            Names = new string[5];
            Values = new float[5];

            using (var context = new MyDbContext())
            {
                if (context.Expenses.Count() != 0)
                {
                    var set = new HashSet<string>();
                    string[] names = context.Expenses.Select(x => x.Name).ToArray();
                    set.UnionWith(names);
                    Names = set.ToArray();

                    var expenses = new Expense[Names.Count()];

                    for (int i = 0; i < Names.Count(); i++)
                    {
                        var j = Names[i];
                        expenses[i] = new Expense
                        {
                            Summa = context.Expenses.Where(x => x.Name == j).Count(),
                            Name = j
                        };
                    }
                    var result = expenses.OrderByDescending(u => u.Summa).ThenBy(u => u.Name);
                    Names = result.Select(u => u.Name).ToArray();
                    Values = result.Select(u => u.Summa).ToArray();
                }
            }
        }
    }
}
