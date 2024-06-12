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
using System.Windows.Media;
using Microsoft.Data.Sqlite;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Management.Classes;

namespace Management
{
    /// <summary>
    /// Логика взаимодействия для Admin.xaml
    /// </summary>
    public partial class Admin : Window
    {
        //Получаем объект БД из соответствующего класса.
        private Database _db = Database.GetDatabase();
        public Admin(string userRole)
        {
            InitializeComponent();
            TaskTabel.ItemsSource = Database.GetDatabase().GetListOfProjects();

            //Обновляем содержимое таблицы с проектами.
            UpdateTable();

            //Скрытие ненужных кнопок для той или иной роли.
            switch (userRole)
            {
                case "Администратор":
                    addUserButton.Visibility = Visibility.Visible;
                    break;

                case "Управляющий":
                    addUserButton.Visibility = Visibility.Hidden;
                    break;
            }
        }
        
        /// <summary>
        /// Метод, открывающий окно для добавления нового пользователя.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddNewUser(object sender, RoutedEventArgs e)
        {
            AddEmployee addEmployeeWindow = new AddEmployee();
            addEmployeeWindow.ShowDialog();

        }

        /// <summary>
        /// Метод для закрытия окна
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            this.Close();

        }

        /// <summary>
        /// Метод, который обновляет содержимое списка TableOfProject. Сначала идет обнуление всех элементов в этой таблице, а потом источником для них устанавливается список с элементами типа ProjectModel, который мы получаем из БД.
        /// </summary>
        private void UpdateTable()
        {
            TaskTabel.ItemsSource = null;
            TaskTabel.ItemsSource = _db.GetListOfProjects();
        }

        private void addProjectButton_Click(object sender, RoutedEventArgs e)
        {
            Add add = new Add();
            add.ShowDialog();
        }

        private void editProjectButton_Click(object sender, RoutedEventArgs e)
        {
            ProjectModel SelectedItem = (ProjectModel)TaskTabel.SelectedItem;
            if (SelectedItem == null)
            {
                MessageBox.Show("Выберете элемент");
            }
            else
            {
                Edit edit = new Edit(SelectedItem);
                edit.Show();
            }
        }
    }

}
