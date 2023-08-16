using System;
using System.Windows;

namespace Konvert
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class StartForm : Window
    {
        public StartForm()
        {
            InitializeComponent();
        }
        private void HyperExit_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }
        private void HiperAdd_Click(object sender, RoutedEventArgs e)
        {
            AddForm addForm = new();
            addForm.Show();
            Close();
        }
        private void HyperPrint_Click(object sender, RoutedEventArgs e)
        {
            PrintForm printForm = new();
            printForm.Show();
            Close();
        }
    }
}
