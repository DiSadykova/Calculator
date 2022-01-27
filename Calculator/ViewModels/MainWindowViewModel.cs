using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;


namespace Calculator.ViewModels
{
    class MainWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanfed([CallerMemberName] string PropertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }
        private string exp;
        public string Exp
        {
            get => exp;
            set
            {
                exp = value;
            }
        }
        private List<double> varList;
        public List<double> VarList
        {
            get => varList;
            set
            {
                varList = value;
            }
        }
        
        private List<char> opList;
        public List<char> OpList
        {
            get => opList;
            set
            {
                opList = value;
            }
        }

        private double result;
        public double Result
        {
            get => result;
            set
            {
                result = value;
            }
        }
        public ICommand AddCommand {get;}
        private void OnAddCommandExecute(object p)
        {
            
        }
        private bool CanAddCommandExecuted(object p)
        {
            if (Exp!="")
            {
                return true;
            }
            else
                return false;
        }
        public MainWindowViewModel()
        {
            AddCommand = new RelayCommand(OnAddCommandExecute, CanAddCommandExecuted);
        }

    }
}
