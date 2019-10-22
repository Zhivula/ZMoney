using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ZhiMoney.DataBase;

namespace ZhiMoney.Model
{
    class IncomeModel
    {
        /// <summary>
        /// Добавление записи в базу данных, в таблицу Income 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="summa"></param>
        protected void AddRecord(string name, float summa)
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
        protected void ParseSummaToSingle(string o, out float result,out bool canSummaRecord)
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
