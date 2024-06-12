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
using System.Diagnostics;

namespace Management
{
    /// <summary>
    /// Логика взаимодействия для Task.xaml
    /// </summary>
    public partial class Task : Window
    {
        ProjectModel Item;
        ProjectModel project;
        Database db = Database.GetDatabase();
        public Task(ProjectModel selectProject)
        {
            InitializeComponent();
            Taskk.Text = selectProject.Task.ToString();
            Goal.Text = selectProject.Target.ToString();
            Term.Text = selectProject.Term.ToString();
            Resources.Text = selectProject.Resourses.ToString();
            Budget.Text = selectProject.Budget.ToString();
            Employee.Text = selectProject.EmployeeName.ToString();
            Item = selectProject;
            project = selectProject;
        }
    
      

        private void ButtonClose(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonDone(object sender, RoutedEventArgs e)
        {
            if (project.Readiness == "Не готов")
            {
                db.CompleteProject(project.Id, "Готов");
            }
            else
            {
                MessageBox.Show("Задание уже выполенно");
            }
        }
    }
}
