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
using System.Windows.Shapes;
using System.Printing;

namespace Konvert
{
    
    /// <summary>
    /// Логика взаимодействия для Preview_Form.xaml
    /// </summary>
    public partial class Preview_Form : Window
    {
        private string printerName;
        //readonly StackPanel PrintBox = new();
        //private readonly TextBox[] lineBox;
        public Preview_Form(int envelope_format)
        {
            Inventory.sqlRequest = "SELECT * FROM Recipient WHERE Firm = N'" + Variables.Firm + "'";
            Inventory.FindInTable();
            InitializeComponent();
            TextBox[] lineBox = new TextBox[12];

            switch (envelope_format)
            {
                case 1:
                    
                    break;
                case 2:
                    break;
                case 3:
                    break;
            }
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
        {
            Close();
        }
    }
}
