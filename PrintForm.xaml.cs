using System;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using System.Drawing.Printing;
using System.Printing;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Controls;

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
            //PaperSize bigKonvert = new("Большой конверт", 902, 638);
            //PaperSize smallKonvert = new("Маленький конверт", 950, 412);


            string query = "SELECT Id, Firm FROM Recipient";
            using (SqlConnection connection = new(KonvertBisness.connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet, "Recipient");

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
            MessageBox.Show(FirmBox.Text);
            KonvertBisness.firmInv = FirmBox.Text;
            KonvertBisness.sqlRequest = "SELECT * FROM Recipient WHERE Firm = '" + KonvertBisness.firmInv + "'";
            KonvertBisness.FindInTable();

            // Получение списка доступных принтеров
            LocalPrintServer printServer = new LocalPrintServer();
            PrintQueueCollection printQueues = printServer.GetPrintQueues();
            foreach (PrintQueue printer in printQueues)
            {
                if (printer.FullName == PrinterNameBox.Text)
                {
                    // Выбор нужного принтера
                    PrintQueue selectedPrinter = printer;
                    break;
                }
            }
            BigForm bigForm = new();
            bigForm.ShowDialog();
            if (bigForm.DialogResult == false)
            {
                StartForm startForm = new();
                startForm.Show();
                Close();
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

