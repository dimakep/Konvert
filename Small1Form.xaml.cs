using System.Windows;
using System.Windows.Controls;
using System.Printing;
using System.Windows.Input;
using System;

namespace Konvert
{
    /// <summary>
    /// Логика взаимодействия для BigForm.xaml
    /// </summary>
    public partial class Small1Form : Window
        
    {
        private string printerName;
        private readonly Inventory KonvertBisness = new(); // Подключение класса Inventory
        public Small1Form(string firm, int index, string region, string area, string city, string street, 
            string home, string frame, string structure, string flat, string printer)
        {
            InitializeComponent();
            RecipientBox.Text =firm;
            IndexBox.Text = Convert.ToString(index);
            RegionBox.Text = region;
            AreaBox.Text = area;
            CityBox.Text = city;
            StreetBox.Text = street;
            HomeBox.Text = home;
            FrameBox.Text = frame;
            StructureBox.Text = structure;
            FlatBox.Text = flat;
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
    }
}
