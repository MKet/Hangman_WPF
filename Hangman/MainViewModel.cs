using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Hangman_MVVM
{
    class MainViewModel : ViewModel
    {
        private HangmanViewModel hangmanVM;
        private DatamanagerViewModel dataVM;

        private ViewModel selectedViewModel;
        public ViewModel SelectedViewModel
        {
            get
            {
                return selectedViewModel;
            }
            set
            {
                if (selectedViewModel != value)
                {
                    selectedViewModel = value;
                    InvokePropertyChanged(this, nameof(SelectedViewModel));
                }
            }
        }

        private RelayCommand gameNavCommand;
        public ICommand GameNavCommand => gameNavCommand;

        private RelayCommand wordNavCommand;
        public ICommand WordNavCommand => wordNavCommand;

        public MainViewModel(IDataManager dataManager, IMessengerService messenger)
        {
            hangmanVM = new HangmanViewModel(dataManager, messenger);
            hangmanVM.dataManager = dataManager;
            hangmanVM.messenger = messenger;

            dataVM = new DatamanagerViewModel(dataManager);

            SelectedViewModel = hangmanVM;

            gameNavCommand = new RelayCommand(SwitchToGame, () => true);
            wordNavCommand = new RelayCommand(SwitchToDataManager, () => true);
        }
        
        private void SwitchToDataManager()
        {
            SelectedViewModel = dataVM;
        }

        private void SwitchToGame()
        {
            SelectedViewModel = hangmanVM;
        }

    }
}
