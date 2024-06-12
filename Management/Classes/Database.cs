using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;

namespace Management.Classes
{
    internal class Database
    {
        //Соединение с БД (Включаем в самом начале файла Database для того, что бы соединение существовало всегда, а потом уже либо включаем его, либо выключаем)
        SqliteConnection connection = new SqliteConnection("Data Source=Management.db");

        //Поле класса Database, которое определяет объект для взаимодействия с этим файлом.
        private static Database _db;

        //Функция, возвращающая нам объект класса Database в то место, где вы вызываем эту функцию
        //Реализация паттерна Одиночка происходит именно тут, что позволяет нам создать единственный экземпляр класса для взаимодействия с бд
        public static Database GetDatabase()
        {

            //Если вдруг объект пустой (что происходит 1 раз при запуске программы), то создаем его
            if (_db == null)
            {
                _db = new Database();
            }

            //А в последствии и отдаем туда, откуда вызывали функцию.
            return _db;
        }


        /// <summary>
        /// Фукнкция, возвращающая роль того профиля, под которым мы логинимся в программе
        /// </summary>
        /// <param name="userLogin"> Это логин, который мы вводим </param>
        /// <param name="userPassword"> Это пароль, который мы вводим </param>
        /// <returns> Возвращает нам строку с соответствующей ролью для нашего аккаунта </returns>
        public string GetUserRole(string userLogin, string userPassword)
        {
            //Изначально роль не определена, что будет означать, что вход не удался.
            string role = "Роль не определена";

            connection.Open();

            //Запрос, который нам отдает название роли
            string sqlExpression = $"SELECT Name FROM Roles JOIN Users ON Roles.Id = Users.IdRole WHERE Users.Login = '{userLogin}' AND Users.Password = '{userPassword}'";
            SqliteCommand command = new SqliteCommand(sqlExpression, connection);

            using (SqliteDataReader reader = command.ExecuteReader())
            {
                if (reader.HasRows) // если есть данные
                {
                    while (reader.Read())   // построчно считываем данные
                    {

                        //Запись названия роли в переменную
                        role = reader.GetString(0);

                    }
                }
            }

            connection.Close();

            //Возвращаем роль в то место, где вызываем функцию.
            return role;
        }

        /// <summary>
        /// Метод для добавления нового пользователя программой.
        /// </summary>
        /// <param name="login"> Это логин для нового пользователя, который мы вводим </param>
        /// <param name="password"> Это пароль для нового пользователя, который мы вводим </param>
        /// <param name="name"> Это ФИО нового пользователя, которое мы вводим </param>
        /// <param name="role"> Это роль в виде строки для нового пользователя, которую мы выбираем в КомбоБоксе </param>
        public void AddNewUser(string login, string password, string name, string role)
        {

            connection.Open();

            //Запрос на добавление новой записи в таблицу Users
            string sqlExpression = $"INSERT INTO Users(FullName, IdRole, Login, Password) VALUES ('{name}', (SELECT Id FROM Roles WHERE Name = '{role}'), '{login}', '{password}')";
            SqliteCommand command = new SqliteCommand(sqlExpression, connection);
            command.ExecuteNonQuery();
            connection.Close();
        }

        /// <summary>
        /// Функция, которая возвращает полный список всех проектов в таблице Projects
        /// </summary>
        /// <returns></returns>
        public List<ProjectModel> GetListOfProjects()
        {
            //Это объект одного проекта, который после его инициализации (т.е. когда программа опишет его задачи, цели и т.д из БД) добавляется в список ниже.
            ProjectModel project = new ProjectModel();

            //Это сам список проектов, который мы потом отдадим в конце функции
            List<ProjectModel> projects = new List<ProjectModel>();

            connection.Open();
            string sqlExpression = $"SELECT * FROM Projects";
            SqliteCommand command = new SqliteCommand(sqlExpression, connection);

            using (SqliteDataReader reader = command.ExecuteReader())
            {
                if (reader.HasRows) // если есть данные
                {
                    while (reader.Read())   // построчно считываем данные
                    {

                        //Как раз та самая инициализация одного объекта проекта, который потом будет при каждой итерации перезаписываться и добавляться в список.
                        project.Id = reader.GetInt32(0);
                        project.Target = reader.GetString(1);
                        project.Task = reader.GetString(2);
                        project.Term = reader.GetString(3);
                        project.Resourses = reader.GetString(4);
                        project.Budget = reader.GetInt64(5);
                        project.idEmployee = reader.GetInt32(6);
                        project.Readiness = reader.GetString(7);
                        projects.Add(project);
                    }
                }
            }

            //Это цикл для того, что бы IdUser перевести уже в вид ФИО для вывода в таблице именно его имени, а не цифры Id.
            foreach (ProjectModel projectModel in projects)
            {
                sqlExpression = $"SELECT FullName FROM Users WHERE Users.Id = {project.idEmployee}";
                command = new SqliteCommand(sqlExpression, connection);
                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows) // если есть данные
                    {
                        while (reader.Read())   // построчно считываем данные
                        {
                            projectModel.EmployeeName = reader.GetString(0);
                        }
                    }
                }
            }
            connection.Close();
            return projects;
        }

        public List<User> GetListOfUsers()
        {
            List<User> users = new List<User>();
            connection.Open();
            string sqlExpression = $"SELECT * FROM Users";
            SqliteCommand command = new SqliteCommand(sqlExpression, connection);

            using (SqliteDataReader reader = command.ExecuteReader())
            {
                if (reader.HasRows) // если есть данные
                {
                    while (reader.Read())   // построчно считываем данные
                    {
                        User user = new User();
                        user.Id = reader.GetInt32(0);
                        user.FullName = reader.GetString(1);
                        user.IdRole = reader.GetInt32(2);
                        user.Login = reader.GetString(3);
                        user.Password = reader.GetString(4);

                        users.Add(user);
                    }
                }
            }
            connection.Close();
            return users;
        }

        public void CompleteProject(int id, string readiness)
        {
            connection.Open();
            string sqlExpression = $"UPDATE Projects SET Readiness = '{readiness}' WHERE Id = {id}";
            SqliteCommand command = new SqliteCommand(sqlExpression, connection);
            command.ExecuteNonQuery();
            connection.Close();
        }

        public void EditProject(int id, string task, string target, string term, string resourses, long budget)
        {
            connection.Open();
            string sqlExpression = $"UPDATE Projects SET Task = '{task}', Target = '{target}', Term = '{term}', Resourses = '{resourses}', Budget = {budget} WHERE Id = {id}";
            SqliteCommand command = new SqliteCommand(sqlExpression, connection);
            command.ExecuteNonQuery();
            connection.Close();
        }

        public void AddProject(string task, string target, string term, string resourses, long budget)
        {
            connection.Open();
            string sqlExpression = $"INSERT INTO Projects (Task, Target, Term, Resourses, Budget) VALUES ('{task}', '{target}','{term}', '{resourses}', {budget})";
            SqliteCommand command = new SqliteCommand(sqlExpression, connection);
            command.ExecuteNonQuery();
            connection.Close();
        }
    }
}
