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
    /// Interaction logic for RemoveWork.xaml
    /// </summary>
    public partial class RemoveWork : Window
    {
        private int i;
        public int I { get => i; set => i = value; }

        public RemoveWork()
        {
            InitializeComponent();
        }


        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnYes_Click(object sender, RoutedEventArgs e)
        {
            FileHandeling fh = new FileHandeling();
            fh.DeleteWork(i);

            this.Close();
        }
    }
}
