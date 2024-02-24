using System;
using System.Windows;
using System.Windows.Controls;
using System.Printing;

namespace Konvert
{
    /// <summary>
    /// Логика взаимодействия для Preview_Form.xaml
    /// </summary>
    /// 

    public partial class PreviewForm : Window
    {
        private readonly Canvas PrintBox = new();
        private readonly double px = 37.795275591;
        public PreviewForm(int envelopeFormat)
        {
            InventoryLite.sqlRequest = "SELECT * FROM Recipient WHERE Firm = N'" + Variables.Firm + "'";
            InventoryLite.FindInTable();
            InitializeComponent();
            TextBox[] lineBox = new TextBox[14];
            switch (envelopeFormat)
            {
                case 1: // 1 Маленький конверт E65
                    {
                        double[] e65_1 = { 0.9, 1.4, 1.9, 2.8, 4.7, 5.7, 6.2, 7.2, 7.8, 8.3, 9.1};

                        Preview_Form.Width = (px * 22) + 10;
                        Preview_Form.Height = (px * 11) + 90;

                        //Canvas PrintBox = new();
                        PrintBox = new();
                        PrintBox.Width = 831.49606299;
                        PrintBox.Height = 415.7480315;
                        Grid.SetColumn(PrintBox, 0);
                        Grid.SetRow(PrintBox, 0);
                        Grid.SetColumnSpan(PrintBox, 3);

                        lineBox[0] = new();
                        lineBox[0].Text = "ООО ``ГИПАР``";
                        lineBox[0].BorderThickness = new Thickness(0);
                        lineBox[0].FontSize = 14;
                        Canvas.SetLeft(lineBox[0], px * 2.5);
                        Canvas.SetTop(lineBox[0], px * e65_1[0]);
                        PrintBox.Children.Add(lineBox[0]);

                        lineBox[1] = new();
                        lineBox[1].Text = "Смоленская обл., г. Вязьма";
                        lineBox[1].BorderThickness = new Thickness(0);
                        lineBox[1].FontSize = 14;
                        Canvas.SetLeft(lineBox[1], px * 2.5);
                        Canvas.SetTop(lineBox[1], px * e65_1[1]);
                        PrintBox.Children.Add(lineBox[1]);

                        lineBox[2] = new();
                        lineBox[2].Text = "ул. Репина д. 16 оф. 4";
                        lineBox[2].BorderThickness = new Thickness(0);
                        lineBox[2].FontSize = 14;
                        Canvas.SetLeft(lineBox[2], px * 1.5);
                        Canvas.SetTop(lineBox[2], px * e65_1[2]);
                        PrintBox.Children.Add(lineBox[2]);

                        lineBox[3] = new();
                        lineBox[3].Text = "215110";
                        lineBox[3].BorderThickness = new Thickness(0);
                        lineBox[3].FontSize = 14;
                        Canvas.SetLeft(lineBox[3], px * 2);
                        Canvas.SetTop(lineBox[3], px * e65_1[3]);
                        PrintBox.Children.Add(lineBox[3]);

                        lineBox[4] = new();
                        lineBox[4].Text = Variables.Firm;
                        lineBox[4].BorderThickness = new Thickness(0);
                        lineBox[4].Width = px * 10;
                        lineBox[4].FontSize = 14;
                        Canvas.SetLeft(lineBox[4], px * 12);
                        Canvas.SetTop(lineBox[4], px * e65_1[4]);
                        PrintBox.Children.Add(lineBox[4]);

                        lineBox[5] = new();
                        lineBox[5].Text = Variables.Street;
                        lineBox[5].BorderThickness = new Thickness(0);
                        lineBox[5].FontSize = 14;
                        Canvas.SetLeft(lineBox[5], px * 12.3);
                        Canvas.SetTop(lineBox[5], px * e65_1[5]);
                        PrintBox.Children.Add(lineBox[5]);

                        lineBox[6] = new();
                        lineBox[6].Text = Variables.Home;
                        lineBox[6].BorderThickness = new Thickness(0);
                        lineBox[6].FontSize = 14;
                        Canvas.SetLeft(lineBox[6], px * 11.8);
                        Canvas.SetTop(lineBox[6], px * e65_1[6]);
                        PrintBox.Children.Add(lineBox[6]);

                        lineBox[7] = new();
                        lineBox[7].Text = Variables.Frame;
                        lineBox[7].BorderThickness = new Thickness(0);
                        lineBox[7].FontSize = 14;
                        Canvas.SetLeft(lineBox[7], px * 14.8);
                        Canvas.SetTop(lineBox[7], px * e65_1[6]);
                        PrintBox.Children.Add(lineBox[7]);

                        lineBox[8] = new();
                        lineBox[8].Text = Variables.Structure;
                        lineBox[8].BorderThickness = new Thickness(0);
                        lineBox[8].FontSize = 14;
                        Canvas.SetLeft(lineBox[8], px * 17);
                        Canvas.SetTop(lineBox[8], px * e65_1[6]);
                        PrintBox.Children.Add(lineBox[8]);

                        lineBox[9] = new();
                        lineBox[9].Text = Variables.Flat;
                        lineBox[9].BorderThickness = new Thickness(0);
                        lineBox[9].FontSize = 14;
                        Canvas.SetLeft(lineBox[9], px * 19.3);
                        Canvas.SetTop(lineBox[9], px * e65_1[6]);
                        PrintBox.Children.Add(lineBox[9]);

                        lineBox[10] = new();
                        lineBox[10].Text = Variables.City;
                        lineBox[10].BorderThickness = new Thickness(0);
                        lineBox[10].FontSize = 14;
                        Canvas.SetLeft(lineBox[10], px * 13.8);
                        Canvas.SetTop(lineBox[10], px * e65_1[7]);
                        PrintBox.Children.Add(lineBox[10]);

                        lineBox[11] = new();
                        lineBox[11].Text = Variables.Area;
                        lineBox[11].BorderThickness = new Thickness(0);
                        lineBox[11].FontSize = 14;
                        Canvas.SetLeft(lineBox[11], px * 12.4);
                        Canvas.SetTop(lineBox[11], px * e65_1[8]);
                        PrintBox.Children.Add(lineBox[11]);

                        lineBox[12] = new();
                        lineBox[12].Text = Variables.Region;
                        lineBox[12].BorderThickness = new Thickness(0);
                        lineBox[12].FontSize = 14;
                        Canvas.SetLeft(lineBox[12], px * 13.4);
                        Canvas.SetTop(lineBox[12], px * e65_1[9]);
                        PrintBox.Children.Add(lineBox[12]);

                        lineBox[13] = new();
                        lineBox[13].Text = Convert.ToString(Variables.Index);
                        lineBox[13].BorderThickness = new Thickness(0);
                        lineBox[13].FontSize = 14;
                        Canvas.SetLeft(lineBox[13], px * 12);
                        Canvas.SetTop(lineBox[13], px * e65_1[10]);
                        PrintBox.Children.Add(lineBox[13]);

                        Main_Grid.Children.Add(PrintBox);
                        break;
                    }
                case 2: // 2 Маленький конверт E65
                    {
                        double[] e65_2 = { 0.7, 1.3, 1.9, 2.5, 4.6, 5.9, 6.6, 7.3, 7.8, 8.5};

                        if (Variables.Region != "") Variables.Region += " обл.";
                        if (Variables.Area != "") Variables.Area += " р-он";

                        Preview_Form.Width = (px * 22) + 10;
                        Preview_Form.Height = (px * 11) + 90;
                        PrintBox = new();
                        PrintBox.Width = 831.49606299;
                        PrintBox.Height = 415.7480315;
                        Grid.SetColumn(PrintBox, 0);
                        Grid.SetRow(PrintBox, 0);
                        Grid.SetColumnSpan(PrintBox, 3);

                        lineBox[0] = new();
                        lineBox[0].Text = "ООО ``ГИПАР``";
                        lineBox[0].BorderThickness = new Thickness(0);
                        lineBox[0].FontSize = 14;
                        Canvas.SetLeft(lineBox[0], px * 2);
                        Canvas.SetTop(lineBox[0], px * e65_2[0]);
                        PrintBox.Children.Add(lineBox[0]);

                        lineBox[1] = new();
                        lineBox[1].Text = "Смоленская обл., г. Вязьма";
                        lineBox[1].BorderThickness = new Thickness(0);
                        lineBox[1].FontSize = 14;
                        Canvas.SetLeft(lineBox[1], px * 2);
                        Canvas.SetTop(lineBox[1], px * e65_2[1]);
                        PrintBox.Children.Add(lineBox[1]);

                        lineBox[2] = new();
                        lineBox[2].Text = "ул. Репина д. 16 оф. 4";
                        lineBox[2].BorderThickness = new Thickness(0);
                        lineBox[2].FontSize = 14;
                        Canvas.SetLeft(lineBox[2], px * 1);
                        Canvas.SetTop(lineBox[2], px * e65_2[2]);
                        PrintBox.Children.Add(lineBox[2]);

                        lineBox[3] = new();
                        lineBox[3].Text = "215110";
                        lineBox[3].BorderThickness = new Thickness(0);
                        lineBox[3].FontSize = 14;
                        Canvas.SetLeft(lineBox[3], px * 6.5);
                        Canvas.SetTop(lineBox[3], px * e65_2[3]);
                        PrintBox.Children.Add(lineBox[3]);

                        lineBox[4] = new();
                        lineBox[4].Text = Variables.Firm;
                        lineBox[4].BorderThickness = new Thickness(0);
                        lineBox[4].Width = px * 10;
                        lineBox[4].FontSize = 14;
                        Canvas.SetLeft(lineBox[4], px * 11.5);
                        Canvas.SetTop(lineBox[4], px * e65_2[4]);
                        PrintBox.Children.Add(lineBox[4]);

                        lineBox[5] = new();
                        lineBox[5].Text = Variables.Region;
                        lineBox[5].BorderThickness = new Thickness(0);
                        lineBox[5].FontSize = 14;
                        Canvas.SetLeft(lineBox[5], px * 11.5);
                        Canvas.SetTop(lineBox[5], px * e65_2[5]);
                        PrintBox.Children.Add(lineBox[5]);

                        lineBox[6] = new();
                        lineBox[6].Text = Variables.Area;
                        lineBox[6].BorderThickness = new Thickness(0);
                        lineBox[6].FontSize = 14;
                        Canvas.SetLeft(lineBox[6], px * 11);
                        Canvas.SetTop(lineBox[6], px * e65_2[6]);
                        PrintBox.Children.Add(lineBox[6]);

                        lineBox[7] = new();
                        lineBox[7].Text = "г. " + Variables.City + "  ул. " + Variables.Street;
                        lineBox[7].BorderThickness = new Thickness(0);
                        lineBox[7].FontSize = 14;
                        Canvas.SetLeft(lineBox[7], px * 11);
                        Canvas.SetTop(lineBox[7], px * e65_2[7]);
                        PrintBox.Children.Add(lineBox[7]);

                        lineBox[8] = new();
                        lineBox[8].Text = "дом " + Variables.Home + "   корп. " + Variables.Frame + "   стр. " + Variables.Structure + "   кв. " + Variables.Flat;
                        lineBox[8].BorderThickness = new Thickness(0);
                        lineBox[8].FontSize = 14;
                        Canvas.SetLeft(lineBox[8], px * 11);
                        Canvas.SetTop(lineBox[8], px * e65_2[8]);
                        PrintBox.Children.Add(lineBox[8]);

                        lineBox[9] = new();
                        lineBox[9].Text = Convert.ToString(Variables.Index);
                        lineBox[9].BorderThickness = new Thickness(0);
                        lineBox[9].FontSize = 14;
                        Canvas.SetLeft(lineBox[9], px * 11.5);
                        Canvas.SetTop(lineBox[9], px * e65_2[9]);
                        PrintBox.Children.Add(lineBox[9]);


                        Main_Grid.Children.Add(PrintBox);
                        break;
                    }
                case 3: //Бльшой конверт C5
                    {
                        double[] c5 = { 1.7, 2.3, 2.9, 4.4, 9.3, 10.8, 11.4, 11.9, 12.4, 13.2};

                        if (Variables.Region != "") Variables.Region += " обл.";
                        if (Variables.Area != "") Variables.Area += " р-он";


                        Preview_Form.Width = (px * 23) + 10;
                        Preview_Form.Height = (px * 16.2) + 90;
                        PrintBox = new();
                        PrintBox.Width = 869.29133858;
                        PrintBox.Height = 612.28346457;
                        Grid.SetColumn(PrintBox, 0);
                        Grid.SetRow(PrintBox, 0);
                        Grid.SetColumnSpan(PrintBox, 3);
                        Grid gridStack2 = new();
                        gridStack2.ShowGridLines = true;

                        lineBox[0] = new();
                        lineBox[0].Text = "ООО ``ГИПАР``";
                        lineBox[0].BorderThickness = new Thickness(0);
                        lineBox[0].FontSize = 14;
                        Canvas.SetLeft(lineBox[0], px * 3);
                        Canvas.SetTop(lineBox[0], px * c5[0]);
                        PrintBox.Children.Add(lineBox[0]);

                        lineBox[1] = new();
                        lineBox[1].Text = "Смоленская обл., г. Вязьма";
                        lineBox[1].BorderThickness = new Thickness(0);
                        lineBox[1].FontSize = 14;
                        Canvas.SetLeft(lineBox[1], px * 3);
                        Canvas.SetTop(lineBox[1], px * c5[1]);
                        PrintBox.Children.Add(lineBox[1]);

                        lineBox[2] = new();
                        lineBox[2].Text = "ул. Репина д. 16 оф. 4";
                        lineBox[2].BorderThickness = new Thickness(0);
                        lineBox[2].FontSize = 14;
                        Canvas.SetLeft(lineBox[2], px * 1.5);
                        Canvas.SetTop(lineBox[2], px * c5[2]);
                        PrintBox.Children.Add(lineBox[2]);

                        lineBox[3] = new();
                        lineBox[3].Text = "215110";
                        lineBox[3].BorderThickness = new Thickness(0);
                        lineBox[3].FontSize = 14;
                        Canvas.SetLeft(lineBox[3], px * 7);
                        Canvas.SetTop(lineBox[3], px * c5[3]);
                        PrintBox.Children.Add(lineBox[3]);

                        lineBox[4] = new();
                        lineBox[4].Text = Variables.Firm;
                        lineBox[4].BorderThickness = new Thickness(0);
                        lineBox[4].FontSize = 14;
                        Canvas.SetLeft(lineBox[4], px * 12.5);
                        Canvas.SetTop(lineBox[4], px * c5[4]);
                        PrintBox.Children.Add(lineBox[4]);

                        lineBox[5] = new();
                        lineBox[5].Text = Variables.Region;
                        lineBox[5].BorderThickness = new Thickness(0);
                        lineBox[5].FontSize = 14;
                        Canvas.SetLeft(lineBox[5], px * 12.5);
                        Canvas.SetTop(lineBox[5], px * c5[5]);
                        PrintBox.Children.Add(lineBox[5]);

                        lineBox[6] = new();
                        lineBox[6].Text = Variables.Area;
                        lineBox[6].BorderThickness = new Thickness(0);
                        lineBox[6].FontSize = 14;
                        Canvas.SetLeft(lineBox[6], px * 11.5);
                        Canvas.SetTop(lineBox[6], px * c5[6]);
                        PrintBox.Children.Add(lineBox[6]);

                        lineBox[7] = new();
                        lineBox[7].Text = "г. " + Variables.City + "  ул. " + Variables.Street;
                        lineBox[7].BorderThickness = new Thickness(0);
                        lineBox[7].FontSize = 14;
                        Canvas.SetLeft(lineBox[7], px * 11.5);
                        Canvas.SetTop(lineBox[7], px * c5[7]);
                        PrintBox.Children.Add(lineBox[7]);

                        lineBox[8] = new();
                        lineBox[8].Text = "дом " + Variables.Home + "   корп. " + Variables.Frame + "   стр. " + Variables.Structure + "   кв. " + Variables.Flat;
                        lineBox[8].BorderThickness = new Thickness(0);
                        lineBox[8].FontSize = 14;
                        Canvas.SetLeft(lineBox[8], px * 11.5);
                        Canvas.SetTop(lineBox[8], px * c5[8]);
                        PrintBox.Children.Add(lineBox[8]);

                        lineBox[9] = new();
                        lineBox[9].Text = Convert.ToString(Variables.Index);
                        lineBox[9].BorderThickness = new Thickness(0);
                        lineBox[9].FontSize = 14;
                        Canvas.SetLeft(lineBox[9], px * 12.5);
                        Canvas.SetTop(lineBox[9], px * c5[9]);
                        PrintBox.Children.Add(lineBox[9]);

                        Main_Grid.Children.Add(PrintBox);
                        break;
                    }
                default:
                break;
            }
        }
        private void BtnPrint_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog printDialog = new();
            printDialog.PrintQueue = new PrintQueue(new PrintServer(), Variables.Printer);
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
