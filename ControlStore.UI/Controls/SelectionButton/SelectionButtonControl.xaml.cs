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
    /// Lógica de interacción para SelectionButtonControl.xaml
    /// </summary>
    public partial class SelectionButtonControl : UserControl
    {
        public SelectionButtonControl()
        {
            InitializeComponent();
        }


        public static readonly DependencyProperty IconProperty =
            DependencyProperty.Register(
                nameof(Icon),
                typeof(PackIconKind),
                typeof(SelectionButtonControl),
                new PropertyMetadata(PackIconKind.None));

        public PackIconKind Icon
        {
            get => (PackIconKind)GetValue(IconProperty);
            set => SetValue(IconProperty, value);
        }


        public static readonly DependencyProperty TitleTextProperty =
        DependencyProperty.Register(
            nameof(TitleText),
            typeof(string),
            typeof(SelectionButtonControl),
            new PropertyMetadata("Texto del titulo"));

        public string TitleText
        {
            get => (string)GetValue(TitleTextProperty);
            set => SetValue(TitleTextProperty, value);
        }

        public static readonly DependencyProperty DescriptionTextProperty =
        DependencyProperty.Register(
            nameof(DescriptionText),
            typeof(string),
            typeof(SelectionButtonControl),
            new PropertyMetadata("Texto de descripcion"));

        public string DescriptionText
        {
            get => (string)GetValue(DescriptionTextProperty);
            set => SetValue(DescriptionTextProperty, value);
        }

        public static readonly DependencyProperty ScaleInformationProperty =
    DependencyProperty.Register(
        nameof(ScaleInformation),
        typeof(double),
        typeof(SelectionButtonControl),
        new PropertyMetadata(1.0));

        public double ScaleInformation
        {
            get => (double)GetValue(ScaleInformationProperty);
            set => SetValue(ScaleInformationProperty, value);
        }


        public static readonly DependencyProperty IsCheckProperty =
    DependencyProperty.Register(
        nameof(IsCheck),
        typeof(bool),
        typeof(SelectionButtonControl),
        new PropertyMetadata(false));

        public bool IsCheck
        {
            get => (bool)GetValue(IsCheckProperty);
            set => SetValue(IsCheckProperty, value);
        }

        public static readonly DependencyProperty CommandProperty =
    DependencyProperty.Register(
        nameof(Command),
        typeof(ICommand),
        typeof(SelectionButtonControl),
        new PropertyMetadata(null));

        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }
    }
}
