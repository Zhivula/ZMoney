using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ZhiMoney.Model;

namespace ZhiMoney.ViewModel
{
    class IncomeViewModel : IncomeModel, INotifyPropertyChanged
    {
        private string name;
        private float summa;
        private bool canNameRecord;
        private bool canSummaRecord;

        public string Name
        {
            get => name;
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    name = value;
                    canNameRecord = true;
                }
                else
                {
                    canNameRecord = false;
                }
                OnPropertyChanged(nameof(Name));
            }
        }
        public float Summa
        {
            get => summa;
            set
            {
                base.ParseSummaToSingle(value.ToString(),out summa,out canSummaRecord);
                OnPropertyChanged(nameof(Summa));
            }
        }
        public new ICommand AddRecord => new DelegateCommand(o =>
        {
            if (canNameRecord && canSummaRecord) base.AddRecord(Name, Summa);
            else MessageBox.Show("Некорректные данные.");
        });
        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #endregion
    }
}