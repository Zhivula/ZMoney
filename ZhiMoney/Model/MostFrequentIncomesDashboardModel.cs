using System.Collections.Generic;
using System.Linq;
using ZhiMoney.Abstraction;
using ZhiMoney.DataBase;

namespace ZhiMoney.Model
{
    class MostFrequentIncomesDashboardModel : Dashboard
    {
        public MostFrequentIncomesDashboardModel()
        {
            mainTitle = "Наиболее частые";
            leftTitle = "Наименование";
            rightTitle = "Количество";

            var income = new IncomeModel();
            Names = new string[10];
            Values = new float[10];
            for (int i = 0; i < 10; i++)
            {
                Names[i] = i.ToString();
                Values[i] = i;
            }
            //using (var context = new MyDbContext())
            //{
            //    var set = new HashSet<string>();
            //    set.Concat(context.Incomes.Select(x => x.Name));
            //    Names = set.ToArray();
            //    Values = new float[set.Count];
            //    for (int i = 0; i < set.Count; i++)
            //    {
            //        Values[i] = context.Incomes.Where(x => x.Name == Names[i]).Select(o => o.Summa).Sum();
            //    }
            //}
        }
    }
}
