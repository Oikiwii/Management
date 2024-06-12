using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using Microsoft.Data.Sqlite;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Management.Classes;

namespace Management
{
    /// <summary>
    /// Логика взаимодействия для Entrance.xaml
    /// </summary>
    public partial class Entrance : Window
    {
        public Entrance()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Метод входа в программу
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EnterClick(object sender, RoutedEventArgs e)
        {

            //Проверка на пустоту вводимых данных.
            if (LoginTextBox.Text == string.Empty || PasswordTextBox.Text == string.Empty)
            {
                MessageBox.Show("Не все поля заполнены.");
            }
            else
            {
                //Получаем роль того профиля, под которым мы заходим с вводимыми данными.
                string role = Database.GetDatabase().GetUserRole(LoginTextBox.Text, PasswordTextBox.Text);

                //Проверка на то, что мы успешно вошли. Если мы получим строку "Роль не определена", значит ввели неверные данные. Иначе все хорошо.
                if (role == "Роль не определена")
                {
                    MessageBox.Show("Ошибка входа. Введены неверные данные.");
                }
                else
                {
                    if (role == "Сотрудник")
                    {
                        MessageBox.Show($"Успешный вход. Ваша роль - {role}");
                        MainWindow mainWindow = new MainWindow();
                        mainWindow.Show();
                        this.Close();
                    }
                    else
                    {
                        //Открытие следующего окна с передачей роли для того, что бы скрыть необходимые кнопки. 
                        MessageBox.Show($"Успешный вход. Ваша роль - {role}");
                        Admin adminWindow = new Admin(role);
                        adminWindow.Show();
                        this.Close();
                    }
                }
            }
            
        }
    }
}
