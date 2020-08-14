using System;
using System.Windows;
using wpfpslib;

namespace WPFTestApplication
{
    public partial class MainWindow
        : Window
    {
        public MainWindow() => InitializeComponent();

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            PresentationSource presentationSource = PresentationSource.FromVisual(this);

            presentationSource.ContentRendered += MainWindow_ContentRendered;
        }

        private void MainWindow_ContentRendered(object sender, EventArgs e)
        {
            (sender as PresentationSource).ContentRendered -= MainWindow_ContentRendered;
            (grid_bbh.Effect as BlurBehindEffect).FrameworkElement = grid_bbh;
            (gb1.Effect as NormalMapEffect).Range = 1 / gb1.ActualWidth;
        }
    }
}
