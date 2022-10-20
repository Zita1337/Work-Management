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
    /// Interaction logic for FinishWork.xaml
    /// </summary>
    public partial class FinishWork : Window
    {
        private int i;
        public int I { get => i; set => i = value; }

        public FinishWork()
        {
            InitializeComponent();
        }


        private void btnNo_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnYes_Click(object sender, RoutedEventArgs e)
        {
            FileHandeling fh = new FileHandeling();
            fh.CompleteWork_FinishedWork(i);

            this.Close();
        }
    }
}
