using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MaterialDesignThemes.Wpf;

namespace ControlStore.Desktop.Features.Startup.ViewModels.OperationModeSelection
{
    public partial class OperationModeSelectionViewModel : ObservableObject
    {
        public OperationModeSelectionViewModel(Action actionClose)
        {
            _actionClose = actionClose;
        }
        private Action _actionClose;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(ContinueCommand))]
        private bool _selectedServerMode = false;
        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(ContinueCommand))]
        private bool _selectedClientMode = false;

        [RelayCommand]
        private void SelectServerMode()
        {
            SelectedServerMode = true;
            SelectedClientMode = false;
        }

        [RelayCommand]
        private void SelectClientMode()
        {
            SelectedServerMode = false;
            SelectedClientMode = true;
        }

        [RelayCommand(CanExecute = nameof(CanSelectionMode))]
        private void Continue()
        {
            _actionClose?.Invoke();
            MessageBox.Show($"Selected Mode: {(SelectedServerMode ? "Server" : "Client")}");
        }

        private bool CanSelectionMode()
        {
            return SelectedServerMode || SelectedClientMode;
        }

    }
}
