using System.Windows.Input;

namespace Visualizer
{
    class ButtonsViewModel
    {
        public ButtonsViewModel()
        {
            riverHandler = new();

            PreviousButtonCommand = new RelayCommand(PreviousButtonClicked);
            NextButtonCommand = new RelayCommand(NextButtonClicked);
            GenerateButtonCommand = new RelayCommand(GenerateButtonClicked);
            UpdateButtonCommand = new RelayCommand(UpdateButtonClicked);
        }

        private void PreviousButtonClicked()
        {
            currentRiverIndex--;
            if (currentRiverIndex < 0)
            {
                currentRiverIndex = ChartViewModel.riverCount - 1;
            }
            ChartViewModel.UpdateChart(currentRiverIndex);
        }

        private void NextButtonClicked()
        {
            currentRiverIndex++;
            if (currentRiverIndex > ChartViewModel.riverCount)
            {
                currentRiverIndex = 0;
            }
            ChartViewModel.UpdateChart(currentRiverIndex);
        }

        private void GenerateButtonClicked()
        {
            ChartViewModel.GenerateDataPoint(currentRiverIndex);
        }

        private void UpdateButtonClicked()
        {
            ChartViewModel.UpdateChart(currentRiverIndex);
        }

        public ICommand PreviousButtonCommand { get; private set; }
        public ICommand NextButtonCommand { get; private set; }
        public ICommand GenerateButtonCommand { get; private set; }
        public ICommand UpdateButtonCommand { get; private set; }

        private RiverHandler riverHandler;
        private int currentRiverIndex = 0;
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
