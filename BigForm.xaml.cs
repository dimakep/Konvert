using System.Windows;
using System.Windows.Controls;
using System.Printing;
using System;
using System.Windows.Media;

namespace Konvert
{
    /// <summary>
    /// Логика взаимодействия для BigForm.xaml
    /// </summary>
    public partial class BigForm : Window
    {
        public readonly string printerName;

        public BigForm(string firm, int index, string region, string area, string city, string street,
            string home, string frame, string structure, string flat, string printer)
        {
            InitializeComponent();
            RecipientBox.Text = firm;
            IndexBox.Text = Convert.ToString(index);
            RegionBox.Text = region + " " + area;
            CityBox.Text = city + " " + street;
            HomeBox.Text += home;
            FrameBox.Text += frame;
            StructureBox.Text += structure;
            FlatBox.Text += flat;
            printerName = printer;
        }

        private void BtnPrint_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog printDialog = new();
            printDialog.PrintQueue = new PrintQueue(new PrintServer(), printerName);

            printDialog.PrintTicket.PageOrientation = PageOrientation.Landscape;
            printDialog.PrintTicket.PageMediaSize = new PageMediaSize(612, 869);

            PrintTicket printTicket = printDialog.PrintQueue.UserPrintTicket;
            double printWidth = printTicket.PageMediaSize.Width.Value;
            double printHeight = printTicket.PageMediaSize.Height.Value;

            printDialog.PrintVisual(PrintBox, "Print");
            DialogResult = true;
        }

        private void Print1()
        {
            PrintDialog printDialog = new PrintDialog();
            {
                // Создать визуальный элемент для страницы
                DrawingVisual visual = new DrawingVisual();

                // Получить контекст рисования
                using (DrawingContext dc = visual.RenderOpen())
                {
                    
                    // Определить текст, который необходимо печатать
                    FormattedText text = new FormattedText(txtContent.Text,
                        System.Globalization.CultureInfo.CurrentCulture,
                        FlowDirection.LeftToRight,
                        new Typeface("Calibri"), 20, Brushes.Black);

                    // Указать максимальную ширину, в пределах которой выполнять перенос текста, 
                    text.MaxTextWidth = printDialog.PrintableAreaWidth / 2;

                    // Получить размер выводимого текста. 
                    Size textSize = new Size(text.Width, text.Height);

                    // Найти верхний левый угол, куда должен быть помещен текст. 
                    double margin = 96 * 0.25;
                    Point point = new Point(
                        (printDialog.PrintableAreaWidth - textSize.Width) / 2 - margin,
                        (printDialog.PrintableAreaHeight - textSize.Height) / 2 - margin);

                    // Нарисовать содержимое, 
                    dc.DrawText(text, point);

                    // Добавить рамку (прямоугольник без фона). 
                    dc.DrawRectangle(null, new Pen(Brushes.Black, 1),
                        new Rect(margin, margin, printDialog.PrintableAreaWidth - margin * 2,
                            printDialog.PrintableAreaHeight - margin * 2));
                }

                // Напечатать визуальный элемент. 
                printDialog.PrintVisual(visual, "Печать с помощью классов визуального уровня");
            }
        }
        private void BtnBack_Click(object sender, RoutedEventArgs e)
        { Close(); }

    }
}
