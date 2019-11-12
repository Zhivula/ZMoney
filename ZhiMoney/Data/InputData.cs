using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ZhiMoney.Data
{
    class InputData
    {
        /// <summary>
        /// Конвертирует входные строковые данные в данные типа float.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public float ConvertStringToFloat(string value)
        {
            //Для начала просто проверям на пустоту входную строку.
            if (!string.IsNullOrWhiteSpace(value))
            {
                //если пользователь ввел действительно число, но через точку,
                //то мы его просто заменим на нужную нам запятую, чтобы лишний раз не напрягать пользователя.
                value = value.Replace('.', ',');

                if (float.TryParse(value, out float result))
                {
                    //Вводимое число обязательно должно быть больше нуля, иначе нет смысла вносить его в базу данных вдальнейшем.
                    if (result > 0)
                    {
                        return result;
                    }
                    else MessageBox.Show("Входные данные меньше либо равны нулю");
                }
                else MessageBox.Show("Неудалось выполнить преобразование");

            }
            else MessageBox.Show("Попытка ввести пустую строку");

            return 0; // если какое-либо условие не было выполнено, то возвращаем ноль как знак невыполненения условий.
        }
    }
}
