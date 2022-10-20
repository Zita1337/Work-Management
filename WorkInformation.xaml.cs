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
    /// Interaction logic for WorkInformation.xaml
    /// </summary>
    public partial class WorkInformation : Window
    {
        private int i;
        private DateTime date;
        public int I { get => i; set => i = value; }
        public DateTime Date { get => date; set => date = value; }

        public WorkInformation()
        {
            InitializeComponent();
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            FileHandeling fh = new FileHandeling();

            SolidColorBrush solidColorBrush = new SolidColorBrush();
            solidColorBrush = fh.Priority(i);
            linePriority.Stroke = solidColorBrush;

            txtbxName.Text = (fh.RetrieveProjectEmpName(i));
            txtbxTitle.Text = (fh.RetrieveProjectWorkTitle(i));
            richtxtbxDescription.Document.Blocks.Clear();
            richtxtbxDescription.Document.Blocks.Add(new Paragraph(new Run(fh.RetrieveProjectDescription(i))));
            cldDateRequired.SelectedDate = date;
        }

        private void btnContent_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
