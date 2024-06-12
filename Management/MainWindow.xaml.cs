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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Management.Classes;

namespace Management
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
     private Database bd = Database.GetDatabase();
        public MainWindow()
        {
            InitializeComponent();
            TaskTabel.ItemsSource = Database.GetDatabase().GetListOfProjects();


        }

        private void ButtonClose(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ButtonView(object sender, RoutedEventArgs e)
        {
            ProjectModel SelectedItem = (ProjectModel)TaskTabel.SelectedItem;
            if (SelectedItem == null)
            {
                MessageBox.Show("Выберете элемент");
            }
            else
            {
                Task task = new Task(SelectedItem);
                task.Show();
            }
        }
    }
}
