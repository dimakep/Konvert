using System.Windows;
using System.Windows.Controls;
using System.Printing;
using System;

namespace Konvert
{
    /// <summary>
    /// Логика взаимодействия для BigForm.xaml
    /// </summary>
    public partial class Small1Form : Window
    {
        private readonly string printerName;

        public Small1Form()
        {
            InitializeComponent();
            /*
            RecipientBox.Text = firm;
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
            */
        }
        private void BtnPrint_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog printDialog = new();
            printDialog.PrintQueue = new PrintQueue(new PrintServer(), printerName);
            printDialog.PrintTicket.PageOrientation = PageOrientation.Landscape;
            printDialog.PrintTicket.PageResolution = new PageResolution(96, 96);
            Size pageSize = new(printDialog.PrintableAreaWidth, printDialog.PrintableAreaHeight);
            PrintBox.Measure(pageSize);
            PrintBox.Arrange(new Rect(pageSize.Width - PrintBox.DesiredSize.Width, (pageSize.Height - PrintBox.DesiredSize.Height) / 2,
                                        PrintBox.DesiredSize.Width,
                                        PrintBox.DesiredSize.Height));
            printDialog.PrintVisual(PrintBox, "Print");
            DialogResult = true;
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        { Close(); }
    }
}
