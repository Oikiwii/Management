using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using Microsoft.Data.Sqlite;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Management.Classes;

namespace Management
{
    /// <summary>
    /// Логика взаимодействия для AddEmployee.xaml
    /// </summary>
    public partial class AddEmployee : Window
    {
        //Получаем объект БД из соответствующего класса.
        private Database _db = Database.GetDatabase();
        public AddEmployee()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Метод для добавления нового пользователя по нажатию на кнопку
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddUser(object sender, RoutedEventArgs e)
        {
            //Проверка на пустоту вводимых данных
            if (LoginTextBox.Text == string.Empty || PasswordTextBox.Text == string.Empty || RoleComboBox.Text == string.Empty)
            {
                MessageBox.Show("Не все поля заполнены!");
            }
            else
            {
                //Вызываем метод для добавления нового пользователя у объекта _db с передачей необходимых данных
                _db.AddNewUser(LoginTextBox.Text, PasswordTextBox.Text, NameTextBox.Text, RoleComboBox.Text);
                MessageBox.Show("Пользователь добавлен!");
                this.Close();
            }
        }

        private void ButtonClose(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void RoleComboBox_SelectionChanged()
        {

        }
    }
}
