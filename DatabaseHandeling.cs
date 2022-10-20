using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Data.Common;
using Microsoft.Data.Sqlite;
using Dapper;
using System.Xml.Linq;

namespace CToDo
{
    public class DatabaseHandeling
    {
        private string password;
        public string Password { get => password; set => password = value; }

        private const string ConnectionString = "Server=.\\sqlexpress;Database=CToDoLogIn;Integrated Security=true";

        #region User Verification
        public bool CheckUserLogIn(string username, string password)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                string sql = "SELECT EmployeeID FROM Employees WHERE EmployeeUsername = @username AND EmployeePassword = @password";

                SqlCommand cmdUser = new SqlCommand(sql, conn);
                try
                {
                    conn.Open();
                    cmdUser.Connection = conn;
                    cmdUser.Parameters.Add("@username", SqlDbType.VarChar).Value = username;
                    cmdUser.Parameters.Add("@password", SqlDbType.VarChar).Value = password;

                    var checkUser = (Int32)cmdUser.ExecuteScalar();
                    conn.Close();

                    FileHandeling fh = new FileHandeling();
                    fh.SaveUsername(username);
                    fh.CreateTempUsers();
                    RememberUsers();

                    return true;

                }
                catch { return false; }
            }
        }
        #endregion
        public void RememberUsers()
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                string sql = "SELECT * FROM Employees";

                SqlCommand cmdUser = new SqlCommand(sql, conn);

                try
                {
                    conn.Open();
                    cmdUser.Connection = conn; 
                    using (SqlDataReader reader = cmdUser.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int iD = reader.GetInt32(0);
                            string name = reader.GetString(1);
                            string surname = reader.GetString(2);
                            string username = reader.GetString(3);
                            int status = reader.GetInt32(4);
                            reader.GetString(5);

                            FileHandeling fh = new FileHandeling();
                            fh.SaveUsers(iD, username, name, surname, status);

                        }
                    }
                    conn.Close();

                }
                catch { }
            }
        }
        public void AddToDatabase(string name, string surname, string password, string username, int status)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                string sql = "INSERT INTO Employees(FirstName, LastName, EmployeeUsername, EmployeeStatus, EmployeePassword) VALUES(@name, @surname, @username, @status, @password)";

                SqlCommand cmdUser = new SqlCommand(sql, conn);
                try
                {
                    conn.Open();
                    cmdUser.Connection = conn;
                    cmdUser.Parameters.Add("@username", SqlDbType.VarChar).Value = username;
                    cmdUser.Parameters.Add("@password", SqlDbType.VarChar).Value = password;
                    cmdUser.Parameters.Add("@name", SqlDbType.VarChar).Value = name;
                    cmdUser.Parameters.Add("@surname", SqlDbType.VarChar).Value = surname;
                    cmdUser.Parameters.Add("@status", SqlDbType.Int).Value = status;

                    cmdUser.ExecuteNonQuery();
                    conn.Close();
                }
                catch { }
            }
        }
        public int FindID(string username)
        {
            int iD = 0;
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                string sql = "SELECT EmployeeID FROM Employees WHERE EmployeeUsername = @username";

                SqlCommand cmdUser = new SqlCommand(sql, conn);

                try
                {
                    conn.Open();
                    cmdUser.Connection = conn; 
                    cmdUser.Parameters.Add("@username", SqlDbType.VarChar).Value = username;

                    using (SqlDataReader reader = cmdUser.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            iD = reader.GetInt32(0);

                            return iD;
                        }
                    }
                    conn.Close();

                }
                catch { }
                return 0;
            }
        }
        public void DeleteUser(string nameAndSurname)
        {
            FileHandeling fh = new FileHandeling();
            int iD = fh.EmpID(nameAndSurname);

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                string sql = "DELETE FROM Employees WHERE EmployeeID = @iD";

                SqlCommand cmdUser = new SqlCommand(sql, conn);
                try
                {
                    conn.Open();
                    cmdUser.Connection = conn;
                    cmdUser.Parameters.Add("@iD", SqlDbType.Int).Value = iD;

                    cmdUser.ExecuteNonQuery();
                    conn.Close();
                }
                catch { }
            }
        }
        public void DeleteProjectEmpList()
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                string sql = "DELETE FROM Employees";

                SqlCommand cmdUser = new SqlCommand(sql, conn);
                try
                {
                    conn.Open();
                    cmdUser.Connection = conn;

                    cmdUser.ExecuteNonQuery();
                    conn.Close();
                }
                catch { }
            }

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                string sql = "INSERT INTO Employees(FirstName, LastName, EmployeeUsername, EmployeeStatus, EmployeePassword) VALUES(@name, @surname, @username, @status, @password)";

                SqlCommand cmdUser = new SqlCommand(sql, conn);
                try
                {
                    conn.Open();
                    cmdUser.Connection = conn;
                    cmdUser.Parameters.Add("@username", SqlDbType.VarChar).Value = "Admin";
                    cmdUser.Parameters.Add("@password", SqlDbType.VarChar).Value = "Admin";
                    cmdUser.Parameters.Add("@name", SqlDbType.VarChar).Value = "Admin";
                    cmdUser.Parameters.Add("@surname", SqlDbType.VarChar).Value = "Admin";
                    cmdUser.Parameters.Add("@status", SqlDbType.Int).Value = "1";

                    cmdUser.ExecuteNonQuery();
                    conn.Close();
                }
                catch { }
            }

            FileHandeling fh = new FileHandeling();
            fh.DeleteTempFileUsers();
            fh.CreateTempUsers();
        }
    }

}
