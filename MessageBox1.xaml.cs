using System.Windows;

namespace Konvert
{
    /// <summary>
    /// Логика взаимодействия для MessageBox.xaml
    /// </summary>
    public partial class MessageBox1 : Window
    {
        public MessageBox1(string caption, string message)
        {
            InitializeComponent();
            CaptionLabel.Content = caption;
            MessageLabel.Content = message;
        }

        private void BtnYes_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void BtnNo_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
