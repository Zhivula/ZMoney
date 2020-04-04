using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZhiMoney.Data;
using ZhiMoney.DataBase;

namespace ZhiMoney.Model
{
    class MostIncomesDashboardModel : Dashboard
    {
        public MostIncomesDashboardModel()
        {
            mainTitle = "Самые доходные";
            leftTitle = "Наименование";
            rightTitle = "Сумма(р.)";

            Names = new string[5];
            Values = new float[5];

            using (var context = new MyDbContext())
            {
                if (context.Incomes.Count() != 0)
                {
                    var set = new HashSet<string>();
                    string[] names = context.Incomes.Select(x => x.Name).ToArray();
                    set.UnionWith(names);
                    Names = set.ToArray();

                    var incomes = new Income[Names.Count()];
                    
                    for (int i = 0; i < Names.Count(); i++)
                    {
                        var j = Names[i];
                        incomes[i] = new Income
                        {
                            Summa = context.Incomes.Where(x => x.Name == j).Select(o => o.Summa).Sum(),
                            Name = j
                        };
                    }
                    var result = incomes.OrderByDescending(u=>u.Summa).ThenBy(u=>u.Name);
                    Names = result.Select(u=> u.Name).ToArray();
                    Values = result.Select(u=> u.Summa).ToArray();
                }
            }
        }
    }
}
