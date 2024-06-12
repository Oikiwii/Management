using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Microsoft.Data.Sqlite;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Management.Classes;

namespace Management
{
    /// <summary>
    /// Логика взаимодействия для Edit.xaml
    /// </summary>
    public partial class Edit : Window
    {
        private Database db = Database.GetDatabase();
        ProjectModel SelectedItem;
        public Edit(ProjectModel selectProject)
        {
            InitializeComponent();
        
        
        List<User> users = db.GetListOfUsers();
            foreach (User user in users)
            {
                if(user.IdRole == 3)
                {
                    CB.Items.Add(user.FullName);
                }
            }
            Task.Text = selectProject.Task.ToString();
            Goal.Text = selectProject.Target.ToString();
            Term.Text = selectProject.Term.ToString();
            Resources.Text = selectProject.Resourses.ToString();
            Budget.Text = selectProject.Budget.ToString();

            SelectedItem = selectProject;

        }

        private void ButtonClose(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ButtonSave(object sender, RoutedEventArgs e)
        {

            if (Task.Text == string.Empty|| Goal.Text == string.Empty || Term.Text == string.Empty || Resources.Text == string.Empty || Budget.Text == string.Empty|| CB.Text == string.Empty)
            {
                MessageBox.Show("Не все поля заполнены");
               
            }
            else
            {
                db.EditProject(SelectedItem.Id, SelectedItem.Task, SelectedItem.Target, SelectedItem.Term, SelectedItem.Resourses, SelectedItem.Budget);
                MessageBox.Show("Данные изменены");
            }
        }
    }
}
