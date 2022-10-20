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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CToDo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnLogIn_Click(object sender, RoutedEventArgs e)
        {
            DatabaseHandeling dbh = new DatabaseHandeling();
            bool logIn = dbh.CheckUserLogIn(txtboxUsername.Text, txtboxPassword.Text);

            if (logIn == true)
            {
                FileHandeling fh = new FileHandeling();
                if (fh.CheckEmpStatus() == 1)
                {
                    ManagerWindow empManWin = new ManagerWindow();
                    empManWin.Show();

                    this.Close();
                }
                else
                {
                    EmployeeWindow empWin = new EmployeeWindow();
                    empWin.Show();

                    this.Close();
                }
            }
            else 
            {
                MessageBox.Show("Incorrect Username or Password");
            }
        }
        private void Window_Activated(object sender, EventArgs e)
        {
            FileHandeling fh = new FileHandeling();
            fh.CreateProjectFile();
            fh.CreateProjectFile_FinishedWork();
        }
    }
}
