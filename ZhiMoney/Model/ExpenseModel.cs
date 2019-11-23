using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using ZhiMoney.Data;
using ZhiMoney.DataBase;

namespace ZhiMoney.Model
{
    class ExpenseModel : IInputDataModel
    {
        public ObservableCollection<string> Combobox;

        public ExpenseModel()
        {
            Combobox = new ObservableCollection<string>()
            {                
                 "   Доходы" ,
                 "   Расходы",
                 "   Общая"
            };
        }
        /// <summary>
        /// Добавление записи в базу данных, в таблицу Expense 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="summa"></param>
        public void AddRecord(string name, float summa)
        {
            using (var context = new MyDbContext())
            {
                var expense = new Expense()
                {
                    Name = name,
                    Summa = summa,
                    Date = DateTime.Now
                };
                context.Expenses.Add(expense);
                context.SaveChanges();
            }
            MessageBox.Show("Запись успешно внесена!");
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
                list = context.Expenses.Select(x => x.Date).ToList();
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
                list = context.Expenses.Select(x => x.Summa).ToList();
            }
            return list;
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
                List<string> itemsExpense = context.Expenses.Select(x => x.Name).ToList();
                foreach (string item in itemsExpense) prefixTree.Add(item);
            }
        }
        public float GetCurrentSumma()
        {
            using (var context = new MyDbContext())
            {
                float incomeSumma;
                float expenseSumma;
                incomeSumma = context.Incomes.Select(x => x.Summa).Sum();
                expenseSumma = context.Expenses.Select(x => x.Summa).Sum();
                return incomeSumma - expenseSumma;
            }
        }
    }
}
