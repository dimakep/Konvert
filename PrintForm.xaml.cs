using System;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using System.Printing;
using System.Data;
using System.Data.SqlClient;

namespace Konvert
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class PrintForm : Window
    {
        private readonly Inventory KonvertBisness = new(); // Подключение класса Inventory

        public PrintForm()
        {
            InitializeComponent();
            ImagePrintIco.Source = new BitmapImage(new Uri("D:\\Konvert\\Forms\\StartForm\\printer.png"));
            
            string query = "SELECT Id, Firm FROM Recipient";
            using (SqlConnection connection = new(KonvertBisness.connectionString))
            {
                SqlCommand command = new(query, connection);
                SqlDataAdapter adapter = new(command);
                DataSet dataSet = new();
                _ = adapter.Fill(dataSet, "Recipient");
            
                // Создаем объект CollectionViewSource для привязки ComboBox к данным
                CollectionViewSource viewSource = new CollectionViewSource();
                viewSource.Source = dataSet.Tables["Recipient"].DefaultView;

                // Привязываем ComboBox к объекту CollectionViewSource
                FirmBox.ItemsSource = viewSource.View;
                FirmBox.DisplayMemberPath = "Firm";
                FirmBox.SelectedValuePath = "Id";
            }
            
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            StartForm startForm = new();
            startForm.Show();
            Close();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddForm addForm = new AddForm();
            addForm.Show();
            Close();
        }


        private void BtnPrint_Click(object sender, RoutedEventArgs e)
        {
            KonvertBisness.firmInv = FirmBox.Text;
            KonvertBisness.sqlRequest = "SELECT * FROM Recipient WHERE Firm = N'" + KonvertBisness.firmInv + "'";
            KonvertBisness.FindInTable();
            if ((bool)BigTab.IsChecked)
            {
                BigForm BigForm = new(KonvertBisness.firmInv, KonvertBisness.indexInv, KonvertBisness.regionInv,
                    KonvertBisness.areaInv, KonvertBisness.cityInv, KonvertBisness.streetInv, KonvertBisness.homeInv,
                    KonvertBisness.frameInv, KonvertBisness.structureInv, KonvertBisness.flatInv, PrinterNameBox.Text);
                _ = BigForm.ShowDialog();

            }
            if ((bool)Small1Tab.IsChecked)
            {
                Small1Form small1Form = new(KonvertBisness.firmInv, KonvertBisness.indexInv, KonvertBisness.regionInv,
                    KonvertBisness.areaInv, KonvertBisness.cityInv, KonvertBisness.streetInv, KonvertBisness.homeInv,
                    KonvertBisness.frameInv, KonvertBisness.structureInv, KonvertBisness.flatInv, PrinterNameBox.Text);
                _ = small1Form.ShowDialog();
            }
            if ((bool)Small2Tab.IsChecked)
            {
                Small2Form small2Form = new(KonvertBisness.firmInv, KonvertBisness.indexInv, KonvertBisness.regionInv,
                    KonvertBisness.areaInv, KonvertBisness.cityInv, KonvertBisness.streetInv, KonvertBisness.homeInv,
                    KonvertBisness.frameInv, KonvertBisness.structureInv, KonvertBisness.flatInv, PrinterNameBox.Text);
                _ = small2Form.ShowDialog();
            }

        }

        private void Small1Tab_Click(object sender, RoutedEventArgs e)
        {
            ImageKonvert.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/Маленький конверт.jpg", UriKind.Absolute));
        }

        private void Small2Tab_Click(object sender, RoutedEventArgs e)
        {
            ImageKonvert.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/Большой конверт.jpg", UriKind.Absolute));
        }

        private void BigTab_Click(object sender, RoutedEventArgs e)
        {
            ImageKonvert.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/Большой конверт.jpg", UriKind.Absolute));
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Получаем список доступных принтеров
            LocalPrintServer printServer = new LocalPrintServer();
            PrintQueueCollection printQueues = printServer.GetPrintQueues();

            // Заполняем ComboBox списком принтеров
            foreach (PrintQueue printer in printQueues)
            {
                PrinterNameBox.Items.Add(printer.Name);
            }
            // Получаем имя принтера по умолчанию
            KonvertBisness.defaultPrinterName = printServer.DefaultPrintQueue.Name;
            PrinterNameBox.SelectedItem = KonvertBisness.defaultPrinterName;
        }

    }
}

