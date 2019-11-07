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
        public List<Income> ItemsIncomes { get; set; }
        public ObservableCollection<Expense> ItemsExpense { get; set; }
        public JournalModel()
        {
            ItemsIncomes = new List<Income>();
            ItemsExpense = new ObservableCollection<Expense>();
            ///Подключение к базе данных 
            ///Добавление в Items данных Income и Expense
            using (var context = new MyDbContext())
            {
                foreach (Income i in context.Incomes) ItemsIncomes.Add(i);
                foreach (Expense i in context.Expenses) ItemsExpense.Add(i);
            }
        }
    }
}