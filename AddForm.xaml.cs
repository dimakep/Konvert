﻿using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace Konvert

{

    public partial class AddForm : Window
    {
        private readonly Inventory KonvertBisness = new(); // Подключение класса Inventory
        private int positionDB;

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
                KonvertBisness.DBAddArray();
                MessageBox1 messageBox1 = new("ВНИМАНИЕ!", "Сохранить изменения?");
                messageBox1.ShowDialog();
                if (messageBox1.DialogResult == true)
                {
                    KonvertBisness.firmInv = FirmBox.Text;
                    KonvertBisness.sqlRequest = "SELECT * FROM Recipient WHERE Firm = '" + KonvertBisness.firmInv + "'";
                    KonvertBisness.FindInTable();
                    if (KonvertBisness.idInv == 0)
                    {
                        DBFromBox();
                        KonvertBisness.AddInTable();
                    }
                    else
                    {
                        DBFromBox();
                        KonvertBisness.UpdateInTable();
                    }
                    e.Cancel = false;
                }
                if (messageBox1.DialogResult == false)
                {
                    e.Cancel = false;
                    StartForm startForm = new StartForm();
                    startForm.Show();
                }
            }
            else
            {
                StartForm startForm = new();
                startForm.Show();
            }
        }
        private void CounterDB()
        {
            KonvertBisness.DBAddArray();
            TextBoxGrid.Text = positionDB + " / " + KonvertBisness.idFromDB.Count;
        }
        private void IndexBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsValid(((TextBox)sender).Text + e.Text);
        }
        private void IndexBox_GotFocus(object sender, RoutedEventArgs e)
        {
            KonvertBisness.firmInv = FirmBox.Text;
            KonvertBisness.CoincidenceFind();
            BoxFromDB();
        }
        public static bool IsValid(string str)
        {
            int i;
            return int.TryParse(str, out i) && i >= 1 && i <= 999999;
        }
        public void ClearBox() ///Очистка TextBox
        {
            FirmBox.Text = "";
            IndexBox.Text = "";
            RegionBox.Text = "";
            AreaBox.Text = "";
            CityBox.Text = "";
            StreetBox.Text = "";
            HomeBox.Text = "";
            FrameBox.Text = "";
            StructureBox.Text = "";
            FlatBox.Text = "";
            CounterDB();
        }
        ///
        /// Присваивание данных из БД в TextBox
        ///
        private void BoxFromDB() /// Передача данных из TextBoxs в переменные
        {
            FirmBox.Text = KonvertBisness.firmInv;
            IndexBox.Text = Convert.ToString(KonvertBisness.indexInv);
            RegionBox.Text = KonvertBisness.regionInv;
            AreaBox.Text = KonvertBisness.areaInv;
            CityBox.Text = KonvertBisness.cityInv;
            StreetBox.Text = KonvertBisness.streetInv;
            HomeBox.Text = KonvertBisness.homeInv;
            FrameBox.Text = KonvertBisness.frameInv;
            StructureBox.Text = KonvertBisness.structureInv;
            FlatBox.Text = KonvertBisness.flatInv;
        } 
        /// 
        /// Присваивание данныъх из TextBox в БД
        /// 
        private void DBFromBox() /// Передача данных из переменных в TextBox
        {
            KonvertBisness.firmInv = FirmBox.Text;
            KonvertBisness.indexInv = Convert.ToInt32(IndexBox.Text);
            KonvertBisness.regionInv = RegionBox.Text;
            KonvertBisness.areaInv = AreaBox.Text;
            KonvertBisness.cityInv = CityBox.Text;
            KonvertBisness.streetInv = StreetBox.Text;
            KonvertBisness.homeInv = HomeBox.Text;
            KonvertBisness.frameInv = FrameBox.Text;
            KonvertBisness.structureInv = StructureBox.Text;
            KonvertBisness.flatInv = FlatBox.Text;
        }
        /// 
        /// Кнопки работы с БД
        /// 
        private void BtnNew_Click(object sender, RoutedEventArgs e)
        {
            ClearBox();
            positionDB = 0;
            CounterDB();
        }
        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            DBFromBox();
            KonvertBisness.AddInTable();
            ClearBox(); ///Очистка TextBoxs
            positionDB = 0;
        }
        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            DBFromBox();
            KonvertBisness.UpdateInTable();
            ClearBox();
            positionDB = 0;
        }
        private void BtnDel_Click(object sender, RoutedEventArgs e)
        {
            if (FirmBox.Text != "")
            {
                MessageBox1 messageBox1 = new("Внимание!!!", "Вы действительно\nхотите удалить запись?");
                messageBox1.ShowDialog();
                if (messageBox1.DialogResult == true)
                {
                    KonvertBisness.idInv = positionDB;
                    KonvertBisness.firmInv = FirmBox.Text;
                    KonvertBisness.DelInTable();
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
            Close();
        }
        /// 
        /// Переход в форму для печати
        /// 
        private void BtnPrint_Click(object sender, RoutedEventArgs e)
        {
            PrintForm printForm = new();
            printForm.Show();
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
                KonvertBisness.idInv = KonvertBisness.idFromDB[positionDB - 1];
                CounterDB();
                KonvertBisness.sqlRequest = "SELECT * FROM Recipient WHERE Id = '" + KonvertBisness.idInv + "'";
                KonvertBisness.FindInTable();
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
                    KonvertBisness.idInv = KonvertBisness.idFromDB[positionDB - 1];
                }
                else
                {
                    KonvertBisness.idInv = KonvertBisness.idFromDB[positionDB - 1];
                }
                CounterDB();
                KonvertBisness.sqlRequest = "SELECT * FROM Recipient WHERE Id = '" + KonvertBisness.idInv + "'";
                KonvertBisness.FindInTable();
                BoxFromDB();
            }
        }
        private void BtnForward_Click(object sender, RoutedEventArgs e)
        {
            positionDB++;
            if (positionDB > KonvertBisness.idFromDB.Count)
            {
                positionDB = KonvertBisness.idFromDB.Count;
            }
            else
            {
                KonvertBisness.idInv = KonvertBisness.idFromDB[positionDB - 1];
            }
            CounterDB();
            KonvertBisness.sqlRequest = "SELECT * FROM Recipient WHERE Id = '" + KonvertBisness.idInv + "'";
            KonvertBisness.FindInTable();
            ClearBox();
            BoxFromDB();
        }
        private void BtnFForward_Click(object sender, RoutedEventArgs e)
        {
            positionDB = KonvertBisness.idFromDB.Count;
            CounterDB();
            KonvertBisness.idInv = KonvertBisness.idFromDB[positionDB - 1];
            KonvertBisness.sqlRequest = "SELECT * FROM Recipient WHERE Id = '" + KonvertBisness.idInv + "'";
            KonvertBisness.FindInTable();
            BoxFromDB();
        }
  
    }
}
