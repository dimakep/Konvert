using System;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using System.Printing;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Linq;

namespace Konvert
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class PrintForm : Window
    {
        //private readonly Inventory Inventory = new(); // Подключение класса Inventory
        int envelopeFormat = 1;

        public PrintForm()
        {
            InitializeComponent();
            string query = "SELECT Id, Firm FROM Recipient";
            using SqlConnection connection = new(Inventory.connectionString);
            SqlCommand command = new(query, connection);
            SqlDataAdapter adapter = new(command);
            DataSet dataSet = new();
            _ = adapter.Fill(dataSet, "Recipient");
            // Создаем объект CollectionViewSource для привязки ComboBox к данным
            CollectionViewSource viewSource = new();
            viewSource.Source = dataSet.Tables["Recipient"].DefaultView;

            // Привязываем ComboBox к объекту CollectionViewSource
            FirmBox.ItemsSource = viewSource.View;
            FirmBox.DisplayMemberPath = "Firm";
            FirmBox.SelectedValuePath = "Id";
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            StartForm startForm = new();
            startForm.Show();
            Close();
        }
        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddForm addForm = new();
            addForm.Show();
            Close();
        }
        private void BtnPrint_Click(object sender, RoutedEventArgs e)
        {
            
            Variables.Firm = FirmBox.Text;
            Preview_Form preview_Form = new(envelopeFormat);
            preview_Form.ShowDialog();
            //Small1Form small1Form = new();
            //small1Form.Show();

            /*
            if ((bool)BigTab.IsChecked)
            {
                BigForm BigForm = new(Inventory.firmInv, Inventory.indexInv, Inventory.regionInv,
                    Inventory.areaInv, Inventory.cityInv, Inventory.streetInv, Inventory.homeInv,
                    Inventory.frameInv, Inventory.structureInv, Inventory.flatInv, PrinterNameBox.Text);
                _ = BigForm.ShowDialog();

            }
            if ((bool)Small1Tab.IsChecked)
            {
                Small1Form small1Form = new(Inventory.firmInv, Inventory.indexInv, Inventory.regionInv,
                    Inventory.areaInv, Inventory.cityInv, Inventory.streetInv, Inventory.homeInv,
                    Inventory.frameInv, Inventory.structureInv, Inventory.flatInv, PrinterNameBox.Text);
                _ = small1Form.ShowDialog();
            }
            if ((bool)Small2Tab.IsChecked)
            {
                Small2Form small2Form = new(Inventory.firmInv, Inventory.indexInv, Inventory.regionInv,
                    Inventory.areaInv, Inventory.cityInv, Inventory.streetInv, Inventory.homeInv,
                    Inventory.frameInv, Inventory.structureInv, Inventory.flatInv, PrinterNameBox.Text);
                _ = small2Form.ShowDialog();
            }
            */
        }
        private void Small1Tab_Click(object sender, RoutedEventArgs e)
        {
            ImageKonvert.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/Маленький конверт.jpg", UriKind.Absolute));
            envelopeFormat = 1;
        }
        private void Small2Tab_Click(object sender, RoutedEventArgs e)
        {
            ImageKonvert.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/Большой конверт.jpg", UriKind.Absolute));
            envelopeFormat = 2;
        }
        private void BigTab_Click(object sender, RoutedEventArgs e)
        {
            ImageKonvert.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/Большой конверт.jpg", UriKind.Absolute));
            envelopeFormat = 3;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Получаем список доступных принтеров
            LocalPrintServer printServer = new();
            PrintQueueCollection printQueues = printServer.GetPrintQueues();

            // Заполняем ComboBox списком принтеров
            foreach (PrintQueue printer in printQueues)
            {
                PrinterNameBox.Items.Add(printer.Name);
            }
            // Получаем имя принтера по умолчанию
            Variables.Printer = printServer.DefaultPrintQueue.Name;
            PrinterNameBox.SelectedItem = Variables.Printer;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            StartForm startForm = new();
            startForm.Show();
        }
    }
}

