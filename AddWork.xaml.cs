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
    /// Interaction logic for AddWork.xaml
    /// </summary>
    public partial class AddWork : Window
    {
        public AddWork()
        {
            InitializeComponent();
        }


        private void btnCancle_Click(object sender, RoutedEventArgs e)
        {
            FileHandeling fh = new FileHandeling();
            if (fh.CheckEmpStatus() == 1)
            {
                this.Close();
            }
            else
            {
                this.Close();
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            FileHandeling fh = new FileHandeling();
            string user = "";
            string title = "";
            string work = "" ;
            string date = "";

            try
            {
                user = cmbxSelectPerson.SelectedItem.ToString().Trim();

            } catch { MessageBox.Show("Please select the Person who is working on this project"); }
            try
            {
                title = txtbxTitle.Text.Trim('\n');

            } catch { MessageBox.Show("Please add a title to the work"); }
            try
            {
                TextRange textRange = new TextRange(
                    richtxtbxDescription.Document.ContentStart,
                    richtxtbxDescription.Document.ContentEnd
                    ) ;
                work = textRange.Text.Trim('\n', '\r');

            } catch { MessageBox.Show("Please add a description of the work"); }
            try
            {
                date = calTime.SelectedDate.ToString();
            } catch { MessageBox.Show("Please add a date of when the work should be finished"); }

            try
            {
                if (user != "" && title != "" && work != "" && date != "")
                {
                    fh.AddToProjectFile(user, title, work, date);

                    txtbxTitle.Text = "";
                    calTime.SelectedDates.Clear();
                    richtxtbxDescription.Document.Blocks.Clear();
                }
            }
            catch { }

        }
        private void Window_Activated(object sender, EventArgs e)
        {

            FileHandeling fh = new FileHandeling();
            //returns to lists containing the users names and suranems respectavely
            List<string> names = new List<string>(fh.RetrieveUserNames());
            List<string> surnames = new List<string>(fh.RetrieveUserSurnames());

            try
            {
                cmbxSelectPerson.Items.Clear();
                //Creates a new TextBlock that has the name and surname of the users as it's Text


                int i = 0;
                while (true)
                {
                    cmbxSelectPerson.Items.Add(" " + names[i] + " " + surnames[i]);
                    i++;
                }
            }
            catch { }
        }
    }
    
}
