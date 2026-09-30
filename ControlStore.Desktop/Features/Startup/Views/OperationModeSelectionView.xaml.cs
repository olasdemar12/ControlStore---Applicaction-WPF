using ControlStore.Desktop.Features.Startup.ViewModels.OperationModeSelection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ControlStore.Desktop.Features.Startup.Views
{
    /// <summary>
    /// Lógica de interacción para OperationModeSelectionView.xaml
    /// </summary>
    public partial class OperationModeSelectionView : Window
    {
        public OperationModeSelectionView()
        {
            InitializeComponent();
            Width = SystemParameters.PrimaryScreenWidth * 0.8;
            Height = SystemParameters.PrimaryScreenHeight * 0.8;
            var closeWindow = () => { 
                //Abrir una ventana antes de cerrar la actual:

                this.Close(); };
            this.DataContext = new OperationModeSelectionViewModel(closeWindow);
        }

        private void CloseButtonAction(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void MinimizeButtonAction(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
    }
}
