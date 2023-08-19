using System.Windows;
using System.Windows.Controls;
using System.Printing;
using System;

namespace Konvert
{
    /// <summary>
    /// Логика взаимодействия для BigForm.xaml
    /// </summary>
    public partial class Small2Form : Window

    {
        private string printerName;
        private readonly Inventory KonvertBisness = new(); // Подключение класса Inventory
        public Small2Form(string firm, int index, string region, string area, string city, string street, 
            string home, string frame, string structure, string flat, string printer)
        {
            InitializeComponent();

            if (region != "") region = $"{region} обл.,";
            if (area != "") area = $"{area} р-он";
            if (street != "") street = $"ул. {street}";
            if (home != "") home = $"д. {home}";
            if (frame != "") frame = $"корп. {frame}";
            if (structure != "") structure = $"стр. {structure}";
            if (flat != "") flat = $"кв. {flat}";
            city = $"г. {city}";
            if (region == "" && area == "")
            {
                region = $"{city}   {street}";

            }
            RecipientBox.Text = firm;
            RegionBox.Text = $"{region} {area}";
            CityBox.Text = $"{city}   {street}";
            HomeBox.Text = $"{home}  {frame}  {structure}  {flat}";
            IndexBox.Text = Convert.ToString(index);
            printerName = printer;

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
