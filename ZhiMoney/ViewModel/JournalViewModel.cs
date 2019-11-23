using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using ZhiMoney.DataBase;
using ZhiMoney.Model;

namespace ZhiMoney.ViewModel
{
    class JournalViewModel : INotifyPropertyChanged
    {
        private string selecteditem;
        private JournalModel journalModel;

        public List<IInputData> Items { get; set; }

        public ObservableCollection<string> Combobox { get; set; }

        public string SelectedItem
        {
            get => selecteditem;
            set
            {
                selecteditem = value;
                OnPropertyChanged(nameof(SelectedItem));
                UpDateTable();
            }
        }

        public JournalViewModel()
        {
            journalModel = new JournalModel();
            Items = new List<IInputData>();

            Combobox = journalModel.Combobox;
            SelectedItem = Combobox.First();
            
            UpDateTable();
        }
        
        public List<IInputData> FillingTable()
        {
            var result = new List<IInputData>();
            if (SelectedItem == Combobox.First())
            {
                result = journalModel.GetItemsIncomes();
            }
            if(SelectedItem == Combobox.Last())
            {
                result = journalModel.GetItemsExpenses();
            }
            return result;
        }

        private void UpDateTable()
        {
            Items = FillingTable();
        }

        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #endregion
    }
}
