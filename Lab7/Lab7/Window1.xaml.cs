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
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        public bool CheckTextBox(bool check, string afterStr, string beforeStr, System.Windows.Controls.Label label)
        {
            if (check)
            {
                label.Content = afterStr;
            }
            else
            {
                label.Content = beforeStr;
            }
            return check;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            WorkType workType = new WorkType(textBox1.Text, textBox2.Text, textBox3.Text);
            bool CorrectData = CheckTextBox(workType.CheckDate(), "Введите дату", "Такой даты нет", Lable1) &
            CheckTextBox(workType.ChecBrend(), "Введите марку", "Не должно содержать цифры", Lable2) &
            CheckTextBox(workType.CheckPrice(), "Введите цену", "Вы должны ввести число", Lable3);

            if (CorrectData)
            {
                switch (Box.Text)
                {
                    case "Замена шин":
                        MainWindow.main.AddNewWork(new Work1(textBox1.Text, textBox2.Text, textBox3.Text)); break;
                    case "Ремонт проколов":
                        MainWindow.main.AddNewWork(new Work2(textBox1.Text, textBox2.Text, textBox3.Text)); break;
                    case "Балансировка колес":
                        MainWindow.main.AddNewWork(new Work3(textBox1.Text, textBox2.Text, textBox3.Text)); break;
                    case "Развал-схождение":
                        MainWindow.main.AddNewWork(new Work4(textBox1.Text, textBox2.Text, textBox3.Text)); break;
                }
                textBox1.Text = textBox2.Text = textBox3.Text = "";
            }
        }


        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged_2(object sender, TextChangedEventArgs e)
        {

        }

        private void ComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
