using System.Windows;

namespace Konvert
{
    /// <summary>
    /// Логика взаимодействия для MessageBox.xaml
    /// </summary>
    public partial class MessageBox2 : Window
    {
        public MessageBox2(string caption, string message)
        {
            InitializeComponent();
            CaptionLabel.Content = caption;
            MessageLabel.Content = message;
        }

        private void BtnYes_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
