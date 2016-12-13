using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Hangman_MVVM
{
    abstract class ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void InvokePropertyChanged<T>(T sender, string name)
        {
            PropertyChanged?.Invoke(sender, new PropertyChangedEventArgs(name));
        }
        
        protected void InvokePropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        protected bool _Ready = false;
        public bool Ready
        {
            get
            {
                return _Ready;
            }
            protected set
            {
                if (_Ready != value)
                {
                    _Ready = value;
                    InvokePropertyChanged();
                    InvokePropertyChanged(this, nameof(Busy));
                }
            }
        }

        public bool Busy => !Ready;

        protected class RelayCommand : ICommand
        {
            private Action WhattoExecute;
            private Func<bool> WhentoExecute;
            public RelayCommand(Action WhattoExecute, Func<bool> WhentoExecute)
            {
                this.WhattoExecute = WhattoExecute;
                this.WhentoExecute = WhentoExecute;
            }

            public event EventHandler CanExecuteChanged;

            public void ExecuteChangedInvoke()
            {
                CanExecuteChanged?.Invoke(this, new EventArgs());
            }

            public bool CanExecute(object parameter) => WhentoExecute();
            public void Execute(object parameter) => WhattoExecute();
        }
    }
}
