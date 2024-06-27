using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Visualizer
{
    class ButtonsViewModel
    {
        public ButtonsViewModel()
        {
            PreviousButtonCommand = new RelayCommand(PreviousButtonClicked);
            NextButtonCommand = new RelayCommand(NextButtonClicked);
            GenerateButtonCommand = new RelayCommand(GenerateButtonClicked);
            UpdateButtonCommand = new RelayCommand(UpdateButtonClicked);
        }

        private void PreviousButtonClicked()
        {
        }

        private void NextButtonClicked()
        {
        }

        private void GenerateButtonClicked()
        {
        }

        private void UpdateButtonClicked()
        {
        }

        public ICommand PreviousButtonCommand { get; private set; }
        public ICommand NextButtonCommand { get; private set; }
        public ICommand GenerateButtonCommand { get; private set; }
        public ICommand UpdateButtonCommand { get; private set; }
    }

    public class RelayCommand : ICommand
    {
        private readonly Action _execute;

        public RelayCommand(Action execute)
        {
            _execute = execute;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            _execute();
        }
    }
}
