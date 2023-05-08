using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Konvert
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class StartForm : Window
    {
        private readonly Inventory KonvertBisness = new(); // Подключение класса Inventory
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
