using System.Windows.Input;

namespace Visualizer
{
    class ButtonsViewModel
    {
        public ButtonsViewModel()
        {
            currentIndex = 0;

            PreviousButtonCommand = new RelayCommand(PreviousButtonClicked);
            NextButtonCommand = new RelayCommand(NextButtonClicked);
            GenerateButtonCommand = new RelayCommand(GenerateButtonClicked);
            UpdateButtonCommand = new RelayCommand(UpdateButtonClicked);
        }

        private void PreviousButtonClicked()
        {
            if (--currentIndex < 0)
            {
                currentIndex = ChartViewModel.riverCount - 1;
            }
            ChartViewModel.UpdateChart(currentIndex);
        }

        private void NextButtonClicked()
        {
            if (++currentIndex >= ChartViewModel.riverCount)
            {
                currentIndex = 0;
            }
            ChartViewModel.UpdateChart(currentIndex);
        }

        private void GenerateButtonClicked()
        {
            ChartViewModel.GenerateDataPoint(currentIndex);
        }

        private void UpdateButtonClicked()
        {
            ChartViewModel.UpdateChart(currentIndex);
        }

        public ICommand PreviousButtonCommand { get; private set; }
        public ICommand NextButtonCommand { get; private set; }
        public ICommand GenerateButtonCommand { get; private set; }
        public ICommand UpdateButtonCommand { get; private set; }

        private int currentIndex { get; set; }
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
