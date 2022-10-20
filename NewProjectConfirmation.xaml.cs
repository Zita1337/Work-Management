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
    /// Interaction logic for NewProjectConfirmation.xaml
    /// </summary>
    public partial class NewProjectConfirmation : Window
    {
        public NewProjectConfirmation()
        {
            InitializeComponent();
        }

        private void btnYes_Click(object sender, RoutedEventArgs e)
        {
            FileHandeling fh = new FileHandeling();
            fh.NewProject();

            switch (cmbxEmp.SelectedIndex)
            {
                case 0:
                    fh.NewProject();
                    break;
                case 1:
                    DatabaseHandeling db = new DatabaseHandeling();
                    db.DeleteProjectEmpList();
                    fh.NewProject();
                    db.RememberUsers();
                    break;
                default:
                    MessageBox.Show("Please select an employee option");
                    break;
            }

            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
