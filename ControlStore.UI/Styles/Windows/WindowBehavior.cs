using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace ControlStore.UI.Styles.Windows
{
    public static class WindowBehavior
    {
        public static readonly DependencyProperty IsDraggableProperty =
            DependencyProperty.RegisterAttached(
                "IsDraggable",
                typeof(bool),
                typeof(WindowBehavior),
                new PropertyMetadata(false, OnIsDraggableChanged));

        public static bool GetIsDraggable(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsDraggableProperty);
        }

        public static void SetIsDraggable(DependencyObject obj, bool value)
        {
            obj.SetValue(IsDraggableProperty, value);
        }

        private static void OnIsDraggableChanged(
            DependencyObject d,
            DependencyPropertyChangedEventArgs e)
        {
            if (d is not Window window)
                return;

            if ((bool)e.NewValue)
            {
                window.MouseLeftButtonDown += Window_MouseLeftButtonDown;
            }
            else
            {
                window.MouseLeftButtonDown -= Window_MouseLeftButtonDown;
            }
        }

        private static void Window_MouseLeftButtonDown(
            object sender,
            MouseButtonEventArgs e)
        {
            if (sender is Window window &&
                e.LeftButton == MouseButtonState.Pressed)
            {
                window.DragMove();
            }
        }
    }
}
