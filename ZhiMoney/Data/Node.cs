using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZhiMoney.Data
{
    /// <summary>
    /// Класс, представляющий одну ячейку в префиксном дереве.
    /// </summary>
    internal class Node
    {
        /// <summary>
        /// Символ, который будет храниться в ячейке.
        /// Слово будет формироваться из последовательности таких ячеек(содержащихся в них символов).
        /// </summary>
        public char Symbol { get; set; }

        /// <summary>
        /// Число, показывающее количество добавления новых слов через определенный путь.
        /// </summary>
        public int Data { get; set; }

        /// <summary>
        /// Указывает, является ли данная ячейка окончанием слова.
        /// </summary>
        public bool IsWord { get; set; }

        /// <summary>
        /// Префикс данной ячейки. 
        /// Содержит последовательность всех символов, которые "стоят" перед данной ячейкой.
        /// </summary>
        public string Prefix { get; set; }

        /// <summary>
        /// Словарь всех других ячеек, которые исходят от данной ячейки.
        /// </summary>
        public Dictionary<char, Node> SubNodes { get; set; }

        /// <summary>
        /// Создание новой ячейки.
        /// </summary>
        /// <param name="symbol"> Символ</param>
        /// <param name="data"> Счетчик</param>
        /// <param name="prefix"> Префикс</param>
        public Node(char symbol, int data, string prefix)
        {
            Symbol = symbol;
            Data = data;
            Prefix = prefix;
            SubNodes = new Dictionary<char, Node>();
        }
        /// <summary>
        /// Поиск ячейки по вводимому символу.
        /// </summary>
        /// <param name="symbol"> Искомый символ</param>
        /// <returns></returns>
        public Node TryFind(char symbol)
        {
            if (SubNodes.TryGetValue(symbol,out Node value))
            {
                return value;
            }
            else
            {
                return null;
            }
        }

        public override string ToString()
        {
            return Symbol.ToString();
        }

        public override bool Equals(object obj)
        {
            if(obj is Node item)
            {
                return Data.Equals(item);
            }
            else
            {
                return false;
            }
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
