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
    /// Lógica de interacción para InformationCard.xaml
    /// </summary>
    public partial class InformationCard : UserControl
    {
        public InformationCard()
        {
            InitializeComponent();
        }


        public static readonly DependencyProperty IconProperty =
            DependencyProperty.Register(
                nameof(Icon),
                typeof(PackIconKind),
                typeof(InformationCard),
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
            typeof(InformationCard),
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
            typeof(InformationCard),
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
                typeof(InformationCard),
                new PropertyMetadata(1.0));

        public double ScaleInformation
        {
            get => (double)GetValue(ScaleInformationProperty);
            set => SetValue(ScaleInformationProperty, value);
        }

    }

}
