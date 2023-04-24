using System.Windows;
using System.Windows.Controls;
using System.Printing;

namespace Konvert
{
    /// <summary>
    /// Логика взаимодействия для BigForm.xaml
    /// </summary>
    public partial class BigForm : Window
    {
        private readonly Inventory KonvertBisness = new(); // Подключение класса Inventory
        public BigForm()
        {
            InitializeComponent();
        }

        private void BtnPrint_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog printDialog = new();
            printDialog.PrintQueue = new PrintQueue(new PrintServer(), KonvertBisness.defaultPrinterName);
            printDialog.PrintTicket.PageOrientation = PageOrientation.Landscape;
            printDialog.PrintTicket.PageMediaSize = new PageMediaSize(612, 869);
            printDialog.PrintVisual(PrintBox, "Print");
            DialogResult = true;
            
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        { DialogResult = false; }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
