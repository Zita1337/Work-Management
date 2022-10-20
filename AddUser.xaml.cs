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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CToDo
{
    /// <summary>
    /// Interaction logic for AddUser.xaml
    /// </summary>
    public partial class AddUser : Window
    {
        public AddUser()
        {
            InitializeComponent();
        }

        private void btnCancle_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            FileHandeling fh = new FileHandeling();

            int status;
            string username = "";
            string password = "";
            string name = "";
            string surname = "";

            username = txtbxUsername.Text.Trim(' ');
            password = txtbxPassword.Text.Trim(' ');
            name = txtbxName.Text.Trim(' ');
            surname = txtbxSurname.Text.Trim(' ');
            if (cmbbxStatus.Text == "Manager")
            {
                status = 1;
            }
            else
            {
                status = 0;
            }


            DatabaseHandeling dbh = new DatabaseHandeling();
            if (name != "" && surname != "" && password != "" && username != "" && status != null)
            {
                dbh.AddToDatabase(name, surname, password, username, status);
                int iD = dbh.FindID(username);
                fh.SaveUsers(iD, username, name, surname, status);

                this.Close();
            }
            else
            {
                MessageBox.Show("Please fill in all the fields");
            }

        }
    }
}
