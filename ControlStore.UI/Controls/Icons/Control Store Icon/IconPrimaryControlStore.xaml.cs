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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ControlStore.UI.Controls
{
    /// <summary>
    /// Lógica de interacción para IconPrimaryControlStore.xaml
    /// </summary>
    public partial class IconPrimaryControlStore : UserControl
    {
        public IconPrimaryControlStore()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty IconSizeProperty =
    DependencyProperty.Register(
        nameof(IconSize),
        typeof(double),
        typeof(IconPrimaryControlStore),
        new PropertyMetadata(1.0));

        public double IconSize
        {
            get => (double)GetValue(IconSizeProperty);
            set => SetValue(IconSizeProperty, value);
        }
    }
}
