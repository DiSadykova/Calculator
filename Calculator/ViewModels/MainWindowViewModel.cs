using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Calculator.Models;


namespace Calculator.ViewModels
{
    class MainWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged([CallerMemberName] string PropertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }
        private readonly CalculatorModel model;

        private string exp;
        public string Exp
        {
            get => model.Exp;
            set
            {
                exp = value;
            }
        }

        private string result;
        public string Result
        {
            get => model.Result;
            set
            {
                result = value;
            }
        }


        public ICommand InsertCommand => new RelayCommand(o => Insert((string)o));
        public void Insert(string digit)
        {
            model.Insert(digit);
            OnPropertyChanged(nameof(Exp));
            OnPropertyChanged(nameof(Result));
        }
        public ICommand InsertOperationCommand => new RelayCommand(o => InsertOperation((Operations)o));
        public void InsertOperation(Operations operation)
        {
            model.InsertOperation(operation);
            OnPropertyChanged(nameof(Exp));
            OnPropertyChanged(nameof(Result));
        }

        public ICommand AddCommand { get; }
        private void OnAddCommandExecute(object p)
        {
            
        }

        private bool CanAddCommandExecuted(object p)
        {
            if (model.BracketCheck())
                return true;
            else
                return false;

            if (model.OperationCheck())
                return true;
            else
                return false;
        }

        public MainWindowViewModel()
        {
            AddCommand = new RelayCommand(OnAddCommandExecute, CanAddCommandExecuted);
            model = new CalculatorModel();
        }

    }
}
