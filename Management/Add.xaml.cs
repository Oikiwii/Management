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
using System.Diagnostics;

namespace Management
{
    /// <summary>
    /// Логика взаимодействия для Add.xaml
    /// </summary>
    public partial class Add : Window
    {
        private Database db = Database.GetDatabase();
        public Add()
        {
            InitializeComponent();
            List<User> users = db.GetListOfUsers();
            foreach (User user in users)
            {
                if (user.IdRole == 3)
                {
                    CB.Items.Add(user.FullName);
                }
            }
        }

        private void ButtonClose(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ButtonSave(object sender, RoutedEventArgs e)
        {
            if (Task.Text == string.Empty || Goal.Text == string.Empty || Term.Text == string.Empty || Resources.Text == string.Empty || Budget.Text == string.Empty || CB.Text == string.Empty)
            {
                MessageBox.Show("Не все поля заполнены");

            }
            else
            {
                db.AddProject(Task.Text, Goal.Text, Term.Text, Resources.Text, long.Parse(Budget.Text));
                MessageBox.Show("Данные добавлены");
            }
        }

    
    }
}
