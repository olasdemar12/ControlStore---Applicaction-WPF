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
            var closeWindow = () => { 
                //Abrir una ventana antes de cerrar la actual:

                this.Close(); };
            this.DataContext = new OperationModeSelectionViewModel(closeWindow);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
