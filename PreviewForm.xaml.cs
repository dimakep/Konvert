using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Printing;
using System.Globalization;

namespace Konvert
{
    /// <summary>
    /// Логика взаимодействия для Preview_Form.xaml
    /// </summary>
    /// 

    public partial class Preview_Form : Window
    {
        private Canvas PrintBox;
        private readonly double px = 37.795275591;
        private readonly double[] small1 = { 1, 0.5, 1, 1.7, 3.7, 4.7, 5.2, 6.25, 6.8, 7.35, 8.1};
        private readonly double[] small2 = { 0.7, 0.6, 1.3, 1.9, 4, 5.4, 6, 7.2, 7.9};
        private readonly double[] big = { 1.7, 0.7, 1.4, 2.7, 7.6, 8.9, 10.3, 10.9, 11.6 };
        private readonly TextBox[] lineBox = new TextBox[14];
        public Preview_Form(int envelopeFormat)
        {
            InitializeComponent();

            Inventory.sqlRequest = "SELECT * FROM Recipient WHERE Firm = N'" + Variables.Firm + "'";
            Inventory.FindInTable();

            switch (envelopeFormat)
            {
                case 1: // 1 Маленький конверт
                    Small1Konvert();
                    break;

                case 2: // 2 Маленький конверт
                    Small2Konvert();
                    break;

                case 3: //Бльшой конверт
                    BigKonvert();
                    break;
                default:
                    break;
            }
        }

        private void Small1Konvert()
        {
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
            lineBox[4].Text = Variables.Firm;
            lineBox[4].BorderThickness = new Thickness(0);
            Canvas.SetLeft(lineBox[4], px * 11.6);
            Canvas.SetTop(lineBox[4], px * (small1[0] + small1[4]));

            lineBox[5] = new();
            lineBox[5].Text = Variables.Street;
            lineBox[5].BorderThickness = new Thickness(0);
            Canvas.SetLeft(lineBox[5], px * 11.6);
            Canvas.SetTop(lineBox[5], px * (small1[0] + small1[5]));

            lineBox[6] = new();
            lineBox[6].Text = Variables.Home;
            lineBox[6].BorderThickness = new Thickness(0);
            Canvas.SetLeft(lineBox[6], px * 11);
            Canvas.SetTop(lineBox[6], px * (small1[0] + small1[6]));
           
            lineBox[7] = new();
            lineBox[7].Text = Variables.Frame;
            lineBox[7].BorderThickness = new Thickness(0);
            Canvas.SetLeft(lineBox[7], px * 14);
            Canvas.SetTop(lineBox[7], px * (small1[0] + small1[6]));

            lineBox[8] = new();
            lineBox[8].Text = Variables.Structure;
            lineBox[8].BorderThickness = new Thickness(0);
            Canvas.SetLeft(lineBox[8], px * 16.5);
            Canvas.SetTop(lineBox[8], px * (small1[0] + small1[6]));

            lineBox[9] = new();
            lineBox[9].Text = Variables.Flat;
            lineBox[9].BorderThickness = new Thickness(0);
            Canvas.SetLeft(lineBox[9], px * 19.1);
            Canvas.SetTop(lineBox[9], px * (small1[0] + small1[6]));

            lineBox[10] = new();
            lineBox[10].Text = Variables.City;
            lineBox[10].BorderThickness = new Thickness(0);
            Canvas.SetLeft(lineBox[10], px * 13.5);
            Canvas.SetTop(lineBox[10], px * (small1[0] + small1[7]));

            lineBox[11] = new();
            lineBox[11].Text = Variables.Area;
            lineBox[11].BorderThickness = new Thickness(0);
            Canvas.SetLeft(lineBox[11], px * 12);
            Canvas.SetTop(lineBox[11], px * (small1[0] + small1[8]));

            lineBox[12] = new();
            lineBox[12].Text = Variables.Region;
            lineBox[12].BorderThickness = new Thickness(0);
            Canvas.SetLeft(lineBox[12], px * 13);
            Canvas.SetTop(lineBox[12], px * (small1[0] + small1[9]));

            lineBox[13] = new();
            lineBox[13].Text = Convert.ToString(Variables.Index);
            lineBox[13].BorderThickness = new Thickness(0);
            Canvas.SetLeft(lineBox[13], px * 11.5);
            Canvas.SetTop(lineBox[13], px * (small1[0] + small1[10]));

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
        }
        private void Small2Konvert()
        {
            if (Variables.Region != "") Variables.Region = $"{Variables.Region} обл.,";
            if (Variables.Area != "") Variables.Area = $"{Variables.Area} р-он";
            if (Variables.Street != "") Variables.Street = $"ул. {Variables.Street}";
            if (Variables.Home != "") Variables.Home = $"д. {Variables.Home}";
            if (Variables.Frame != "") Variables.Frame = $"корп. {Variables.Frame}";
            if (Variables.Structure != "") Variables.Structure = $"стр. {Variables.Structure}";
            if (Variables.Flat != "") Variables.Flat = $"кв. {Variables.Flat}";
            Variables.City = $"г. {Variables.City}";
            if (Variables.Region == "" && Variables.Area == "")
            {
                Variables.Region = $"{Variables.City}   {Variables.Street}";
                Variables.City = Variables.Street = "";
            }

            PreviewForm.Width = (px * 22) + 10;
            PreviewForm.Height = (px * 11) + 90;
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
            Canvas.SetTop(lineBox[0], px * small2[0]);

            lineBox[1] = new();
            lineBox[1].Text = "Смоленская обл., г. Вязьма";
            lineBox[1].BorderThickness = new Thickness(0);
            Canvas.SetLeft(lineBox[1], px * 2.5);
            Canvas.SetTop(lineBox[1], px * (small2[0] + small2[1]));

            lineBox[2] = new();
            lineBox[2].Text = "ул. Репина д. 16 оф. 4";
            lineBox[2].BorderThickness = new Thickness(0);
            Canvas.SetLeft(lineBox[2], px * 2.5);
            Canvas.SetTop(lineBox[2], px * (small2[0] + small2[2]));

            lineBox[3] = new();
            lineBox[3].Text = "215110";
            lineBox[3].BorderThickness = new Thickness(0);
            Canvas.SetLeft(lineBox[3], px * 6);
            Canvas.SetTop(lineBox[3], px * (small2[0] + small2[3]));

            lineBox[4] = new();
            lineBox[4].VerticalContentAlignment = VerticalAlignment.Top;
            lineBox[4].Text = Variables.Firm;
            lineBox[4].BorderThickness = new Thickness(0);
            lineBox[4].Width = px * 10;
            lineBox[4].Height = px * 1.4;
            lineBox[4].TextWrapping = TextWrapping.WrapWithOverflow;
            lineBox[4].MaxLines = 2;
            Canvas.SetLeft(lineBox[4], px * 11.2);
            Canvas.SetTop(lineBox[4], px * (small2[0] + small2[4]));

            lineBox[5] = new();
            lineBox[5].Text = $"{Variables.Region} {Variables.Area}";
            lineBox[5].BorderThickness = new Thickness(0);
            lineBox[4].Width = px * 10;
            lineBox[4].Height = px * 1.4;
            lineBox[4].TextWrapping = TextWrapping.WrapWithOverflow;
            lineBox[4].MaxLines = 2;
            Canvas.SetLeft(lineBox[5], px * 11.2);
            Canvas.SetTop(lineBox[5], px * (small2[0] + small2[5]));

            lineBox[6] = new();
            lineBox[6].Text = $"{Variables.City}   {Variables.Street}";
            lineBox[6].BorderThickness = new Thickness(0);
            Canvas.SetLeft(lineBox[6], px * 10.4);
            Canvas.SetTop(lineBox[6], px * (small2[0] + small2[6]));

            lineBox[7] = new();
            lineBox[7].Text = $"{Variables.Home}  {Variables.Frame}  {Variables.Structure}  {Variables.Flat}";
            lineBox[7].BorderThickness = new Thickness(0);
            Canvas.SetLeft(lineBox[7], px * 12);
            Canvas.SetTop(lineBox[7], px * (small2[0] + small2[7]));

            lineBox[8] = new();
            lineBox[8].Text = Convert.ToString(Variables.Index, CultureInfo.CurrentCulture);
            lineBox[8].BorderThickness = new Thickness(0);
            lineBox[8].TextAlignment = TextAlignment.Center;
            lineBox[8].Width = px * 2.8;
            Canvas.SetLeft(lineBox[8], px * 10.4);
            Canvas.SetTop(lineBox[8], px * (small2[0] + small2[8]));

            PrintBox.Children.Add(lineBox[0]);
            PrintBox.Children.Add(lineBox[1]);
            PrintBox.Children.Add(lineBox[2]);
            PrintBox.Children.Add(lineBox[3]);
            PrintBox.Children.Add(lineBox[4]);
            PrintBox.Children.Add(lineBox[5]);
            PrintBox.Children.Add(lineBox[6]);
            PrintBox.Children.Add(lineBox[7]);
            PrintBox.Children.Add(lineBox[8]);

            Main_Grid.Children.Add(PrintBox);
        }
        private void BigKonvert()
        {
            if (Variables.Region != "") Variables.Region = $"{Variables.Region} обл.,";
            if (Variables.Area != "") Variables.Area = $"{Variables.Area} р-он";
            if (Variables.Street != "") Variables.Street = $"ул. {Variables.Street}";
            if (Variables.Home != "") Variables.Home = $"д. {Variables.Home}";
            if (Variables.Frame != "") Variables.Frame = $"корп. {Variables.Frame}";
            if (Variables.Structure != "") Variables.Structure = $"стр. {Variables.Structure}";
            if (Variables.Flat != "") Variables.Flat = $"кв. {Variables.Flat}";
            Variables.City = $"г. {Variables.City}";
            if (Variables.Region == "" && Variables.Area == "")
            {
                Variables.Region = $"{Variables.City}   {Variables.Street}";
                Variables.City = Variables.Street = "";
            }

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

            lineBox[0] = new();
            lineBox[0].Text = "ООО ``ГИПАР``";
            lineBox[0].BorderThickness = new Thickness(0);
            Canvas.SetLeft(lineBox[0], px * 2.5);
            Canvas.SetTop(lineBox[0], px * big[0]);

            lineBox[1] = new();
            lineBox[1].Text = "Смоленская обл., г. Вязьма";
            lineBox[1].BorderThickness = new Thickness(0);
            Canvas.SetLeft(lineBox[1], px * 2.5);
            Canvas.SetTop(lineBox[1], px * (big[0] + big[1]));

            lineBox[2] = new();
            lineBox[2].Text = "ул. Репина д. 16 оф. 4";
            lineBox[2].BorderThickness = new Thickness(0);
            Canvas.SetLeft(lineBox[2], px * 2.5);
            Canvas.SetTop(lineBox[2], px * (big[0] + big[2]));

            lineBox[3] = new();
            lineBox[3].Text = "215110";
            lineBox[3].BorderThickness = new Thickness(0);
            Canvas.SetLeft(lineBox[3], px * 6.7);
            Canvas.SetTop(lineBox[3], px * (big[0] + big[3]));

            lineBox[4] = new();
            lineBox[4].VerticalContentAlignment = VerticalAlignment.Top;
            lineBox[4].Text = Variables.Firm;
            lineBox[4].BorderThickness = new Thickness(0);
            lineBox[4].Width = px * 10;
            lineBox[4].Height = px * 1.4;
            lineBox[4].TextWrapping = TextWrapping.WrapWithOverflow;
            lineBox[4].MaxLines = 2;
            Canvas.SetLeft(lineBox[4], px * 12.8);
            Canvas.SetTop(lineBox[4], px * (big[0] + big[4]));

            lineBox[5] = new();
            lineBox[5].Text = $"{Variables.Region} {Variables.Area}";
            lineBox[5].BorderThickness = new Thickness(0);
            lineBox[4].Width = px * 10;
            lineBox[4].Height = px * 1.4;
            lineBox[4].TextWrapping = TextWrapping.WrapWithOverflow;
            lineBox[4].MaxLines = 2;
            Canvas.SetLeft(lineBox[5], px * 12.8);
            Canvas.SetTop(lineBox[5], px * (big[0] + big[5]));

            lineBox[6] = new();
            lineBox[6].Text = $"{Variables.City}   {Variables.Street}";
            lineBox[6].BorderThickness = new Thickness(0);
            Canvas.SetLeft(lineBox[6], px * 11.3);
            Canvas.SetTop(lineBox[6], px * (big[0] + big[6]));

            lineBox[7] = new();
            lineBox[7].Text = $"{Variables.Home}  {Variables.Frame}  {Variables.Structure}  {Variables.Flat}";
            lineBox[7].BorderThickness = new Thickness(0);
            Canvas.SetLeft(lineBox[7], px * 11.3);
            Canvas.SetTop(lineBox[7], px * (big[0] + big[7]));

            lineBox[8] = new();
            lineBox[8].Text = Convert.ToString(Variables.Index, CultureInfo.CurrentCulture);
            lineBox[8].BorderThickness = new Thickness(0);
            lineBox[8].TextAlignment = TextAlignment.Center;
            lineBox[8].Width = px * 2.8;
            Canvas.SetLeft(lineBox[8], px * 11.3);
            Canvas.SetTop(lineBox[8], px * (big[0] + big[8]));

            PrintBox.Children.Add(lineBox[0]);
            PrintBox.Children.Add(lineBox[1]);
            PrintBox.Children.Add(lineBox[2]);
            PrintBox.Children.Add(lineBox[3]);
            PrintBox.Children.Add(lineBox[4]);
            PrintBox.Children.Add(lineBox[5]);
            PrintBox.Children.Add(lineBox[6]);
            PrintBox.Children.Add(lineBox[7]);
            PrintBox.Children.Add(lineBox[8]);

            Main_Grid.Children.Add(PrintBox);
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
