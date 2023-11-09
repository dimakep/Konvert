using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Globalization;


namespace Konvert

{

    public partial class AddForm : Window
    {
        //private readonly Inventory Inventory = new(); // Подключение класса Inventory
        private int positionDB;
        private string closeForm = "StartForm";
        //Variables Variables = new Variables();

        public AddForm()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CounterDB();
            FirmBox.Focus();
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (FirmBox.Text != "")
            {
                Inventory.DBAddArray();
                MessageBox1 messageBox1 = new("ВНИМАНИЕ!", "Сохранить изменения?");
                messageBox1.ShowDialog();
                if (messageBox1.DialogResult == true)
                {
                    Variables.Firm = FirmBox.Text;
                    Inventory.sqlRequest = "SELECT * FROM Recipient WHERE Firm = '" + Variables.Firm + "'";
                    Inventory.FindInTable();
                    if (Variables.ID == 0)
                    {
                        DBFromBox();
                        Inventory.AddInTable();
                    }
                    else
                    {
                        DBFromBox();
                        Inventory.UpdateInTable();
                    }
                    e.Cancel = false;
                }
                
                if (messageBox1.DialogResult == false)
                {
                    switch (closeForm)
                    {
                        case "StartForm":
                            StartForm startForm = new();
                            startForm.Show();
                            break;

                        case "PrintForm":
                            PrintForm printForm = new();
                            printForm.Show();
                            break;
                        default:
                            break;
                    }
                    e.Cancel = false;
                }
            }
            else
            {
                switch (closeForm)
                {
                    case "StartForm":
                        StartForm startForm = new();
                        startForm.Show();
                        break;

                    case "PrintForm":
                        PrintForm printForm = new();
                        printForm.Show();
                        break;
                    default:
                        break;
                }
                e.Cancel = false;
            }
        }
        private void CounterDB()
        {
            Inventory.DBAddArray();
            TextBoxGrid.Text = positionDB + " / " + Inventory.idFromDB.Count;
        }
        ///
        /// Проверка ввода в поле индекс только цифр
        /// 
        private void IndexBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsValid(((TextBox)sender).Text + e.Text);
        }
        public static bool IsValid(string str)
        {
            return int.TryParse(str, out int i) && i >= 1 && i <= 999999;
        }
        ///
        /// Проверка заполнения TextBox "Организация" при переходе в "Индекс"
        ///
        private void IndexBox_GotFocus(object sender, RoutedEventArgs e)
        {
            Variables.Firm = FirmBox.Text;
            Inventory.CoincidenceFind();
            BoxFromDB();
        }

        public void ClearBox() ///Очистка TextBox
        {
            FirmBox.Text = Variables.Firm = "";
            IndexBox.Text = "";
            Variables.Index = 0;
            RegionBox.Text = Variables.Region = "";
            AreaBox.Text = Variables.Area = "";
            CityBox.Text = Variables.City = "";
            StreetBox.Text = Variables.Street = "";
            HomeBox.Text = Variables.Home = "";
            FrameBox.Text = Variables.Frame = "";
            StructureBox.Text = Variables.Structure = "";
            FlatBox.Text = Variables.Flat = "";
            CounterDB();
        }
        private void BoxFromDB() /// Передача данных из TextBoxs в переменные
        {
            FirmBox.Text = Variables.Firm;
            IndexBox.Text = Convert.ToString(Variables.Index, CultureInfo.CurrentCulture);
            RegionBox.Text = Variables.Region;
            AreaBox.Text = Variables.Area;
            CityBox.Text = Variables.City;
            StreetBox.Text = Variables.Street;
            HomeBox.Text = Variables.Home;
            FrameBox.Text = Variables.Frame;
            StructureBox.Text = Variables.Structure;
            FlatBox.Text = Variables.Flat;
        } 
        private void DBFromBox() /// Передача данных из переменных в TextBox
        {
            Variables.Firm = FirmBox.Text;
            Variables.Index = Convert.ToInt32(IndexBox.Text, CultureInfo.CurrentCulture);
            Variables.Region = RegionBox.Text;
            Variables.Area = AreaBox.Text;
            Variables.City = CityBox.Text;
            Variables.Street = StreetBox.Text;
            Variables.Home = HomeBox.Text;
            Variables.Frame = FrameBox.Text;
            Variables.Structure = StructureBox.Text;
            Variables.Flat = FlatBox.Text;
        }
        /// 
        /// Кнопки работы с БД
        /// 
        private void BtnNew_Click(object sender, RoutedEventArgs e) /// Очистка TextBox`s для ввода новых данных
        {
            ClearBox();
            positionDB = 0;
            CounterDB();
        }
        private void BtnAdd_Click(object sender, RoutedEventArgs e) /// Добавление строки в базу данных
        {
            DBFromBox();
            Inventory.AddInTable();
            ClearBox(); ///Очистка TextBoxs
            positionDB = 0;
        }
        private void BtnEdit_Click(object sender, RoutedEventArgs e) /// Изменение строки в базе данных
        {
            DBFromBox();
            Inventory.UpdateInTable();
            ClearBox();
            positionDB = 0;
        }
        private void BtnDel_Click(object sender, RoutedEventArgs e) /// Удаление строки из базы данных
        {
            if (FirmBox.Text != "")
            {
                MessageBox1 messageBox1 = new("Внимание!!!", "Вы действительно\nхотите удалить запись?");
                messageBox1.ShowDialog();
                if (messageBox1.DialogResult == true)
                {
                    Variables.ID = positionDB;
                    Variables.Firm = FirmBox.Text;
                    Inventory.DelInTable();
                    ClearBox();
                    CounterDB();
                }
            }
            positionDB = 0;
        }
        /// 
        /// Возврат в стартовую форму
        /// 
        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            closeForm = "StartForm";
            Close();
        }
        /// 
        /// Переход в форму для печати
        /// 
        private void BtnPrint_Click(object sender, RoutedEventArgs e)
        {
            closeForm = "PrintForm";
            Close();
        }
        /// 
        /// Кнопки перехода по строкам БД с загрузкой данных в TextBox`s
        /// 
        private void BtnFFBack_Click(object sender, RoutedEventArgs e)
        {
            if (positionDB >= 1)
            {
                positionDB = 1;
                Variables.ID = Inventory.idFromDB[positionDB - 1];
                CounterDB();
                Inventory.sqlRequest = "SELECT * FROM Recipient WHERE Id = '" + Variables.ID + "'";
                Inventory.FindInTable();
                BoxFromDB();
            }
        }
        private void BtnFBack_Click(object sender, RoutedEventArgs e)
        {
            if (positionDB >= 1)
            {
                positionDB--;
                if (positionDB < 1)
                {
                    positionDB = 1;
                    Variables.ID = Inventory.idFromDB[positionDB - 1];
                }
                else
                {
                    Variables.ID = Inventory.idFromDB[positionDB - 1];
                }
                CounterDB();
                Inventory.sqlRequest = "SELECT * FROM Recipient WHERE Id = '" + Variables.ID + "'";
                Inventory.FindInTable();
                BoxFromDB();
            }
        }
        private void BtnForward_Click(object sender, RoutedEventArgs e)
        {
            positionDB++;
            if (positionDB > Inventory.idFromDB.Count)
            {
                positionDB = Inventory.idFromDB.Count;
            }
            else
            {
                Variables.ID = Inventory.idFromDB[positionDB - 1];
            }
            CounterDB();
            Inventory.sqlRequest = "SELECT * FROM Recipient WHERE Id = '" + Variables.ID + "'";
            Inventory.FindInTable();
            BoxFromDB();
        }
        private void BtnFForward_Click(object sender, RoutedEventArgs e)
        {
            positionDB = Inventory.idFromDB.Count;
            CounterDB();
            Variables.ID = Inventory.idFromDB[positionDB - 1];
            Inventory.sqlRequest = "SELECT * FROM Recipient WHERE Id = '" + Variables.ID + "'";
            Inventory.FindInTable();
            BoxFromDB();
        }

    }
}
