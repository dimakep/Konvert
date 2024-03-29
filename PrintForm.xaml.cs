﻿using System;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using System.Printing;
using System.Data;
using System.Data.SQLite;


namespace Konvert
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class PrintForm : Window
    {
        int envelopeFormat = 1;
        public CollectionViewSource viewSource = new();
        private string closeForm = "StartForm";

        public PrintForm()
        {
            InitializeComponent();
            string query = "SELECT Id, Firm FROM Recipient";
            using SQLiteConnection connection = new(InventoryLite.connectionString);
            SQLiteCommand command = new(query, connection);
            SQLiteDataAdapter adapter = new(command);
            DataSet dataSet = new();
            _ = adapter.Fill(dataSet, "Recipient");
            /// Создаем объект CollectionViewSource для привязки ComboBox к данным
            CollectionViewSource viewSource = new();
            viewSource.Source = dataSet.Tables["Recipient"].DefaultView;

            /// Привязываем ComboBox к объекту CollectionViewSource
            FirmBox.ItemsSource = viewSource.View;
            FirmBox.DisplayMemberPath = "Firm";
            FirmBox.SelectedValuePath = "Id";
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            
            Close();
        }
        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            closeForm = "AddForm";
            Close();
        }
        private void BtnPrint_Click(object sender, RoutedEventArgs e)
        {
            Variables.Firm = FirmBox.Text;
            PreviewForm previewForm = new(envelopeFormat);
            previewForm.ShowDialog();
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
            switch (closeForm)
            {
                case "StartForm":
                    StartForm startForm = new();
                    startForm.Show();
                    break;

                case "AddForm":
                    AddForm addForm = new();
                    addForm.Show();
                    break;
                default:
                    break;
            }
            e.Cancel = false;
        }
    }
}

