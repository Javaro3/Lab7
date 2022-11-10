using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Lab7
{
    /// <summary>
    /// Логика взаимодействия для Window5.xaml
    /// </summary>
    public partial class Window5 : Window
    {
        public Window5()
        {
            InitializeComponent();
        }

        private void btn_Click(object sender, RoutedEventArgs e)
        {
            WorkType leftDate = new WorkType(date1Box.Text, "date1", "1");
            WorkType rightDate = new WorkType(date2Box.Text, "date2", "2");
            if (leftDate.CheckDate() && rightDate.CheckDate())
            {
                date1.Content = "Введите 1 дату";
                date2.Content = "Введите 2 дату";
                MainWindow.main.GetSumPriceByTypeWork(res, date1Box.Text, date2Box.Text);
                date1Box.Text = date2Box.Text = "";
            }
            else
            {
                date1.Content = date2.Content = "Даты не верны";
            }
        }
    }
}
