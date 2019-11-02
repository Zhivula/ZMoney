using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using ZhiMoney.DataBase;

namespace ZhiMoney.Model
{
    class IncomeModel
    {
        public ObservableCollection<string> Combobox;
        public IncomeModel()
        {
            Combobox = new ObservableCollection<string>()
            {
                 "   Доходы" ,
                 "   Расходы"
            };
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
        /// <summary>
        /// Обработка входящих данных.
        /// Попытка преобразовать входящие данные в тип float (он же и Single)
        /// </summary>
        /// <param name="o"></param>
        /// <param name="result"></param>
        /// <param name="canSummaRecord"></param>
        public void ParseSummaToSingle(string o, out float result,out bool canSummaRecord)
        {
            if (float.TryParse(o.ToString(), out result))
            {
                result = float.Parse(o);
                canSummaRecord = true;
            }
            else
            {
                canSummaRecord = false;
                MessageBox.Show("Неудалось выполнить преобразование.");
            }

        }

    }
}
