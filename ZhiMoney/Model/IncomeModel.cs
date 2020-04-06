using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms.DataVisualization.Charting;
using ZhiMoney.Data;
using ZhiMoney.DataBase;

namespace ZhiMoney.Model
{
    class IncomeModel : IInputDataModel
    {
        public ObservableCollection<string> Combobox;
        public ObservableCollection<int> ComboboxDate;

        public IncomeModel()
        {
            Combobox = new ObservableCollection<string>()
            {
                 "   Доходы" ,
                 "   Расходы",
                 "   Общая"
            };
            ComboboxDate = new ObservableCollection<int>()
            {
                30,
                90,
                180,
                365
            };
        }
        /// <summary>
        /// Возвращает список с датами, когда вносились пополениния счета.
        /// </summary>
        /// <returns></returns>
        public List<DateTime> GetDateTimes()
        {
            var list = new List<DateTime>();
            using (var context = new MyDbContext())
            {
                list = context.Incomes.Select(x=> x.Date).ToList();
            }
            return list;
        }
        /// <summary>
        /// Возвращает список с суммами, которые вносились пользователем.
        /// </summary>
        /// <returns></returns>
        public List<float> GetSumma()
        {
            var list = new List<float>();
            using (var context = new MyDbContext())
            {
                list = context.Incomes.Select(x => x.Summa).ToList();
            }
            return list;
        }
        /// <summary>
        /// Добавление записи в базу данных, в таблицу Income 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="summa"></param>
        public void AddRecord(string name, float summa)
        {
            using (var context = new MyDbContext())
            {
                var income = new Income()
                {
                    Name = name,
                    Summa = summa,
                    Date = DateTime.Now
                };
                context.Incomes.Add(income);
                context.SaveChanges();
            }
            MessageBox.Show("Запись успешно внесена!");
        }

        public void AlgorthmSort(out DateTime[] date, out float[] summa, int days = 30)
        {
            var Date = GetDateTimes();
            var Summa = GetSumma();
            date = new DateTime[days];
            summa = new float[days];

            for (int i = 0; i < days; i++)
            {
                date[i] = DateTime.Now.AddDays(i - days + 1);
                summa[i] = 0;
                for (int j = 0; j < Date.Count(); j++)
                {
                    if (date[i].ToShortDateString() == Date[j].ToShortDateString())
                    {
                        summa[i] += Summa[j];
                    }
                }
            }
        }

        /// <summary>
        /// Заполняет префиксное дерево из базы данных.
        /// </summary>
        /// <param name="prefixTree"></param>
        public void FillPrefixTree(ref PrefixTree prefixTree)
        {
            using (var context = new MyDbContext())
            {
                List<string> itemsIncome = context.Incomes.Select(x => x.Name).ToList();
                foreach (string item in itemsIncome) prefixTree.Add(item);
            }
        }

        public float GetCurrentSumma()
        {          
            using (var context = new MyDbContext())
            {
                float incomeSumma = 0;
                float expenseSumma = 0;
                if (context.Incomes.Count() != 0) incomeSumma = context.Incomes.Select(x => x.Summa).Sum();
                if (context.Expenses.Count() != 0) expenseSumma = context.Expenses.Select(x => x.Summa).Sum();
                return incomeSumma - expenseSumma;
            }
        }

        public IInputData GetLastElement()
        {
            throw new NotImplementedException();
        }
    }
}
