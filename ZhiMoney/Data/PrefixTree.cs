using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZhiMoney.Data
{
    public class PrefixTree
    {
        /// <summary>
        /// Корень префиксного дерева.
        /// </summary>
        private Node root;

        /// <summary>
        /// Количество ячеек в префиксном дереве.
        /// </summary>
        public int Count { get; set; }

        public PrefixTree()
        {
            root = new Node('\0', 0, "");
            Count = 1;
        }

        /// <summary>
        /// Добавление слова в префиксное дерево.
        /// </summary>
        /// <param name="key">Добавляемое слово</param>
        public void Add(string key)
        {
            AddNode(key, root);
        }

        private void AddNode(string key, Node node)
        {
            if (string.IsNullOrEmpty(key))
            {
                if (!node.IsWord)
                {
                    node.IsWord = true;
                }
            }
            else
            {
                var symbol = key[0];
                var subnode = node.TryFind(symbol);
                if (subnode != null)
                {
                    subnode.Data++;
                    AddNode(key.Substring(1), subnode);
                }
                else
                {
                    var newNode = new Node(key[0], 1, node.Prefix + key[0]);
                    node.SubNodes.Add(key[0], newNode);
                    Count++;
                    AddNode(key.Substring(1), newNode);
                }
            }

        }

        /// <summary>
        /// Удаление слова из префиксного дерева.
        /// Смысл такой, что мы обходим каждую ячейку пока не дойдем до последней ячейки слова,
        /// а затем просто меняем IsWord = false. Получается, они просто остаются висеть в памяти, ведь при поиске 
        /// последняя ячейка будет IsWord = false, а значит мы это слово просто не получим и его как бы нет.
        /// </summary>
        /// <param name="key">Удаляемое слово</param>
        public void Remove(string key)
        {
            RemoveNode(key, root);
        }

        private void RemoveNode(string key, Node node)
        {
            if (string.IsNullOrEmpty(key))
            {
                if (node.IsWord)
                {
                    node.IsWord = false;
                }
            }
            else
            {
                var subnode = node.TryFind(key[0]);
                if (subnode != null)
                {
                    Count--;
                    RemoveNode(key.Substring(1), subnode);
                }
            }
        }

        /// <summary>
        /// Попытка найти слово. Этот метод создан СПЕЦИАЛЬНО ТОЛЬКО ДЛЯ ПРЕДЛОЖЕНИЯ ИСКОМЫХ ВАРИАНТОВ.
        /// Он срабатывает когда пользователь начинает вводить символы,
        /// затем метод вернет один из популярных слов, которые есть в префиксном дереве.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="word"></param>
        /// <returns></returns>
        public bool TrySearchWord(string key, ref string word)
        {
             return SearchWord(key, root, ref word);
        }

        private bool SearchWord(string key, Node node, ref string word)
        {
            var result = false;
            if (string.IsNullOrEmpty(key))
            {
                if (node.IsWord)
                {
                     word = node.Prefix;
                     result = true;
                }
                else
                {
                    result = SearchWord(MoreImportantChar(node.SubNodes).ToString(), node, ref word);
                }
            }
            else
            {
                var subnode = node.TryFind(key[0]);
                if (subnode != null)
                {
                    result = SearchWord(key.Substring(1), subnode, ref word);
                }
            }
            return result;
        }

        /// <summary>
        /// Находит ячейку с наибольшим счетчиком (наиболее популярную).
        /// </summary>
        /// <param name="subnode">Словарь в котором будем искать самую часто встречающуюся ячейку</param>
        /// <returns>Символ самой часто встречающуейся ячейки</returns>
        private char MoreImportantChar(Dictionary<char, Node> subnode)
        {
            if (subnode != null)
            {
                char symbol = subnode.First().Value.Symbol;
                var current = subnode.First().Value.Data;
                foreach(var node in subnode)
                {
                    if (node.Value.Data > current)
                    {
                        symbol = node.Value.Symbol;
                        current = node.Value.Data;
                    }
                }
                return symbol;
            }

            return '\0';
        }
    }
}