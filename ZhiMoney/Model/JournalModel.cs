﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using ZhiMoney.DataBase;

namespace ZhiMoney.Model
{
    class JournalModel
    {
        public ObservableCollection<string> Combobox;

        public JournalModel()
        {
            Combobox = new ObservableCollection<string>()
            {
                 "   Доходы" ,
                 "   Расходы"
            };
        }

        ///Подключение к базе данных 
        ///Добавление в ItemsItemsIncomes данных Income
        public List<IInputData> GetItemsIncomes()
        {
            var ItemsIncomes = new List<IInputData>();
            using (var context = new MyDbContext())
            {
                ItemsIncomes.AddRange(context.Incomes);
            }
            return ItemsIncomes;
        }

        ///Подключение к базе данных 
        ///Добавление в ItemsItemsExpenses данных Expense
        public List<IInputData> GetItemsExpenses()
        {
            var ItemsExpense = new List<IInputData>();
            using (var context = new MyDbContext())
            {
                ItemsExpense.AddRange(context.Expenses);
            }
            return ItemsExpense;
        }
    }
}
