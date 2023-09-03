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
using MaterialDesignThemes;

namespace Konvert
{
    /// <summary>
    /// Логика взаимодействия для Preview_Form.xaml
    /// </summary>
    /// 
    
    public partial class Preview_Form : Window
    {
        //private readonly double[,] small1 = new double[2,14];
        private string printerName;
        private readonly Canvas PrintBox = new();

        //private readonly TextBox[] lineBox;
        private readonly double px = 37.795275591;
        public Preview_Form(int envelopeFormat)
        {
            double[] x1 = { 1, 8.5, 1, 2.8, 2.6, 2.4, 3, 0, 0, 0, 0, 0, 0, 0, 0 };
            double[] x2 = { 0.6, 8.5, 1.3, 2.8, 2.6, 2.4, 3, 0, 0, 0, 0, 0, 0, 0 };
            double[] x3 = { 1.5, 8.5, 1.3, 10, 1.6, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            double[] small1 = { 1.1, 0.5, 1, 1.5, 2.5, 3.7, 4.2, 4.7, 5.2, 5.7, 6.25, 6.8, 7.35, 7.9, 8.9 };
            double[] small2 = { 0.7, 0.7, 1.4, 2.1, 2.8, 4.2, 4.8, 5.4, 6, 7.4, 8.1, 8.8, 9.4, 0.6 };
            double[] big = { 2, 0.7, 1.4, 2.1, 2.7, 3.4, 7.6, 8.3, 8.9, 9.6, 10.3, 10.9, 11.5, 12.2, 14.5 };
            Inventory.sqlRequest = "SELECT * FROM Recipient WHERE Firm = N'" + Variables.Firm + "'";
            Inventory.FindInTable();
            InitializeComponent();
            TextBox[] lineBox = new TextBox[14];
            switch (envelopeFormat)
            {
                case 1: // 1 Маленький конверт
                    double lineLeft = px * small1[1];
                    double lineTop = px * small1[1];
                    PreviewForm.Width = (px * 22) + 10;
                    PreviewForm.Height = (px * 11) + 90;

                    //Canvas PrintBox = new();
                    PrintBox = new();
                    PrintBox.Width = px * 22;
                    PrintBox.Height = px * 11;
                    PrintBox.Background = Brushes.White;
                    Grid.SetColumn(PrintBox, 0);
                    Grid.SetRow(PrintBox, 0);
                    Grid.SetColumnSpan(PrintBox, 3);

                    
                    lineBox[0] = new();
                    lineBox[0].Text = "ООО ``ГИПАР``";
                    lineBox[0].BorderThickness = new Thickness(0);
                    Canvas.SetLeft(lineBox[0], px * 2.5);
                    Canvas.SetTop(lineBox[0], px * small1[0]);

                    lineBox[1] = new();
                    lineBox[1].Text = "Смоленская обл., г. Вязьма";
                    lineBox[1].BorderThickness = new Thickness(0);
                    Canvas.SetLeft(lineBox[1], px * 2.5);
                    Canvas.SetTop(lineBox[1], px * (small1[0] + small1[1]));

                    lineBox[2] = new();
                    lineBox[2].Text = "ул. Репина д. 16 оф. 4";
                    lineBox[2].BorderThickness = new Thickness(0);
                    Canvas.SetLeft(lineBox[2], px * 2.5);
                    Canvas.SetTop(lineBox[2], px * (small1[0] + small1[2]));

                    lineBox[3] = new();
                    lineBox[3].Text = "215110";
                    lineBox[3].BorderThickness = new Thickness(0);
                    Canvas.SetLeft(lineBox[3], px * 1.5);
                    Canvas.SetTop(lineBox[3], px * (small1[0] + small1[3]));

                    lineBox[4] = new();
                    lineBox[4].Text = "КОМУ";
                    lineBox[4].BorderThickness = new Thickness(0);
                    Canvas.SetLeft(lineBox[4], px * 11);
                    Canvas.SetTop(lineBox[4], px * (small1[0] + small1[5]));

                    lineBox[5] = new();
                    lineBox[5].Text = "УЛИЦА";
                    lineBox[5].BorderThickness = new Thickness(0);
                    Canvas.SetLeft(lineBox[5], px * 11);
                    Canvas.SetTop(lineBox[5], px * (small1[0] + small1[7]));

                    lineBox[6] = new();
                    lineBox[6].Text = "ДОМ";
                    lineBox[6].BorderThickness = new Thickness(0);
                    Canvas.SetLeft(lineBox[6], px * 10);
                    Canvas.SetTop(lineBox[6], px * (small1[0] + small1[8]));

                    lineBox[7] = new();
                    lineBox[7].Text = "КОРПУС";
                    lineBox[7].BorderThickness = new Thickness(0);
                    Canvas.SetLeft(lineBox[7], px * 12.8);
                    Canvas.SetTop(lineBox[7], px * (small1[0] + small1[8]));

                    lineBox[8] = new();
                    lineBox[8].Text = "СТРОЕНИЕ";
                    lineBox[8].BorderThickness = new Thickness(0);
                    Canvas.SetLeft(lineBox[8], px * 15.4);
                    Canvas.SetTop(lineBox[8], px * (small1[0] + small1[8]));

                    lineBox[9] = new();
                    lineBox[9].Text = "КВАРТИРА";
                    lineBox[9].BorderThickness = new Thickness(0);
                    Canvas.SetLeft(lineBox[9], px * 17.8);
                    Canvas.SetTop(lineBox[9], px * (small1[0] + small1[8]));

                    lineBox[10] = new();
                    lineBox[10].Text = "ГОРОД";
                    lineBox[10].BorderThickness = new Thickness(0);
                    Canvas.SetLeft(lineBox[10], px * 12);
                    Canvas.SetTop(lineBox[10], px * (small1[0] + small1[10]));

                    lineBox[11] = new();
                    lineBox[11].Text = "РАЙОН";
                    lineBox[11].BorderThickness = new Thickness(0);
                    Canvas.SetLeft(lineBox[11], px * 11);
                    Canvas.SetTop(lineBox[11], px * (small1[0] + small1[11]));

                    lineBox[12] = new();
                    lineBox[12].Text = "ОБЛАСТЬ";
                    lineBox[12].BorderThickness = new Thickness(0);
                    Canvas.SetLeft(lineBox[12], px * 12);
                    Canvas.SetTop(lineBox[12], px * (small1[0] + small1[12]));

                    lineBox[13] = new();
                    lineBox[13].Text = "215110";
                    lineBox[13].BorderThickness = new Thickness(0);
                    Canvas.SetLeft(lineBox[13], px * 10);
                    Canvas.SetTop(lineBox[13], px * (small1[0] + small1[13]));

                    PrintBox.Children.Add(lineBox[0]);
                    PrintBox.Children.Add(lineBox[1]);
                    PrintBox.Children.Add(lineBox[2]);
                    PrintBox.Children.Add(lineBox[3]);
                    PrintBox.Children.Add(lineBox[4]);
                    PrintBox.Children.Add(lineBox[5]);
                    PrintBox.Children.Add(lineBox[6]);
                    PrintBox.Children.Add(lineBox[7]);
                    PrintBox.Children.Add(lineBox[8]);
                    PrintBox.Children.Add(lineBox[9]);
                    PrintBox.Children.Add(lineBox[10]);
                    PrintBox.Children.Add(lineBox[11]);
                    PrintBox.Children.Add(lineBox[12]);
                    PrintBox.Children.Add(lineBox[13]);


                    Main_Grid.Children.Add(PrintBox);
                    break;

                case 2: // 2 Маленький конверт
                    PreviewForm.Width = (px * 22) + 10;
                    PreviewForm.Height = (px * 11) + 90;
                    PrintBox = new();
                    PrintBox.Width = px * 22;
                    PrintBox.Height = px * 11;
                    PrintBox.Background = Brushes.White;
                    Grid.SetColumn(PrintBox, 0);
                    Grid.SetRow(PrintBox, 0);
                    Grid.SetColumnSpan(PrintBox, 3);

                    TextBox line1 = new();
                    line1.FontSize = 14;
                    line1.Text = "ООО ``ГИПАР``";
                    line1.BorderThickness = new Thickness(0);
                    Canvas.SetLeft(line1, px * 2.5);
                    Canvas.SetTop(line1, px * 1.1);

                    TextBox line2 = new();
                    line1.FontSize = 14;
                    line2.Text = "Смоленская обл., г. Вязьма";
                    line2.BorderThickness = new Thickness(0);
                    Canvas.SetLeft(line2, px * 2.5);
                    Canvas.SetTop(line2, px * 1.6);

                    TextBox line3 = new();
                    line1.FontSize = 14;
                    line3.Text = "ул. Репина д. 16 оф. 4";
                    line3.BorderThickness = new Thickness(0);
                    Canvas.SetLeft(line3, px * 2.5);
                    Canvas.SetTop(line3, px * 2.1);

                    TextBox line4 = new();
                    line1.FontSize = 14;
                    line4.Text = "215110";
                    line4.BorderThickness = new Thickness(0);
                    Canvas.SetLeft(line4, px * 1.5);
                    Canvas.SetTop(line4, px * 3.1);

                    PrintBox.Children.Add(line1);
                    PrintBox.Children.Add(line2);
                    PrintBox.Children.Add(line3);
                    PrintBox.Children.Add(line4);


                    Main_Grid.Children.Add(PrintBox);
                    break;

                case 3: //Бльшой конверт
                    PreviewForm.Width = (px * 23) + 10;
                    PreviewForm.Height = (px * 16.2) + 90;
                    PrintBox = new();
                    PrintBox.Width = px * 23;
                    PrintBox.Height = px * 16.2;
                    PrintBox.Background = Brushes.White;
                    Grid.SetColumn(PrintBox, 0);
                    Grid.SetRow(PrintBox, 0);
                    Grid.SetColumnSpan(PrintBox, 3);
                    Grid gridStack2 = new();
                    gridStack2.ShowGridLines = true;

                    line1 = new();
                    line1.FontSize = 14;
                    line1.Text = "ООО ``ГИПАР``";
                    line1.BorderThickness = new Thickness(0);
                    Canvas.SetLeft(line1, px * 2.5);
                    Canvas.SetTop(line1, px * 1.1);

                    line2 = new();
                    line1.FontSize = 14;
                    line2.Text = "Смоленская обл., г. Вязьма";
                    line2.BorderThickness = new Thickness(0);
                    Canvas.SetLeft(line2, px * 2.5);
                    Canvas.SetTop(line2, px * 1.6);

                    line3 = new();
                    line1.FontSize = 14;
                    line3.Text = "ул. Репина д. 16 оф. 4";
                    line3.BorderThickness = new Thickness(0);
                    Canvas.SetLeft(line3, px * 2.5);
                    Canvas.SetTop(line3, px * 2.1);

                    line4 = new();
                    line1.FontSize = 14;
                    line4.Text = "215110";
                    line4.BorderThickness = new Thickness(0);
                    Canvas.SetLeft(line4, px * 1.5);
                    Canvas.SetTop(line4, px * 3.1);

                    PrintBox.Children.Add(line1);
                    PrintBox.Children.Add(line2);
                    PrintBox.Children.Add(line3);
                    PrintBox.Children.Add(line4);

                    Main_Grid.Children.Add(PrintBox);
                    break;
                default:
                    break;
            }
        }

        void GridColumn(object name, int x)
        {
            
        }
        private void BtnPrint_Click(object sender, RoutedEventArgs e)
        {
            /*
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
            */
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
