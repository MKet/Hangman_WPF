using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Hangman_MVVM
{
    class WPFMessenger : IMessengerService
    {
        public void Debug(string message)
        {
            System.Diagnostics.Debug.Print(message);
        }

        public void Error(string message)
        {
            MessageBox.Show(message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public void Info(string message)
        {
            MessageBox.Show(message, "Info", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
