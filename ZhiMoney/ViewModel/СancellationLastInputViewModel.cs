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
    class СancellationLastInputViewModel : INotifyPropertyChanged
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

        private СancellationLastInputIncomesModel model;

        public СancellationLastInputViewModel()
        {
            model = new СancellationLastInputIncomesModel();
            InstallingTheLastElement();
        }
        public ICommand Cancellation => new DelegateCommand(o =>
        {
            model.DeleteLastElement();
            InstallingTheLastElement();
        });
        private void InstallingTheLastElement()
        {
            if (model.GetLastElement() != null) Name = model.GetLastElement().Name;
            if (model.GetLastElement() != null) Summa = model.GetLastElement().Summa;
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