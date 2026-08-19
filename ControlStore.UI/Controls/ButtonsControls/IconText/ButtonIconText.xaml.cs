using MaterialDesignThemes.Wpf;
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
    /// Lógica de interacción para ButtonIconText.xaml
    /// </summary>
    public partial class ButtonIconText : UserControl
    {
        public ButtonIconText()
        {
            InitializeComponent();
        }


        public static readonly DependencyProperty IconProperty =
            DependencyProperty.Register(
                nameof(IconButton),
                typeof(PackIconKind),
                typeof(ButtonIconText),
                new PropertyMetadata(PackIconKind.Plus));

        public PackIconKind IconButton
        {
            get => (PackIconKind)GetValue(IconProperty);
            set => SetValue(IconProperty, value);
        }

        public static readonly DependencyProperty IconSizeProperty =
            DependencyProperty.Register(
                nameof(IconSize),
                typeof(double),
                typeof(ButtonIconText),
                new PropertyMetadata(30.0));
        public double IconSize
        {
            get => (double)GetValue(IconSizeProperty);
            set => SetValue(IconSizeProperty, value);
        }

        public static readonly DependencyProperty TextButtonProperty =
            DependencyProperty.Register(
                nameof(TextButton),
                typeof(string),
                typeof(ButtonIconText),
                new PropertyMetadata("Texto del boton"));

        public string TextButton
        {
            get => (string)GetValue(TextButtonProperty);
            set => SetValue(TextButtonProperty, value);
        }

        public static readonly DependencyProperty StyleButtonProperty =
    DependencyProperty.Register(
        nameof(StyleButton),
        typeof(Style),
        typeof(ButtonIconText),
        new PropertyMetadata(null));

        public Style StyleButton
        {
            get => (Style)GetValue(StyleButtonProperty);
            set => SetValue(StyleButtonProperty, value);
        }
    }
}
