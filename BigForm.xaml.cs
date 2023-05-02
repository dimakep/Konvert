using System.Windows;
using System.Windows.Controls;
using System.Printing;
using System;

namespace Konvert
{
    /// <summary>
    /// Логика взаимодействия для BigForm.xaml
    /// </summary>
    public partial class BigForm : Window
    {
        private readonly Inventory KonvertBisness = new(); // Подключение класса Inventory
        string printerName;

        public BigForm(string firm, int index, string region, string area, string city, string street,
            string home, string frame, string structure, string flat, string printer)
        {
            InitializeComponent();
            RecipientBox.Text = firm;
            IndexBox.Text = Convert.ToString(index);
            RegionBox.Text = region + " " + area;
            CityBox.Text = city + " " + street;
            HomeBox.Text += home;
            FrameBox.Text += frame;
            StructureBox.Text += structure;
            FlatBox.Text += flat;
            printerName = printer;
        }

        private void BtnPrint_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog printDialog = new();
            printDialog.PrintQueue = new PrintQueue(new PrintServer(), printerName);
            printDialog.PrintTicket.PageOrientation = PageOrientation.Landscape;
            printDialog.PrintTicket.PageMediaSize = new PageMediaSize(612, 869);
            printDialog.PrintVisual(PrintBox, "Print");
            DialogResult = true;
            
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        { Close(); }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
