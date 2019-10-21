using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ZhiMoney.Model;

namespace ZhiMoney.ViewModel
{
    class IncomeViewModel : IncomeModel, INotifyPropertyChanged
    {
        private string name;
        private float summa;


        public string Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        public float Summa
        {
            get => summa;
            set
            {
                summa = value;
                OnPropertyChanged(nameof(Summa));
            }
        }
        public new ICommand AddRecord => new DelegateCommand(
                o =>
                {
                    base.AddRecord(Name, Summa);
                },
                c =>
                {
                    //здесь нужна нормальная проверка, а не true
                    //!string.IsNullOrWhiteSpace(Name) && Summa != null
                    return true;
                });


        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
